namespace HDP.Persistence.Repository.Contracts;

public interface IDietaRepository
{
    Task<Dieta[]> GetDietas();
    Task<Dieta> GetDietaPorId(int idDieta);
}