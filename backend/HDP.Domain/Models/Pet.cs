using System;
using System.Collections.Generic;

namespace HDP.Domain.Models
{
    public partial class Pet
    {
        public Pet()
        {
            Hospedagens = new HashSet<Hospedagem>();
        }

        public Guid Petid { get; set; }
        public string Nome { get; set; } = null!;
        public DateOnly Datanascimento { get; set; }
        public string Sexo { get; set; } = null!;
        public string Tipo { get; set; } = null!;
        public string? Motivobloqueio { get; set; }
        public float Peso { get; set; }
        public Guid Tutorid { get; set; }

        public virtual Tutor Tutor { get; set; } = null!;
        public virtual ICollection<Hospedagem> Hospedagens { get; set; }
    }
}
