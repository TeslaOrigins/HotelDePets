using System;
using System.Collections.Generic;

namespace HDP.Persistence
{
    public partial class Alimento
    {
        public Alimento()
        {
            DietaAlimentos = new HashSet<DietaAlimento>();
        }

        public int AlimentoId { get; set; }
        public string Nome { get; set; } = null!;
        public int QuantidadeEstoque { get; set; }
        public float PrecoReabastecimento { get; set; }
        public DateOnly DataEntrada { get; set; }

        public virtual ICollection<DietaAlimento> DietaAlimentos { get; set; }
    }
}
