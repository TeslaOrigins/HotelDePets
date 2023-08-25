using System;
using System.Collections.Generic;

namespace HDP.Persistence
{
    public partial class ServicoHospedagem
    {
        public int ServicoHospedagemId { get; set; }
        public int? HospedagemId { get; set; }
        public int? ServicoId { get; set; }

        public virtual Hospedagem? Hospedagem { get; set; }
        public virtual Servico? Servico { get; set; }
    }
}
