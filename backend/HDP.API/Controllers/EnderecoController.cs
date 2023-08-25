using HDP.Application.Services.Contracts;
using HDP.Application.ViewModels.Endereco;

namespace HDP.API.Controllers;

[ApiController]
[Route("Endereco")]
public class EnderecoController : ControllerBase
{
    // private readonly IEnderecoService _enderecoService;
    
    // public TutorController(ITutorService enderecoService){
    //     _enderecoService = enderecoService;
    // }
    
    // [AllowAnonymous]
    // [HttpGet]
    // public async Task<IActionResult> GetTutor()
    // {
    //     try
    //     {
    //         return Ok(await _enderecoService.GetTutor());
    //     }
    //     catch (Exception e)
    //     {
    //         return Problem(e.Message);
    //     }
    // }
}