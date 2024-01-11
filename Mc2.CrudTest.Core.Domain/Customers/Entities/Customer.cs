using Mc2.CrudTest.Presentation.Shared.HelperClasses;

namespace Mc2.CrudTest.Core.Domain.Customers.Entities
{
    public class Customer : BaseEntity
    {
        #region Properties
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string BankAccountNumber { get; set; } 
        #endregion

        public Customer() { }
        public Customer(string firstName,
                        string lastName,
                        DateTime dateOfBirth,
                        string phoneNumber,
                        string email,
                        string bankAccountNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            PhoneNumber = phoneNumber;
            Email = email;
            BankAccountNumber = bankAccountNumber;
        }
    }
}
