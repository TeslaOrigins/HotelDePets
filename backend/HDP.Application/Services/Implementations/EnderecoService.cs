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
    private readonly IEnderecoRepository _enderecoRepository;
    private readonly ITutorRepository _tutorRepository;
    private readonly IMapper _mapper;
    
    public EnderecoService(IMapper mapper, IEnderecoRepository enderecoRepository, ITutorRepository tutorRepository)
    {
        _mapper = mapper;
        _enderecoRepository = enderecoRepository;
        _tutorRepository = tutorRepository;
    }
    
    public async Task<EnderecoViewModel[]> GetEnderecos()
    {
        try{
            return _mapper.Map<EnderecoViewModel[]>(await _enderecoRepository.GetEnderecos());
        }catch(Exception e){
            throw new Exception(e.Message);
        }
    }

    // public async Task<TipoEnderecoViewModel> CadastrarTipoEndereco(CadastroTipoEnderecoViewModel dados)
    // {
    //     try
    //     {
    //         List<string> errors = new();
    //
    //         var tipoEnderecoExiste = await ValidaTipoEnderecoExistente(dados);
    //
    //         if (tipoEnderecoExiste)
    //         {
    //             errors.Add("Tipo de endereço já existente no sistema");
    //         }
    //
    //         if (errors.Any())
    //             throw new Exception(errors.ToArray().ToString());
    //
    //         var tipoEndereco = _mapper.Map<TipoEndereco>(dados);
    //         
    //         _EnderecoRepository.Add(tipoEndereco);
    //         if (await _EnderecoRepository.SaveChangesAsync())
    //         {
    //             return _mapper.Map<TipoEnderecoViewModel>(await _EnderecoRepository.GetTipoEnderecoPorId(tipoEndereco.Id));
    //         }
    //
    //         return null;
    //     }
    //     catch (BusinessException<TipoEnderecoViewModel> BE)
    //     {
    //         throw new BusinessException<TipoEnderecoViewModel>(BE.messages, BE.obj);
    //     }
    //     catch (NotFoundException NFE)
    //     {
    //         throw new NotFoundException(NFE.Message);
    //     }
    //     catch (Exception e)
    //     {
    //         Console.WriteLine(e);
    //         throw;
    //     }
    // }
    
    public async Task<EnderecoViewModel[]> GetEnderecosByTutorId(int idTutor){
        try{
            if( (await _tutorRepository.GetTutorPorId(idTutor)) == null)
                throw new NotFoundException("Tutor não encontrado");
            var enderecos = await _enderecoRepository.GetEnderecosPorTutorId(idTutor);
            return _mapper.Map<EnderecoViewModel[]>(enderecos);
        }
        catch(NotFoundException NFE){
            throw new NotFoundException(NFE.Message);
        }
        catch(Exception e){
            throw new Exception(e.Message);
        }
    }
    
    // private async Task<bool> ValidaTipoEnderecoExistente(CadastroTipoEnderecoViewModel dados)
    // {
    //     try
    //     {
    //         var tipoEndereco = await _enderecoRepository.GetTipoEnderecoPorNome(dados.Name);
    //
    //         return tipoEndereco != null;
    //     }
    //     catch (Exception e)
    //     {
    //         throw new Exception(e.Message);
    //     }
    // }
}