using System;
using System.Collections.Generic;

namespace HDP.Persistence
{
    public partial class Hospedagem
    {
        public Hospedagem()
        {
            CuidadoEspecials = new HashSet<CuidadoEspecial>();
            Reservas = new HashSet<Reserva>();
            ServicoHospedagems = new HashSet<ServicoHospedagem>();
        }

        public int HospedagemId { get; set; }
        public DateOnly? DataEntrada { get; set; }
        public DateOnly? DataSaida { get; set; }
        public string? Observacoes { get; set; }
        public bool? CheckIn { get; set; }
        public int PetId { get; set; }
        public float PrecoHospedagem { get; set; }

        public virtual Pet Pet { get; set; } = null!;
        public virtual ICollection<CuidadoEspecial> CuidadoEspecials { get; set; }
        public virtual ICollection<Reserva> Reservas { get; set; }
        public virtual ICollection<ServicoHospedagem> ServicoHospedagems { get; set; }
    }
}
