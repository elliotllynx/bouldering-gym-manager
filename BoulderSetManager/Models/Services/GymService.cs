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

        public async Task<int> CreateGym(GymDTO dto)
        {
            using var db = new GymDbContext();
            var gym = new Gym
            {
                Name = dto.Name,
                Location = dto.Location
            };
            db.Gyms.Add(gym);
            await db.SaveChangesAsync();
            return gym.Id;
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

        public async Task UpdateGym(GymDTO dto)
        {
            using var db = new GymDbContext();
            var gym = await db.Gyms.FindAsync(dto.Id);
            if (gym != null)
            {
                gym.Name = dto.Name;
                gym.Location = dto.Location;
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
