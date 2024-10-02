using System;
using System.Collections.Generic;

namespace HDP.Domain.Models
{
    public partial class CuidadosEspeciais
    {
        public CuidadosEspeciais()
        {
            Itens = new HashSet<Item>();
        }

        public Guid Cuidadosespeciaisid { get; set; }
        public int Porcoespordia { get; set; }
        public int Valorporcao { get; set; }
        public string Descricaousomedicamento { get; set; } = null!;

        public virtual ICollection<Item> Itens { get; set; }
    }
}
