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
            modelBuilder.Entity<Gym>().HasData(
                new Gym { Id = 1, Name = "Boulder Arena", Location = "Brno" },
                new Gym { Id = 2, Name = "Rock Gym", Location = "Praha" }
            );

            modelBuilder.Entity<Wall>().HasData(
                new Wall { Id = 1, Name = "Overhang", GymId = 1 },
                new Wall { Id = 2, Name = "Slab", GymId = 1 },
                new Wall { Id = 3, Name = "Vertical", GymId = 2 }
            );

            modelBuilder.Entity<BoulderingProblem>().HasData(
                new BoulderingProblem { Id = 1, Grade = "6A", Type = "Crimpy", Author = "Jan", BuiltDate = DateTime.Today.AddDays(-25), RetireDate = DateTime.Today.AddDays(5), WallId = 1 },
                new BoulderingProblem { Id = 2, Grade = "6B+", Type = "Slopey", Author = "Martin", BuiltDate = DateTime.Today.AddDays(-5), RetireDate = DateTime.Today.AddDays(25), WallId = 2 },
                new BoulderingProblem { Id = 3, Grade = "5C", Type = "Dynamic", Author = "Eva", BuiltDate = DateTime.Today.AddDays(-15), RetireDate = DateTime.Today.AddDays(15), WallId = 2 },
                new BoulderingProblem { Id = 4, Grade = "7A", Type = "Pinchy", Author = "Jan", BuiltDate = DateTime.Today.AddDays(-29), RetireDate = DateTime.Today.AddDays(1), WallId = 3 }
            );
        }

    }

}
