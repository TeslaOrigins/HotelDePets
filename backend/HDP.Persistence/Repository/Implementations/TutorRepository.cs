using HDP.Domain.Models;
using HDP.Persistence.Contexts;
using HDP.Persistence.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HDP.Persistence.Repository.Implementations;

public class TutorRepository : GeneralRepository, ITutorRepository
{
    private readonly HDPContext _context;
    public TutorRepository(HDPContext context) : base(context)
    {
        _context = context;
    }
    
    public async Task<Tutor[]> GetTutores()
    {
        var main_query = from tutor in _context.Tutores
            select tutor;
        
        return await main_query.ToArrayAsync();    
    }
    
    public async Task<Tutor> GetTutorPorId(Guid idTutor,bool pets = false,bool hospedagens =false)
    {
        var main_query = from tutor in _context.Tutores
            where tutor.Tutorid == idTutor
            select tutor;
        if(pets)
            main_query.Include(t => t.Pets);
        if(hospedagens)
            main_query.Include(t => t.Pets).ThenInclude(p => p.Hospedagens);
        return await main_query.FirstOrDefaultAsync();   
    }
    
    public async Task<Tutor> GetTutorPorNome(string nomeTutor)
    {
        var main_query = from tutor in _context.Tutores
            where tutor.Nome == nomeTutor
            select tutor;
        
        return await main_query.FirstOrDefaultAsync();   
    }
    
    public async Task<Tutor> GetTutorPorNomeNormalizado(string NomeNormalizadoTutor)
    {
        var main_query = from tutor in _context.Tutores
            where tutor.Nome.Trim().ToUpper() == NomeNormalizadoTutor
            select tutor;
        
        return await main_query.FirstOrDefaultAsync();   
    }
    
    // public async Task<Endereco> GetEnderecoPorPlaca(String placaEndereco)
    // {
    //     var main_query = from Endereco in _context.Endereco
    //         where Endereco.PlacaEndereco == placaEndereco
    //         select Endereco;
    //     
    //     return await main_query.FirstOrDefaultAsync();   
    // }
}