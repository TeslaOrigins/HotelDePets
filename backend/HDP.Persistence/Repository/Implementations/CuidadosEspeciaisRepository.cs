using HDP.Domain.Models;
using HDP.Persistence.Contexts;
using HDP.Persistence.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HDP.Persistence.Repository.Implementations;

public class CuidadosEspeciaisRepository : GeneralRepository, ICuidadosEspeciaisRepository
{
    private readonly HDPContext _context;

    public CuidadosEspeciaisRepository(HDPContext context) : base(context)
    {
        _context = context;
    }

    public async Task<CuidadosEspeciais?> GetCuidadosEspeciaisPorId(Guid id)
    {
        var query = from ce in _context.CuidadosEspeciais
            where ce.CuidadosEspeciaisId == id
            select ce;
        return await query.FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<CuidadosEspeciais>> ListarCuidadosEspeciais()
    {
        var query = from ce in _context.CuidadosEspeciais
            select ce;
        return await query.ToListAsync();
    }
}
