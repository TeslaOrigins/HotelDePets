using AutoMapper;
using HDP.Application.ViewModels.Tutor;
using HDP.Domain.Models;
using HDP.Persistence;

namespace HDP.Application.Helpers.Profiles;

public class TutorProfile : Profile
{
    public TutorProfile()
    {
        CreateMap<Tutor, TutorViewModel>()
            .ForMember(o => o.Datanascimento,
                        map => map.MapFrom(o => o.Datanascimento.Value.ToDateTime(TimeOnly.Parse("00:00 AM"))));

        CreateMap<Tutor, CadastroTutorViewModel>();

        CreateMap<CadastroTutorViewModel, Tutor>()
            .ForMember(o => o.Datanascimento,
                        map => map.MapFrom(o => DateOnly.FromDateTime(o.Datanascimento.Value)));
    }
}