using Mc2.CrudTest.Core.Domain.Customers.Events;
using Mc2.CrudTest.Presentation.Shared.HelperClasses;

namespace Mc2.CrudTest.Core.Domain.Customers.Entities
{
    public class Customer : BaseEntity
    {
        #region Properties
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public decimal PhoneNumber { get; set; }
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }
        #endregion

        public Customer() { }
        public Customer(string firstName,
                        string lastName,
                        DateTime dateOfBirth,
                        decimal phoneNumber,
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


        public void CreateCustomerEvent()
        {
            AddDomainEvent(new CustomerCreated
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                DateOfBirth = DateOfBirth,
                PhoneNumber = PhoneNumber,
                Email = Email,
                BankAccountNumber = BankAccountNumber
            });
        }

        public void UpdateCustomerEvent()
        {
            AddDomainEvent(new CustomerUpdated
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                DateOfBirth = DateOfBirth,
                PhoneNumber = PhoneNumber,
                Email = Email,
                BankAccountNumber = BankAccountNumber
            });
        }

        public void DeleteCustomerEvent()
        {
            AddDomainEvent(new CustomerDeleted
            {
                Id = Id
            });
        }

    }
}
