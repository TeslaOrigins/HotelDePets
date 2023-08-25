using HDP.Application.ViewModels.Tutor;

namespace HDP.Application.Services.Contracts;

public interface ITutorService
{
    Task<TutorViewModel[]> GetTutor();
    Task<TutorViewModel> GetTutorPorId(int idTutor);
    Task<TutorViewModel> CadastrarTutor(CadastroTutorViewModel dados);
    Task<TutorViewModel> AtualizaTutor(AtualizaTutorViewModel dados);
    Task<bool> RemoveTutor(int idTutor);
}