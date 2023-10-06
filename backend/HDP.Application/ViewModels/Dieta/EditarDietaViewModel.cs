using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HDP.Application.ViewModels.Dieta
{
    public class AlterarDietaViewModel
    {
        public int DietaId{get;set;}
        public string Quantidade { get; set; }
        public int Observacoes { get; set; }
        public DateTime Horario {get;set;}
        public int[] idsAlimento{get;set;}
    }
}