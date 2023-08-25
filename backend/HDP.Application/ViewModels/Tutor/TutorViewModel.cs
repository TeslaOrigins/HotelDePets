using HDP.Application.ViewModels.Endereco;

namespace HDP.Application.ViewModels.Tutor;

public class TutorViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string NomeNormalizado { get; set; }
    public string? Cpf { get; set; }
    public int EnderecoId { get; set; }
    public virtual ICollection<EnderecoViewModel> Endereco { get; set; }
}