using System;
using System.Collections.Generic;
using SantafeApi.Infraestrucutre.Data;

#nullable disable

namespace SantafeApi.Entities
{
    public partial class Cliente
    {
        public Cliente()
        {
            ControleOs = new HashSet<ControleO>();

        }

        public int CodCliente { get; set; }
        public string NomeCliente { get; set; }
        public string CnpjCliente { get; set; }
        public string TecResponsavel { get; set; }
        public string EnderecoCliente { get; set; }
        public string TipoDoLocal { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public int CodUsuario { get; set; }
        public string DataCad { get; set; }

        public virtual ICollection<ControleO> ControleOs { get; set; }
        public virtual SantafeApiUser CodUseruarioNavigation { get; set; }
    }
}
