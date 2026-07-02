using Microsoft.EntityFrameworkCore;
using DAL.Entities;

namespace DAL
{
    public class GymDbContext : DbContext
    { 
        public DbSet<Gym> Gyms { get; set; }
        public DbSet<Wall> Walls { get; set; }
        public DbSet<BoulderProblem> BoulderingProblems { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options) 
            => options.UseSqlite("Data Source=gym.db");

        private DateTime _today = DateTime.Today;
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
            modelBuilder.Entity<BoulderProblem>().HasData(
                // Technical (1)
                new BoulderProblem { Id = 1, Grade = "5C", Style = "Vertical", Author = "Jan", BuiltDate = _today.AddDays(-17), RetireDate = _today.AddDays(13), WallId = 1 },
                new BoulderProblem { Id = 2, Grade = "6A", Style = "Vertical", Author = "Martin", BuiltDate = _today.AddDays(-18), RetireDate = _today.AddDays(12), WallId = 1 },
                new BoulderProblem { Id = 3, Grade = "6B", Style = "Slab", Author = "Eva", BuiltDate = _today.AddDays(-9), RetireDate = _today.AddDays(21), WallId = 1 },
                new BoulderProblem { Id = 4, Grade = "6C+", Style = "Vertical", Author = "Jan", BuiltDate = _today.AddDays(-16), RetireDate = _today.AddDays(14), WallId = 1 },
                new BoulderProblem { Id = 5, Grade = "7A", Style = "Overhang", Author = "Tomáš", BuiltDate = _today.AddDays(-40), RetireDate = _today.AddDays(-10), WallId = 1 },

                // Stargate (2)
                new BoulderProblem { Id = 6, Grade = "6A+", Style = "Overhang", Author = "Martin", BuiltDate = _today.AddDays(-6), RetireDate = _today.AddDays(24), WallId = 2 },
                new BoulderProblem { Id = 7, Grade = "6B+", Style = "Overhang", Author = "Eva", BuiltDate = _today.AddDays(-13), RetireDate = _today.AddDays(17), WallId = 2 },
                new BoulderProblem { Id = 8, Grade = "7A+", Style = "Overhang", Author = "Tomáš", BuiltDate = _today.AddDays(-35), RetireDate = _today.AddDays(-5), WallId = 2 },
                new BoulderProblem { Id = 9, Grade = "7B", Style = "Overhang", Author = "Jan", BuiltDate = _today.AddDays(-30), RetireDate = _today.AddDays(0), WallId = 2 },
                new BoulderProblem { Id = 10, Grade = "7C", Style = "Overhang", Author = "Martin", BuiltDate = _today.AddDays(-21), RetireDate = _today.AddDays(9), WallId = 2 },

                // Nose (3)
                new BoulderProblem { Id = 11, Grade = "5C", Style = "Slab", Author = "Eva", BuiltDate = _today.AddDays(-34), RetireDate = _today.AddDays(-4), WallId = 3 },
                new BoulderProblem { Id = 12, Grade = "6A", Style = "Slab", Author = "Jan", BuiltDate = _today.AddDays(-22), RetireDate = _today.AddDays(8), WallId = 3 },
                new BoulderProblem { Id = 13, Grade = "6B", Style = "Vertical", Author = "Tomáš", BuiltDate = _today.AddDays(-4), RetireDate = _today.AddDays(26), WallId = 3 },
                new BoulderProblem { Id = 14, Grade = "6C", Style = "Slab", Author = "Martin", BuiltDate = _today.AddDays(-44), RetireDate = _today.AddDays(-14), WallId = 3 },
                new BoulderProblem { Id = 15, Grade = "7A", Style = "Vertical", Author = "Eva", BuiltDate = _today.AddDays(-19), RetireDate = _today.AddDays(11), WallId = 3 },
                new BoulderProblem { Id = 16, Grade = "7B+", Style = "Slab", Author = "Jan", BuiltDate = _today.AddDays(-7), RetireDate = _today.AddDays(23), WallId = 3 },

                // Wave (4)
                new BoulderProblem { Id = 17, Grade = "6A", Style = "Overhang", Author = "Tomáš", BuiltDate = _today.AddDays(0), RetireDate = _today.AddDays(30), WallId = 4 },
                new BoulderProblem { Id = 18, Grade = "6A+", Style = "Overhang", Author = "Martin", BuiltDate = _today.AddDays(-37), RetireDate = _today.AddDays(-7), WallId = 4 },
                new BoulderProblem { Id = 19, Grade = "6B+", Style = "Overhang", Author = "Eva", BuiltDate = _today.AddDays(-18), RetireDate = _today.AddDays(12), WallId = 4 },
                new BoulderProblem { Id = 20, Grade = "7A", Style = "Overhang", Author = "Jan", BuiltDate = _today.AddDays(-39), RetireDate = _today.AddDays(-9), WallId = 4 },
                new BoulderProblem { Id = 21, Grade = "7B", Style = "Overhang", Author = "Tomáš", BuiltDate = _today.AddDays(0), RetireDate = _today.AddDays(30), WallId = 4 },
                new BoulderProblem { Id = 22, Grade = "7C+", Style = "Overhang", Author = "Martin", BuiltDate = _today.AddDays(-7), RetireDate = _today.AddDays(23), WallId = 4 },

                // Monster (5)
                new BoulderProblem { Id = 23, Grade = "7A", Style = "Overhang", Author = "Jan", BuiltDate = _today.AddDays(0), RetireDate = _today.AddDays(30), WallId = 5 },
                new BoulderProblem { Id = 24, Grade = "7B", Style = "Overhang", Author = "Eva", BuiltDate = _today.AddDays(-27), RetireDate = _today.AddDays(3), WallId = 5 },
                new BoulderProblem { Id = 25, Grade = "7C", Style = "Overhang", Author = "Tomáš", BuiltDate = _today.AddDays(-2), RetireDate = _today.AddDays(28), WallId = 5 },
                new BoulderProblem { Id = 26, Grade = "8A", Style = "Overhang", Author = "Martin", BuiltDate = _today.AddDays(-16), RetireDate = _today.AddDays(14), WallId = 5 },
                new BoulderProblem { Id = 27, Grade = "8B", Style = "Overhang", Author = "Jan", BuiltDate = _today.AddDays(-7), RetireDate = _today.AddDays(23), WallId = 5 },

                // Parkour 1 (6)
                new BoulderProblem { Id = 28, Grade = "5C", Style = "Vertical", Author = "Eva", BuiltDate = _today.AddDays(-28), RetireDate = _today.AddDays(2), WallId = 6 },
                new BoulderProblem { Id = 29, Grade = "6A", Style = "Vertical", Author = "Tomáš", BuiltDate = _today.AddDays(-5), RetireDate = _today.AddDays(25), WallId = 6 },
                new BoulderProblem { Id = 30, Grade = "6B", Style = "Overhang", Author = "Martin", BuiltDate = _today.AddDays(-27), RetireDate = _today.AddDays(3), WallId = 6 },
                new BoulderProblem { Id = 31, Grade = "6C", Style = "Vertical", Author = "Jan", BuiltDate = _today.AddDays(0), RetireDate = _today.AddDays(30), WallId = 6 },

                // Parkour 2 (7)
                new BoulderProblem { Id = 32, Grade = "6A+", Style = "Vertical", Author = "Eva", BuiltDate = _today.AddDays(-20), RetireDate = _today.AddDays(10), WallId = 7 },
                new BoulderProblem { Id = 33, Grade = "6B", Style = "Overhang", Author = "Jan", BuiltDate = _today.AddDays(-2), RetireDate = _today.AddDays(28), WallId = 7 },
                new BoulderProblem { Id = 34, Grade = "6C+", Style = "Vertical", Author = "Tomáš", BuiltDate = _today.AddDays(-16), RetireDate = _today.AddDays(14), WallId = 7 },
                new BoulderProblem { Id = 35, Grade = "7A", Style = "Overhang", Author = "Martin", BuiltDate = _today.AddDays(-26), RetireDate = _today.AddDays(4), WallId = 7 },
                new BoulderProblem { Id = 36, Grade = "7B+", Style = "Overhang", Author = "Eva", BuiltDate = _today.AddDays(-16), RetireDate = _today.AddDays(14), WallId = 7 },

                // Kids (8)
                new BoulderProblem { Id = 37, Grade = "4A", Style = "Slab", Author = "Martin", BuiltDate = _today.AddDays(-11), RetireDate = _today.AddDays(19), WallId = 8 },
                new BoulderProblem { Id = 38, Grade = "4B", Style = "Vertical", Author = "Eva", BuiltDate = _today.AddDays(2), RetireDate = _today.AddDays(32), WallId = 8 },
                new BoulderProblem { Id = 39, Grade = "4C", Style = "Slab", Author = "Jan", BuiltDate = _today.AddDays(-2), RetireDate = _today.AddDays(28), WallId = 8 },
                new BoulderProblem { Id = 40, Grade = "5A", Style = "Vertical", Author = "Tomáš", BuiltDate = _today.AddDays(-21), RetireDate = _today.AddDays(9), WallId = 8 },
                new BoulderProblem { Id = 41, Grade = "5B", Style = "Slab", Author = "Martin", BuiltDate = _today.AddDays(0), RetireDate = _today.AddDays(30), WallId = 8 },

                // Movement (9)
                new BoulderProblem { Id = 42, Grade = "6B", Style = "Vertical", Author = "Jan", BuiltDate = _today.AddDays(-29), RetireDate = _today.AddDays(1), WallId = 9 },
                new BoulderProblem { Id = 43, Grade = "6C", Style = "Overhang", Author = "Eva", BuiltDate = _today.AddDays(-9), RetireDate = _today.AddDays(21), WallId = 9 },
                new BoulderProblem { Id = 44, Grade = "7A", Style = "Vertical", Author = "Tomáš", BuiltDate = _today.AddDays(7), RetireDate = _today.AddDays(23), WallId = 9 },
                new BoulderProblem { Id = 45, Grade = "7A+", Style = "Overhang", Author = "Martin", BuiltDate = _today.AddDays(-21), RetireDate = _today.AddDays(9), WallId = 9 },
                new BoulderProblem { Id = 46, Grade = "7B", Style = "Vertical", Author = "Jan", BuiltDate = _today.AddDays(-23), RetireDate = _today.AddDays(7), WallId = 9 },

                // Competition (10)
                new BoulderProblem { Id = 47, Grade = "7A", Style = "Overhang", Author = "Eva", BuiltDate = _today.AddDays(-15), RetireDate = _today.AddDays(15), WallId = 10 },
                new BoulderProblem { Id = 48, Grade = "7B", Style = "Overhang", Author = "Tomáš", BuiltDate = _today.AddDays(-39), RetireDate = _today.AddDays(-9), WallId = 10 },
                new BoulderProblem { Id = 49, Grade = "7B+", Style = "Vertical", Author = "Martin", BuiltDate = _today.AddDays(-38), RetireDate = _today.AddDays(-8), WallId = 10 },
                new BoulderProblem { Id = 50, Grade = "7C", Style = "Overhang", Author = "Jan", BuiltDate = _today.AddDays(-3), RetireDate = _today.AddDays(27), WallId = 10 },
                new BoulderProblem { Id = 51, Grade = "8A", Style = "Overhang", Author = "Eva", BuiltDate = _today.AddDays(-4), RetireDate = _today.AddDays(26), WallId = 10 },
                new BoulderProblem { Id = 52, Grade = "8B+", Style = "Overhang", Author = "Tomáš", BuiltDate = _today.AddDays(-15), RetireDate = _today.AddDays(15), WallId = 10 },

                // Rockets (11)
                new BoulderProblem { Id = 53, Grade = "6C", Style = "Overhang", Author = "Martin", BuiltDate = _today.AddDays(-13), RetireDate = _today.AddDays(27), WallId = 11 },
                new BoulderProblem { Id = 54, Grade = "7A", Style = "Overhang", Author = "Jan", BuiltDate = _today.AddDays(-54), RetireDate = _today.AddDays(-24), WallId = 11 },
                new BoulderProblem { Id = 55, Grade = "7A+", Style = "Overhang", Author = "Eva", BuiltDate = _today.AddDays(1), RetireDate = _today.AddDays(29), WallId = 11 },
                new BoulderProblem { Id = 56, Grade = "7B", Style = "Overhang", Author = "Tomáš", BuiltDate = _today.AddDays(3), RetireDate = _today.AddDays(27), WallId = 11 },
                new BoulderProblem { Id = 57, Grade = "7C+", Style = "Overhang", Author = "Martin", BuiltDate = _today.AddDays(-7), RetireDate = _today.AddDays(23), WallId = 11 },

                // Summit (12)
                new BoulderProblem { Id = 58, Grade = "6B+", Style = "Vertical", Author = "Jan", BuiltDate = _today.AddDays(-15), RetireDate = _today.AddDays(15), WallId = 12 },
                new BoulderProblem { Id = 59, Grade = "6C", Style = "Slab", Author = "Eva", BuiltDate = _today.AddDays(-12), RetireDate = _today.AddDays(28), WallId = 12 },
                new BoulderProblem { Id = 60, Grade = "7A", Style = "Vertical", Author = "Tomáš", BuiltDate = _today.AddDays(9), RetireDate = _today.AddDays(39), WallId = 12 },
                new BoulderProblem { Id = 61, Grade = "7B", Style = "Overhang", Author = "Martin", BuiltDate = _today.AddDays(-37), RetireDate = _today.AddDays(-7), WallId = 12 },
                new BoulderProblem { Id = 62, Grade = "7C", Style = "Vertical", Author = "Jan", BuiltDate = _today.AddDays(-23), RetireDate = _today.AddDays(7), WallId = 12 },
                new BoulderProblem { Id = 63, Grade = "8A+", Style = "Overhang", Author = "Eva", BuiltDate = _today.AddDays(0), RetireDate = _today.AddDays(30), WallId = 12 }
            );
        }

    }

}
