using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FurnitureDetailsAPI.Model
{
    public class Furniture
    {
        [Key]
        public int FurnitureId { get; set; }
        [Required]
        public string FurnitureName { get; set; }
        [Required]
        public double Price { get; set; }

    }
}
