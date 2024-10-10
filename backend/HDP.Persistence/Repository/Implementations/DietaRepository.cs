using HDP.Domain.Models;
using HDP.Persistence.Contexts;
using HDP.Persistence.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HDP.Persistence.Repository.Implementations;

public class DietaRepository : GeneralRepository, IDietaRepository
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
    
    public async Task<Dieta> GetDietaPorId(Guid idDieta)
    {
        var mainQuery = from dieta in _context.Dieta
            where dieta.Dietaid == idDieta
            select dieta;
        
        return await mainQuery.FirstOrDefaultAsync();   
    }
}