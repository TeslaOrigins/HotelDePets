using HDP.Application.ViewModels.Endereco;

namespace HDP.Application.ViewModels.Tutor;

public class TutorViewModel
{
    public int TutorId { get; set; }
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string Email{get;set;}
    public string NomeNormalizado { get; set; }
    public string? Cpf { get; set; }
    public virtual ICollection<EnderecoViewModel> Enderecos { get; set; }
}