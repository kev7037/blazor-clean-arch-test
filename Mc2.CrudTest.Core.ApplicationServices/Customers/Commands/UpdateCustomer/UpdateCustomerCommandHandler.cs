using FluentValidation;
using Mc2.CrudTest.Core.ApplicationServices.Customers.Contracts;
using Mc2.CrudTest.Core.Domain;
using Mc2.CrudTest.Core.Domain.Customers.Entities;
using MediatR;

namespace Mc2.CrudTest.Core.ApplicationServices.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, long>
    {
        private readonly ICustomerCommandRepository _customerRepository;
        private readonly IValidator<UpdateCustomerCommand> _validator;
        private readonly ICustomerUnitOfWork _unitOfWork;

        public UpdateCustomerCommandHandler(ICustomerCommandRepository customerRepository,
                                            ICustomerUnitOfWork unitOfWork,
                                            IValidator<UpdateCustomerCommand> validator)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<long> Handle(UpdateCustomerCommand command, CancellationToken cancellationToken)
        {
            FluentValidation.Results.ValidationResult validationResult = await _validator.ValidateAsync(command);
            if (validationResult.IsValid)
            {
                Customer customer = new Customer
                {
                    Id = command.Id,
                    FirstName = command.FirstName,
                    LastName = command.LastName,
                    DateOfBirth = command.DateOfBirth,
                    PhoneNumber = command.PhoneNumber,
                    Email = command.Email,
                    BankAccountNumber = command.BankAccountNumber
                };
                await _customerRepository.UpdateAsync(customer);
                customer.UpdateCustomerEvent();

                await _unitOfWork.CommitAsync();

            }

            return command.Id;
        }
    }
}