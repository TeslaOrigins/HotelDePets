
namespace HDP.Application.ViewModels.Pet;

public class AlterarPetViewModel
{
        public string Nome { get; set; } = null!;
        public DateTime Datanascimento { get; set; }
        public string Sexo { get; set; } = null!;
        public string Tipo { get; set; } = null!;
        public float Peso { get; set; }

}