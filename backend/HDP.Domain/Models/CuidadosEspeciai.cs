using System;
using System.Collections.Generic;

namespace HDP.Domain.Models
{
    public class CuidadosEspeciais
    {
        public Guid CuidadosEspeciaisId { get; set; }
        public string Nome { get; set; }
        public string DescricaoUsoMedicamento { get; set; }
        public int PorcaoPorDia { get; set; }
        public DateTime Validade { get; set; }
    }
}
