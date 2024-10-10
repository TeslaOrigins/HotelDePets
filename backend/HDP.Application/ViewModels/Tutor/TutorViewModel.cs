
using HDP.Application.ViewModels.Pet;

namespace HDP.Application.ViewModels.Tutor;

public class TutorViewModel
{
        public Guid Tutorid { get; set; }
        public string? Nome { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string? Telefone { get; set; }
        public string Cpf { get; set; } 
        public string? Email { get; set; }
        public string? Rua { get; set; }
        public string? Cep { get; set; }
        public string? Bairro { get; set; }
        public short? Numero { get; set; }
        public bool Ativo {get;set;}
        
        // Lista de Pets do Tutor
        public List<PetViewModel>? Pets { get; set; }
}