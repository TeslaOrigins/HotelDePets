using AutoMapper;
using HDP.Application.Exceptions;
using HDP.Application.Services.Contracts;
using HDP.Application.ViewModels;
using HDP.Application.ViewModels.Pet;
using HDP.Persistence;
using HDP.Persistence.Repository.Contracts;

namespace HDP.Application.Services.Implementations;

public class PetService : IPetService
{
    private readonly IPetRepository _petRepository;
    private readonly IVeterinarioRepository _veterinarioRepository;
    private readonly IMapper _mapper;
    
    public PetService(IMapper mapper, IPetRepository petRepository, IVeterinarioRepository veterinarioRepository){
        _petRepository = petRepository;
        _mapper = mapper;
        _veterinarioRepository = veterinarioRepository;
    }
    
    public async Task<PetViewModel[]> GetPet()
    {
        try{
            return _mapper.Map<PetViewModel[]>(await _petRepository.GetPet());
        }catch(Exception e){
            throw new Exception(e.Message);
        }
    }
    
    public async Task<PetViewModel> GetPetPorId(int idPet)
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
                return _mapper.Map<PetViewModel>(await _petRepository.GetPetPorId(pet.PetId));
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
    
    public async Task<PetViewModel> AlterarPet(AlterarPetViewModel dados)
    {
        try
        {
            var petDomain = await _petRepository.GetPetPorId(dados.Id);

            if(petDomain == null)
                throw new NotFoundException("Não foi possível encontrar o pet especificado");

            if (dados.Nome != null)
            {
                petDomain.Nome = dados.Nome;
            }
            if (dados.Idade != null)
            {
                petDomain.Idade = dados.Idade;
            }
            if (dados.Raca != null)
            {
                petDomain.Raca = dados.Raca;
            }
            if (dados.Sexo != null)
            {
                petDomain.Sexo = dados.Sexo;
            }
            if (dados.Especie != null)
            {
                petDomain.Especie = dados.Especie;
            }
            if (dados.FotoUrl != null)
            {
                petDomain.FotoUrl = dados.FotoUrl;
            }

            if (await _petRepository.SaveChangesAsync())
            {
                return _mapper.Map<PetViewModel>(await _petRepository.GetPetPorId(petDomain.PetId));
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
    
    public async Task<bool> ApagarPet(int idPet)
    {
        try
        {
            var petDomain = await _petRepository.GetPetPorId(idPet);
            
            if(petDomain == null)
                throw new NotFoundException("Não foi possivel encontrar o pet especificado");
            _petRepository.Delete(petDomain);
            
            return await _petRepository.SaveChangesAsync();
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