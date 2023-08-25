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
    [HttpGet]
    public async Task<IActionResult> GetTutor()
    {
        try
        {
            return Ok(await _tutorService.GetTutor());
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
    
    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTutorPorId(int id)
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
    public async Task<IActionResult> PostTutor(CadastroTutorViewModel tutor)
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
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTutor(AtualizaTutorViewModel Tutor)
    {
        try
        {
            var tutors = await _tutorService.AtualizaTutor(Tutor);
            if(tutors == null)
                return Problem("Não foi possível atualizar o cadastro deste Tutor");
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
    public async Task<IActionResult> DeleteTutor(int idTutor)
    {
        try
        {
            var tutors = await _tutorService.RemoveTutor(idTutor);
            if(!tutors)
                return Problem("Não foi possível atualizar o cadastro deste Tutor");
            
            return Ok("Tutor removido com sucesso");
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