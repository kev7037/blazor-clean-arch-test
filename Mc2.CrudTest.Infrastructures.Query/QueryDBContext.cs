using Mc2.CrudTest.Core.Domain.Customers.Entities;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Infrastructures.Query
{
    public class QueryDBContext : DbContext
    {
        public QueryDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) => base.OnModelCreating(modelBuilder);
    }
}
