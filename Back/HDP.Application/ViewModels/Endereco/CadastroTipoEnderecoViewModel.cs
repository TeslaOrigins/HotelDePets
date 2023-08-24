namespace HDP.Application.ViewModels.Caminhao;

public class CadastroTipoCaminhaoViewModel
{
    public string Name { get; set; }
    public virtual ICollection<CaminhaoViewModel> Caminhoes { get; set; }
}