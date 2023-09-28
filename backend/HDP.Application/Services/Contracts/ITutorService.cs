using HDP.Application.ViewModels.Tutor;
using HDP.Persistence;

namespace HDP.Application.Services.Contracts;

public interface ITutorService
{
    Task<TutorViewModel[]> GetTutor();
    Task<TutorViewModel> GetTutorPorId(int idTutor);
    Task<TutorViewModel> CadastrarTutor(CadastroTutorViewModel dados);
    Task<TutorViewModel> AlterarTutor(AlterarTutorViewModel dados);
    Task<Tutor> ApagarTutor(int idTutor);
}