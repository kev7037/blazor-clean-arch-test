using Mc2.CrudTest.Core.Domain.Customers.Entities;
using Mc2.CrudTest.Infrastructures.Command.Customers;
using Mc2.CrudTest.Infrastructures.Command.Interceptors;
using Mc2.CrudTest.Presentation.Shared.HelperClasses;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Infrastructures.Command
{
    public class CommandDBContext : DbContext
    {
        private readonly PublishDomainEventsInterceptor _publishDomainEventsInterceptor;
        public CommandDBContext(DbContextOptions<CommandDBContext> options, PublishDomainEventsInterceptor publishDomainEventsInterceptor) : base(options) => _publishDomainEventsInterceptor = publishDomainEventsInterceptor;

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
    }

}
