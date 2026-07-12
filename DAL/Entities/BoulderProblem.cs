using System.ComponentModel.DataAnnotations;
using DAL.Enums;

namespace DAL.Entities
{
    public class BoulderProblem
    {
        [Key] public int Id { get; set; }
        public string Grade { get; set; }
        public BoulderStyle Style { get; set; }
        public string Author { get; set; }
        public DateTime BuiltDate { get; set; }
        public DateTime RetireDate { get; set; }
        public Status Status { get; set; }
        public int WallId { get; set; }
        public Wall Wall { get; set; }
    }
}
