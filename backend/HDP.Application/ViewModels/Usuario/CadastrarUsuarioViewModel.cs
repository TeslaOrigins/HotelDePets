using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HDP.Application.ViewModels.Usuario
{
    public class CadastrarUsuarioViewModel
    {
        public string Nome { get; set; } = null!;
        public string Cpf { get; set; } = null!;
        public string NomeUsuario {get;set;} = null!;
        public DateTime Datanascimento { get; set; }
        public string Senha { get; set; }
    }
}