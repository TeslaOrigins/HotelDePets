using HDP.Application.ViewModels.Tutor;

namespace HDP.Application.ViewModels.Pet;

public class PetViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string NomeNormalizado { get; set; }
    public int Idade { get; set; }
    public string Raca { get; set; }
    public string? Sexo { get; set; }
    public string Especie { get; set; }
    public string? FotoUrl { get; set; }
    public int Peso { get; set; }
    public int TutorId { get; set; }
    public virtual TutorViewModel Tutor { get; set; }
}