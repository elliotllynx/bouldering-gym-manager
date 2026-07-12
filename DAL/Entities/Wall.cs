using System.ComponentModel.DataAnnotations;
using DAL.Enums;

namespace DAL.Entities
{
    public class Wall
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }
        public int GymId { get; set; }
        public Gym Gym { get; set; }
    }
}
