using System;
using System.Collections.Generic;

#nullable disable

namespace SantafeApi.Entities
{
    public partial class LocalItem
    {
        public int CodLocalItem { get; set; }
        public int CodCliente { get; set; }
        public int CodItem { get; set; }
        public string NomeLocalItem { get; set; }
    }
}
