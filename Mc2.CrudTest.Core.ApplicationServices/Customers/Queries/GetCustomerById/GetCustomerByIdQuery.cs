using Mc2.CrudTest.Core.Domain.Customers.DTOs;
using MediatR;

namespace Mc2.CrudTest.Core.ApplicationServices.Customers.Queries.GetCustomerById
{
    public class GetCustomerByIdQuery : IRequest<CustomerDto>
    {
        public long Id { get; set; }
    }
}
