using System;
using System.Collections.Generic;

namespace HDP.Domain.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Hospedagens = new HashSet<Hospedagem>();
        }

        public Guid Usuarioid { get; set; }
        public string Nome { get; set; } = null!;
        public string Cpf { get; set; } = null!;
        public DateOnly Datanascimento { get; set; }
        public bool Admin { get; set; }

        public virtual ICollection<Hospedagem> Hospedagens { get; set; }
    }
}
