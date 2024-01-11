using Mc2.CrudTest.Core.Domain.Customers.DTOs;
using Mc2.CrudTest.Presentation.Shared.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Core.ApplicationServices.Customers.Queries.GetCustomerById
{
    public class GetCustomerByIdQuery : PageQuery<CustomerDto>
    {
        public long Id { get; set; }
    }
}
