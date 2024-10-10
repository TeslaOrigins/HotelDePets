using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HDP.Domain.Models;
using HDP.Persistence.Contexts;
using HDP.Persistence.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HDP.Persistence.Repository.Implementations
{
    public class UsuarioRepository : GeneralRepository,IUsuarioRepository
    {
        private readonly HDPContext _context;
        public UsuarioRepository(HDPContext context) : base(context)
        {
            _context = context;
        }
        
        public async Task<Usuario[]> GetUsuarios()
        {
            var main_query = from Usuario in _context.Usuarios
                select Usuario;
            
            return await main_query.ToArrayAsync();    
        }
        
        public async Task<Usuario> GetUsuarioPorId(Guid idUsuario)
        {
            var main_query = from Usuario in _context.Usuarios
                where Usuario.Usuarioid == idUsuario
                select Usuario;

            return await main_query.FirstOrDefaultAsync();   
        }
        
        public async Task<Usuario> GetUsuarioPorNome(string nomeUsuario)
        {
            var main_query = from Usuario in _context.Usuarios
                where Usuario.NomeUsuario == nomeUsuario
                select Usuario;
            
            return await main_query.FirstOrDefaultAsync();   
        }
        
        public async Task<Usuario> GetUsuarioPorNomeNormalizado(string NomeNormalizadoUsuario)
        {
            var main_query = from Usuario in _context.Usuarios
                where Usuario.Nome.Trim().ToUpper() == NomeNormalizadoUsuario
                select Usuario;
            
            return await main_query.FirstOrDefaultAsync();   
        }
    }
}