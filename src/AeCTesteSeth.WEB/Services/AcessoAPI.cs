using AeCTesteSeth.BLL.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection.Metadata;

namespace AeCTesteSeth.WEB.Services
{
    public class AcessoAPI
    {
        private readonly HttpClient _httpClient;
        public AcessoAPI(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("API");
        }

        

        public async Task Login(Login login)
        {
            await _httpClient.PostAsJsonAsync("api/Acesso/Login", login);
        }

       
    }
}
