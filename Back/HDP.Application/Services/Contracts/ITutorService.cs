using HDP.Application.ViewModels;
using HDP.Application.ViewModels.Tutor;

namespace HDP.Application.Services.Contracts;

public interface ITutorService
{
    Task<TutorViewModel[]> GetTutor();
    Task<TutorViewModel> GetTutorPorId(Guid idTutor);
    Task<TutorViewModel> CadastrarTutor(CadastroTutorViewModel dados);
    Task<TutorViewModel> AtualizaTutor(AtualizaTutorViewModel dados);
    Task<bool> RemoveTutor(Guid idTutor);
}