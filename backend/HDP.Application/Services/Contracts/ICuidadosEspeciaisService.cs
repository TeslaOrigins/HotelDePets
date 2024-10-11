using HDP.Application.ViewModels.CuidadosEspeciais;
using HDP.Persistence.Repository.Contracts;

namespace HDP.Application.Services.Contracts;

public interface ICuidadosEspeciaisService
{
    Task<CuidadosEspeciaisViewModel> CadastrarCuidadosEspeciais(CadastroCuidadosEspeciaisViewModel dados);
    Task<CuidadosEspeciaisViewModel> GetCuidadosEspeciaisPorId(Guid id);
    Task<IEnumerable<CuidadosEspeciaisViewModel>> ListarCuidadosEspeciais();
    Task<CuidadosEspeciaisViewModel> AlterarCuidadosEspeciais(Guid id, AlterarCuidadosEspeciaisViewModel dados);
    Task<bool> DeletarCuidadosEspeciais(Guid id);
}