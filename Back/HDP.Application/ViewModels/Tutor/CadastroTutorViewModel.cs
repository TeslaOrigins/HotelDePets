using HDP.Application.ViewModels.Caminhao;

namespace HDP.Application.ViewModels.Cliente;

public class CadastroClienteViewModel
{
    public string Name { get; set; }
    public string? Cpf { get; set; }
    public virtual ICollection<CadastroCaminhaoViewModel> Caminhoes { get; set; }
}