﻿using Mc2.CrudTest.Core.Domain.Customers.DTOs;
using Mc2.CrudTest.Presentation.Shared.HelperClasses;
using MediatR;

namespace Mc2.CrudTest.Core.ApplicationServices.Customers.Queries.GetAllCustomers
{
    public class GetAllCustomersQuery : IRequest<List<CustomerDto>>
    {
    }
}
