using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sorteio.Data.EntityData
{
    [Table("VencedorSorteio")]
    public class VencedorSorteioData
    {
        [Key]
        public int id_vencedor_sorteio { get; set; }
        public int id_sorteio { get; set; }
        public int id_usuario { get; set; }
        public int numero_sorteado { get; set; }
        public DateTime data_sorteio { get; set; }
    }
}
