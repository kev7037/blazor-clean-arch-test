using Mc2.CrudTest.Core.Domain.Customers.Entities;
using Mc2.CrudTest.Infrastructures.Command.Customers;
using Mc2.CrudTest.Infrastructures.Command.Interceptors;
using Mc2.CrudTest.Presentation.Shared.HelperClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Mc2.CrudTest.Infrastructures.Command
{
    public class CommandDBContext : DbContext
    {
        private readonly PublishDomainEventsInterceptor _publishDomainEventsInterceptor;
        public CommandDBContext(DbContextOptions<CommandDBContext> options, PublishDomainEventsInterceptor publishDomainEventsInterceptor) : base(options) => _publishDomainEventsInterceptor = publishDomainEventsInterceptor;

        public CommandDBContext(DbContextOptions<CommandDBContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CustomerConfig())
                .Ignore<List<IDomainEvent>>()
                .ApplyConfigurationsFromAssembly(typeof(CommandDBContext).Assembly);

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_publishDomainEventsInterceptor);
            base.OnConfiguring(optionsBuilder);
        }

        public class CommandContextDesignTimeFactory : IDesignTimeDbContextFactory<CommandDBContext>
        {
            public CommandDBContext CreateDbContext(string[] args)
            {
                var builder = new DbContextOptionsBuilder<CommandDBContext>();
                builder.UseSqlServer("Server=crudtest-db;Database=db_crudtest;MultipleActiveResultSets=true;User ID=sa; Password=password@1234#;Persist Security Info=True;TrustServerCertificate=True;");
                return new CommandDBContext(builder.Options);
            }
        }
    }

}
