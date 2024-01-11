using Mc2.CrudTest.Core.Domain.Customers.Entities;
using Mc2.CrudTest.Infrastructures.Command.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Mc2.CrudTest.Infrastructures.Command
{
    public class CommandDBContext : DbContext
    {
        public CommandDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new CustomerConfig());
        }
    }


    public class CommandContextDesignTimeFactory :
       IDesignTimeDbContextFactory<CommandDBContext>
    {
        public CommandDBContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<CommandDBContext>();
            builder.UseSqlServer("Server=crudtest_db;Database=crudtest;MultipleActiveResultSets=true;User ID=sa; Password=a_A123456;Persist Security Info=True;TrustServerCertificate=True");
            return new CommandDBContext(builder.Options);
        }
    }
}
