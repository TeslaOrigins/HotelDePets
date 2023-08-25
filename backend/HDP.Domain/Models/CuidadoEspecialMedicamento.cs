using System;
using System.Collections.Generic;

namespace HDP.Persistence
{
    public partial class CuidadoEspecialMedicamento
    {
        public int CemId { get; set; }
        public int? MedicamentoId { get; set; }
        public int? CuidadoespecialId { get; set; }

        public virtual CuidadoEspecial? Cuidadoespecial { get; set; }
        public virtual Medicamento? Medicamento { get; set; }
    }
}
