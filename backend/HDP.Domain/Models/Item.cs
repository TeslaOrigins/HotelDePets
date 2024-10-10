using System;
using System.Collections.Generic;

namespace HDP.Domain.Models
{
    public partial class Item
    {
        public Item()
        {
            Cuidadosespeciais = new HashSet<CuidadosEspeciais>();
            Dieta = new HashSet<Dieta>();
        }

        public Guid Itemid { get; set; }
        public string Nome { get; set; } = null!;
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
        public string Tipo { get; set; } = null!;
        public bool Ativo { get; set; }
        public virtual ICollection<CuidadosEspeciais> Cuidadosespeciais { get; set; }
        public virtual ICollection<Dieta> Dieta { get; set; }
    }
}
