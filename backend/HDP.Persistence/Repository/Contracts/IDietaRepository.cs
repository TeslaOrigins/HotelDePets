namespace HDP.Persistence.Repository.Contracts;

public class IDietaRepository
{
    Task<Dieta[]> GetDietas();
    Task<Dieta> GetDietaPorId(int idDieta);
}