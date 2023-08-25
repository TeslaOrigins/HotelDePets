using HDP.Domain.Models;

namespace HDP.Persistence.Repository.Contracts;

public interface ITutorRepository : IGeneralRepository
{
    Task<Tutor[]> GetTutor();
    Task<Tutor> GetTutorPorId(int idTutor);
    Task<Tutor> GetTutorPorNome(string nomeTutor);
    Task<Tutor> GetTutorPorNomeNormalizado(string NomeNormalizadoTutor);
}