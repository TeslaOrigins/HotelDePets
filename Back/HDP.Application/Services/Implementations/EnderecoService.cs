using AutoMapper;
using HDP.Application.Exceptions;
using HDP.Application.Services.Contracts;
using HDP.Application.ViewModels;
using HDP.Application.ViewModels.Endereco;
using HDP.Application.ViewModels.Tutor;
using HDP.Domain.Models;
using HDP.Domain.Models.Constantes;
using HDP.Persistence.Repository.Contracts;

namespace HDP.Application.Services.Implementations;

public class EnderecoService : IEnderecoService
{
    private readonly IEnderecoRepository _EnderecoRepository;
    private readonly ITutorRepository _TutorRepository;
    private readonly IMapper _mapper;
    public EnderecoService(IMapper mapper,IEnderecoRepository EnderecoRepository,ITutorRepository TutorRepository)
    {
        _mapper = mapper;
        _EnderecoRepository = EnderecoRepository;
        _TutorRepository = TutorRepository;
    }
    
    public async Task<EnderecoViewModel[]> GetEnderecos()
    {
        try{
            return _mapper.Map<EnderecoViewModel[]>(await _EnderecoRepository.GetEnderecos());
        }catch(Exception e){
            throw new Exception(e.Message);
        }
    }

    public async Task<TipoEnderecoViewModel> CadastrarTipoEndereco(CadastroTipoEnderecoViewModel dados)
    {
        try
        {
            List<string> errors = new();

            var tipoEnderecoExiste = await ValidaTipoEnderecoExistente(dados);

            if (tipoEnderecoExiste)
            {
                errors.Add("Tipo de endereço já existente no sistema");
            }

            if (errors.Any())
                throw new Exception(errors.ToArray().ToString());

            var tipoEndereco = _mapper.Map<TipoEndereco>(dados);
            
            _EnderecoRepository.Add(tipoEndereco);
            if (await _EnderecoRepository.SaveChangesAsync())
            {
                return _mapper.Map<TipoEnderecoViewModel>(await _EnderecoRepository.GetTipoEnderecoPorId(tipoEndereco.Id));
            }

            return null;
        }
        catch (BusinessException<TipoEnderecoViewModel> BE)
        {
            throw new BusinessException<TipoEnderecoViewModel>(BE.messages, BE.obj);
        }
        catch (NotFoundException NFE)
        {
            throw new NotFoundException(NFE.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    public async Task<EnderecoViewModel[]> GetEnderecosByTutorId(Guid IdTutor){
        try{
            if( (await _TutorRepository.GetTutorPorId(IdTutor)) == null)
                throw new NotFoundException("Tutor não encontrado");
            var Enderecos = await _EnderecoRepository.GetEnderecosPorTutorId(IdTutor);
            return _mapper.Map<EnderecoViewModel[]>(Enderecos);
        }
        catch(NotFoundException NFE){
            throw new NotFoundException(NFE.Message);
        }
        catch(Exception e){
            throw new Exception(e.Message);
        }
    }
    private async Task<bool> ValidaTipoEnderecoExistente(CadastroTipoEnderecoViewModel dados)
    {
        try
        {
            var tipoEndereco = await _EnderecoRepository.GetTipoEnderecoPorNome(dados.Name);

            return tipoEndereco != null;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}