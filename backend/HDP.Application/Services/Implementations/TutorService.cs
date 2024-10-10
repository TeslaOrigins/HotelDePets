using AutoMapper;
using HDP.Application.Exceptions;
using HDP.Application.Services.Contracts;

using HDP.Application.ViewModels.Tutor;
using HDP.Domain.Models;
using HDP.Persistence;
using HDP.Persistence.Repository.Contracts;

namespace HDP.Application.Services.Implementations;

public class TutorService : ITutorService
{
    private readonly ITutorRepository _tutorRepository;

    private readonly IMapper _mapper;
    
    public TutorService(IMapper mapper, ITutorRepository tutorRepository){
        _tutorRepository = tutorRepository;
        _mapper = mapper;

    }
    
    public async Task<TutorViewModel[]> GetTutores()
    {
        try{
            return _mapper.Map<TutorViewModel[]>(await _tutorRepository.GetTutores());
        }catch(Exception e){
            throw new Exception(e.Message);
        }
    }
    
    public async Task<TutorViewModel> GetTutorPorId(Guid idTutor)
    {
        try{
            var tutor = await _tutorRepository.GetTutorPorId(idTutor);
            if(tutor == null)
                throw new NotFoundException("Não foi encontrado um Tutor com o id especificado");
            return _mapper.Map<TutorViewModel>(tutor);
        }
        catch(NotFoundException NFE)
        {
            throw new NotFoundException(NFE.Message);
        }
        catch(Exception e){
            throw new Exception(e.Message);
        }
    }
    
    public async Task<TutorViewModel> CadastrarTutor(CadastroTutorViewModel dados)
    {
        try
        {
            List<string> errors = new();

            var tutorExiste = await ValidaTutorExistente(dados);

            
            if (tutorExiste)
            {
                errors.Add("Tutor já existe no sistema");
            }


            if (errors.Any())
                throw new Exception(errors.ToArray().ToString());

            var tutor = _mapper.Map<Tutor>(dados);
            
            tutor.Nome = dados.Nome.ToUpper().Trim();
            tutor.Ativo = true;
            
            _tutorRepository.Add(tutor);
            if (await _tutorRepository.SaveChangesAsync())
            {
                return _mapper.Map<TutorViewModel>(await _tutorRepository.GetTutorPorId(tutor.Tutorid));
            }

            return null;
        }
        catch (BusinessException<TutorViewModel> BE)
        {
            throw new BusinessException<TutorViewModel>(BE.messages, BE.obj);
        }
        catch (NotFoundException NFE)
        {
            throw new NotFoundException(NFE.Message);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    
    public async Task<TutorViewModel> AlterarTutor(AlterarTutorViewModel dados,Guid Tutorid)
    {
        try
        {
            var tutorDomain = await _tutorRepository.GetTutorPorId(Tutorid);

            tutorDomain.Cpf = dados.Cpf;
            tutorDomain.Nome = dados.Nome.ToUpper().Trim();
            tutorDomain.Datanascimento =  DateOnly.FromDateTime(dados.Datanascimento.Value);
            tutorDomain.Bairro = dados.Bairro;
            tutorDomain.Numero = dados.Numero;
            tutorDomain.Cep = dados.Cep;
            tutorDomain.Rua = dados.Rua;
            tutorDomain.Email = dados.Email;
            tutorDomain.Telefone = dados.Telefone;
            if (await _tutorRepository.SaveChangesAsync())
            {
                return _mapper.Map<TutorViewModel>(await _tutorRepository.GetTutorPorId(tutorDomain.Tutorid));
            }

            return null;
        }
        catch (BusinessException<TutorViewModel> BE)
        {
            throw new BusinessException<TutorViewModel>(BE.messages, BE.obj);
        }
        catch (NotFoundException NFE)
        {
            throw new NotFoundException(NFE.Message);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    
    public async Task<bool> RemoverTutor(Guid idTutor)
    {
        try
        {
            var tutorDomain = await _tutorRepository.GetTutorPorId(idTutor);

            if (tutorDomain == null)
            {
                throw new NotFoundException("O tutor especificado não existe no sistema");
            }

            // se todos os pets não tem hospedagem registrada ele deleta.
            if(tutorDomain.Pets.All(p => !p.Hospedagens.Any()))
                _tutorRepository.Delete(tutorDomain);
            else
                throw new BusinessException<TutorViewModel>(new string[] {"O tutor possui um pet com hospedagem registrada, não é possivel remove-lo do sistema."}, _mapper.Map<TutorViewModel>(tutorDomain));

            if (await _tutorRepository.SaveChangesAsync())
                return true;
            
            return false;
            
        }
        catch (BusinessException<TutorViewModel> BE)
        {
            throw new BusinessException<TutorViewModel>(BE.messages, BE.obj);
        }
        catch (NotFoundException NFE)
        {
            throw new NotFoundException(NFE.Message);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    private async Task<bool> ValidaTutorExistente(CadastroTutorViewModel dados)
    {
        try
        {
            var tutor = await _tutorRepository.GetTutorPorNomeNormalizado(dados.Nome.ToUpper().Trim());

            return tutor != null;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<TutorViewModel> InativarReativarTutor( Guid Tutorid)
    {
        try
        {
            var tutorDomain = await _tutorRepository.GetTutorPorId(Tutorid);

            if (tutorDomain == null)
            {
                throw new NotFoundException("O tutor especificado não existe no sistema");
            }

            // se todos os pets não tem hospedagem registrada ele inativa.
            if(tutorDomain.Pets.All(p => !p.Hospedagens.Any()))
                tutorDomain.Ativo = !tutorDomain.Ativo;
            else
                throw new BusinessException<TutorViewModel>(new string[] {"O tutor possui um pet com hospedagem registrada, não é possivel inativa-lo no sistema."}, _mapper.Map<TutorViewModel>(tutorDomain));
            await _tutorRepository.SaveChangesAsync();

            return _mapper.Map<TutorViewModel>(tutorDomain);
            
        }
        catch (BusinessException<TutorViewModel> BE)
        {
            throw new BusinessException<TutorViewModel>(BE.messages, BE.obj);
        }
        catch (NotFoundException NFE)
        {
            throw new NotFoundException(NFE.Message);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}