using System;
using System.Collections.Generic;

namespace HDP.Persistence
{
    public partial class DietaAlimento
    {
        public int DietaId { get; set; }
        public int AlimentoId { get; set; }
        public int DietaAlimentoId { get; set; }

        public virtual Alimento Alimento { get; set; } = null!;
        public virtual Dieta Dieta { get; set; } = null!;
    }
}
