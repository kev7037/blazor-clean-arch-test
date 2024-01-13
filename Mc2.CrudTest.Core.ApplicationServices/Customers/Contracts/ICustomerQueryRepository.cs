using Mc2.CrudTest.Core.Domain.Customers.DTOs;

namespace Mc2.CrudTest.Core.ApplicationServices.Customers.Contracts
{
    public interface ICustomerQueryRepository
    {
        Task<List<CustomerDto>> GetAllCustomers();
        Task<CustomerDto?> GetCustomerById(long id);
        Task<bool> IsEmailUnique(string email);
        Task<bool> IsFullNameAndDateOfBirthCombinationUnique(string firstName, string lastName, DateTime dateOfBirth, long? id = null);
    }
}
