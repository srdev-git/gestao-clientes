using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SRDev.GestaoClientes.Web.Models;
using static SRDev.GestaoClientes.Web.Pages.Clientes.DetalhesModel;

namespace SRDev.GestaoClientes.Web.Services
{
    public class ClienteService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClienteService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ClientePaginadoViewModel> ObterClientesPaginados(int pageNumber, int pageSize)
        {
            var token = _httpContextAccessor.HttpContext?.Session.GetString("AuthToken");

            if (string.IsNullOrEmpty(token))
            {
                throw new InvalidOperationException("Usuário não autenticado. Faça login primeiro.");
            }

            // Adiciona o token no cabeçalho da requisição
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            var response = await _httpClient.GetAsync($"api/cliente/paginado?pageNumber={pageNumber}&pageSize={pageSize}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ClientePaginadoViewModel>(
                    json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new ClientePaginadoViewModel();
        }
                
        public async Task<ClienteDetalheViewModel> ObterClienteDetalhe(Guid id)
        {
            var token = _httpContextAccessor.HttpContext?.Session.GetString("AuthToken");

            if (string.IsNullOrEmpty(token))
            {
                throw new InvalidOperationException("Usuário não autenticado. Faça login primeiro.");
            }

            // Adiciona o token no cabeçalho da requisição
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            var response = await _httpClient.GetAsync($"api/cliente/{id}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ClienteDetalheViewModel>(
                    json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? throw new Exception("Cliente inválido.");
        }
        
        public async Task ExcluirCliente(Guid id)
        {
            var token = _httpContextAccessor.HttpContext?.Session.GetString("AuthToken");

            if (string.IsNullOrEmpty(token))
            {
                throw new InvalidOperationException("Usuário não autenticado. Faça login primeiro.");
            }

            // Adiciona o token no cabeçalho da requisição
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            var response = await _httpClient.DeleteAsync($"api/cliente/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task AtualizarCliente(ClienteEditarViewModel cliente)
        {
            var token = _httpContextAccessor.HttpContext?.Session.GetString("AuthToken");

            if (string.IsNullOrEmpty(token))
            {
                throw new InvalidOperationException("Usuário não autenticado. Faça login primeiro.");
            }

            // Adiciona o token no cabeçalho da requisição
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var json = JsonSerializer.Serialize(cliente);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/cliente/{cliente.Id}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task<UploadResponse> UploadLogotipo(Guid id, IFormFile logotipoFile)
        {
            var token = _httpContextAccessor.HttpContext?.Session.GetString("AuthToken");

            if (string.IsNullOrEmpty(token))
            {
                throw new InvalidOperationException("Usuário não autenticado. Faça login primeiro.");
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using var content = new MultipartFormDataContent();
            using var stream = new MemoryStream();
            await logotipoFile.CopyToAsync(stream);
            stream.Seek(0, SeekOrigin.Begin);

            var fileContent = new ByteArrayContent(stream.ToArray());
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(logotipoFile.ContentType);
            content.Add(fileContent, "file", logotipoFile.FileName);

            var response = await _httpClient.PostAsync($"api/Cliente/upload/{id}", content);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<UploadResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return result ?? throw new Exception("Retorno inválido");
        }
    }
}
