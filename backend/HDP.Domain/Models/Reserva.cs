using System;
using System.Collections.Generic;

namespace HDP.Persistence
{
    public partial class Reserva
    {
        public int ReservaId { get; set; }
        public DateOnly? DataReserva { get; set; }
        public int? HospedagemId { get; set; }

        public virtual Hospedagem? Hospedagem { get; set; }
    }
}
