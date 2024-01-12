using Mc2.CrudTest.Core.ApplicationServices.Customers.Contracts;
using Mc2.CrudTest.Core.Domain.Customers.Entities;
using Mc2.CrudTest.Core.Domain;
using MediatR;

namespace Mc2.CrudTest.Core.ApplicationServices.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, long>
    {
        private readonly ICustomerCommandRepository _customerRepository;
        private readonly ICustomerUnitOfWork _unitOfWork;

        public UpdateCustomerCommandHandler(ICustomerCommandRepository customerRepository, ICustomerUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<long> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                BankAccountNumber = request.BankAccountNumber
            };

            await _customerRepository.UpdateAsync(customer);
            await _unitOfWork.CommitAsync();

            return customer.Id;
        }

    }
}