using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HDP.Domain.Models;

namespace HDP.Persistence.Repository.Contracts
{
    public interface IUsuarioRepository : IGeneralRepository
    {
        
        Task<Usuario[]> GetUsuarios();
        Task<Usuario> GetUsuarioPorId(Guid idUsuario);
        Task<Usuario> GetUsuarioPorNome(string nomeUsuario);
        Task<Usuario> GetUsuarioPorNomeNormalizado(string NomeNormalizadoUsuario);
    }
}