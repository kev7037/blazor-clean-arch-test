using Mc2.CrudTest.Core.ApplicationServices.Customers.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Infrastructures.Command.Customers
{
    public class CustomerCommandRepository : CommandDBContext, ICustomerCommandRepository
    {
        protected readonly CommandDBContext _dbContext;
        public CustomerCommandRepository(DbContextOptions options, CommandDBContext dbContext) : base(options)
        {
            _dbContext = dbContext;
        }

        public Task<bool> IsCustomerUnique() => throw new NotImplementedException();
        public Task<bool> IsEmailIsUnique() => throw new NotImplementedException();
    }
}
