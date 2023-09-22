using AutoMapper;
using HDP.Application.ViewModels.Tutor;
using HDP.Persistence;

namespace HDP.Application.Helpers.Profiles;

public class TutorProfile : Profile
{
    public TutorProfile()
    {
        CreateMap<Tutor, TutorViewModel>();

        CreateMap<Tutor, CadastroTutorViewModel>();

        CreateMap<CadastroTutorViewModel, Tutor>();
    }
}