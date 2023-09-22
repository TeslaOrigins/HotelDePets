using HDP.Application.ViewModels.Endereco;

namespace HDP.Application.ViewModels.Tutor;

public class AtualizaTutorViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Telefone { get; set; }
    public string? Cpf { get; set; }
    public string? Email { get; set; }
    public virtual ICollection<AtualizaEnderecoViewModel> Enderecos { get; set; }
}