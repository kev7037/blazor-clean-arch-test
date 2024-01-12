using Mc2.CrudTest.Core.Domain.Customers.DTOs;

namespace Mc2.CrudTest.Presentation.Client.Pages
{
    partial class CustomerIndexComponent
    {
        private List<CustomerDto>? Customers { get; set; }
        private CustomerDto customer = new();

        protected override async Task OnInitializedAsync()
        {
            base.OnInitialized();
            await LoadCustomers();
        }

        private async Task LoadCustomers()
        {
            Customers = await clientService.GetAllCustomers();
        }

        private async Task AddCustomer()
        {
            await clientService.SaveCustomer(customer);
            await LoadCustomers();
            customer = new();
        }

        private async Task EditCustomer(CustomerDto customer)
        {
            await clientService.UpdateCustomer(customer);
            await LoadCustomers();
        }

        private async Task DeleteCustomer(long id)
        {
            await clientService.DeleteCustomer(id);
            await LoadCustomers();
        }
    }
}