using BoulderSetManager.Models.Entities;
using DAL;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace BoulderSetManager.Models.Services
{
    public class BoulderingProblemService
    {
        public async Task<List<BoulderingProblemDTO>> GetBouldersForWall(int wallId)
        {
            using var db = new GymDbContext();
            return await db.BoulderingProblems
                .Where(b => b.WallId == wallId)
                .Select(b => new BoulderingProblemDTO
                {
                    Id = b.Id,
                    Grade = b.Grade,
                    Type = b.Type,
                    Author = b.Author,
                    BuiltDate = b.BuiltDate,
                    RetireDate = b.RetireDate,
                    WallId = b.WallId
                })
                .ToListAsync<BoulderingProblemDTO>();
        }

        public async Task<int> CreateBoulder(BoulderingProblemDTO dto)
        {
            using var db = new GymDbContext();
            var boulder = new BoulderingProblem
            {
                Grade = dto.Grade,
                Type = dto.Type,
                Author = dto.Author,
                BuiltDate = dto.BuiltDate,
                RetireDate = dto.RetireDate,
                WallId = dto.WallId
            };
            db.BoulderingProblems.Add(boulder);
            await db.SaveChangesAsync();
            return boulder.Id;
        }

        public async Task UpdateBoulder(BoulderingProblemDTO dto)
        {
            using var db = new GymDbContext();
            var boulder = await db.BoulderingProblems.FindAsync(dto.Id);
            if (boulder != null)
            {
                boulder.Grade = dto.Grade;
                boulder.Type = dto.Type;
                boulder.Author = dto.Author;
                boulder.BuiltDate = dto.BuiltDate;
                boulder.RetireDate = dto.RetireDate;
                boulder.WallId = dto.WallId;
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

        public async Task<List<BoulderingProblemDTO>> GetExpiringBoulders(int gymId, int daysAhead)
        {
            using var db = new GymDbContext();
            var cutoff = DateTime.Now.AddDays(daysAhead);
            return await db.BoulderingProblems
                .Where(b => b.RetireDate <= cutoff)
                .Select(b => new BoulderingProblemDTO
                {
                    Id = b.Id,
                    Grade = b.Grade,
                    Type = b.Type,
                    Author = b.Author,
                    BuiltDate = b.BuiltDate,
                    RetireDate = b.RetireDate,
                    WallId = b.WallId
                })
                .ToListAsync<BoulderingProblemDTO>();
        }
    }
}
