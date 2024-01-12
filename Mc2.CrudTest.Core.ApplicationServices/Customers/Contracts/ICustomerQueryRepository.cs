using Mc2.CrudTest.Core.Domain.Customers.DTOs;

namespace Mc2.CrudTest.Core.ApplicationServices.Customers.Contracts
{
    public interface ICustomerQueryRepository
    {
        Task<List<CustomerDto>> GetAllCustomers();
        Task<CustomerDto?> GetCustomerById(long id);
    }
}
