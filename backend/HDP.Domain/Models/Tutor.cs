using System;
using System.Collections.Generic;

namespace HDP.Domain.Models
{
    public partial class Tutor
    {
        public Tutor()
        {
            Pets = new HashSet<Pet>();
        }

        public Guid Tutorid { get; set; }
        public string? Nome { get; set; }
        public DateOnly? Datanascimento { get; set; }
        public string? Telefone { get; set; }
        public string Cpf { get; set; } = null!;
        public string? Email { get; set; }
        public string? Rua { get; set; }
        public string? Cep { get; set; }
        public string? Bairro { get; set; }
        public short? Numero { get; set; }
        public bool Ativo {get;set;}
        public virtual ICollection<Pet> Pets { get; set; }
    }
}
