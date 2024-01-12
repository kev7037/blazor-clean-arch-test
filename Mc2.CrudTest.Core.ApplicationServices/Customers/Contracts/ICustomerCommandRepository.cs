using Mc2.CrudTest.Core.Domain.Customers.Entities;

namespace Mc2.CrudTest.Core.ApplicationServices.Customers.Contracts
{
    public interface ICustomerCommandRepository
    {
        Task AddAsync(Customer customers);
        Task UpdateAsync(Customer customer);
        Task DeleteAsync(long id);

    }
}
