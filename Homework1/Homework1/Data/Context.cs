using Homework1.Models;
using Microsoft.EntityFrameworkCore;
using Homework1.Models;

namespace Homework1.Data
{
    public class Context : DbContext
    {
            public Context(DbContextOptions<Context> options) : base(options) {}
            public DbSet<Values> Values {get; set;}
            
            public DbSet<Students> Students {get; set;}
            public DbSet<Students_Description> Students_description {get; set;}
    }
}