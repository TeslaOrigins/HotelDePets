using HDP.Application.ViewModels.Cliente;
using HDP.Domain.Models.Constantes;

namespace HDP.Application.ViewModels.Caminhao;

public class CaminhaoViewModel
{
    public Guid Id { get; set; }
    public Guid ClienteId { get; set; }
    public string PlacaCaminhao { get; set; }
    public Guid TipoCaminhaoId { get; set; }
}