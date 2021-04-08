using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sorteio.Data.EntityData
{
    [Table("TipoFormaDePagamento")]
    public class TipoFormaDePagamentoData
    {
        [Key]
        public int id_tipo_forma_de_pagamento { get; set; }
        public string nome { get; set; }
        public bool status { get; set; }
    }
}
