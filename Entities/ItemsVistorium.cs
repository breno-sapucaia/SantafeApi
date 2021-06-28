using System;
using System.Collections.Generic;

#nullable disable

namespace SantafeApi.Entities
{
    public partial class ItemsVistorium
    {
        public int CodItemVis { get; set; }
        public int CodLocal { get; set; }
        public string NomeLocal { get; set; }
        public int CodItem { get; set; }
        public int CodCliente { get; set; }
        public string NomeItemVis { get; set; }
        public string ParamItem { get; set; }

    }
}
