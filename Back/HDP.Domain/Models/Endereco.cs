using HDP.Domain.Models.Constantes;

namespace HDP.Domain.Models;

public partial class Endereco
{
    public Guid Id { get; set; }
    public Guid ClienteId { get; set; }
    public Cliente Cliente { get; set; }
    public string PlacaEndereco { get; set; }
    public Guid TipoEnderecoId { get; set; }
    public TipoEndereco TipoEndereco { get; set; }
    public virtual ICollection<Venda> Vendas {get;set;}
}