using Mc2.CrudTest.Core.ApplicationServices.Customers.Contracts;
using Mc2.CrudTest.Core.Domain.Customers.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Infrastructures.Query.Customers
{
    public class CustomerQueryRepository : QueryDBContext, ICustomerQueryRepository
    {
        protected readonly QueryDBContext _dbContext;
        public CustomerQueryRepository(DbContextOptions options, QueryDBContext dbContext) : base(options) => _dbContext = dbContext;

        public async Task<List<CustomerDto>> GetAllCustomers()
            => await _dbContext
                     .Customers
                     .AsNoTracking()
                     .Select(x => new CustomerDto()
                     {
                         Id = x.Id,
                         FirstName = x.FirstName,
                         LastName = x.LastName,
                         DateOfBirth = x.DateOfBirth,
                         PhoneNumber = x.PhoneNumber,
                         Email = x.Email,
                         BankAccountNumber = x.BankAccountNumber
                     }).ToListAsync();

        public async Task<CustomerDto?> GetCustomerById(long id)
            => await _dbContext
                     .Customers
                     .Where(x => x.Id == id)
                     .AsNoTracking()
                     .Select(x => new CustomerDto()
                     {
                         Id = x.Id,
                         FirstName = x.FirstName,
                         LastName = x.LastName,
                         DateOfBirth = x.DateOfBirth,
                         PhoneNumber = x.PhoneNumber,
                         Email = x.Email,
                         BankAccountNumber = x.BankAccountNumber
                     })
                     .FirstOrDefaultAsync();

        public async Task<bool> IsEmailUnique(string email) 
            => await _dbContext
                     .Customers
                     .Where(x => x.Email.Equals(email.Trim().ToLower()))
                     .AsNoTracking()
                     .AnyAsync();

        public async Task<bool> IsFullNameAndDateOfBirthCombinationUnique(string firstName, string lastName, DateTime dateOfBirth, long? id)
            => !(await _dbContext
                     .Customers
                     .Where(x => x.FirstName.Equals(firstName.Trim().ToLower())
                              && x.LastName.Equals(lastName.Trim().ToLower())
                              && x.DateOfBirth.Date.Equals(dateOfBirth.Date)
                              && (id == null || x.Id != id))
                     .AsNoTracking()
                     .AnyAsync());

    }
}
