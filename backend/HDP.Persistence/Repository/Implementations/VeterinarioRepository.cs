using Microsoft.EntityFrameworkCore;

namespace HDP.Persistence.Repository.Implementations;

public class VeterinarioRepository : GeneralRepository
{
    private readonly HDPContext _context;
    public VeterinarioRepository(HDPContext context) : base(context)
    {
        _context = context;
    }
    
    public async Task<Veterinario[]> GetVeterinarios()
    {
        var mainQuery = from veterinario in _context.Veterinario
            select veterinario;
        return await mainQuery.ToArrayAsync();    
    }

    public async Task<Veterinario> GetVeterinarioPorNome(String logradouroVeterinario)
    {
        var mainQuery = from veterinario in _context.Veterinario
            where veterinario.Nome == logradouroVeterinario
            select veterinario;
        
        return await mainQuery.FirstOrDefaultAsync();   
    }
    
    public async Task<Veterinario> GetVeterinarioPorId(int idVeterinario)
    {
        var mainQuery = from veterinario in _context.Veterinario
            where veterinario.VeterinarioId == idVeterinario
            select veterinario;
        
        return await mainQuery.FirstOrDefaultAsync();   
    }
    
    public async Task<Veterinario[]> GetVeterinariosPorPetId(int PetId)
    {
        var mainQuery = from veterinario in _context.Veterinario
            where veterinario.PetId == PetId
            select veterinario;
        
        return await mainQuery.ToArrayAsync();   
    }
    
    // public async Task<TipoVeterinario> GetTipoVeterinarioPorNome(String nomeTipoVeterinario)
    // {
    //     var mainQuery = from Veterinario in _context.Veterinarios
    //         where Veterinario.TipoVeterinario.Name == nomeTipoVeterinario
    //         select Veterinario.TipoVeterinario;
    //     
    //     return await mainQuery.FirstOrDefaultAsync();   
    // }
    //
    // public async Task<TipoVeterinario> GetTipoVeterinarioPorId(int idTipoVeterinario)
    // {
    //     var mainQuery = from tipoVeterinario in _context.TiposVeterinarios
    //         where tipoVeterinario.Id == idTipoVeterinario
    //         select tipoVeterinario;
    //     
    //     return await mainQuery.FirstOrDefaultAsync();   
    // }
}