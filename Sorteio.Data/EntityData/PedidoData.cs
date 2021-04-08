using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sorteio.Data.EntityData
{
    [Table("Pedido")]
    public class PedidoData
    {
        [Key]
        public int id_pedido { get; set; }
        public int id_usuario { get; set; }
        public DateTime data_pedido { get; set; }
        public decimal valor_total { get; set; }
        public bool status { get; set; }
    }
}
