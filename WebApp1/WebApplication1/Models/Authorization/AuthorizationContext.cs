using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models.Authorization
{
    public class AuthorizationContext : IdentityDbContext<AuthorizationUser>
    {
        public AuthorizationContext(DbContextOptions<AuthorizationContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }
    }
}
