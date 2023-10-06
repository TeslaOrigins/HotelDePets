﻿using HDP.Domain.Models;

namespace HDP.Persistence.Repository.Contracts;

public interface IPetRepository : IGeneralRepository
{
    Task<Pet[]> GetPet();
    Task<Pet> GetPetPorId(int idPet);
    Task<Pet> GetPetPorNome(string nomePet);
    //Task<Pet> GetPetPorNomeNormalizado(string NomeNormalizadoPet);
}