using System.Collections.ObjectModel;

namespace HDP.Domain.Models.Constantes;

public partial class TipoPagamento
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Venda> Vendas { get; set; }
}