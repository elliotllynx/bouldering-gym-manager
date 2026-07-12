using BoulderSetManager.Models.Entities;
using DAL;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BoulderSetManager.Models.Services
{
    public class BoulderProblemService
    {
        public async Task<List<BoulderProblemDTO>> GetBouldersForWall(int wallId)
        {
            using var db = new GymDbContext();
            return await db.BoulderingProblems
                .Where(b => b.WallId == wallId)
                .Select(b => new BoulderProblemDTO
                {
                    Id = b.Id,
                    Grade = b.Grade,
                    Style = b.Style,
                    Author = b.Author,
                    BuiltDate = b.BuiltDate,
                    RetireDate = b.RetireDate,
                    WallId = b.WallId,
                    Status = b.Status
                })
                .ToListAsync();
        }

        public async Task<int> CreateBoulder(BoulderProblemDTO dto)
        {
            using var db = new GymDbContext();
            var boulder = new BoulderProblem
            {
                Grade = dto.Grade,
                Style = dto.Style,
                Author = dto.Author,
                BuiltDate = dto.BuiltDate,
                RetireDate = dto.RetireDate,
                WallId = dto.WallId,
                Status = dto.Status
            };
            db.BoulderingProblems.Add(boulder);
            await db.SaveChangesAsync();
            return boulder.Id;
        }

        public async Task UpdateBoulder(BoulderProblemDTO dto)
        {
            using var db = new GymDbContext();
            var boulder = await db.BoulderingProblems.FindAsync(dto.Id);
            if (boulder != null)
            {
                boulder.Grade = dto.Grade;
                boulder.Style = dto.Style;
                boulder.Author = dto.Author;
                boulder.BuiltDate = dto.BuiltDate;
                boulder.RetireDate = dto.RetireDate;
                boulder.WallId = dto.WallId;
                boulder.Status = dto.Status;
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteBoulder(int boulderId)
        {
            using var db = new GymDbContext();
            var boulder = await db.BoulderingProblems.FindAsync(boulderId);
            if (boulder != null)
            {
                db.BoulderingProblems.Remove(boulder);
                await db.SaveChangesAsync();
            }
        }
    }
}
