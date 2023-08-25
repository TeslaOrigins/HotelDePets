using HDP.Domain.Models.Constantes;

namespace HDP.Application.ViewModels.Endereco;

public class CadastroEnderecoViewModel
{
    public string Logradouro { get; set; }
    public int numero { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }
}