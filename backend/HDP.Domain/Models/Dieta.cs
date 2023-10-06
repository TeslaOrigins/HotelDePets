using System;
using System.Collections.Generic;

namespace HDP.Persistence
{
    public partial class Dieta
    {
        public Dieta()
        {
            DietaAlimentos = new HashSet<DietaAlimento>();
        }

        public int DietaId { get; set; }
        public DateTime HorarioAlimentacao { get; set; }
        public int Quantidade { get; set; }
        public string? Observacoes { get; set; }
        public int PetId { get; set; }

        public virtual Pet Pet { get; set; } = null!;
        public virtual ICollection<DietaAlimento> DietaAlimentos { get; set; }
    }
}
