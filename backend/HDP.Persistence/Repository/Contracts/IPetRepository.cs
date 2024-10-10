using HDP.Domain.Models;

namespace HDP.Persistence.Repository.Contracts;

public interface IPetRepository : IGeneralRepository
{
    Task<Pet[]> GetPets();
    Task<Pet> GetPetPorId(Guid idPet);
    Task<Pet> GetPetPorNome(string nomePet);
    //Task<Pet> GetPetPorNomeNormalizado(string NomeNormalizadoPet);
}