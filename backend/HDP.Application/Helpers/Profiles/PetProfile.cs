using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HDP.Application.ViewModels.Pet;
using HDP.Domain.Models;
using HDP.Persistence;

namespace HDP.Application.Helpers.Profiles
{
    public class PetProfile : Profile
    {
        public PetProfile(){
            CreateMap<CadastroPetViewModel,Pet>()
                        .ForMember(o => o.Datanascimento,
                        map => map.MapFrom(o => DateOnly.FromDateTime(o.Datanascimento)));
            CreateMap<AlterarPetViewModel,Pet>();
            CreateMap<Pet,PetViewModel>()
                        .ForMember(o => o.Datanascimento,
                        map => map.MapFrom(o => o.Datanascimento.ToDateTime(TimeOnly.Parse("00:00 AM"))));;
        }
    }
}