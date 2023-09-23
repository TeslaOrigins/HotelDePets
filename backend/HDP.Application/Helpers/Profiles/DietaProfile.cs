using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HDP.Application.ViewModels;
using HDP.Persistence;

namespace HDP.Application.Helpers.Profiles
{
    public class DietaProfile : Profile
    {
        public DietaProfile(){
            CreateMap<CadastroDietaViewModel,Dieta>()
                .ForMember(dietaEntidade => dietaEntidade.DietaAlimentos,
                                map => map.MapFrom(dietaViewModel => 
                                                    dietaViewModel.idsAlimento
                                                        .ToList()
                                                            .Select(idAlimento => 
                                                                    new DietaAlimento(){AlimentoId = idAlimento})));
            
        }
    }
}