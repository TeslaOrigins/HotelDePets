using HDP.Application.Exceptions;
using HDP.Application.Services.Contracts;
using HDP.Application.ViewModels.Usuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HDP.API.Controllers;
   
[ApiController]
[Route("usuario")]
public class UsuarioController : ControllerBase
{
    
    private readonly IUsuarioService _usuarioService;
    
    public UsuarioController(IUsuarioService UsuarioService){
        _usuarioService = UsuarioService;
    }
    
    [AllowAnonymous]
    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetUsuarios()
    {
        try
        {
            return Ok(await _usuarioService.GetUsuarios());
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
    
  
    
    [AllowAnonymous]
    [Route("cadastrar")]
    [HttpPost]
    public async Task<IActionResult> CadastrarUsuario(CadastrarUsuarioViewModel Usuario)
    {
        try
        {
            var Usuarios = await _usuarioService.CadastrarUsuario(Usuario);
            if(Usuarios == null)
                return Problem("Não foi possível finalizar o cadastro deste Usuario");
            return Ok(Usuarios);
        } 
        catch(BusinessException<UsuarioViewModel> BE){
            return BadRequest(BE.messages);
        } 
        catch(NotFoundException NFE){
            return NotFound(NFE.Message);
        } 
        catch(Exception e ){
            return Problem(e.Message);
        }
    }
    
    [AllowAnonymous]
    [Route("alterar/{id}")]
    [HttpPut]
    public async Task<IActionResult> AlterarUsuario(Guid id,AlterarUsuarioViewModel Usuario)
    {
        try
        {
            var Usuarios = await _usuarioService.AlterarUsuario(Usuario,id);
            if(Usuarios == null)
                return Problem("Não foi possível alterar o Usuario especificado");
            return Ok(Usuarios);
        } 
        catch(BusinessException<UsuarioViewModel> BE){
            return BadRequest(BE.messages);
        } 
        catch(NotFoundException NFE){
            return NotFound(NFE.Message);
        } 
        catch(Exception e ){
            return Problem(e.Message);
        }
    }
    [AllowAnonymous]
    [Route("inativar/{id}")]
    [HttpPatch]
    public async Task<IActionResult> InativarUsuario(Guid id)
    {
        try
        {
            var Usuarios = await _usuarioService.InativarReativarUsuario(id);
            if(Usuarios == null)
                return Problem("Não foi possível inativar o usuario especificado");
            return Ok(Usuarios);
        } 
        catch(BusinessException<UsuarioViewModel> BE){
            return BadRequest(BE.messages);
        } 
        catch(NotFoundException NFE){
            return NotFound(NFE.Message);
        } 
        catch(Exception e ){
            return Problem(e.Message);
        }
    }
  
}