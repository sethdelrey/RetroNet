using _90sTest.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace _90sTest.Data
{
    public class FeedContext : DbContext
    {
        public DbSet<Post> Post { get; set; }
        public DbSet<User> User { get; set; }

        public FeedContext(DbContextOptions<FeedContext> options)
        : base(options)
        { }

        public FeedContext()
            : base()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=90sTest;Integrated Security=True");//ConfigurationManager.ConnectionStrings["90sTestDatabase"].ConnectionString);
        }
    }
}
