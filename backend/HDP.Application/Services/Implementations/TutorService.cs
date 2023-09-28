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

            // foreach (var enderecoExistente in enderecoExiste)
            // {
            //     if (enderecoExistente.Existe != null)
            //         enderecoExistente.Apagar(enderecoExistente);
            //     // errors.Add(
            //     //     "O Endereço de logradouro: " + enderecoExistente.Logradouro + " já está cadastrado no sistema");
            // }

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
    
    public async Task<TutorViewModel> AlterarTutor(AlterarTutorViewModel dados)
    {
        try
        {
            List<int> enderecosId = new List<int>();
            foreach (var endereco in dados.Enderecos)
            {
                var endDomain = await GetEnderecoPorIdAnotherService(endereco.Id);

                if (endDomain != null)
                {
                    endDomain.Logradouro = endereco.Logradouro;
                    endDomain.Numero = (int) endereco.Numero;
                    endDomain.Cidade = endereco.Cidade;
                    endDomain.Estado = endereco.Estado;   
                    
                    _enderecoRepository.Add(endDomain);
                } else {
                    enderecosId.Add(endereco.Id);
                }
            }

            Endereco[] enderecos = _mapper.Map<Endereco[]>(GetEnderecosOldAnotherService(dados.TutorId, enderecosId));
            
            foreach (var end in enderecos)
            {
                _enderecoRepository.Delete(end);
            }

            if (await _enderecoRepository.SaveChangesAsync()){
                var tutorDomain = await GetTutorPorIdAnotherService(dados.TutorId);

                tutorDomain.Cpf = dados.Cpf;

                if (await _tutorRepository.SaveChangesAsync())
                {
                    return _mapper.Map<TutorViewModel>(await _tutorRepository.GetTutorPorId(tutorDomain.TutorId));
                }
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
    
    public async Task<Tutor> ApagarTutor(int idTutor)
    {
        try
        {
            var tutorDomain = await GetTutorPorIdAnotherService(idTutor);
            
            _tutorRepository.Delete(tutorDomain);

            if (await _tutorRepository.SaveChangesAsync())
                return tutorDomain;
            
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
    
    private async Task<ICollection<CadastroEnderecoViewModel>> ValidaEnderecoExistente(ICollection<CadastroEnderecoViewModel> dados)
    {
        try
        {
            List<EnderecoExistenteViewModel> enderecoExistente = new();

            foreach (var endereco in dados)
            {
                var end = await _enderecoRepository.GetEnderecoPorLogradouro(endereco.Logradouro);

                if (end != null)
                {
                    endereco.Existe = true;
                }
            }

            return dados;
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
    
    private async Task<Endereco> GetEnderecoPorIdAnotherService(int idEndereco)
    {
        var tutor = await _enderecoRepository.GetEnderecoPorId(idEndereco);

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
    
    private async Task<Endereco[]> GetEnderecosOldAnotherService(int idTutor, List<int> enderecosId)
    {
        var enderecos = await _enderecoRepository.GetEnderecosOldPorEnderecoId(idTutor, enderecosId);

        if (enderecos != null)
        {
            return enderecos;
        }

        return null;
    }
}