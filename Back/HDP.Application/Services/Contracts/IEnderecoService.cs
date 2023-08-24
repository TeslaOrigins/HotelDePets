using HDP.Application.ViewModels.Endereco;

namespace HDP.Application.Services.Contracts;

public interface IEnderecoService
{
    Task<EnderecoViewModel[]> GetEnderecos();
    Task<TipoEnderecoViewModel> CadastrarTipoEndereco(CadastroTipoEnderecoViewModel dados);
    Task<EnderecoViewModel[]> GetEnderecosByTutorId(Guid IdTutor);
}