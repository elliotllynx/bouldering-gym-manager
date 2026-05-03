using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Entities
{
    public class Section
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int GymId { get; set; }
    }
}
