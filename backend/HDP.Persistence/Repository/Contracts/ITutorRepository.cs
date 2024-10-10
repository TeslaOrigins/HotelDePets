using HDP.Domain.Models;

namespace HDP.Persistence.Repository.Contracts;

public interface ITutorRepository : IGeneralRepository
{
    Task<Tutor[]> GetTutores();
    Task<Tutor> GetTutorPorId(Guid idTutor,bool pets = false,bool hospedagens =false);
    Task<Tutor> GetTutorPorNome(string nomeTutor);
    Task<Tutor> GetTutorPorNomeNormalizado(string NomeNormalizadoTutor);
}