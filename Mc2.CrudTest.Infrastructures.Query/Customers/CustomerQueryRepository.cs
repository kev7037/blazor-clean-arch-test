using Mc2.CrudTest.Core.ApplicationServices.Customers.Contracts;
using Mc2.CrudTest.Core.Domain.Customers.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Infrastructures.Query.Customers
{
    public class CustomerQueryRepository : QueryDBContext, ICustomerQueryRepository
    {
        protected readonly QueryDBContext _dbContext;
        public CustomerQueryRepository(DbContextOptions options, QueryDBContext dbContext) : base(options)
        {
            _dbContext = dbContext;
        }
        public Task<List<CustomerDto>> GetAllCustomers() => throw new NotImplementedException();
        //=> await _dbContext.Customers.AsNoTracking().ToList();
        public Task<CustomerDto> GetCustomerById() => throw new NotImplementedException();
        //=> await _dbContext.Customers.AsNoTracking().FirstOrDefault(x => x.Id == id);
    }
}
