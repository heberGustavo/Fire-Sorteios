using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sorteio.Data.EntityData
{
    [Table("CategoriaSorteio")]
    public class CategoriaSorteioData
    {
        [Key]
        public int id_categoria_sorteio { get; set; }
        public string nome { get; set; }
        public bool status { get; set; }
    }
}
