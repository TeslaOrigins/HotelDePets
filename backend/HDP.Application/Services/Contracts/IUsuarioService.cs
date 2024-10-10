using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HDP.Application.ViewModels.Usuario;

namespace HDP.Application.Services.Contracts
{
    public interface IUsuarioService
    {
        Task<UsuarioViewModel[]> GetUsuarios();
        Task<UsuarioViewModel> CadastrarUsuario(CadastrarUsuarioViewModel dados);
        Task<UsuarioViewModel> AlterarUsuario(AlterarUsuarioViewModel dados,Guid Usuarioid);
        Task<UsuarioViewModel> InativarReativarUsuario(Guid Usuarioid);

    }
}