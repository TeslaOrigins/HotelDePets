using HDP.Application.ViewModels.Veterinario;

namespace HDP.Application.ViewModels.Pet;

public class AlterarPetViewModel
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public int? Idade { get; set; }
    public string? Raca { get; set; }
    public string? Sexo { get; set; }
    public string? Especie { get; set; }
    public string? FotoUrl { get; set; }
    public int? Peso { get; set; }
    public AlterarVeterinarioViewModel Veterinario {get;set;}
}