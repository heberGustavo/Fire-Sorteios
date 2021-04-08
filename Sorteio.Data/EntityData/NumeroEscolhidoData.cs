using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sorteio.Data.EntityData
{
    [Table("NumeroEscolhido")]
    public class NumeroEscolhidoData
    {
        [Key]
        public int id_numero_escolhido { get; set; }
        public int id_pedido { get; set; }
        public int numero { get; set; }
    }
}
