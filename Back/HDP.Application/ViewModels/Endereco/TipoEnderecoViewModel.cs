namespace HDP.Application.ViewModels.Caminhao;

public class TipoCaminhaoViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<CaminhaoViewModel>? Caminhoes { get; set; }
}