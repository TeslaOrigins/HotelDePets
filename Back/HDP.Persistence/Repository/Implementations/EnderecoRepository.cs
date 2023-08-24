using HDP.Domain.Models;
using HDP.Domain.Models.Constantes;
using HDP.Persistence.Contexts;
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
    
    public async Task<Endereco[]> GetCaminhoes()
    {
        var main_query = from Endereco in _context.Caminhoes
            select Endereco;
        return await main_query.ToArrayAsync();    
    }

    public async Task<Endereco> GetEnderecoPorPlaca(String placaEndereco)
    {
        var main_query = from Endereco in _context.Caminhoes
            where Endereco.PlacaEndereco == placaEndereco
            select Endereco;
        
        return await main_query.FirstOrDefaultAsync();   
    }
    
    public async Task<Endereco> GetEnderecoPorId(Guid idEndereco)
    {
        var main_query = from Endereco in _context.Caminhoes
            where Endereco.Id == idEndereco
            select Endereco;
        
        return await main_query.FirstOrDefaultAsync();   
    }
    
    public async Task<Endereco[]> GetCaminhoesPorClienteId(Guid clienteId)
    {
        var main_query = from Endereco in _context.Caminhoes
            where Endereco.ClienteId == clienteId
            select Endereco;
        
        return await main_query.ToArrayAsync();   
    }
    
    public async Task<TipoEndereco> GetTipoEnderecoPorNome(String nomeTipoEndereco)
    {
        var main_query = from Endereco in _context.Caminhoes
            where Endereco.TipoEndereco.Name == nomeTipoEndereco
            select Endereco.TipoEndereco;
        
        return await main_query.FirstOrDefaultAsync();   
    }
    
    public async Task<TipoEndereco> GetTipoEnderecoPorId(Guid idTipoEndereco)
    {
        var main_query = from tipoEndereco in _context.TiposCaminhoes
            where tipoEndereco.Id == idTipoEndereco
            select tipoEndereco;
        
        return await main_query.FirstOrDefaultAsync();   
    }
}