using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;
using DAL;
using BoulderSetManager.Models.Entities;
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

        public async Task CreateGym(string name, string location)
        {
            using var db = new GymDbContext();
            var gym = new Gym { Name = name, Location = location };
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

        public async Task UpdateGym(int id, string name, string location)
        {
            using var db = new GymDbContext();
            var gym = await db.Gyms.FindAsync(id);
            if (gym != null)
            {
                if (!string.IsNullOrWhiteSpace(name)) gym.Name = name;
                if (!string.IsNullOrWhiteSpace(location)) gym.Location = location;
                await db.SaveChangesAsync();
            }
        }
        public async Task<GymDTO> GetGym(int id)
        {
            using var db = new GymDbContext();
            var gym = await db.Gyms.FindAsync(id);
            if (gym != null)
            {
                return new GymDTO
                {
                    Id = gym.Id,
                    Name = gym.Name,
                    Location = gym.Location
                };
            }
            return null;
        }
    }
}
