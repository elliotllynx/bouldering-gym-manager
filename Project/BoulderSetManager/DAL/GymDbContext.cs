using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Collections.Specialized.BitVector32;
using DAL.Entities;

namespace DAL
{
    public class GymDbContext : DbContext
    { 
        public DbSet<Gym> Gyms { get; set; }
        public DbSet<Entities.Wall> Walls { get; set; }
        public DbSet<BoulderingProblem> BoulderingProblems { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options) 
            => options.UseSqlite("Data Source=gym.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // seeding data when presenting a project
        }

    }

}
