
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Collections.Generic;

using WebApplication2.Models;


namespace WebApplication2.Context
{
    public class CrudContext: DbContext
    {
        public DbSet<Student> Students { get; set; } = null!;
        public CrudContext(DbContextOptions<CrudContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
