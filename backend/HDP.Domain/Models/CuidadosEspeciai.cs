using System;
using System.Collections.Generic;

namespace HDP.Domain.Models
{
    public class CuidadosEspeciais
    {

        public CuidadosEspeciais()
        {
            Itens = new HashSet<Item>();
        }

        public Guid CuidadosEspeciaisId { get; set; }
        public string Nome { get; set; }
        public string DescricaoUsoMedicamento { get; set; }
        public int PorcaoPorDia { get; set; }      
        public Guid Hospedagemid {get;set;}
        public virtual Hospedagem Hospedagem {get;set;}
        public virtual ICollection<Item> Itens { get; set; }
    }
}
