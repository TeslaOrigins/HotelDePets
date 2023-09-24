using Microsoft.EntityFrameworkCore;

namespace HDP.Persistence.Repository.Implementations;

public class DietaRepository : GeneralRepository
{
    private readonly HDPContext _context;
    public DietaRepository(HDPContext context) : base(context)
    {
        _context = context;
    }
    
    public async Task<Dieta[]> GetDietas()
    {
        var mainQuery = from dieta in _context.Dieta
            select dieta;
        return await mainQuery.ToArrayAsync();    
    }
    
    public async Task<Dieta> GetDietaPorId(int idDieta)
    {
        var mainQuery = from dieta in _context.Dieta
            where dieta.DietaId == idDieta
            select dieta;
        
        return await mainQuery.FirstOrDefaultAsync();   
    }
}