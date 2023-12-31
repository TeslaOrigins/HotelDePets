using HDP.Domain.Models;
using HDP.Domain.Models.Constantes;

namespace HDP.Persistence.Repository.Contracts;

public interface IEnderecoRepository : IGeneralRepository
{
    Task<Endereco[]> GetEnderecos();
    Task<Endereco> GetEnderecoPorLogradouro(String logradouroEndereco);
    Task<Endereco> GetEnderecoPorId(int idEndereco);
    Task<Endereco[]> GetEnderecosPorTutorId(int TutorId);
    // Task<TipoEndereco> GetTipoEnderecoPorNome(String nomeTipoEndereco);
    // Task<TipoEndereco> GetTipoEnderecoPorId(int idTipoEndereco);
}