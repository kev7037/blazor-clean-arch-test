using FluentValidation;
using IbanNet;
using Mc2.CrudTest.Core.ApplicationServices.Customers.Commands.CreateCustomer;
using Mc2.CrudTest.Core.ApplicationServices.Customers.Contracts;
using Mc2.CrudTest.Presentation.Shared.Extentions;
using System.Text.RegularExpressions;

namespace Mc2.CrudTest.Core.ApplicationServices.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerValidator : AbstractValidator<UpdateCustomerCommand>
    {
        private readonly ICustomerQueryRepository _customerQueryRepository;
        public UpdateCustomerValidator(ICustomerQueryRepository customerQueryRepository)
        {
            _customerQueryRepository = customerQueryRepository;

            RuleFor(c => c.Id).NotEmpty().WithMessage("Id must not be empty")
                              .GreaterThanOrEqualTo(1).WithMessage("Id range not valid")
                              .LessThanOrEqualTo(long.MaxValue).WithMessage("Id range not valid");

            RuleFor(c => c.FirstName).NotEmpty().MaximumLength(100).WithMessage("First Name must not be empty");

            RuleFor(c => c.LastName).NotEmpty().MaximumLength(100).WithMessage("Last Name must not be empty");

            RuleFor(c => c.Email).NotEmpty().WithMessage("Email must not be empty")
                                 .Must(ValidateEmailAddress).WithMessage("Email cannot contains special characters")
                                 .EmailAddress().WithMessage("Invalid email address")
                                 .MustAsync((email, CancellationToken) => IsEmailUnique(email)).WithMessage("Email already exits in system");
            ;

            RuleFor(c => c.PhoneNumber).NotEmpty()
                                                   .WithMessage("Phone number must not be empty")
                                                   .GreaterThan(1)
                                                   .WithMessage("Phone number is not valid!")
                                                   .Must((phoneNumber) => HelperMethods.ValidatePhoneNumber(phoneNumber))
                                                   .WithMessage("Phone number is not valid!");

            RuleFor(c => c.DateOfBirth).NotEmpty().WithMessage("Date of birth must not be empty");

            RuleFor(c => c.BankAccountNumber).NotEmpty().WithMessage("Bank account number must not be empty")
                                             .Must(IsValidIban).WithMessage("Invalid bank account number")
                                             .Length(12, 34).WithMessage("IBAN length should be between 12 and 34 characters.");

            RuleFor(c => c)
                .MustAsync(async (command, CancellationToken) => await FullNameAndBirthDateCombinationIsUnique(command))
                .WithMessage("Combination of FirstName, LastName and DateOfBirth is not unique!");
        }

        private bool IsValidIban(string iban)
        {
            IbanValidator ibanValidator = new IbanValidator();
            return ibanValidator.Validate(iban).IsValid;
        }
        private bool ValidateEmailAddress(string email)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                              @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                              @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(email))
                return true;
            else
                return false;
        }

        private async Task<bool> IsEmailUnique(string email)
             => !(await _customerQueryRepository.IsEmailUnique(email));

        private async Task<bool> FullNameAndBirthDateCombinationIsUnique(UpdateCustomerCommand command)
            => await _customerQueryRepository.IsFullNameAndDateOfBirthCombinationUnique(command.FirstName, command.LastName, command.DateOfBirth, command.Id);

    }
}
