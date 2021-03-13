using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Product
    {
        [Required]
        public int ID { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string ProductName { get; set; }
        [Required]
        [Range(0.01, 50000)]
        public int Cost { get; set; }
    }
}
