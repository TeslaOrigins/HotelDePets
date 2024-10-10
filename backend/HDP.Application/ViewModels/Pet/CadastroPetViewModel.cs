using HDP.Application.ViewModels.Tutor;

namespace HDP.Application.ViewModels.Pet;

public class CadastroPetViewModel
{

    public string Nome { get; set; } = null!;
    public DateOnly Datanascimento { get; set; }
    public string Sexo { get; set; } = null!;
    public string Tipo { get; set; } = null!;
    public float Peso { get; set; }
    public Guid Tutorid { get; set; }
    public Guid TutorId { get; set; }

}