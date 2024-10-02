using System;
using System.Collections.Generic;

namespace HDP.Domain.Models
{
    public partial class Dieta
    {
        public Dieta()
        {
            Itens = new HashSet<Item>();
        }

        public Guid Dietaid { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
        public Guid Hospedagemid { get; set; }

        public virtual Hospedagem Hospedagem { get; set; } = null!;

        public virtual ICollection<Item> Itens { get; set; }
    }
}
