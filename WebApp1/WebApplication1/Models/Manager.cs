using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Manager
    {
        [Required]
        public int ManagerId { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string ManagerName { get; set; }
    }
}
