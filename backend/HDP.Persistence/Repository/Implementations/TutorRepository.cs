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
        var main_query = from tutor in _context.Tutor
            select tutor;
        
        return await main_query.ToArrayAsync();    
    }
    
    public async Task<Tutor> GetTutorPorId(int idTutor)
    {
        var main_query = from tutor in _context.Tutor
            where tutor.TutorId == idTutor
            select tutor;
        
        main_query = main_query.Include(x => x.Enderecos);
        
        return await main_query.FirstOrDefaultAsync();   
    }
    
    public async Task<Tutor> GetTutorPorNome(string nomeTutor)
    {
        var main_query = from tutor in _context.Tutor
            where tutor.Nome == nomeTutor
            select tutor;
        
        return await main_query.FirstOrDefaultAsync();   
    }
    
    public async Task<Tutor> GetTutorPorNomeNormalizado(string NomeNormalizadoTutor)
    {
        var main_query = from tutor in _context.Tutor
            where tutor.NomeNormalizado == NomeNormalizadoTutor
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