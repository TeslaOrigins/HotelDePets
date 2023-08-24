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
    private readonly ITutorService _TutorService;
    public TutorController(ITutorService TutorService){
        _TutorService = TutorService;
    }
     [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetTutor()
    {
        try
        {
            return Ok(await _TutorService.GetTutor());
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
            var Tutor = await _TutorService.GetTutorPorId(id);
         
            return Ok(Tutor);
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
    public async Task<IActionResult> PostTutor(CadastroTutorViewModel Tutor)
    {
        try
        {
            var Tutors = await _TutorService.CadastrarTutor(Tutor);
            if(Tutors == null)
                return Problem("Não foi possível finalizar o cadastro deste Tutor");
            return Ok(Tutors);
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
            var Tutors = await _TutorService.AtualizaTutor(Tutor);
            if(Tutors == null)
                return Problem("Não foi possível atualizar o cadastro deste Tutor");
            return Ok(Tutors);
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
            var Tutors = await _TutorService.RemoveTutor(idTutor);
            if(!Tutors)
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