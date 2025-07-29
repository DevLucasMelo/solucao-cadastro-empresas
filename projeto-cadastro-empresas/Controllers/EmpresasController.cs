using CadastroEmpresasApp.Data;
using CadastroEmpresasApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;  
using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json.Serialization;


namespace CadastroEmpresasApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] 
    public class EmpresasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;

        public EmpresasController(ApplicationDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost("cadastrar")]
        public async Task<IActionResult> CadastrarEmpresa([FromBody] CnpjDto dto)
        {
            var usuId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (usuId == null)
                return Unauthorized();

            var client = _httpClientFactory.CreateClient();
            var url = $"https://www.receitaws.com.br/v1/cnpj/{dto.Cnpj}";
            var response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return BadRequest("Erro ao consultar a ReceitaWS");

            var json = await response.Content.ReadAsStringAsync();
            var receitaResponse = JsonSerializer.Deserialize<ReceitaWsResponse>(json);

            if (receitaResponse.Status == "ERROR")
                return BadRequest("CNPJ inválido ou não encontrado");

            var cnpj = receitaResponse.Cnpj?.Replace(".", "").Replace("/", "").Replace("-", "").Trim();

            bool jaExiste = await _context.Empresas.AnyAsync(e => e.emp_cnpj == cnpj && e.emp_usu_id.ToString() == usuId);
            if (jaExiste)
                return BadRequest("Empresa já cadastrada.");

            var empresa = new Empresa
            {
                emp_nome_empresarial = receitaResponse.Nome,
                emp_nome_fantasia = receitaResponse.Fantasia,
                emp_cnpj = cnpj,
                emp_situacao = receitaResponse.Situacao,
                emp_abertura = receitaResponse.Abertura,
                emp_tipo = receitaResponse.Tipo,
                emp_natureza_juridica = receitaResponse.NaturezaJuridica,
                emp_atividade_principal = receitaResponse.AtividadePrincipal != null && receitaResponse.AtividadePrincipal.Count > 0 ? receitaResponse.AtividadePrincipal[0].Text : null,
                emp_logradouro = receitaResponse.Logradouro,
                emp_numero = receitaResponse.Numero,
                emp_complemento = receitaResponse.Complemento,
                emp_bairro = receitaResponse.Bairro,
                emp_municipio = receitaResponse.Municipio,
                emp_uf = receitaResponse.Uf,
                emp_cep = receitaResponse.Cep,
                emp_usu_id = int.Parse(usuId)
            };

            _context.Empresas.Add(empresa);
            await _context.SaveChangesAsync();

            return Ok("Empresa cadastrada com sucesso.");
        }

        [HttpGet("listar")]
        public async Task<IActionResult> ListarEmpresas([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var usuId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (usuId == null)
                return Unauthorized();

            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            var query = _context.Empresas
                .Where(e => e.emp_usu_id.ToString() == usuId);

            var totalItems = await query.CountAsync();

            var empresas = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = new
            {
                totalItems,
                page,
                pageSize,
                totalPages = (int)System.Math.Ceiling((double)totalItems / pageSize),
                empresas
            };

            return Ok(result);
        }
    }

    public class CnpjDto
    {
        public string Cnpj { get; set; }
    }

    public class ReceitaWsResponse
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("cnpj")]
        public string Cnpj { get; set; }

        [JsonPropertyName("nome")]
        public string Nome { get; set; }

        [JsonPropertyName("fantasia")]
        public string Fantasia { get; set; }

        [JsonPropertyName("situacao")]
        public string Situacao { get; set; }

        [JsonPropertyName("abertura")]
        public string Abertura { get; set; }

        [JsonPropertyName("tipo")]
        public string Tipo { get; set; }

        [JsonPropertyName("natureza_juridica")]
        public string NaturezaJuridica { get; set; }

        [JsonPropertyName("atividade_principal")]
        public List<Atividade> AtividadePrincipal { get; set; }

        [JsonPropertyName("logradouro")]
        public string Logradouro { get; set; }

        [JsonPropertyName("numero")]
        public string Numero { get; set; }

        [JsonPropertyName("complemento")]
        public string Complemento { get; set; }

        [JsonPropertyName("bairro")]
        public string Bairro { get; set; }

        [JsonPropertyName("municipio")]
        public string Municipio { get; set; }

        [JsonPropertyName("uf")]
        public string Uf { get; set; }

        [JsonPropertyName("cep")]
        public string Cep { get; set; }
    }

    public class Atividade
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}