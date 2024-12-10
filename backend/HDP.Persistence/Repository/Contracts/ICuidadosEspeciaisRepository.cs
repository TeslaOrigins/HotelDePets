using HDP.Domain.Models;

namespace HDP.Persistence.Repository.Contracts;

public interface ICuidadosEspeciaisRepository : IGeneralRepository
{
    Task<CuidadosEspeciais?> GetCuidadosEspeciaisPorId(Guid id);
    Task<IEnumerable<CuidadosEspeciais>> ListarCuidadosEspeciais();
}
