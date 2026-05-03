using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;
using DAL;
using BoulderSetManager.Models.Entities;
using SQLitePCL;

namespace BoulderSetManager.Models.Services
{
    public class GymService
    {
        private readonly GymDbContext _context;

        public GymService(GymDbContext context)
        {
            _context = context;
        }

        public async Task<List<GymDTO>> GetAllGyms()
        {
            return await _context.Gyms
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
            var gym = new Gym
            {
                Name = dto.Name,
                Location = dto.Location
            };
            _context.Gyms.Add(gym);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGym(int gymId)
        {
            var gym = await _context.Gyms.FindAsync(gymId);
            if (gym != null)
            {
                _context.Gyms.Remove(gym);
                await _context.SaveChangesAsync();
            }
        }
    }
}
