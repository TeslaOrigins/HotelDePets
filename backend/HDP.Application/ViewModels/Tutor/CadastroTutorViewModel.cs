using HDP.Application.ViewModels.Endereco;

namespace HDP.Application.ViewModels.Tutor;

public class CadastroTutorViewModel
{
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string? Cpf { get; set; }
    public string Email { get; set; }
    public virtual ICollection<CadastroEnderecoViewModel> Enderecos { get; set; }
}