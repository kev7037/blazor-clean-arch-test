using Mc2.CrudTest.Core.ApplicationServices.Customers.Contracts;
using Mc2.CrudTest.Core.Domain.Customers.DTOs;
using MediatR;

namespace Mc2.CrudTest.Core.ApplicationServices.Customers.Queries.GetCustomerById
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDto>
    {
        private readonly ICustomerQueryRepository _customerQueryRepository;

        public GetCustomerByIdQueryHandler(ICustomerQueryRepository customerQueryRepository) => _customerQueryRepository = customerQueryRepository;

        public async Task<CustomerDto?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
            => await _customerQueryRepository.GetCustomerById(request.Id);
    }
}
