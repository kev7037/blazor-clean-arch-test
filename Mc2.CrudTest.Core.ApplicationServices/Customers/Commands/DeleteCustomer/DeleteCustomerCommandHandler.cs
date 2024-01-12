using FluentValidation;
using Mc2.CrudTest.Core.ApplicationServices.Customers.Contracts;
using Mc2.CrudTest.Core.Domain;
using Mc2.CrudTest.Core.Domain.Customers.Entities;
using MediatR;

namespace Mc2.CrudTest.Core.ApplicationServices.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, long>
    {
        private readonly ICustomerCommandRepository _customerRepository;
        private readonly IValidator<DeleteCustomerCommand> _validator;
        private readonly ICustomerUnitOfWork _unitOfWork;

        public DeleteCustomerCommandHandler(ICustomerCommandRepository customerRepository,
                                            ICustomerUnitOfWork unitOfWork,
                                            IValidator<DeleteCustomerCommand> validator)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<long> Handle(DeleteCustomerCommand command, CancellationToken cancellationToken)
        {
            FluentValidation.Results.ValidationResult validationResult = await _validator.ValidateAsync(command);
            if (validationResult.IsValid)
            {
                Customer customer = new Customer
                {
                    Id = command.Id,
                };

                await _customerRepository.DeleteAsync(customer.Id);
                customer.DeleteCustomerEvent();

                await _unitOfWork.CommitAsync();
            }

            return command.Id;
        }

    }
}