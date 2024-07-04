using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clean.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Clean.Infrastructure.Data
{
   public class ApplicationDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}

}