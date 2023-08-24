using AutoMapper;
using HDP.Application.ViewModels;
using HDP.Application.ViewModels.Tutor;
using HDP.Domain.Models;

namespace HDP.Application.Helpers.Profiles;

public class TutorProfile : Profile
{
    public TutorProfile()
    {
        CreateMap<Tutor, TutorViewModel>()
            .ForMember(ViewModel => ViewModel.Endereco, 
                map => map.MapFrom(
                    x => x.Endereco));;
        CreateMap<Tutor, CadastroTutorViewModel>();
        CreateMap<Tutor, TutorRelatorioGeralViewModel>();

        CreateMap<CadastroTutorViewModel, Tutor>()
            .ForMember(ViewModel => ViewModel.Endereco, 
                map => map.MapFrom(
                    x => x.Endereco));
    }
}