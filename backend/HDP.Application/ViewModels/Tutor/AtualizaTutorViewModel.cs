using HDP.Application.ViewModels.Endereco;

namespace HDP.Application.ViewModels.Tutor;

public class AtualizaTutorViewModel
{
    public int TutorId { get; set; }
    public string? Nome { get; set; }
    public string? Telefone { get; set; }
    public string? Cpf { get; set; }
    public string? Email { get; set; }
    public ICollection<AtualizarEnderecoViewModel> Enderecos {get;set;}
}