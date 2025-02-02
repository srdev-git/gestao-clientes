using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SRDev.GestaoClientes.Web.Services
{
    public class AutenticacaoService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://localhost:5240/api/Autenticacao/login";

        public AutenticacaoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> LoginAsync(string email, string senha)
        {
            var loginRequest = new { Email = email, Senha = senha };
            var content = new StringContent(JsonSerializer.Serialize(loginRequest), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(BaseUrl, content);

            if (!response.IsSuccessStatusCode)
                return null;

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var loginResponse = JsonSerializer.Deserialize<LoginResponse>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return loginResponse?.Token;
        }

        private class LoginResponse
        {
            public string Token { get; set; }
        }
    }
}
