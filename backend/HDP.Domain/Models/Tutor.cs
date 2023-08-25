using System;
using System.Collections.Generic;

namespace HDP.Persistence
{
    public partial class Tutor
    {
        public Tutor()
        {
            Enderecos = new HashSet<Endereco>();
            Pets = new HashSet<Pet>();
        }

        public int TutorId { get; set; }
        public string? Telefone { get; set; }
        public string Cpf { get; set; } = null!;
        public string? Email { get; set; }
        public string Nome { get; set; } = null!;
        public string NomeNormalizado { get; set; } = null!;

        public virtual ICollection<Endereco> Enderecos { get; set; }
        public virtual ICollection<Pet> Pets { get; set; }
    }
}
