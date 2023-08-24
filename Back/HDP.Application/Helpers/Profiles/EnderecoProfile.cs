using AutoMapper;
using HDP.Application.ViewModels;
using HDP.Application.ViewModels.Endereco;
using HDP.Domain.Models;
using HDP.Domain.Models.Constantes;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HDP.Application.Helpers.Profiles;

public class EnderecoProfile : Profile
{
    public EnderecoProfile()
    {
        CreateMap<Endereco, EnderecoViewModel>();
        CreateMap<Endereco, EnderecoRelatorioViewModel>();
        CreateMap<Endereco, CadastroEnderecoViewModel>();
        CreateMap<TipoEndereco, CadastroTipoEnderecoViewModel>();
        CreateMap<TipoEndereco, TipoEnderecoViewModel>();
        
        CreateMap<CadastroEnderecoViewModel, Endereco>()
            .ForMember(ViewModel => ViewModel.TipoEnderecoId,
                map => map.MapFrom(
                    x => x.IdTipoEndereco));
        CreateMap<CadastroTipoEnderecoViewModel, TipoEndereco>();
        CreateMap<TipoEnderecoViewModel, TipoEndereco>();
    }
}