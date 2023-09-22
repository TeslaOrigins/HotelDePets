using HDP.Persistence.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HDP.Persistence.Repository.Implementations;

public class EnderecoRepository : GeneralRepository, IEnderecoRepository
{
    private readonly HDPContext _context;
    public EnderecoRepository(HDPContext context) : base(context)
    {
        _context = context;
    }
    
    public async Task<Endereco[]> GetEnderecos()
    {
        var mainQuery = from endereco in _context.Endereco
            select endereco;
        return await mainQuery.ToArrayAsync();    
    }

    public async Task<Endereco> GetEnderecoPorLogradouro(String logradouroEndereco)
    {
        var mainQuery = from endereco in _context.Endereco
            where endereco.Logradouro == logradouroEndereco
            select endereco;
        
        return await mainQuery.FirstOrDefaultAsync();   
    }
    
    public async Task<Endereco> GetEnderecoPorId(int idEndereco)
    {
        var mainQuery = from endereco in _context.Endereco
            where endereco.EnderecoId == idEndereco
            select endereco;
        
        return await mainQuery.FirstOrDefaultAsync();   
    }
    
    public async Task<Endereco[]> GetEnderecosPorTutorId(int TutorId)
    {
        var mainQuery = from endereco in _context.Endereco
            where endereco.TutorId == TutorId
            select endereco;
        
        return await mainQuery.ToArrayAsync();   
    }
    
    public async Task<Endereco[]> GetEnderecosOldPorEnderecoId(int TutorId, List<int> enderecosId)
    {
        var mainQuery = from e in _context.Endereco
            where e.TutorId == TutorId && !enderecosId.Contains(e.EnderecoId)
            select e;
        
        return await mainQuery.ToArrayAsync();   
    }
    
    // public async Task<TipoEndereco> GetTipoEnderecoPorNome(String nomeTipoEndereco)
    // {
    //     var mainQuery = from Endereco in _context.Enderecos
    //         where Endereco.TipoEndereco.Name == nomeTipoEndereco
    //         select Endereco.TipoEndereco;
    //     
    //     return await mainQuery.FirstOrDefaultAsync();   
    // }
    //
    // public async Task<TipoEndereco> GetTipoEnderecoPorId(int idTipoEndereco)
    // {
    //     var mainQuery = from tipoEndereco in _context.TiposEnderecos
    //         where tipoEndereco.Id == idTipoEndereco
    //         select tipoEndereco;
    //     
    //     return await mainQuery.FirstOrDefaultAsync();   
    // }
}