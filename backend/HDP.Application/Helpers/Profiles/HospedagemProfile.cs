using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HDP.Application.ViewModels.Hospedagem;
using HDP.Domain.Models;

namespace HDP.Application.Helpers.Profiles
{
    public class HospedagemProfile : Profile
    {
        public HospedagemProfile(){
            CreateMap<CadastroHospedagemViewModel,Hospedagem>();
            CreateMap<AlterarHospedagemViewModel,Hospedagem>();
            CreateMap<Hospedagem,HospedagemViewModel>();
        }
    }
}