using Mc2.CrudTest.Core.Domain.Customers.DTOs;
using MediatR;

namespace Mc2.CrudTest.Core.ApplicationServices.Customers.Queries.GetAllCustomers
{
    public class GetAllCustomersQuery : IRequest<List<CustomerDto>>
    {
    }
}
