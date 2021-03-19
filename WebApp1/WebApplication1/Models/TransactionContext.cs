using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class TransactionContext : DbContext
    {
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        //public DbSet<User> Users { get; set; }

        public TransactionContext(DbContextOptions<TransactionContext> options):
            base(options)
        {
            Database.EnsureCreated();
        }
    }
}
