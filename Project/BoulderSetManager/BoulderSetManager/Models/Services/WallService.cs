
using System.Collections.ObjectModel;
using BoulderSetManager.Models.Entities;
using DAL;
using Microsoft.EntityFrameworkCore;
using DAL.Entities;

namespace BoulderSetManager.Models.Services
{
    public class WallService
    {
        public async Task<List<WallDTO>> GetGymWalls(int gymId)
        {
            using var db = new GymDbContext();
            var walls = await db.Walls
                .Where(w => w.GymId == gymId)
                .Select(w => new WallDTO
                {
                    Id = w.Id,
                    Name = w.Name,
                    GymId = w.GymId,
                    Boulders = new ObservableCollection<BoulderingProblemDTO>(
                        db.BoulderingProblems
                            .Where(b => b.WallId == w.Id)
                            .Select(b => new BoulderingProblemDTO
                            {
                                Id = b.Id,
                                Grade = b.Grade,
                                Type = b.Type,
                                Author = b.Author,
                                BuiltDate = b.BuiltDate,
                                RetireDate = b.RetireDate,
                                WallId = b.WallId
                            }).ToList())
                })
                .ToListAsync();
            return walls;
        }

        public async Task<int> CreateWall(WallDTO dto)
        {
            using var db = new GymDbContext();
            var wall = new Wall()
            {
                Name = dto.Name,
                GymId = dto.GymId
            };
            db.Walls.Add(wall);
            await db.SaveChangesAsync();
            return wall.Id;
        }


        public async Task UpdateWall(int wallId, string name)
        {
            using var db = new GymDbContext();
            var wall = await db.Walls.FindAsync(wallId);
            if (wall != null)
            {
                wall.Name = name;
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteWall(int wallId)
        {
            using var db = new GymDbContext();
            var wall = await db.Walls.FindAsync(wallId);
            if (wall != null)
            {
                db.Walls.Remove(wall);
                await db.SaveChangesAsync();
            }
        }
    }
}
