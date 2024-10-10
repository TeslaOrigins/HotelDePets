using HDP.Application.ViewModels.Hospedagem;
using HDP.Application.ViewModels.Tutor;

namespace HDP.Application.Services.Contracts;

public interface IHospedagemService
{
    Task<IEnumerable<HospedagemViewModel>> ListarHospedagens();
    Task<HospedagemViewModel> GetHospedagemPorId(Guid idHospedagem);
    Task<HospedagemViewModel> CadastrarHospedagem(CadastroHospedagemViewModel dados);
    Task<HospedagemViewModel> CancelarHospedagem(Guid idHospedagem);
    Task<IEnumerable<HospedagemViewModel>> ListarHospedagensPorStatus(string status);
    Task<IEnumerable<HospedagemViewModel>> ListarHospedagensPorPeriodo(DateTime dataInicio, DateTime dataFim);
    Task<HospedagemViewModel> AtualizarHospedagem(Guid id, AlterarHospedagemViewModel dados);
    Task<bool> DeletarHospedagem(Guid id);
}