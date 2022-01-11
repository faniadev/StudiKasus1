using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EnrollmentServices.Dtos;
using Microsoft.Extensions.Configuration;

namespace EnrollmentServices.SyncDataServices.Http
{
    public class HttpPaymentDataClient : IPaymentDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpPaymentDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task CreateEnrollmentInPayment(EnrollmentDto enrol)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(enrol),
                Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_configuration["PaymentService"],
                httpContent);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync POST to Payment Service was OK !");
            }
            else
            {
                Console.WriteLine("--> Sync POST to Payment Service failed");
            }
        }
    }
}
        
 