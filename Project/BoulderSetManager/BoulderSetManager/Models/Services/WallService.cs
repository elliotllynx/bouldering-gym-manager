using System;
using System.Collections.Generic;
using System.Text;
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
            return await db.Walls
                .Where(s => s.GymId == gymId)
                .Select(s => new WallDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    GymId = s.GymId
                })
                .ToListAsync<WallDTO>();
        }

        public async Task CreateWall(string name, int gymId)
        {
            using var db = new GymDbContext();
            var wall = new Wall{ Name = name, GymId = gymId };
            db.Walls.Add(wall);
            await db.SaveChangesAsync();
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
