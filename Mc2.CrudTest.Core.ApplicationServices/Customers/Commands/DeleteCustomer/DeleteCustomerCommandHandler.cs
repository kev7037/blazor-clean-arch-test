using Mc2.CrudTest.Core.ApplicationServices.Customers.Contracts;
using Mc2.CrudTest.Core.Domain;
using MediatR;

namespace Mc2.CrudTest.Core.ApplicationServices.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, long>
    {
        private readonly ICustomerCommandRepository _customerRepository;
        private readonly ICustomerUnitOfWork _unitOfWork;

        public DeleteCustomerCommandHandler(ICustomerCommandRepository customerRepository, ICustomerUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<long> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            await _customerRepository.DeleteAsync(request.Id);
            await _unitOfWork.CommitAsync();

            return request.Id;
        }

    }
}