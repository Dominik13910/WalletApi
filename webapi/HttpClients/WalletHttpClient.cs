using System.Text.Json;
using System.Text;
using webapi.Models;
using AutoMapper;
using webapi.Services;
using Azure;
using webapi.Dto.Wallet;

namespace webapi.HttpClients
{
    public class WalletHttpClient
    {
        private readonly HttpClient _httpClient;
        public WalletHttpClient(HttpClient httpClient)
        {

            _httpClient = httpClient;
        }
        public async Task PerformPostAsync(WalletDto obj)
        {
            try
            {
                HttpClient client = new HttpClient();
                string str = JsonSerializer.Serialize(obj);

                 HttpContent content = new StringContent(str, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("https://localhost:7084/Exchange/deserializeJson",
                content);
                response.EnsureSuccessStatusCode();

                string resp = await response.Content.ReadAsStringAsync();

                Console.WriteLine(resp);
                
               


            }
            catch (Exception ex)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", ex.Message);
            }

          
        }
     
    }

}
