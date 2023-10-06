using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HDP.Application.ViewModels.Alimento;
using HDP.Persistence;

namespace HDP.Application.Helpers.Profiles
{
    public class AlimentoProfile : Profile
    {
        public AlimentoProfile(){
            CreateMap<CadastroAlimentoViewModel,Alimento>();
            CreateMap<AlterarAlimentoViewModel,Alimento>();
            CreateMap<Alimento,AlimentoViewModel>();
        }
    }
}