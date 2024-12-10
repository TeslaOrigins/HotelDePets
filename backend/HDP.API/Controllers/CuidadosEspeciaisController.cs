using HDP.Application.Services.Contracts;
using HDP.Application.ViewModels.CuidadosEspeciais;
using Microsoft.AspNetCore.Mvc;

namespace HDP.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CuidadosEspeciaisController : ControllerBase
{
    private readonly ICuidadosEspeciaisService _cuidadosEspeciaisService;

    public CuidadosEspeciaisController(ICuidadosEspeciaisService cuidadosEspeciaisService)
    {
        _cuidadosEspeciaisService = cuidadosEspeciaisService;
    }

    [HttpPost("cadastrar")]
    public async Task<IActionResult> CadastrarCuidadosEspeciais([FromBody] CadastroCuidadosEspeciaisViewModel dados)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _cuidadosEspeciaisService.CadastrarCuidadosEspeciais(dados);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCuidadosEspeciaisPorId(Guid id)
    {
        try
        {
            var result = await _cuidadosEspeciaisService.GetCuidadosEspeciaisPorId(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno: {ex.Message}");
        }
    }

    [HttpGet("listar")]
    public async Task<IActionResult> ListarCuidadosEspeciais()
    {
        try
        {
            var result = await _cuidadosEspeciaisService.ListarCuidadosEspeciais();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno: {ex.Message}");
        }
    }

    [HttpPut("alterar/{id}")]
    public async Task<IActionResult> AlterarCuidadosEspeciais(Guid id, [FromBody] AlterarCuidadosEspeciaisViewModel dados)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _cuidadosEspeciaisService.AlterarCuidadosEspeciais(id, dados);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno: {ex.Message}");
        }
    }

    [HttpDelete("deletar/{id}")]
    public async Task<IActionResult> DeletarCuidadosEspeciais(Guid id)
    {
        try
        {
            var sucesso = await _cuidadosEspeciaisService.DeletarCuidadosEspeciais(id);
            if (sucesso)
                return Ok("Cuidados especiais deletados com sucesso.");

            return NotFound("Cuidados especiais não encontrados.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno: {ex.Message}");
        }
    }
}
