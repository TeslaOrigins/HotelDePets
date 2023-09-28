using HDP.Application.ViewModels.Pet;

namespace HDP.Application.Services.Contracts;

public interface IPetService
{
    Task<PetViewModel[]> GetPet();
    Task<PetViewModel> GetPetPorId(int idPet);
    Task<PetViewModel> CadastrarPet(CadastroPetViewModel dados);
    Task<PetViewModel> AlterarPet(AlterarPetViewModel dados);
    Task<bool> ApagarPet(int idPet);
}