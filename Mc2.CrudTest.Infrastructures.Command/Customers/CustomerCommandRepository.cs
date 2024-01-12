using Mc2.CrudTest.Core.ApplicationServices.Customers.Contracts;
using Mc2.CrudTest.Core.Domain.Customers.Entities;
using Mc2.CrudTest.Infrastructures.Command.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Infrastructures.Command.Customers
{
    public class CustomerCommandRepository : CommandDBContext, ICustomerCommandRepository
    {
        protected readonly CommandDBContext _dbContext;
        private readonly PublishDomainEventsInterceptor _publishDomainEventsInterceptor;
        public CustomerCommandRepository(DbContextOptions<CommandDBContext> options,
                                         CommandDBContext dbContext,
                                         PublishDomainEventsInterceptor publishDomainEventsInterceptor) : base(options, publishDomainEventsInterceptor)
        {
            _dbContext = dbContext;
            _publishDomainEventsInterceptor = publishDomainEventsInterceptor;
        }

        public async Task AddAsync(Customer customer) 
            => await _dbContext.Customers.AddAsync(customer);

        public async Task UpdateAsync(Customer customer)
            => _dbContext.Customers.Update(customer);

        public async Task DeleteAsync(long id)
        {
            var customer = await _dbContext.Customers.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (customer != null)
                _dbContext.Customers.Remove(customer);
        }
    }
}
