using System.Collections.ObjectModel;
using BoulderSetManager.Models.Entities;
using DAL;
using Microsoft.EntityFrameworkCore;
using DAL.Entities;
using DAL.Enums;

namespace BoulderSetManager.Models.Services
{
    public class WallService
    { 
        // defaultly skips loading archived walls unless implicitly asked for
        public async Task<List<WallDTO>> GetGymWalls(int gymId, List<Status>? statuses = null)
        {
            statuses ??= [Status.Active, Status.Draft]; // default load

            using var db = new GymDbContext();

            var walls = await db.Walls
                .Where(w => w.GymId == gymId && statuses.Contains(w.Status))
                .ToListAsync();

            // only 2 calls to the database needed, rest of the sorting happens outside of db calls
            var boulders = await db.BoulderingProblems
                .Where(b => walls.Select(w => w.Id).Contains(b.WallId) && statuses.Contains(b.Status))
                .ToListAsync();

            var boultersByWall = boulders
                .GroupBy(b => b.WallId)
                .ToDictionary(g => g.Key, g => g.AsEnumerable());

            return walls.Select(w => new WallDTO
            {
                Id = w.Id,
                Name = w.Name,
                GymId = w.GymId,
                Status = w.Status,
                IsVisible = true,
                Boulders = new ObservableCollection<BoulderProblemDTO>(
                    boultersByWall.TryGetValue(w.Id, out var wallBoulders)
                        ? wallBoulders.Select(b => new BoulderProblemDTO
                        {
                            Id = b.Id,
                            Grade = b.Grade,
                            Style = b.Style,
                            Author = b.Author,
                            BuiltDate = b.BuiltDate,
                            RetireDate = b.RetireDate,
                            WallId = b.WallId,
                            Status = b.Status,
                            IsVisible = true,
                        })
                        : new List<BoulderProblemDTO>())
            }).ToList();
        }

        public async Task<int> CreateWall(WallDTO dto)
        {
            using var db = new GymDbContext();
            var wall = new Wall()
            {
                Name = dto.Name,
                GymId = dto.GymId,
                Status = Status.Active
            };
            db.Walls.Add(wall);
            await db.SaveChangesAsync();
            return wall.Id;
        }


        public async Task UpdateWall(WallDTO dto)
        {
            using var db = new GymDbContext();
            var wall = await db.Walls.FindAsync(dto.Id);
            if (wall != null)
            {
                wall.Name = dto.Name;
                wall.Status = dto.Status;
                await db.SaveChangesAsync();
            }
        }
        public async Task DeleteWall(WallDTO dto)
        {
            using var db = new GymDbContext();
            var wall = await db.Walls.FindAsync(dto.Id);
            if (wall != null)
            {
                db.Walls.Remove(wall);
                await db.SaveChangesAsync();
            }
        }
    }
}
