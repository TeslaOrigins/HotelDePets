using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HDP.Application.ViewModels.Pet;
using HDP.Persistence;

namespace HDP.Application.Helpers.Profiles
{
    public class PetProfile : Profile
    {
        public PetProfile(){
            CreateMap<CadastroPetViewModel,Pet>();
            CreateMap<AlterarPetViewModel,Pet>();
            CreateMap<Pet,PetViewModel>();
        }
    }
}