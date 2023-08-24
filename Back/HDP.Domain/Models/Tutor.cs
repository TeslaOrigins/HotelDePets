namespace HDP.Domain.Models;

public partial class Tutor
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string NomeNormalizado { get; set; }
    public string? Cpf { get; set; }
    public virtual ICollection<Endereco> Endereco { get; set; }
    public virtual ICollection<Venda> Vendas{get; set;}
}