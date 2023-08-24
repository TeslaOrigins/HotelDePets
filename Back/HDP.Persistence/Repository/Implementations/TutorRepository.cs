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
    
    public async Task<Tutor[]> GetTutor()
    {
        var main_query = from Tutor in _context.Tutor
            select Tutor;
        return await main_query.ToArrayAsync();    
    }
    
    public async Task<Tutor> GetTutorPorId(Guid idTutor)
    {
        var main_query = from Tutor in _context.Tutors
            where Tutor.Id == idTutor
            select Tutor;
        
        main_query = main_query.Include(x => x.Endereco);
        
        return await main_query.FirstOrDefaultAsync();   
    }
    
    public async Task<Tutor> GetTutorPorNome(string nomeTutor)
    {
        var main_query = from Tutor in _context.Tutors
            where Tutor.Name == nomeTutor
            select Tutor;
        
        return await main_query.FirstOrDefaultAsync();   
    }
    
    public async Task<Tutor> GetTutorPorNomeNormalizado(string nomeNormalizadoTutor)
    {
        var main_query = from Tutor in _context.Tutors
            where Tutor.NomeNormalizado == nomeNormalizadoTutor
            select Tutor;
        
        return await main_query.FirstOrDefaultAsync();   
    }
    
    public async Task<Caminhao> GetCaminhaoPorPlaca(String placaCaminhao)
    {
        var main_query = from caminhao in _context.Endereco
            where caminhao.PlacaCaminhao == placaCaminhao
            select caminhao;
        
        return await main_query.FirstOrDefaultAsync();   
    }
}