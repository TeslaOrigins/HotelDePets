using HDP.Domain.Models;
using HDP.Persistence.Contexts;
using HDP.Persistence.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HDP.Persistence.Repository.Implementations;

public class HospedagemRepository : GeneralRepository, IHospedagemRepository
{
    private readonly HDPContext _context;
    public HospedagemRepository(HDPContext context) : base(context)
    {
        _context = context;
    }
    
    // Obter hospedagem por ID
    public async Task<Hospedagem?> GetHospedagemPorId(Guid idHospedagem, bool incluirPet = false, bool incluirServicos = false, bool incluirDietas = false, bool incluirCuidadosEspeciais = false)
    {
        var query = from hospedagem in _context.Hospedagens
                    where hospedagem.Hospedagemid == idHospedagem
                    select hospedagem;

        if (incluirPet)
            query = query.Include(h => h.Pet);

        if (incluirServicos)
            query = query.Include(h => h.Servicos);

        if (incluirDietas)
            query = query.Include(h => h.Dietas);

        // if (incluirCuidadosEspeciais)
        //     query = query.Include(h => h.CuidadosEspeciais);

        return await query.FirstOrDefaultAsync();
    }

    // Listar todas as hospedagens
    public async Task<IEnumerable<Hospedagem>> ListarHospedagens(bool incluirPet = false, bool incluirServicos = false, bool incluirDietas = false, bool incluirCuidadosEspeciais = false)
    {
        var query = from hospedagem in _context.Hospedagens
                    select hospedagem;

        if (incluirPet)
            query = query.Include(h => h.Pet);

        if (incluirServicos)
            query = query.Include(h => h.Servicos);

        if (incluirDietas)
            query = query.Include(h => h.Dietas);

        // if (incluirCuidadosEspeciais)
        //     query = query.Include(h => h.CuidadosEspeciais);

        return await query.ToListAsync();
    }

    // Listar hospedagens por status
    public async Task<IEnumerable<Hospedagem>> ListarHospedagensPorStatus(string status, bool incluirPet = false, bool incluirServicos = false, bool incluirDietas = false, bool incluirCuidadosEspeciais = false)
    {
        var query = from hospedagem in _context.Hospedagens
                    where hospedagem.Status.ToLower() == status.ToLower()
                    select hospedagem;

        if (incluirPet)
            query = query.Include(h => h.Pet);

        if (incluirServicos)
            query = query.Include(h => h.Servicos);

        if (incluirDietas)
            query = query.Include(h => h.Dietas);

        // if (incluirCuidadosEspeciais)
        //     query = query.Include(h => h.CuidadosEspeciais);

        return await query.ToListAsync();
    }

    // Listar hospedagens em um intervalo de datas
    public async Task<IEnumerable<Hospedagem>> ListarHospedagensPorPeriodo(DateTime dataInicio, DateTime dataFim, bool incluirPet = false, bool incluirServicos = false, bool incluirDietas = false, bool incluirCuidadosEspeciais = false)
    {
        var query = from hospedagem in _context.Hospedagens
                    where hospedagem.Datacheckin >= dataInicio && hospedagem.Datacheckout <= dataFim
                    select hospedagem;

        if (incluirPet)
            query = query.Include(h => h.Pet);

        if (incluirServicos)
            query = query.Include(h => h.Servicos);

        if (incluirDietas)
            query = query.Include(h => h.Dietas);

        // if (incluirCuidadosEspeciais)
        //     query = query.Include(h => h.CuidadosEspeciais);

        return await query.ToListAsync();
    }
}