using AutoMapper;
using HDP.Application.Exceptions;
using HDP.Application.Services.Contracts;
using HDP.Application.ViewModels.CuidadosEspeciais;
using HDP.Domain.Models;
using HDP.Persistence.Repository.Contracts;

namespace HDP.Application.Services.Implementations;

public class CuidadosEspeciaisService : ICuidadosEspeciaisService
{
    private readonly IMapper _mapper;
    private readonly ICuidadosEspeciaisRepository _cuidadosEspeciaisRepository;

    public CuidadosEspeciaisService(
        IMapper mapper, 
        ICuidadosEspeciaisRepository cuidadosEspeciaisRepository)
    {
        _mapper = mapper;
        _cuidadosEspeciaisRepository = cuidadosEspeciaisRepository;
    }

    public async Task<CuidadosEspeciaisViewModel> CadastrarCuidadosEspeciais(CadastroCuidadosEspeciaisViewModel dados)
    {
        var cuidadosEspeciais = _mapper.Map<CuidadosEspeciais>(dados);
        _cuidadosEspeciaisRepository.Add(cuidadosEspeciais);

        if (await _cuidadosEspeciaisRepository.SaveChangesAsync())
        {
            return _mapper.Map<CuidadosEspeciaisViewModel>(cuidadosEspeciais);
        }

        throw new Exception("Erro ao cadastrar cuidados especiais.");
    }

    public async Task<CuidadosEspeciaisViewModel> GetCuidadosEspeciaisPorId(Guid id)
    {
        var cuidadosEspeciais = await _cuidadosEspeciaisRepository.GetCuidadosEspeciaisPorId(id);
        if (cuidadosEspeciais == null)
            throw new NotFoundException("Cuidados especiais não encontrados.");

        return _mapper.Map<CuidadosEspeciaisViewModel>(cuidadosEspeciais);
    }

    public async Task<IEnumerable<CuidadosEspeciaisViewModel>> ListarCuidadosEspeciais()
    {
        var cuidadosEspeciais = await _cuidadosEspeciaisRepository.ListarCuidadosEspeciais();
        return _mapper.Map<IEnumerable<CuidadosEspeciaisViewModel>>(cuidadosEspeciais);
    }

    public async Task<CuidadosEspeciaisViewModel> AlterarCuidadosEspeciais(Guid id, AlterarCuidadosEspeciaisViewModel dados)
    {
        var cuidadosEspeciais = await _cuidadosEspeciaisRepository.GetCuidadosEspeciaisPorId(id);
        if (cuidadosEspeciais == null)
            throw new NotFoundException("Cuidados especiais não encontrados.");

        _mapper.Map(dados, cuidadosEspeciais);
        _cuidadosEspeciaisRepository.Update(cuidadosEspeciais);

        if (await _cuidadosEspeciaisRepository.SaveChangesAsync())
        {
            return _mapper.Map<CuidadosEspeciaisViewModel>(cuidadosEspeciais);
        }

        throw new Exception("Erro ao atualizar cuidados especiais.");
    }

    public async Task<bool> DeletarCuidadosEspeciais(Guid id)
    {
        var cuidadosEspeciais = await _cuidadosEspeciaisRepository.GetCuidadosEspeciaisPorId(id);
        if (cuidadosEspeciais == null)
            throw new NotFoundException("Cuidados especiais não encontrados.");

        _cuidadosEspeciaisRepository.Delete(cuidadosEspeciais);
        return await _cuidadosEspeciaisRepository.SaveChangesAsync();
    }
}
