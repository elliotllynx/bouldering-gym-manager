using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;
using DAL;
using BoulderSetManager.Models.Entities;
using SQLitePCL;
using Microsoft.EntityFrameworkCore;

namespace BoulderSetManager.Models.Services
{
    public class GymService
    {
        public async Task<List<GymDTO>> GetAllGyms()
        {
            using var db = new GymDbContext();
            return await db.Gyms
                .Select(g => new GymDTO
                {
                    Id = g.Id,
                    Name = g.Name,
                    Location = g.Location
                })
                .ToListAsync();
        }

        public async Task CreateGym(GymDTO dto)
        {
            using var db = new GymDbContext();
            var gym = new Gym
            {
                Name = dto.Name,
                Location = dto.Location
            };
            db.Gyms.Add(gym);
            await db.SaveChangesAsync();
        }

        public async Task DeleteGym(int gymId)
        {
            using var db = new GymDbContext();
            var gym = await db.Gyms.FindAsync(gymId);
            if (gym != null)
            {
                db.Gyms.Remove(gym);
                await db.SaveChangesAsync();
            }
        }
    }
}
