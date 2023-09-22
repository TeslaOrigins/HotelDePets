using HDP.Application.Exceptions;
using HDP.Application.Services.Contracts;
using HDP.Application.ViewModels.Pet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HDP.API.Controllers;

[ApiController]
[Route("Pet")]
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
    public async Task<IActionResult> GetPetPorId(int id)
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
    [HttpPost]
    public async Task<IActionResult> PostPet(CadastroPetViewModel pet)
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
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPet(AtualizaPetViewModel Pet)
    {
        try
        {
            var pets = await _petService.AtualizaPet(Pet);
            if(pets == null)
                return Problem("Não foi possível atualizar o cadastro deste Pet");
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
    [HttpDelete("{idPet}")]
    public async Task<IActionResult> DeletePet(int idPet)
    {
        try
        {
            var pets = await _petService.RemovePet(idPet);
            if(!pets)
                return Problem("Não foi possível atualizar o cadastro deste Pet");
            
            return Ok("Pet removido com sucesso");
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