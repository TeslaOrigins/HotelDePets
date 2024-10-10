using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HDP.Application.ViewModels.Usuario;
using HDP.Domain.Models;
namespace HDP.Application.Helpers.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile(){
            CreateMap<Usuario, UsuarioViewModel>()
                .ForMember(o => o.Datanascimento,
                            map => map.MapFrom(o => o.Datanascimento.ToDateTime(TimeOnly.Parse("00:00 AM"))));

            CreateMap<Usuario, CadastrarUsuarioViewModel>();

            CreateMap<CadastrarUsuarioViewModel, Usuario>()
                .ForMember(o => o.Datanascimento,
                            map => map.MapFrom(o => DateOnly.FromDateTime(o.Datanascimento)));
        }

    }
}