using System;
using System.Collections.Generic;

#nullable disable

namespace SantafeApi.Entities
{
    public partial class Usuario
    {
        public int CodUsuario { get; set; }
        public string NomeUsuario { get; set; }
        public string LoguinUsuario { get; set; }
        public string DomUsuario { get; set; }
        public string PwdUsuario { get; set; }
        public string DataCad { get; set; }
    }
}
