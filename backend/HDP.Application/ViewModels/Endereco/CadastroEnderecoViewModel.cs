using HDP.Domain.Models.Constantes;

namespace HDP.Application.ViewModels.Endereco;

public class CadastroEnderecoViewModel
{
    public bool? Existe { get; set; }
    public string Logradouro { get; set; }
    public string Numero { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }
}