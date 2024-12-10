﻿using HDP.Application.ViewModels.CuidadosEspeciais;
using HDP.Application.ViewModels.Dieta;
using HDP.Application.ViewModels.Servico;

namespace HDP.Application.ViewModels.Hospedagem;

public class HospedagemViewModel
{
    public Guid HospedagemId { get; set; }
    public DateTime? DataInicio { get; set; }
    public DateTime? DataCheckin { get; set; }
    public DateTime? DataCheckout { get; set; }
    public DateTime? DataFim { get; set; }
    public string? Status { get; set; } // Pode ser algo como "Reserva" ou "Hospedagem"
    public bool? Paga { get; set; }
    
    // Pet relacionado à hospedagem
    public Guid PetId { get; set; }
    public string? NomePet { get; set; }
    

    // Serviços adicionados à hospedagem
    public List<ServicoViewModel>? Servicos { get; set; }
    
    // Lista de itens de dieta e cuidados especiais
    public List<DietaViewModel>? Dietas { get; set; }
    public List<CuidadosEspeciaisViewModel>? CuidadosEspeciais { get; set; }
}