using AutoMapper;
using HDP.Application.ViewModels;
using HDP.Application.ViewModels.Endereco;
using HDP.Domain.Models;
using HDP.Domain.Models.Constantes;
using HDP.Persistence;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HDP.Application.Helpers.Profiles;

public class EnderecoProfile : Profile
{
    public EnderecoProfile()
    {
        CreateMap<Endereco, EnderecoViewModel>();
        CreateMap<Endereco, CadastroEnderecoViewModel>();
        
        CreateMap<EnderecoViewModel, Endereco>();
        CreateMap<CadastroEnderecoViewModel, Endereco>();
    }
}