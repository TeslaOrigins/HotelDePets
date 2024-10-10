using HDP.Application.ViewModels.Tutor;
using HDP.Persistence;

namespace HDP.Application.Services.Contracts;

public interface ITutorService
{
    Task<TutorViewModel[]> GetTutores();
    Task<TutorViewModel> GetTutorPorId(Guid idTutor);
    Task<TutorViewModel> CadastrarTutor(CadastroTutorViewModel dados);
    Task<TutorViewModel> AlterarTutor(AlterarTutorViewModel dados,Guid Tutorid);
    Task<TutorViewModel> InativarReativarTutor(Guid Tutorid);
    Task<bool> RemoverTutor(Guid idTutor);
}