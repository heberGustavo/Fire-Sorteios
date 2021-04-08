using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sorteio.Data.EntityData
{
    [Table("Usuario")]
    public class UsuarioData
    {
        [Key]
        public int id_usuario { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string senha { get; set; }
        public string celular { get; set; }
        public string cpf { get; set; }
        public string url_imagem { get; set; }
        public int id_tipo_usuario { get; set; }
        public bool status { get; set; }
    }
}
