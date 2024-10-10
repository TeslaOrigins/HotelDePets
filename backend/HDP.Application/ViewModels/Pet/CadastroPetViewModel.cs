using HDP.Application.ViewModels.Tutor;

namespace HDP.Application.ViewModels.Pet;

public class CadastroPetViewModel
{


        public string Nome { get; set; } = null!;
        public DateTime Datanascimento { get; set; }
        public string Sexo { get; set; } = null!;
        public string Tipo { get; set; } = null!;
        public string? Motivobloqueio { get; set; }
        public bool Bloqueado { get; set; }
        public float Peso { get; set; }
        public Guid Tutorid { get; set; }

}