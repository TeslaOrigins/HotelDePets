using System;
using System.Collections.Generic;

namespace HDP.Persistence
{
    public partial class Endereco
    {
        public int EnderecoId { get; set; }
        public string Logradouro { get; set; } = null!;
        public int Numero { get; set; }
        public string Cidade { get; set; } = null!;
        public string Estado { get; set; } = null!;
        public int TutorId { get; set; }

        public virtual Tutor Tutor { get; set; } = null!;
    }
}
