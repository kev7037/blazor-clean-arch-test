using FluentValidation;
using Mc2.CrudTest.Core.ApplicationServices.Customers.Contracts;
using Mc2.CrudTest.Core.Domain;
using Mc2.CrudTest.Core.Domain.Customers.Entities;
using MediatR;

namespace Mc2.CrudTest.Core.ApplicationServices.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, long>
    {
        private readonly ICustomerCommandRepository _customerRepository;
        private readonly IValidator<CreateCustomerCommand> _validator;
        private readonly ICustomerUnitOfWork _unitOfWork;

        public CreateCustomerCommandHandler(ICustomerCommandRepository customerRepository,
                                            ICustomerUnitOfWork unitOfWork,
                                            IValidator<CreateCustomerCommand> validator)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<long> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
        {
            FluentValidation.Results.ValidationResult validationResult = await _validator.ValidateAsync(command);
            if (validationResult.IsValid)
            {
                Customer customer = new Customer
                {
                    FirstName = command.FirstName,
                    LastName = command.LastName,
                    DateOfBirth = command.DateOfBirth,
                    PhoneNumber = command.PhoneNumber,
                    Email = command.Email,
                    BankAccountNumber = command.BankAccountNumber
                };

                await _customerRepository.AddAsync(customer);
                customer.CreateCustomerEvent();

                await _unitOfWork.CommitAsync();

                return customer.Id;
            }

            return 0;
        }

    }
}