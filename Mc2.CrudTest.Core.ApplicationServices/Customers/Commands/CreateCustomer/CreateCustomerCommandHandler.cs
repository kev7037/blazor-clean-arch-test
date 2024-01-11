using Mc2.CrudTest.Core.ApplicationServices.Customers.Contracts;
using Mc2.CrudTest.Core.Domain;
using Mc2.CrudTest.Core.Domain.Customers.Entities;
using MediatR;

namespace Mc2.CrudTest.Core.ApplicationServices.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, long>
    {
        private readonly ICustomerCommandRepository _customerRepository;
        private readonly ICustomerUnitOfWork _unitOfWork;

        public CreateCustomerCommandHandler(ICustomerCommandRepository customerRepository, ICustomerUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<long> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                BankAccountNumber = request.BankAccountNumber
            };

            await _customerRepository.AddAsync(customer);
            await _unitOfWork.CommitAsync();

            return customer.Id;
        }

    }
}