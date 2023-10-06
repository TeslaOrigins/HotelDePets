using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HDP.Application.ViewModels.Alimento
{
    public class CadastroAlimentoViewModel
    {
        public string Nome { get; set; } = null!;
        public int QuantidadeEstoque { get; set; }
        public float PrecoReabastecimento { get; set; }
        public DateOnly DataEntrada { get; set; }
    }
}