using System;
using System.Collections.Generic;

namespace HDP.Domain.Models
{
    public partial class Servico
    {
        public Guid Servicoid { get; set; }
        public string? Nome { get; set; }
        public decimal? Preco { get; set; }
        public Guid Hospedagemid { get; set; }
        public bool Ativo {get;set;}
        public virtual Hospedagem Hospedagem { get; set; } = null!;
    }
}
