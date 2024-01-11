namespace Mc2.CrudTest.Core.ApplicationServices.Customers.Contracts
{
    public interface ICustomerCommandRepository
    {
        Task<bool> IsEmailIsUnique();

        // by first name, last name, date of birth
        Task<bool> IsCustomerUnique();
    }
}
