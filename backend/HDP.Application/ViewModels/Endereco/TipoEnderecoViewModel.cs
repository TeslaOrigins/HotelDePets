namespace HDP.Application.ViewModels.Endereco;

public class TipoEnderecoViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<EnderecoViewModel>? Enderecos { get; set; }
}