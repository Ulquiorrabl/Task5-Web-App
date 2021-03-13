using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Transaction
    {
        [Required]
        public int TransactionId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Required]
        [Range(0.01, 1000000)]
        public int Cost { get; set; }
        [Required]
        public int ManagerId { get; set; }
        [Required]
        public Manager Manager { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public Product Product { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public User User { get; set; }
    }
}
