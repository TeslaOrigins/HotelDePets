using AutoMapper;
using HDP.Application.Exceptions;
using HDP.Application.Services.Contracts;
using HDP.Application.ViewModels.Endereco;
using HDP.Application.ViewModels.Tutor;
using HDP.Domain.Models;
using HDP.Persistence;
using HDP.Persistence.Repository.Contracts;

namespace HDP.Application.Services.Implementations;

public class TutorService : ITutorService
{
    private readonly ITutorRepository _tutorRepository;
    private readonly IEnderecoRepository _enderecoRepository;
    private readonly IMapper _mapper;
    
    public TutorService(IMapper mapper, ITutorRepository tutorRepository, IEnderecoRepository enderecoRepository){
        _tutorRepository = tutorRepository;
        _mapper = mapper;
        _enderecoRepository = enderecoRepository;
    }
    
    public async Task<TutorViewModel[]> GetTutor()
    {
        try{
            return _mapper.Map<TutorViewModel[]>(await _tutorRepository.GetTutor());
        }catch(Exception e){
            throw new Exception(e.Message);
        }
    }
    
    public async Task<TutorViewModel> GetTutorPorId(int idTutor)
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
            var enderecoExiste = await ValidaEnderecoExistente(dados.Enderecos);
            
            if (tutorExiste)
            {
                errors.Add("Tutor já existe no sistema");
            }

            if (enderecoExiste != null)
            {
                foreach (var enderecoExistente in enderecoExiste)
                {
                    if (enderecoExistente.Existe)
                        errors.Add(
                            "O Endereço de logradouro: " + enderecoExistente.Logradouro + " já está cadastrado no sistema");
                }
            }

            if (errors.Any())
                throw new Exception(errors.ToArray().ToString());

            var tutor = _mapper.Map<Tutor>(dados);
            
            tutor.NomeNormalizado = dados.Nome.ToUpper().Trim();
            
            _tutorRepository.Add(tutor);
            if (await _tutorRepository.SaveChangesAsync())
            {
                return _mapper.Map<TutorViewModel>(await _tutorRepository.GetTutorPorId(tutor.TutorId));
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
    
    public async Task<TutorViewModel> AtualizaTutor(AtualizaTutorViewModel dados)
    {
        try
        {
            var tutorDomain = await GetTutorPorIdAnotherService(dados.TutorId);

            tutorDomain.Cpf = dados.Cpf;
            tutorDomain.Nome = dados.Nome;
            tutorDomain.NomeNormalizado = dados.Nome.ToUpper();
            tutorDomain.Email = dados.Email;
            tutorDomain.Telefone = dados.Telefone;
            if (await _tutorRepository.SaveChangesAsync())
            {
                return _mapper.Map<TutorViewModel>(await _tutorRepository.GetTutorPorId(tutorDomain.TutorId));
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
    
    public async Task<bool> RemoveTutor(int idTutor)
    {
        try
        {
            var tutorDomain = await GetTutorPorIdAnotherService(idTutor);
            
            _tutorRepository.Delete(tutorDomain);
            
            return await _tutorRepository.SaveChangesAsync();
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
            var tutor = await GetTutorPorNomeAnotherService(dados.Nome.ToUpper().Trim());

            return tutor != null;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    
    private async Task<List<EnderecoExistenteViewModel>> ValidaEnderecoExistente(ICollection<CadastroEnderecoViewModel> dados)
    {
        try
        {
            List<EnderecoExistenteViewModel> enderecoExistente = new();

            foreach (var endereco in dados)
            {
                var truck = await _enderecoRepository.GetEnderecoPorLogradouro(endereco.Logradouro);

                if (truck != null)
                {
                    EnderecoExistenteViewModel novoEnderecoExistente = new EnderecoExistenteViewModel()
                    {
                        Existe = true,
                        Logradouro = truck.Logradouro
                    };
                    enderecoExistente.Add(novoEnderecoExistente);
                }
            }

            return enderecoExistente;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    // private async Task<TipoEndereco> GetTipoEnderecoAnotherService(int idTipoEndereco)
    // {
    //     var tiposEnderecos = await _EnderecoRepository.GetTipoEnderecoPorId(idTipoEndereco);
    //
    //     if (tiposEnderecos != null)
    //     {
    //         return tiposEnderecos;
    //     }
    //
    //     return null;
    // }
    
    private async Task<Tutor> GetTutorPorNomeAnotherService(string nomeTutor)
    {
        var tutor = await _tutorRepository.GetTutorPorNomeNormalizado(nomeTutor);
    
        if (tutor != null)
        {
            return tutor;
        }
    
        return null;
    }
    
    private async Task<Tutor> GetTutorPorIdAnotherService(int idTutor)
    {
        var tutor = await _tutorRepository.GetTutorPorId(idTutor);

        if (tutor != null)
        {
            return tutor;
        }

        return null;
    }
    
    private async Task<Endereco[]> GetEnderecosAnotherService(int idTutor)
    {
        var tutor = await _enderecoRepository.GetEnderecosPorTutorId(idTutor);

        if (tutor != null)
        {
            return tutor;
        }

        return null;
    }
}