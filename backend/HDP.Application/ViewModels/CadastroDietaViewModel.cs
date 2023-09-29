namespace HDP.Application.ViewModels;

public class CadastroDietaViewModel
{
    public DateTime HorarioAlimentacao { get; set; }
    public int Quantidade { get; set; }
    public string Observacoes { get; set; }
    public int[] idsAlimento{get;set;}
}