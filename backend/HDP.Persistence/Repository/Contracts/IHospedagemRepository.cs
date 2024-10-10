using HDP.Domain.Models;

namespace HDP.Persistence.Repository.Contracts;

public interface IHospedagemRepository : IGeneralRepository
{
    Task<Hospedagem?> GetHospedagemPorId(Guid idHospedagem, bool incluirPet, bool incluirServicos, bool incluirDietas, bool incluirCuidadosEspeciais);
    Task<IEnumerable<Hospedagem>> ListarHospedagens(bool incluirPet, bool incluirServicos, bool incluirDietas, bool incluirCuidadosEspeciais);

    Task<IEnumerable<Hospedagem>> ListarHospedagensPorStatus(string status, bool incluirPet, bool incluirServicos,
        bool incluirDietas, bool incluirCuidadosEspeciais);

    Task<IEnumerable<Hospedagem>> ListarHospedagensPorPeriodo(DateTime dataInicio, DateTime dataFim, bool incluirPet,
        bool incluirServicos, bool incluirDietas, bool incluirCuidadosEspeciais);
}