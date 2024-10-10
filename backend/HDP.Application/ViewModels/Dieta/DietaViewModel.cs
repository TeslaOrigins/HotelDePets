using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HDP.Application.ViewModels.Dieta
{
    public class DietaViewModel
    {
        public int DietaId{get;set;}
        public int Quantidade { get; set; }
        public string Observacoes { get; set; }
        public DateTime Horario {get;set;}

    }
}