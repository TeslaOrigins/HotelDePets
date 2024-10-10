using HDP.Application.Exceptions;
using HDP.Application.Services.Contracts;
using HDP.Application.ViewModels.Pet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HDP.API.Controllers;

[ApiController]
[Route("pet")]
public class PetController : ControllerBase
{
    private readonly IPetService _petService;
    
    public PetController(IPetService petService){
        _petService = petService;
    }
    
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetPet()
    {
        try
        {
            return Ok(await _petService.GetPet());
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
    
    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPetPorId(Guid id)
    {
        try
        {
            var pet = await _petService.GetPetPorId(id);
         
            return Ok(pet);
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
    [Route("cadastrar")]
    [HttpPost]
    public async Task<IActionResult> CadastrarPet(CadastroPetViewModel pet)
    {
        try
        {
            var pets = await _petService.CadastrarPet(pet);
            if(pets == null)
                return Problem("Não foi possível finalizar o cadastro deste Pet");
            return Ok(pets);
        } 
        catch(BusinessException<PetViewModel> BE){
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
    public async Task<IActionResult> AlterarPet(Guid id,AlterarPetViewModel Pet)
    {
        try
        {
            var pets = await _petService.AlterarPet(Pet,id);
            if(pets == null)
                return Problem("Não foi possível alterar o pet especificado");
            return Ok(pets);
        } 
        catch(BusinessException<PetViewModel> BE){
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