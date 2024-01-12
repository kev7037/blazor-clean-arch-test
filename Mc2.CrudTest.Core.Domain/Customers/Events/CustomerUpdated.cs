﻿using Mc2.CrudTest.Presentation.Shared.HelperClasses;

namespace Mc2.CrudTest.Core.Domain.Customers.Events
{
    public class CustomerUpdated : IDomainEvent
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }
    }
}
