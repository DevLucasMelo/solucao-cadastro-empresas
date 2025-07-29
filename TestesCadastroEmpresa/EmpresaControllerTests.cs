using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Headers;

namespace TestesCadastroEmpresa
{
    public class EmpresaControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public EmpresaControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        private async Task<string> ObterTokenAsync()
        {
            var loginPayload = new
            {
                Email = "lucas@example.com",
                Senha = "minhaSenha123"
            };

            var content = new StringContent(
                JsonSerializer.Serialize(loginPayload),
                Encoding.UTF8,
                "application/json");

            var response = await _client.PostAsync("/api/usuarios/login", content);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseString = await response.Content.ReadAsStringAsync();

            System.Console.WriteLine("Resposta login: " + responseString);

            using var jsonDoc = JsonDocument.Parse(responseString);

            if (jsonDoc.RootElement.TryGetProperty("Token", out var tokenElement) ||
                jsonDoc.RootElement.TryGetProperty("token", out tokenElement))
            {
                return tokenElement.GetString();
            }
            else
            {
                throw new KeyNotFoundException("A resposta do login não contém a propriedade 'Token' ou 'token'.");
            }

            var token = jsonDoc.RootElement.GetProperty("Token").GetString();

            return token;
        }

        [Fact]
        public async Task CadastrarEmpresa_ComCnpjValido_RetornaOkEmessage()
        {
            var token = await ObterTokenAsync();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var empresaPayload = new
            {
                cnpj = "16670085000155"
            };

            var content = new StringContent(
                JsonSerializer.Serialize(empresaPayload),
                Encoding.UTF8,
                "application/json");

            var response = await _client.PostAsync("/api/empresas/cadastrar", content);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseString = await response.Content.ReadAsStringAsync();
            responseString.Should().Contain("Empresa cadastrada com sucesso");
        }

        [Fact]
        public async Task CadastrarEmpresa_ComCnpjInvalido_RetornaBadRequest()
        {
            var token = await ObterTokenAsync();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var empresaPayload = new
            {
                Cnpj = "00000002300000"
            };

            var content = new StringContent(
                JsonSerializer.Serialize(empresaPayload),
                Encoding.UTF8,
                "application/json");

            var response = await _client.PostAsync("/api/empresas/cadastrar", content);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task CadastrarEmpresa_SemToken_RetornaUnauthorized()
        {
            _client.DefaultRequestHeaders.Authorization = null;

            var empresaPayload = new
            {
                Cnpj = "00000000000191"
            };

            var content = new StringContent(
                JsonSerializer.Serialize(empresaPayload),
                Encoding.UTF8,
                "application/json");

            var response = await _client.PostAsync("/api/empresas/cadastrar", content);
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task ListarEmpresas_UsuarioLogado_RetornaApenasSuasEmpresas()
        {
            var token = await ObterTokenAsync();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetAsync("/api/empresas/listar");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseString = await response.Content.ReadAsStringAsync();

            responseString.Should().Contain("empresas");
        }
    }
}
