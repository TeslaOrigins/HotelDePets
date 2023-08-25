using System;
using System.Collections.Generic;

namespace HDP.Persistence
{
    public partial class Servico
    {
        public Servico()
        {
            ServicoHospedagems = new HashSet<ServicoHospedagem>();
        }

        public int ServicoId { get; set; }
        public string? Nome { get; set; }
        public DateOnly? DataServico { get; set; }
        public float? Preco { get; set; }

        public virtual ICollection<ServicoHospedagem> ServicoHospedagems { get; set; }
    }
}
