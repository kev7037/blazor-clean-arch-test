using FluentValidation;
using IbanNet;
using IbanNet.FluentValidation;

namespace Mc2.CrudTest.Core.ApplicationServices.Customers.Commands.CreateCustomer
{
    public class CreateCustomerValidator: AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerValidator(IbanValidator ibanValidator)
        {
            RuleFor(c => c.FirstName).NotEmpty().MaximumLength(100).WithMessage("First Name must not be empty");

            RuleFor(c => c.LastName).NotEmpty().MaximumLength(100).WithMessage("Last Name must not be empty");

            RuleFor(c => c.Email).NotEmpty().WithMessage("Email must not be empty")
                                 .EmailAddress().WithMessage("Invalid email address");

            RuleFor(c => c.PhoneNumber).NotEmpty().WithMessage("Phone number must not be empty");

            RuleFor(c => c.DateOfBirth).NotEmpty().WithMessage("Date of birth must not be empty");

            RuleFor(c => c.BankAccountNumber).NotEmpty().WithMessage("Bank account number must not be empty")
                                             .Iban(ibanValidator).WithMessage("Invalid bank account number");
        }
    }
}
