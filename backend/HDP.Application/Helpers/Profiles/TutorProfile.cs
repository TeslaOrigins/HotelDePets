using AutoMapper;
using HDP.Application.ViewModels;
using HDP.Application.ViewModels.Tutor;
using HDP.Domain.Models;
using HDP.Persistence;

namespace HDP.Application.Helpers.Profiles;

public class TutorProfile : Profile
{
    public TutorProfile()
    {
        CreateMap<Tutor, TutorViewModel>()
            .ForMember(viewModel => viewModel.Endereco, 
                map => map.MapFrom(
                    x => x.Enderecos));;
        CreateMap<Tutor, CadastroTutorViewModel>();

        CreateMap<CadastroTutorViewModel, Tutor>()
            .ForMember(viewModel => viewModel.Enderecos, 
                map => map.MapFrom(
                    x => x.Enderecos));
    }
}