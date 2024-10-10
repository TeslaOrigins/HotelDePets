using HDP.Application.Exceptions;
using HDP.Application.Services.Contracts;
using HDP.Application.ViewModels.Tutor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HDP.API.Controllers;

[ApiController]
[Route("Tutor")]
public class TutorController : ControllerBase
{
    private readonly ITutorService _tutorService;
    
    public TutorController(ITutorService tutorService){
        _tutorService = tutorService;
    }
    
    [AllowAnonymous]
    [Route("all")]
    [HttpGet]
    public async Task<IActionResult> GetTutores()
    {
        try
        {
            return Ok(await _tutorService.GetTutores());
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
    
    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTutorPorId(Guid id)
    {
        try
        {
            var tutor = await _tutorService.GetTutorPorId(id);
         
            return Ok(tutor);
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
    [Route("cadastrar")]
    public async Task<IActionResult> CadastrarTutor(CadastroTutorViewModel tutor)
    {
        try
        {
            var tutors = await _tutorService.CadastrarTutor(tutor);
            if(tutors == null)
                return Problem("Não foi possível finalizar o cadastro deste Tutor");
            return Ok(tutors);
        } 
        catch(BusinessException<TutorViewModel> BE){
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
    [HttpPut]
    [Route("alterar/{id}")]
    public async Task<IActionResult> AlterarTutor(AlterarTutorViewModel Tutor,Guid id)
    {
        try
        {
            var tutors = await _tutorService.AlterarTutor(Tutor,id);
            
            if(tutors == null)
                return Problem("Não foi possível alterar os dados do tutor especificado");
            
            return Ok(tutors);
        } 
        catch(BusinessException<TutorViewModel> BE){
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
    [HttpPatch]
    [Route("inativar/{id}")]
    public async Task<IActionResult> InativarTutor(Guid id)
    {
        try
        {
            var tutors = await _tutorService.InativarReativarTutor(id);
            
            if(tutors == null)
                return Problem("Não foi possível alterar os dados do tutor especificado");
            
            return Ok(tutors);
        } 
        catch(BusinessException<TutorViewModel> BE){
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
    [HttpDelete("{idTutor}")]
    public async Task<IActionResult> DeleteTutor(Guid idTutor)
    {
        try
        {
            var tutors = await _tutorService.RemoverTutor(idTutor);
            
            if(tutors == null)
                return Problem("Não foi possível remover o tutor especificado");
            
            return Ok(tutors);
        } 
        catch(BusinessException<TutorViewModel> BE){
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