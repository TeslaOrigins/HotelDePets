namespace HDP.Domain.Models.Constantes;

public partial class AdicionaisVenda
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Venda> Vendas { get; set; }
}