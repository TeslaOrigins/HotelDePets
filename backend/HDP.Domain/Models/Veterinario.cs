using System;
using System.Collections.Generic;

namespace HDP.Persistence
{
    public partial class Veterinario
    {
        public int VeterinarioId { get; set; }
        public string Nome { get; set; } = null!;
        public string Telefone { get; set; } = null!;
        public int? PetId { get; set; }

        public virtual Pet? Pet { get; set; }
    }
}
