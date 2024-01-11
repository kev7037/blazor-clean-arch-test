using FluentValidation;
using Mc2.CrudTest.Core.ApplicationServices.Customers.Commands.DeleteCustomer;

namespace Mc2.CrudTest.Core.ApplicationServices.Customers.Queries.GetCustomerById
{
    public class GetCustomerByIdValidator : AbstractValidator<DeleteCustomerCommand>
    {
        public GetCustomerByIdValidator()
        {
            RuleFor(c => c.Id).NotEmpty().WithMessage("Id must not be empty")
                              .GreaterThanOrEqualTo(1).WithMessage("Id range not valid")
                              .LessThanOrEqualTo(long.MaxValue).WithMessage("Id range not valid");
        }
    }
}
