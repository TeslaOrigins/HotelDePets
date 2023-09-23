using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HDP.Application.ViewModels.Endereco
{
    public class AtualizarEnderecoViewModel
    {
        public int? EnderecoId { get; set; }
        public string Logradouro { get; set; }
        public string numero { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
    }
}