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
    private readonly ITutorRepository _tutorRepository;
    private readonly IMapper _mapper;
    
    public PetService(IMapper mapper, IPetRepository petRepository, ITutorRepository tutorRepository){
        _tutorRepository = tutorRepository;
        _petRepository = petRepository;
        _mapper = mapper;

    }
    
    public async Task<PetViewModel[]> GetPets()
    {
        try{
            return _mapper.Map<PetViewModel[]>(await _petRepository.GetPets());
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
            var tutorExiste = await _tutorRepository.GetTutorPorId(dados.Tutorid);
            if(tutorExiste == null)
                errors.Add("Tutor não existe no banco de dados");
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
            
            pet.Nome= dados.Nome.ToUpper().Trim();
            
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

            petDomain.Datanascimento = DateOnly.FromDateTime(dados.Datanascimento);
            petDomain.Tipo = dados.Tipo;
            petDomain.Nome = dados.Nome.Trim().ToUpper();
            petDomain.Peso = dados.Peso;
            petDomain.Sexo = dados.Sexo;

          

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

    public async Task<PetViewModel> RemoverPet(Guid idPet)
    {
          try
        {
            var petDomain = await _petRepository.GetPetPorId(idPet);

            if (petDomain == null)
            {
                throw new NotFoundException("O pet especificado não existe no sistema");
            }

            // se todos os pets não tem hospedagem registrada ele inativa.
            if(!petDomain.Hospedagens.Any())
            {
                _petRepository.Delete(petDomain);
            }   
            else{
                throw new BusinessException<PetViewModel>(new string[] {"O pet possui uma hospedagem registrada, não é possivel inativa-lo no sistema."}, _mapper.Map<PetViewModel>(petDomain));
            }
            await _petRepository.SaveChangesAsync();

            return _mapper.Map<PetViewModel>(petDomain);
            
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

    public async Task<PetViewModel> BloquearPet(Guid idPet,string mensagemBloqueio)
    {
         try
        {
            var petDomain = await _petRepository.GetPetPorId(idPet);

            if (petDomain == null)
            {
                throw new NotFoundException("O pet especificado não existe no sistema");
            }

            // se todos os pets não tem hospedagem registrada ele inativa.
            if(!petDomain.Hospedagens.Any())
            {
                petDomain.Bloqueado = !petDomain.Bloqueado;
                petDomain.Motivobloqueio = mensagemBloqueio;
            }   
            else{
                throw new BusinessException<PetViewModel>(new string[] {"O pet possui uma hospedagem registrada, não é possivel inativa-lo no sistema."}, _mapper.Map<PetViewModel>(petDomain));
            }
            await _petRepository.SaveChangesAsync();

            return _mapper.Map<PetViewModel>(petDomain);
            
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
}