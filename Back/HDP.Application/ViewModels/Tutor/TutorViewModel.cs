using HDP.Application.ViewModels.Caminhao;

namespace HDP.Application.ViewModels.Cliente;

public class ClienteViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string NomeNormalizado { get; set; }
    public string? Cpf { get; set; }
    public virtual ICollection<CaminhaoViewModel> Caminhoes { get; set; }
}