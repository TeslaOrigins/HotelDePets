using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper;
using HDP.Application.Exceptions;
using HDP.Application.Services.Contracts;
using HDP.Application.ViewModels.Usuario;
using HDP.Domain.Models;
using HDP.Persistence.Repository.Contracts;

namespace HDP.Application.Services.Implementations
{
    public class UsuarioService : IUsuarioService
    {   
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        
        public UsuarioService(IMapper mapper, IUsuarioRepository usuarioRepository){
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;

        }

        public async Task<UsuarioViewModel> AlterarUsuario(AlterarUsuarioViewModel dados, Guid Usuarioid)
        {
            try
            {
                var usuarioDomain = await _usuarioRepository.GetUsuarioPorId(Usuarioid);

                
                usuarioDomain.Cpf = dados.Cpf;
                usuarioDomain.Nome = dados.Nome.ToUpper().Trim();
                usuarioDomain.Datanascimento =  DateOnly.FromDateTime(dados.Datanascimento);
                usuarioDomain.NomeUsuario = dados.NomeUsuario;
                usuarioDomain.Senha = dados.Senha;

                if (await _usuarioRepository.SaveChangesAsync())
                {
                    return _mapper.Map<UsuarioViewModel>(await _usuarioRepository.GetUsuarioPorId(usuarioDomain.Usuarioid));
                }

                return null;
            }
            catch (BusinessException<UsuarioViewModel> BE)
            {
                throw new BusinessException<UsuarioViewModel>(BE.messages, BE.obj);
            }
            catch (NotFoundException NFE)
            {
                throw new NotFoundException(NFE.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<UsuarioViewModel> CadastrarUsuario(CadastrarUsuarioViewModel dados)
        {
            try
            {
                List<string> errors = new();

                var usuario= await _usuarioRepository.GetUsuarioPorNomeNormalizado(dados.NomeUsuario.ToUpper().Trim());

                
                if (usuario != null)
                {
                    errors.Add("Usuario já existe no sistema");
                }


                if (errors.Any())
                    throw new Exception(errors.ToArray().ToString());

                var tutor = _mapper.Map<Usuario>(dados);
                
                tutor.Nome = dados.Nome.ToUpper().Trim();
                tutor.Ativo = true;
                tutor.Admin = false;
                
                _usuarioRepository.Add(tutor);
                if (await _usuarioRepository.SaveChangesAsync())
                {
                    return _mapper.Map<UsuarioViewModel>(await _usuarioRepository.GetUsuarioPorId(tutor.Usuarioid));
                }

                return null;
            }
            catch (BusinessException<UsuarioViewModel> BE)
            {
                throw new BusinessException<UsuarioViewModel>(BE.messages, BE.obj);
            }
            catch (NotFoundException NFE)
            {
                throw new NotFoundException(NFE.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<UsuarioViewModel[]> GetUsuarios()
        {
            try{
                return _mapper.Map<UsuarioViewModel[]>(await _usuarioRepository.GetUsuarios());
            }catch(Exception e){
                throw new Exception(e.Message);
            }
        }

        public async Task<UsuarioViewModel> InativarReativarUsuario(Guid Usuarioid)
        {
            try
            {
                var usuarioDomain = await _usuarioRepository.GetUsuarioPorId(Usuarioid);

                if (usuarioDomain == null)
                {
                    throw new NotFoundException("O usuario especificado não existe no sistema");
                }

               
                usuarioDomain.Ativo = !usuarioDomain.Ativo;
                await _usuarioRepository.SaveChangesAsync();

                return _mapper.Map<UsuarioViewModel>(usuarioDomain);
                
            }
            catch (BusinessException<UsuarioViewModel> BE)
            {
                throw new BusinessException<UsuarioViewModel>(BE.messages, BE.obj);
            }
            catch (NotFoundException NFE)
            {
                throw new NotFoundException(NFE.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}