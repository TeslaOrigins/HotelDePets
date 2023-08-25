using System;
using System.Collections.Generic;

namespace HDP.Persistence
{
    public partial class CuidadoEspecial
    {
        public CuidadoEspecial()
        {
            CuidadoEspecialMedicamentos = new HashSet<CuidadoEspecialMedicamento>();
        }

        public int CuidadoEspecialId { get; set; }
        public string? Alergias { get; set; }
        public string? CondicoesSaude { get; set; }
        public string? InstrucoesEspeciais { get; set; }
        public int? HospedagemId { get; set; }

        public virtual Hospedagem? Hospedagem { get; set; }
        public virtual ICollection<CuidadoEspecialMedicamento> CuidadoEspecialMedicamentos { get; set; }
    }
}
