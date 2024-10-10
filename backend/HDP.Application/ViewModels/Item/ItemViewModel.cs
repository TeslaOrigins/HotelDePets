namespace HDP.Application.ViewModels;

public class ItemViewModel
{
    public Guid ItemId { get; set; }
    public string? Nome { get; set; }
    public string? Tipo { get; set; } // Alimento ou Medicamento
    public decimal? Preco { get; set; }
}
