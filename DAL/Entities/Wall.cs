using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Wall
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int GymId { get; set; }
        public Gym Gym { get; set; }
    }
}
