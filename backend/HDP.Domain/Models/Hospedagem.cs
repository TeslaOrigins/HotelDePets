using System;
using System.Collections.Generic;

namespace HDP.Domain.Models
{
    public partial class Hospedagem
    {
        public Hospedagem()
        {
            Dietas = new HashSet<Dieta>();
            Servicos = new HashSet<Servico>();
        }

        public Guid Hospedagemid { get; set; }
        public DateTime Datainicio { get; set; }
        public DateTime? Datafim { get; set; }
        public DateTime Datacheckin { get; set; }
        public DateTime? Datacheckout { get; set; }
        public Guid Petid { get; set; }
        public Guid Usuarioid { get; set; }
        public bool Paga { get; set; }
        public string Status { get; set; } = null!;

        public virtual Pet Pet { get; set; } = null!;
        public virtual Usuario Usuario { get; set; } = null!;
        public virtual ICollection<Dieta> Dietas { get; set; }
        public virtual ICollection<Servico> Servicos { get; set; }
        public virtual ICollection<CuidadosEspeciais> CuidadosEspeciais { get; set; }
    }
}
