using HDP.Application.Services.Contracts;
using HDP.Application.ViewModels.Hospedagem;
using Microsoft.AspNetCore.Mvc;

namespace HDP.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HospedagemController : ControllerBase
{
    private readonly IHospedagemService _hospedagemService;

    public HospedagemController(IHospedagemService hospedagemService)
    {
        _hospedagemService = hospedagemService;
    }

    // Cadastrar uma nova hospedagem
    [HttpPost("cadastrar")]
    public async Task<IActionResult> CadastrarHospedagem([FromBody] CadastroHospedagemViewModel dados)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _hospedagemService.CadastrarHospedagem(dados);
            if (result != null)
                return Ok(result);

            return BadRequest("Erro ao cadastrar a hospedagem.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno: {ex.Message}");
        }
    }

    // Obter hospedagem por ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetHospedagemPorId(Guid id)
    {
        try
        {
            var hospedagem = await _hospedagemService.GetHospedagemPorId(id);
            if (hospedagem != null)
                return Ok(hospedagem);

            return NotFound("Hospedagem não encontrada.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno: {ex.Message}");
        }
    }

    // Listar todas as hospedagens
    [HttpGet("listar")]
    public async Task<IActionResult> ListarHospedagens()
    {
        try
        {
            var hospedagens = await _hospedagemService.ListarHospedagens();
            return Ok(hospedagens);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno: {ex.Message}");
        }
    }

    // Listar hospedagens por status
    [HttpGet("status/{status}")]
    public async Task<IActionResult> ListarHospedagensPorStatus(string status)
    {
        try
        {
            var hospedagens = await _hospedagemService.ListarHospedagensPorStatus(status);
            return Ok(hospedagens);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno: {ex.Message}");
        }
    }

    // Listar hospedagens por período
    [HttpGet("periodo")]
    public async Task<IActionResult> ListarHospedagensPorPeriodo([FromQuery] DateTime dataInicio, [FromQuery] DateTime dataFim)
    {
        try
        {
            var hospedagens = await _hospedagemService.ListarHospedagensPorPeriodo(dataInicio, dataFim);
            return Ok(hospedagens);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno: {ex.Message}");
        }
    }

    // Atualizar uma hospedagem existente
    [HttpPut("atualizar/{id}")]
    public async Task<IActionResult> AtualizarHospedagem(Guid id, [FromBody] AlterarHospedagemViewModel dados)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _hospedagemService.AtualizarHospedagem(id, dados);
            if (result != null)
                return Ok(result);

            return NotFound("Hospedagem não encontrada.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno: {ex.Message}");
        }
    }

    // Deletar uma hospedagem
    [HttpDelete("deletar/{id}")]
    public async Task<IActionResult> DeletarHospedagem(Guid id)
    {
        try
        {
            var sucesso = await _hospedagemService.DeletarHospedagem(id);
            if (sucesso)
                return Ok("Hospedagem deletada com sucesso.");

            return NotFound("Hospedagem não encontrada.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno: {ex.Message}");
        }
    }
}
