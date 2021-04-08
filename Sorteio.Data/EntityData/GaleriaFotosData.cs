using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sorteio.Data.EntityData
{
    [Table("GaleriaFotos")]
    public class GaleriaFotosData
    {
        [Key]
        public int id_galeria_fotos { get; set; }
        public string url_imagem { get; set; }
        public bool status { get; set; }
        public int id_sorteio { get; set; }
    }
}
