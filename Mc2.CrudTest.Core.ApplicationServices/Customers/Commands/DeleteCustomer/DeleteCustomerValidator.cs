using FluentValidation;

namespace Mc2.CrudTest.Core.ApplicationServices.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerValidator : AbstractValidator<DeleteCustomerCommand>
    {
        public DeleteCustomerValidator()
        {
            RuleFor(c => c.Id).NotEmpty().WithMessage("Id must not be empty")
                              .GreaterThanOrEqualTo(1).WithMessage("Id range not valid")
                              .LessThanOrEqualTo(long.MaxValue).WithMessage("Id range not valid");
        }
    }
}
