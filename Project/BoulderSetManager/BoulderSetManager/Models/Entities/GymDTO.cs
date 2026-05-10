using System;
using System.Collections.Generic;
using System.Text;

namespace BoulderSetManager.Models.Entities
{ 
    public class GymDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string DisplayName => $"{Name} ({Location})";
    }
}
