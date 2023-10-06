namespace HDP.Persistence.Repository.Contracts;

public interface IVeterinarioRepository : IGeneralRepository
{
    Task<Veterinario[]> GetVeterinarios();
    Task<Veterinario> GetVeterinarioPorNome(String logradouroVeterinario);
    Task<Veterinario> GetVeterinarioPorId(int idVeterinario);
    Task<Veterinario> GetVeterinarioPorPetId(int idVeterinario);
    Task<Veterinario[]> GetVeterinariosPorPetId(int TutorId);
}