using System;
using System.Collections.Generic;
using System.Text;

namespace BoulderSetManager.Models.Entities
{
   public class BoulderingProblemDTO
   {
        public int Id { get; set; }
        public string Grade { get; set; }
        public string Type { get; set; }
        public string Author { get; set; }
        public DateTime BuiltDate { get; set; }
        public DateTime RetireDate { get; set; }
        public int SectionId { get; set; }
   }
}
