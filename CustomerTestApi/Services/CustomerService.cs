using CustomerTestApi.Models;
using CustomerTestApi.Services.IServices;
using Newtonsoft.Json;
using System.Text;

namespace CustomerTestApi.Services.Services
{
    public class CustomerService : ICustomerService
    {
        private IHttpClientFactory _httpClient;
        const string baseUrl = "https://customertestfunctionapp.azurewebsites.net/api/";

        public CustomerService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<List<Customer>> GetCustomerByIdAsync(int id)
        {
            string getUrl = baseUrl + "getcustomer/" + id;

            var client = _httpClient.CreateClient("CustomerFunctionAPi");
            HttpRequestMessage message = new HttpRequestMessage();
            message.Headers.Add("Accept", "application/json");
            message.RequestUri = new Uri(getUrl);
            client.DefaultRequestHeaders.Clear();

            message.Method = HttpMethod.Get;

            HttpResponseMessage apiResponse = await client.SendAsync(message);

            var apiContent = apiResponse.Content.ReadAsStringAsync().Result;
            var apiResponseDto = JsonConvert.DeserializeObject<List<Customer>>(apiContent);
            return apiResponseDto;

        }

        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            string postUrl = "http://localhost:7042/api/CreateCustomer";

            var client = _httpClient.CreateClient("CustomerFunctionAPi");
            HttpRequestMessage message = new HttpRequestMessage();
            message.Headers.Add("Accept", "application/json");
            message.RequestUri = new Uri(postUrl);
            client.DefaultRequestHeaders.Clear();
            
            message.Content = new StringContent(JsonConvert.SerializeObject(customer),
                      Encoding.UTF8, "application/json");
            message.Method = HttpMethod.Post;

            HttpResponseMessage apiResponse = await client.SendAsync(message);

            string apiContent = apiResponse.Content.ReadAsStringAsync().Result;
            Customer apiResponseDto = JsonConvert.DeserializeObject<Customer>(apiContent);
            return apiResponseDto;
        }
    }
}

