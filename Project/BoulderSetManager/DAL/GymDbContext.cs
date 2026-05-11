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
                new Gym { Id = 1, Name = "Block Dock", Location = "Bratislava" },
                new Gym { Id = 2, Name = "Hangar", Location = "Brno" },
                new Gym { Id = 3, Name = "Flash Wall", Location = "Olomouc" }
            );

            modelBuilder.Entity<Wall>().HasData(
                new Wall { Id = 1, Name = "Technical", GymId = 2 },
                new Wall { Id = 2, Name = "Stargate", GymId = 2 },
                new Wall { Id = 3, Name = "Nose", GymId = 2 },
                new Wall { Id = 4, Name = "Wave", GymId = 2 },
                new Wall { Id = 5, Name = "Monster", GymId = 2 },
                new Wall { Id = 6, Name = "Parkour 1", GymId = 2 },
                new Wall { Id = 7, Name = "Parkour 2", GymId = 2 },
                new Wall { Id = 8, Name = "Kids", GymId = 2 },
                new Wall { Id = 9, Name = "Movement", GymId = 2 },
                new Wall { Id = 10, Name = "Competition", GymId = 2 },
                new Wall { Id = 11, Name = "Rockets", GymId = 2 },
                new Wall { Id = 12, Name = "Summit", GymId = 2 }
            );

            modelBuilder.Entity<BoulderingProblem>().HasData(
                // Technical (1)
                new BoulderingProblem { Id = 1, Grade = "5C", Style = "Vertical", Author = "Jan", BuiltDate = new DateTime(2026, 1, 5), RetireDate = new DateTime(2026, 5, 5), WallId = 1 },
                new BoulderingProblem { Id = 2, Grade = "6A", Style = "Vertical", Author = "Martin", BuiltDate = new DateTime(2026, 1, 10), RetireDate = new DateTime(2026, 5, 10), WallId = 1 },
                new BoulderingProblem { Id = 3, Grade = "6B", Style = "Slab", Author = "Eva", BuiltDate = new DateTime(2026, 2, 1), RetireDate = new DateTime(2026, 6, 1), WallId = 1 },
                new BoulderingProblem { Id = 4, Grade = "6C+", Style = "Vertical", Author = "Jan", BuiltDate = new DateTime(2026, 2, 15), RetireDate = new DateTime(2026, 6, 15), WallId = 1 },
                new BoulderingProblem { Id = 5, Grade = "7A", Style = "Overhang", Author = "Tomáš", BuiltDate = new DateTime(2026, 3, 1), RetireDate = new DateTime(2026, 5, 12), WallId = 1 },

                // Stargate (2)
                new BoulderingProblem { Id = 6, Grade = "6A+", Style = "Overhang", Author = "Martin", BuiltDate = new DateTime(2026, 1, 20), RetireDate = new DateTime(2026, 5, 20), WallId = 2 },
                new BoulderingProblem { Id = 7, Grade = "6B+", Style = "Overhang", Author = "Eva", BuiltDate = new DateTime(2026, 2, 3), RetireDate = new DateTime(2026, 6, 3), WallId = 2 },
                new BoulderingProblem { Id = 8, Grade = "7A+", Style = "Overhang", Author = "Tomáš", BuiltDate = new DateTime(2026, 2, 20), RetireDate = new DateTime(2026, 5, 13), WallId = 2 },
                new BoulderingProblem { Id = 9, Grade = "7B", Style = "Overhang", Author = "Jan", BuiltDate = new DateTime(2026, 3, 5), RetireDate = new DateTime(2026, 7, 5), WallId = 2 },
                new BoulderingProblem { Id = 10, Grade = "7C", Style = "Overhang", Author = "Martin", BuiltDate = new DateTime(2026, 3, 15), RetireDate = new DateTime(2026, 7, 15), WallId = 2 },

                // Nose (3)
                new BoulderingProblem { Id = 11, Grade = "5C", Style = "Slab", Author = "Eva", BuiltDate = new DateTime(2026, 1, 8), RetireDate = new DateTime(2026, 5, 8), WallId = 3 },
                new BoulderingProblem { Id = 12, Grade = "6A", Style = "Slab", Author = "Jan", BuiltDate = new DateTime(2026, 1, 25), RetireDate = new DateTime(2026, 5, 14), WallId = 3 },
                new BoulderingProblem { Id = 13, Grade = "6B", Style = "Vertical", Author = "Tomáš", BuiltDate = new DateTime(2026, 2, 10), RetireDate = new DateTime(2026, 6, 10), WallId = 3 },
                new BoulderingProblem { Id = 14, Grade = "6C", Style = "Slab", Author = "Martin", BuiltDate = new DateTime(2026, 3, 1), RetireDate = new DateTime(2026, 7, 1), WallId = 3 },
                new BoulderingProblem { Id = 15, Grade = "7A", Style = "Vertical", Author = "Eva", BuiltDate = new DateTime(2026, 3, 20), RetireDate = new DateTime(2026, 7, 20), WallId = 3 },
                new BoulderingProblem { Id = 16, Grade = "7B+", Style = "Slab", Author = "Jan", BuiltDate = new DateTime(2026, 4, 1), RetireDate = new DateTime(2026, 8, 1), WallId = 3 },

                // Wave (4)
                new BoulderingProblem { Id = 17, Grade = "6A", Style = "Overhang", Author = "Tomáš", BuiltDate = new DateTime(2026, 1, 15), RetireDate = new DateTime(2026, 5, 15), WallId = 4 },
                new BoulderingProblem { Id = 18, Grade = "6A+", Style = "Overhang", Author = "Martin", BuiltDate = new DateTime(2026, 2, 5), RetireDate = new DateTime(2026, 6, 5), WallId = 4 },
                new BoulderingProblem { Id = 19, Grade = "6B+", Style = "Overhang", Author = "Eva", BuiltDate = new DateTime(2026, 2, 25), RetireDate = new DateTime(2026, 5, 11), WallId = 4 },
                new BoulderingProblem { Id = 20, Grade = "7A", Style = "Overhang", Author = "Jan", BuiltDate = new DateTime(2026, 3, 10), RetireDate = new DateTime(2026, 7, 10), WallId = 4 },
                new BoulderingProblem { Id = 21, Grade = "7B", Style = "Overhang", Author = "Tomáš", BuiltDate = new DateTime(2026, 3, 25), RetireDate = new DateTime(2026, 7, 25), WallId = 4 },
                new BoulderingProblem { Id = 22, Grade = "7C+", Style = "Overhang", Author = "Martin", BuiltDate = new DateTime(2026, 4, 5), RetireDate = new DateTime(2026, 8, 5), WallId = 4 },

                // Monster (5)
                new BoulderingProblem { Id = 23, Grade = "7A", Style = "Overhang", Author = "Jan", BuiltDate = new DateTime(2026, 1, 20), RetireDate = new DateTime(2026, 5, 20), WallId = 5 },
                new BoulderingProblem { Id = 24, Grade = "7B", Style = "Overhang", Author = "Eva", BuiltDate = new DateTime(2026, 2, 8), RetireDate = new DateTime(2026, 6, 8), WallId = 5 },
                new BoulderingProblem { Id = 25, Grade = "7C", Style = "Overhang", Author = "Tomáš", BuiltDate = new DateTime(2026, 2, 28), RetireDate = new DateTime(2026, 5, 12), WallId = 5 },
                new BoulderingProblem { Id = 26, Grade = "8A", Style = "Overhang", Author = "Martin", BuiltDate = new DateTime(2026, 3, 15), RetireDate = new DateTime(2026, 7, 15), WallId = 5 },
                new BoulderingProblem { Id = 27, Grade = "8B", Style = "Overhang", Author = "Jan", BuiltDate = new DateTime(2026, 4, 1), RetireDate = new DateTime(2026, 8, 1), WallId = 5 },

                // Parkour 1 (6)
                new BoulderingProblem { Id = 28, Grade = "5C", Style = "Vertical", Author = "Eva", BuiltDate = new DateTime(2026, 1, 12), RetireDate = new DateTime(2026, 5, 12), WallId = 6 },
                new BoulderingProblem { Id = 29, Grade = "6A", Style = "Vertical", Author = "Tomáš", BuiltDate = new DateTime(2026, 2, 2), RetireDate = new DateTime(2026, 6, 2), WallId = 6 },
                new BoulderingProblem { Id = 30, Grade = "6B", Style = "Overhang", Author = "Martin", BuiltDate = new DateTime(2026, 2, 22), RetireDate = new DateTime(2026, 6, 22), WallId = 6 },
                new BoulderingProblem { Id = 31, Grade = "6C", Style = "Vertical", Author = "Jan", BuiltDate = new DateTime(2026, 3, 12), RetireDate = new DateTime(2026, 7, 12), WallId = 6 },

                // Parkour 2 (7)
                new BoulderingProblem { Id = 32, Grade = "6A+", Style = "Vertical", Author = "Eva", BuiltDate = new DateTime(2026, 1, 18), RetireDate = new DateTime(2026, 5, 18), WallId = 7 },
                new BoulderingProblem { Id = 33, Grade = "6B", Style = "Overhang", Author = "Jan", BuiltDate = new DateTime(2026, 2, 8), RetireDate = new DateTime(2026, 6, 8), WallId = 7 },
                new BoulderingProblem { Id = 34, Grade = "6C+", Style = "Vertical", Author = "Tomáš", BuiltDate = new DateTime(2026, 3, 1), RetireDate = new DateTime(2026, 7, 1), WallId = 7 },
                new BoulderingProblem { Id = 35, Grade = "7A", Style = "Overhang", Author = "Martin", BuiltDate = new DateTime(2026, 3, 20), RetireDate = new DateTime(2026, 7, 20), WallId = 7 },
                new BoulderingProblem { Id = 36, Grade = "7B+", Style = "Overhang", Author = "Eva", BuiltDate = new DateTime(2026, 4, 5), RetireDate = new DateTime(2026, 8, 5), WallId = 7 },

                // Kids (8)
                new BoulderingProblem { Id = 37, Grade = "4A", Style = "Slab", Author = "Martin", BuiltDate = new DateTime(2026, 1, 6), RetireDate = new DateTime(2026, 5, 6), WallId = 8 },
                new BoulderingProblem { Id = 38, Grade = "4B", Style = "Vertical", Author = "Eva", BuiltDate = new DateTime(2026, 1, 22), RetireDate = new DateTime(2026, 5, 22), WallId = 8 },
                new BoulderingProblem { Id = 39, Grade = "4C", Style = "Slab", Author = "Jan", BuiltDate = new DateTime(2026, 2, 12), RetireDate = new DateTime(2026, 6, 12), WallId = 8 },
                new BoulderingProblem { Id = 40, Grade = "5A", Style = "Vertical", Author = "Tomáš", BuiltDate = new DateTime(2026, 2, 28), RetireDate = new DateTime(2026, 6, 28), WallId = 8 },
                new BoulderingProblem { Id = 41, Grade = "5B", Style = "Slab", Author = "Martin", BuiltDate = new DateTime(2026, 3, 18), RetireDate = new DateTime(2026, 7, 18), WallId = 8 },

                // Movement (9)
                new BoulderingProblem { Id = 42, Grade = "6B", Style = "Vertical", Author = "Jan", BuiltDate = new DateTime(2026, 1, 14), RetireDate = new DateTime(2026, 5, 14), WallId = 9 },
                new BoulderingProblem { Id = 43, Grade = "6C", Style = "Overhang", Author = "Eva", BuiltDate = new DateTime(2026, 2, 4), RetireDate = new DateTime(2026, 6, 4), WallId = 9 },
                new BoulderingProblem { Id = 44, Grade = "7A", Style = "Vertical", Author = "Tomáš", BuiltDate = new DateTime(2026, 2, 24), RetireDate = new DateTime(2026, 6, 24), WallId = 9 },
                new BoulderingProblem { Id = 45, Grade = "7A+", Style = "Overhang", Author = "Martin", BuiltDate = new DateTime(2026, 3, 14), RetireDate = new DateTime(2026, 7, 14), WallId = 9 },
                new BoulderingProblem { Id = 46, Grade = "7B", Style = "Vertical", Author = "Jan", BuiltDate = new DateTime(2026, 4, 3), RetireDate = new DateTime(2026, 8, 3), WallId = 9 },

                // Competition (10)
                new BoulderingProblem { Id = 47, Grade = "7A", Style = "Overhang", Author = "Eva", BuiltDate = new DateTime(2026, 1, 16), RetireDate = new DateTime(2026, 5, 16), WallId = 10 },
                new BoulderingProblem { Id = 48, Grade = "7B", Style = "Overhang", Author = "Tomáš", BuiltDate = new DateTime(2026, 2, 6), RetireDate = new DateTime(2026, 6, 6), WallId = 10 },
                new BoulderingProblem { Id = 49, Grade = "7B+", Style = "Vertical", Author = "Martin", BuiltDate = new DateTime(2026, 2, 26), RetireDate = new DateTime(2026, 6, 26), WallId = 10 },
                new BoulderingProblem { Id = 50, Grade = "7C", Style = "Overhang", Author = "Jan", BuiltDate = new DateTime(2026, 3, 16), RetireDate = new DateTime(2026, 7, 16), WallId = 10 },
                new BoulderingProblem { Id = 51, Grade = "8A", Style = "Overhang", Author = "Eva", BuiltDate = new DateTime(2026, 4, 6), RetireDate = new DateTime(2026, 8, 6), WallId = 10 },
                new BoulderingProblem { Id = 52, Grade = "8B+", Style = "Overhang", Author = "Tomáš", BuiltDate = new DateTime(2026, 4, 20), RetireDate = new DateTime(2026, 8, 20), WallId = 10 },

                // Rockets (11)
                new BoulderingProblem { Id = 53, Grade = "6C", Style = "Overhang", Author = "Martin", BuiltDate = new DateTime(2026, 1, 9), RetireDate = new DateTime(2026, 5, 9), WallId = 11 },
                new BoulderingProblem { Id = 54, Grade = "7A", Style = "Overhang", Author = "Jan", BuiltDate = new DateTime(2026, 1, 29), RetireDate = new DateTime(2026, 5, 29), WallId = 11 },
                new BoulderingProblem { Id = 55, Grade = "7A+", Style = "Overhang", Author = "Eva", BuiltDate = new DateTime(2026, 2, 18), RetireDate = new DateTime(2026, 6, 18), WallId = 11 },
                new BoulderingProblem { Id = 56, Grade = "7B", Style = "Overhang", Author = "Tomáš", BuiltDate = new DateTime(2026, 3, 8), RetireDate = new DateTime(2026, 7, 8), WallId = 11 },
                new BoulderingProblem { Id = 57, Grade = "7C+", Style = "Overhang", Author = "Martin", BuiltDate = new DateTime(2026, 3, 28), RetireDate = new DateTime(2026, 7, 28), WallId = 11 },

                // Summit (12)
                new BoulderingProblem { Id = 58, Grade = "6B+", Style = "Vertical", Author = "Jan", BuiltDate = new DateTime(2026, 1, 11), RetireDate = new DateTime(2026, 5, 11), WallId = 12 },
                new BoulderingProblem { Id = 59, Grade = "6C", Style = "Slab", Author = "Eva", BuiltDate = new DateTime(2026, 2, 1), RetireDate = new DateTime(2026, 6, 1), WallId = 12 },
                new BoulderingProblem { Id = 60, Grade = "7A", Style = "Vertical", Author = "Tomáš", BuiltDate = new DateTime(2026, 2, 21), RetireDate = new DateTime(2026, 6, 21), WallId = 12 },
                new BoulderingProblem { Id = 61, Grade = "7B", Style = "Overhang", Author = "Martin", BuiltDate = new DateTime(2026, 3, 13), RetireDate = new DateTime(2026, 7, 13), WallId = 12 },
                new BoulderingProblem { Id = 62, Grade = "7C", Style = "Vertical", Author = "Jan", BuiltDate = new DateTime(2026, 4, 2), RetireDate = new DateTime(2026, 8, 2), WallId = 12 },
                new BoulderingProblem { Id = 63, Grade = "8A+", Style = "Overhang", Author = "Eva", BuiltDate = new DateTime(2026, 4, 15), RetireDate = new DateTime(2026, 8, 15), WallId = 12 }
            );
        }

    }

}
