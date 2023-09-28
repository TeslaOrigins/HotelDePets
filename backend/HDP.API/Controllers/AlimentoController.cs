using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HDP.Application.Exceptions;
using HDP.Application.Services.Contracts;
using HDP.Application.ViewModels.Alimento;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HDP.API.Controllers
{
    [ApiController]
    [Route("Pet")]
    public class AlimentoController: ControllerBase
    {
         private readonly IAlimentoService _AlimentoService;
    
    public AlimentoController(IAlimentoService AlimentoService){
        _AlimentoService = AlimentoService;
    }
    
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAlimento()
    {
        try
        {
            return Ok(await _AlimentoService.GetAlimento());
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
    
    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAlimentoPorId(int id)
    {
        try
        {
            var alimento = await _AlimentoService.GetAlimentoPorId(id);
         
            return Ok(alimento);
        }
        catch(NotFoundException NFE){
            return NotFound(NFE.Message);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
    
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> CadastrarAlimento(CadastroAlimentoViewModel alimento)
    {
        try
        {
            var pets = await _AlimentoService.CadastrarAlimento(alimento);
            if(pets == null)
                return Problem("Não foi possível finalizar o cadastro do alimento");
            return Ok(pets);
        } 
        catch(BusinessException<CadastroAlimentoViewModel> BE){
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
    [HttpPut("{id}")]
    public async Task<IActionResult> AlterarAlimento(AlterarAlimentoViewModel alimento)
    {
        try
        {
            var pets = await _AlimentoService.AlterarAlimento(alimento);
            if(pets == null)
                return Problem("Não foi possível Alterar o cadastro deste Pet");
            return Ok(pets);
        } 
        catch(BusinessException<AlimentoViewModel> BE){
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
    [HttpDelete("{idAlimento}")]
    public async Task<IActionResult> ApagarAlimento(int idAlimento)
    {
        try
        {
            var pets = await _AlimentoService.ApagarAlimento(idAlimento);
            if(!pets)
                return Problem("Não foi possível deletar o alimento");
            
            return Ok("Alimento removido com sucesso");
        } 
        catch(BusinessException<AlimentoViewModel> BE){
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
}