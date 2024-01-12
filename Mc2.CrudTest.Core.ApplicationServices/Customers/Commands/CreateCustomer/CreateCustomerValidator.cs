using Mc2.CrudTest.Presentation.Shared.Extentions;
using FluentValidation;
using IbanNet;

namespace Mc2.CrudTest.Core.ApplicationServices.Customers.Commands.CreateCustomer
{
    public class CreateCustomerValidator: AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerValidator()
        {
            RuleFor(c => c.FirstName).NotEmpty().MaximumLength(100).WithMessage("First Name must not be empty");

            RuleFor(c => c.LastName).NotEmpty().MaximumLength(100).WithMessage("Last Name must not be empty");

            RuleFor(c => c.Email).NotEmpty().WithMessage("Email must not be empty")
                                 .EmailAddress().WithMessage("Invalid email address");

            RuleFor(c => c.PhoneNumber).NotEmpty()
                                       .WithMessage("Phone number must not be empty")
                                       .Must((phoneNumber) => HelperMethods.ValidatePhoneNumber(phoneNumber))
                                       .WithMessage("Phone number is not valid!");

            RuleFor(c => c.DateOfBirth).NotEmpty().WithMessage("Date of birth must not be empty");

            RuleFor(c => c.BankAccountNumber).NotEmpty().WithMessage("Bank account number must not be empty")
                                             .Must(IsValidIban).WithMessage("Invalid bank account number")
                                             .Length(12, 34).WithMessage("IBAN length should be between 12 and 34 characters.");
        }

        private bool IsValidIban(string iban)
        {
            var ibanValidator = new IbanValidator();
            return ibanValidator.Validate(iban).IsValid;
        }
    }
}
