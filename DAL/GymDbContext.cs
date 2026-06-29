using Microsoft.EntityFrameworkCore;
using DAL.Entities;

namespace DAL
{
    public class GymDbContext : DbContext
    { 
        public DbSet<Gym> Gyms { get; set; }
        public DbSet<Wall> Walls { get; set; }
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
                new BoulderingProblem { Id = 1, Grade = "5C", Style = "Vertical", Author = "Jan", BuiltDate = today.AddDays(-17), RetireDate = today.AddDays(90), WallId = 1 },
                new BoulderingProblem { Id = 2, Grade = "6A", Style = "Vertical", Author = "Martin", BuiltDate = today.AddDays(-18), RetireDate = today.AddDays(87), WallId = 1 },
                new BoulderingProblem { Id = 3, Grade = "6B", Style = "Slab", Author = "Eva", BuiltDate = today.AddDays(-9), RetireDate = today.AddDays(124), WallId = 1 },
                new BoulderingProblem { Id = 4, Grade = "6C+", Style = "Vertical", Author = "Jan", BuiltDate = today.AddDays(-16), RetireDate = today.AddDays(111), WallId = 1 },
                new BoulderingProblem { Id = 5, Grade = "7A", Style = "Overhang", Author = "Tomáš", BuiltDate = today.AddDays(-40), RetireDate = today.AddDays(77), WallId = 1 },

                // Stargate (2)
                new BoulderingProblem { Id = 6, Grade = "6A+", Style = "Overhang", Author = "Martin", BuiltDate = today.AddDays(-6), RetireDate = today.AddDays(98), WallId = 2 },
                new BoulderingProblem { Id = 7, Grade = "6B+", Style = "Overhang", Author = "Eva", BuiltDate = today.AddDays(-13), RetireDate = today.AddDays(109), WallId = 2 },
                new BoulderingProblem { Id = 8, Grade = "7A+", Style = "Overhang", Author = "Tomáš", BuiltDate = today.AddDays(-35), RetireDate = today.AddDays(100), WallId = 2 },
                new BoulderingProblem { Id = 9, Grade = "7B", Style = "Overhang", Author = "Jan", BuiltDate = today.AddDays(-30), RetireDate = today.AddDays(101), WallId = 2 },
                new BoulderingProblem { Id = 10, Grade = "7C", Style = "Overhang", Author = "Martin", BuiltDate = today.AddDays(-21), RetireDate = today.AddDays(97), WallId = 2 },

                // Nose (3)
                new BoulderingProblem { Id = 11, Grade = "5C", Style = "Slab", Author = "Eva", BuiltDate = today.AddDays(-34), RetireDate = today.AddDays(93), WallId = 3 },
                new BoulderingProblem { Id = 12, Grade = "6A", Style = "Slab", Author = "Jan", BuiltDate = today.AddDays(-22), RetireDate = today.AddDays(116), WallId = 3 },
                new BoulderingProblem { Id = 13, Grade = "6B", Style = "Vertical", Author = "Tomáš", BuiltDate = today.AddDays(-4), RetireDate = today.AddDays(107), WallId = 3 },
                new BoulderingProblem { Id = 14, Grade = "6C", Style = "Slab", Author = "Martin", BuiltDate = today.AddDays(-44), RetireDate = today.AddDays(63), WallId = 3 },
                new BoulderingProblem { Id = 15, Grade = "7A", Style = "Vertical", Author = "Eva", BuiltDate = today.AddDays(-19), RetireDate = today.AddDays(77), WallId = 3 },
                new BoulderingProblem { Id = 16, Grade = "7B+", Style = "Slab", Author = "Jan", BuiltDate = today.AddDays(-7), RetireDate = today.AddDays(105), WallId = 3 },

                // Wave (4)
                new BoulderingProblem { Id = 17, Grade = "6A", Style = "Overhang", Author = "Tomáš", BuiltDate = today.AddDays(0), RetireDate = today.AddDays(141), WallId = 4 },
                new BoulderingProblem { Id = 18, Grade = "6A+", Style = "Overhang", Author = "Martin", BuiltDate = today.AddDays(-37), RetireDate = today.AddDays(55), WallId = 4 },
                new BoulderingProblem { Id = 19, Grade = "6B+", Style = "Overhang", Author = "Eva", BuiltDate = today.AddDays(-18), RetireDate = today.AddDays(131), WallId = 4 },
                new BoulderingProblem { Id = 20, Grade = "7A", Style = "Overhang", Author = "Jan", BuiltDate = today.AddDays(-39), RetireDate = today.AddDays(75), WallId = 4 },
                new BoulderingProblem { Id = 21, Grade = "7B", Style = "Overhang", Author = "Tomáš", BuiltDate = today.AddDays(0), RetireDate = today.AddDays(130), WallId = 4 },
                new BoulderingProblem { Id = 22, Grade = "7C+", Style = "Overhang", Author = "Martin", BuiltDate = today.AddDays(-7), RetireDate = today.AddDays(122), WallId = 4 },

                // Monster (5)
                new BoulderingProblem { Id = 23, Grade = "7A", Style = "Overhang", Author = "Jan", BuiltDate = today.AddDays(0), RetireDate = today.AddDays(102), WallId = 5 },
                new BoulderingProblem { Id = 24, Grade = "7B", Style = "Overhang", Author = "Eva", BuiltDate = today.AddDays(-27), RetireDate = today.AddDays(108), WallId = 5 },
                new BoulderingProblem { Id = 25, Grade = "7C", Style = "Overhang", Author = "Tomáš", BuiltDate = today.AddDays(-2), RetireDate = today.AddDays(137), WallId = 5 },
                new BoulderingProblem { Id = 26, Grade = "8A", Style = "Overhang", Author = "Martin", BuiltDate = today.AddDays(-16), RetireDate = today.AddDays(88), WallId = 5 },
                new BoulderingProblem { Id = 27, Grade = "8B", Style = "Overhang", Author = "Jan", BuiltDate = today.AddDays(-7), RetireDate = today.AddDays(138), WallId = 5 },

                // Parkour 1 (6)
                new BoulderingProblem { Id = 28, Grade = "5C", Style = "Vertical", Author = "Eva", BuiltDate = today.AddDays(-2), RetireDate = today.AddDays(128), WallId = 6 },
                new BoulderingProblem { Id = 29, Grade = "6A", Style = "Vertical", Author = "Tomáš", BuiltDate = today.AddDays(-5), RetireDate = today.AddDays(138), WallId = 6 },
                new BoulderingProblem { Id = 30, Grade = "6B", Style = "Overhang", Author = "Martin", BuiltDate = today.AddDays(-27), RetireDate = today.AddDays(76), WallId = 6 },
                new BoulderingProblem { Id = 31, Grade = "6C", Style = "Vertical", Author = "Jan", BuiltDate = today.AddDays(0), RetireDate = today.AddDays(132), WallId = 6 },

                // Parkour 2 (7)
                new BoulderingProblem { Id = 32, Grade = "6A+", Style = "Vertical", Author = "Eva", BuiltDate = today.AddDays(-20), RetireDate = today.AddDays(111), WallId = 7 },
                new BoulderingProblem { Id = 33, Grade = "6B", Style = "Overhang", Author = "Jan", BuiltDate = today.AddDays(-2), RetireDate = today.AddDays(122), WallId = 7 },
                new BoulderingProblem { Id = 34, Grade = "6C+", Style = "Vertical", Author = "Tomáš", BuiltDate = today.AddDays(-16), RetireDate = today.AddDays(98), WallId = 7 },
                new BoulderingProblem { Id = 35, Grade = "7A", Style = "Overhang", Author = "Martin", BuiltDate = today.AddDays(-26), RetireDate = today.AddDays(81), WallId = 7 },
                new BoulderingProblem { Id = 36, Grade = "7B+", Style = "Overhang", Author = "Eva", BuiltDate = today.AddDays(-16), RetireDate = today.AddDays(109), WallId = 7 },

                // Kids (8)
                new BoulderingProblem { Id = 37, Grade = "4A", Style = "Slab", Author = "Martin", BuiltDate = today.AddDays(-11), RetireDate = today.AddDays(128), WallId = 8 },
                new BoulderingProblem { Id = 38, Grade = "4B", Style = "Vertical", Author = "Eva", BuiltDate = today.AddDays(2), RetireDate = today.AddDays(141), WallId = 8 },
                new BoulderingProblem { Id = 39, Grade = "4C", Style = "Slab", Author = "Jan", BuiltDate = today.AddDays(-2), RetireDate = today.AddDays(139), WallId = 8 },
                new BoulderingProblem { Id = 40, Grade = "5A", Style = "Vertical", Author = "Tomáš", BuiltDate = today.AddDays(-21), RetireDate = today.AddDays(82), WallId = 8 },
                new BoulderingProblem { Id = 41, Grade = "5B", Style = "Slab", Author = "Martin", BuiltDate = today.AddDays(0), RetireDate = today.AddDays(148), WallId = 8 },

                // Movement (9)
                new BoulderingProblem { Id = 42, Grade = "6B", Style = "Vertical", Author = "Jan", BuiltDate = today.AddDays(-29), RetireDate = today.AddDays(81), WallId = 9 },
                new BoulderingProblem { Id = 43, Grade = "6C", Style = "Overhang", Author = "Eva", BuiltDate = today.AddDays(-9), RetireDate = today.AddDays(137), WallId = 9 },
                new BoulderingProblem { Id = 44, Grade = "7A", Style = "Vertical", Author = "Tomáš", BuiltDate = today.AddDays(7), RetireDate = today.AddDays(155), WallId = 9 },
                new BoulderingProblem { Id = 45, Grade = "7A+", Style = "Overhang", Author = "Martin", BuiltDate = today.AddDays(-21), RetireDate = today.AddDays(77), WallId = 9 },
                new BoulderingProblem { Id = 46, Grade = "7B", Style = "Vertical", Author = "Jan", BuiltDate = today.AddDays(-23), RetireDate = today.AddDays(82), WallId = 9 },

                // Competition (10)
                new BoulderingProblem { Id = 47, Grade = "7A", Style = "Overhang", Author = "Eva", BuiltDate = today.AddDays(-15), RetireDate = today.AddDays(122), WallId = 10 },
                new BoulderingProblem { Id = 48, Grade = "7B", Style = "Overhang", Author = "Tomáš", BuiltDate = today.AddDays(-39), RetireDate = today.AddDays(88), WallId = 10 },
                new BoulderingProblem { Id = 49, Grade = "7B+", Style = "Vertical", Author = "Martin", BuiltDate = today.AddDays(-38), RetireDate = today.AddDays(75), WallId = 10 },
                new BoulderingProblem { Id = 50, Grade = "7C", Style = "Overhang", Author = "Jan", BuiltDate = today.AddDays(-3), RetireDate = today.AddDays(101), WallId = 10 },
                new BoulderingProblem { Id = 51, Grade = "8A", Style = "Overhang", Author = "Eva", BuiltDate = today.AddDays(-4), RetireDate = today.AddDays(117), WallId = 10 },
                new BoulderingProblem { Id = 52, Grade = "8B+", Style = "Overhang", Author = "Tomáš", BuiltDate = today.AddDays(-15), RetireDate = today.AddDays(80), WallId = 10 },

                // Rockets (11)
                new BoulderingProblem { Id = 53, Grade = "6C", Style = "Overhang", Author = "Martin", BuiltDate = today.AddDays(-13), RetireDate = today.AddDays(86), WallId = 11 },
                new BoulderingProblem { Id = 54, Grade = "7A", Style = "Overhang", Author = "Jan", BuiltDate = today.AddDays(-54), RetireDate = today.AddDays(76), WallId = 11 },
                new BoulderingProblem { Id = 55, Grade = "7A+", Style = "Overhang", Author = "Eva", BuiltDate = today.AddDays(1), RetireDate = today.AddDays(129), WallId = 11 },
                new BoulderingProblem { Id = 56, Grade = "7B", Style = "Overhang", Author = "Tomáš", BuiltDate = today.AddDays(3), RetireDate = today.AddDays(122), WallId = 11 },
                new BoulderingProblem { Id = 57, Grade = "7C+", Style = "Overhang", Author = "Martin", BuiltDate = today.AddDays(-7), RetireDate = today.AddDays(116), WallId = 11 },

                // Summit (12)
                new BoulderingProblem { Id = 58, Grade = "6B+", Style = "Vertical", Author = "Jan", BuiltDate = today.AddDays(-15), RetireDate = today.AddDays(135), WallId = 12 },
                new BoulderingProblem { Id = 59, Grade = "6C", Style = "Slab", Author = "Eva", BuiltDate = today.AddDays(-12), RetireDate = today.AddDays(121), WallId = 12 },
                new BoulderingProblem { Id = 60, Grade = "7A", Style = "Vertical", Author = "Tomáš", BuiltDate = today.AddDays(9), RetireDate = today.AddDays(148), WallId = 12 },
                new BoulderingProblem { Id = 61, Grade = "7B", Style = "Overhang", Author = "Martin", BuiltDate = today.AddDays(-37), RetireDate = today.AddDays(94), WallId = 12 },
                new BoulderingProblem { Id = 62, Grade = "7C", Style = "Vertical", Author = "Jan", BuiltDate = today.AddDays(-23), RetireDate = today.AddDays(77), WallId = 12 },
                new BoulderingProblem { Id = 63, Grade = "8A+", Style = "Overhang", Author = "Eva", BuiltDate = today.AddDays(0), RetireDate = today.AddDays(119), WallId = 12 }
            );
        }

    }

}
