using System;
using System.Collections.Generic;

namespace HDP.Persistence
{
    public partial class Pet
    {
        public Pet()
        {
            Dieta = new HashSet<Dieta>();
            Hospedagems = new HashSet<Hospedagem>();
            Veterinarios = new HashSet<Veterinario>();
        }

        public int PetId { get; set; }
        public string Nome { get; set; } = null!;
        public string NomeNormalizado { get; set; } = null!;
        public int? Idade { get; set; }
        public string? Raca { get; set; }
        public string Sexo { get; set; } = null!;
        public string? Especie { get; set; }
        public string? FotoUrl { get; set; }
        public float Peso { get; set; }
        public int? TutorId { get; set; }

        public virtual Tutor? Tutor { get; set; }
        public virtual ICollection<Dieta> Dieta { get; set; }
        public virtual ICollection<Hospedagem> Hospedagems { get; set; }
        public virtual ICollection<Veterinario> Veterinarios { get; set; }
    }
}
