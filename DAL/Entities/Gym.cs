using DAL.Enums;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Gym
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public Status Status { get; set; }

    }
}
