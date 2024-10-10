using HDP.Application.ViewModels.Pet;
using HDP.Persistence.Repository.Contracts;

namespace HDP.Application.Services.Contracts;

public interface IPetService
{
    Task<PetViewModel[]> GetPets();
    Task<PetViewModel> GetPetPorId(Guid idPet);
    Task<PetViewModel> CadastrarPet(CadastroPetViewModel dados);
    Task<PetViewModel> AlterarPet(AlterarPetViewModel dados,Guid idPet);
    Task<PetViewModel> RemoverPet(Guid idPet);
    Task<PetViewModel> BloquearPet(Guid idPet,string MensagemBloqueio);
}