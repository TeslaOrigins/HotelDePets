using HDP.Domain.Models;
using HDP.Domain.Models.Constantes;

namespace HDP.Persistence.Repository.Contracts;

public interface IEnderecoRepository : IGeneralRepository
{
    Task<Endereco[]> GetEnderecos();
    Task<Endereco> GetEnderecoPorPlaca(String placaEndereco);
    Task<Endereco> GetEnderecoPorId(Guid idEndereco);
    Task<Endereco[]> GetEnderecosPorClienteId(Guid clienteId);
    Task<TipoEndereco> GetTipoEnderecoPorNome(String nomeTipoEndereco);
    Task<TipoEndereco> GetTipoEnderecoPorId(Guid idTipoEndereco);
}