using Mc2.CrudTest.Core.Domain.Customers.DTOs;
using Mc2.CrudTest.ServerHelper.Models;
using System.Net.Http.Json;

namespace Mc2.CrudTest.ServerHelper
{
    namespace Mc2.CrudTest.ServerHelper
    {
        public class CrudTestClientService
        {

            private readonly HttpClient _httpClient;

            public CrudTestClientService(ApiClientOptions apiClientOptions)
            {
                _httpClient = new HttpClient();
                _httpClient.BaseAddress = new Uri(apiClientOptions.ApiBaseAddress);
            }

            public async Task<List<CustomerDto>?> GetAllCustomers()
                => await _httpClient.GetFromJsonAsync<List<CustomerDto>?>("/CustomersQuery/GetAllCustomers");

            public async Task<CustomerDto?> GetCustomerById(long id)
                => await _httpClient.GetFromJsonAsync<CustomerDto?>($"/CustomersQuery/GetCustomerById/{id}");

            public async Task SaveCustomer(CustomerDto customer)
                => await _httpClient.PostAsJsonAsync("/CustomersCommand/Create", customer);

            public async Task UpdateCustomer(CustomerDto customer)
                => await _httpClient.PutAsJsonAsync("/CustomersCommand/Update", customer);

            public async Task DeleteCustomer(long id)
                => await _httpClient.DeleteAsync($"/CustomersCommand/Delete/{id}");


        }
    }

}
