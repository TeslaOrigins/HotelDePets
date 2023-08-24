namespace HDP.Domain.Models.Constantes;

public partial class TipoCaminhao
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Caminhao> Caminhoes { get; set; }
    public virtual ICollection<Material> Materiais{get;set;}
}