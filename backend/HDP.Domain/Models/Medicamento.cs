using System;
using System.Collections.Generic;

namespace HDP.Persistence
{
    public partial class Medicamento
    {
        public Medicamento()
        {
            CuidadoEspecialMedicamentos = new HashSet<CuidadoEspecialMedicamento>();
        }

        public int MedicamentoId { get; set; }
        public string? Nome { get; set; }
        public DateOnly? DataEntrada { get; set; }
        public float? Preco { get; set; }

        public virtual ICollection<CuidadoEspecialMedicamento> CuidadoEspecialMedicamentos { get; set; }
    }
}
