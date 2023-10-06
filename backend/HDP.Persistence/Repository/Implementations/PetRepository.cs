using HDP.Persistence.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HDP.Persistence.Repository.Implementations;

public class PetRepository : GeneralRepository, IPetRepository
{
    private readonly HDPContext _context;
    public PetRepository(HDPContext context) : base(context)
    {
        _context = context;
    }
    
    public async Task<Pet[]> GetPet()
    {
        var main_query = from pet in _context.Pet
            select pet;
        
        return await main_query.ToArrayAsync();    
    }
    
    public async Task<Pet> GetPetPorId(int idPet)
    {
        var main_query = from pet in _context.Pet
            where pet.PetId == idPet
            select pet;
        
        return await main_query.FirstOrDefaultAsync();   
    }
    
    public async Task<Pet> GetPetPorNome(string nomePet)
    {
        var main_query = from pet in _context.Pet
            where pet.Nome == nomePet
            select pet;
        
        return await main_query.FirstOrDefaultAsync();   
    }
    
    /*public async Task<Pet> GetPetPorNomeNormalizado(string NomeNormalizadoPet)
    {
        var main_query = from pet in _context.Pet
            where pet.NomeNormalizado == NomeNormalizadoPet
            select pet;
        
        return await main_query.FirstOrDefaultAsync();   
    }*/
    
    // public async Task<Endereco> GetEnderecoPorPlaca(String placaEndereco)
    // {
    //     var main_query = from Endereco in _context.Endereco
    //         where Endereco.PlacaEndereco == placaEndereco
    //         select Endereco;
    //     
    //     return await main_query.FirstOrDefaultAsync();   
    // }
}