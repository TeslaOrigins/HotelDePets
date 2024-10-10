using AutoMapper;
using HDP.Application.Exceptions;
using HDP.Application.Services.Contracts;
using HDP.Application.ViewModels;
using HDP.Application.ViewModels.Pet;
using HDP.Domain.Models;
using HDP.Persistence;
using HDP.Persistence.Repository.Contracts;

namespace HDP.Application.Services.Implementations;

public class PetService : IPetService
{
    private readonly IPetRepository _petRepository;
    
    private readonly IDietaRepository _dietaRepository;
    private readonly IMapper _mapper;
    
    public PetService(IMapper mapper, IPetRepository petRepository){
        _petRepository = petRepository;
        _mapper = mapper;

    }
    
    public async Task<PetViewModel[]> GetPet()
    {
        try{
            return _mapper.Map<PetViewModel[]>(await _petRepository.GetPet());
        }catch(Exception e){
            throw new Exception(e.Message);
        }
    }
    
    public async Task<PetViewModel> GetPetPorId(Guid idPet)
    {
        try{
            var pet = await _petRepository.GetPetPorId(idPet);
            if(pet == null)
                throw new NotFoundException("Não foi encontrado um Pet com o id especificado");
            return _mapper.Map<PetViewModel>(pet);
        }
        catch(NotFoundException NFE)
        {
            throw new NotFoundException(NFE.Message);
        }
        catch(Exception e){
            throw new Exception(e.Message);
        }
    }
    
    public async Task<PetViewModel> CadastrarPet(CadastroPetViewModel dados)
    {
        try
        {
            List<string> errors = new();

            //var petExiste = await ValidaPetExistente(dados);
            //var veterinarioExiste = await ValidaVeterinarioExistente(dados.Veterinario);
            
            //if (petExiste)
            //{
           //     errors.Add("Pet já existe no sistema");
           // }

            //if (veterinarioExiste != null)
           // {
           //     if (veterinarioExiste.Existe)
           //         errors.Add("O Endereço de logradouro: " + veterinarioExiste.Nome + " já está cadastrado no sistema");
           // }

            if (errors.Any())
                throw new Exception(errors.ToArray().ToString());

            var pet = _mapper.Map<Pet>(dados);
            
           // pet.NomeNormalizado = dados.Nome.ToUpper().Trim();
            
            _petRepository.Add(pet);
            if (await _petRepository.SaveChangesAsync())
            {
                return _mapper.Map<PetViewModel>(await _petRepository.GetPetPorId(pet.Petid));
            }

            return null;
        }
        catch (BusinessException<PetViewModel> BE)
        {
            throw new BusinessException<PetViewModel>(BE.messages, BE.obj);
        }
        catch (NotFoundException NFE)
        {
            throw new NotFoundException(NFE.Message);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    
    public async Task<PetViewModel> AlterarPet(AlterarPetViewModel dados,Guid IdPet)
    {
        try
        {
            var petDomain = await _petRepository.GetPetPorId(IdPet);

            if(petDomain == null)
                throw new NotFoundException("Não foi possível encontrar o pet especificado");

          

            if (await _petRepository.SaveChangesAsync())
            {
                return _mapper.Map<PetViewModel>(await _petRepository.GetPetPorId(petDomain.Petid));
            }

            return null;
        }
        catch (BusinessException<PetViewModel> BE)
        {
            throw new BusinessException<PetViewModel>(BE.messages, BE.obj);
        }
        catch (NotFoundException NFE)
        {
            throw new NotFoundException(NFE.Message);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
   
  /*
    private async Task<bool> ValidaPetExistente(CadastroPetViewModel dados)
    {
        try
        {
            var pet = await GetPetPorNomeAnotherService(dados.Nome.ToUpper().Trim());

            return pet != null;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    
  
    
    private async Task<Pet> GetPetPorNomeAnotherService(string nomePet)
    {
        var pet = await _petRepository.GetPetPorNomeNormalizado(nomePet);
    
        if (pet != null)
        {
            return pet;
        }
    
        return null;
    }
    
    private async Task<Pet> GetPetPorIdAnotherService(int idPet)
    {
        var pet = await _petRepository.GetPetPorId(idPet);

        if (pet != null)
        {
            return pet;
        }

        return null;
    }
    
    private async Task<Veterinario> GetVeterinarioAnotherService(int idPet)
    {
        var pet = await _veterinarioRepository.GetVeterinarioPorPetId(idPet);

        if (pet != null)
        {
            return pet;
        }

        return null;
    }*/
}