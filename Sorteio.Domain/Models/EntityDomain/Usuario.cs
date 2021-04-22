using System;
using System.Collections.Generic;
using System.Text;

namespace Sorteio.Domain.Models.EntityDomain
{
    public class Usuario
    {
        public int id_usuario { get; set; }
        public string nome { get; set; }
        public string cpf { get; set; }
        public DateTime data_de_nascimento { get; set; }
        public string email { get; set; }
        public string senha { get; set; }
        public string celular { get; set; }
        public string cep { get; set; }
        public string logadouro { get; set; }
        public int numero { get; set; }
        public string bairro { get; set; }
        public string complemento { get; set; }
        public string estado { get; set; }
        public string cidade { get; set; }
        public string referencia { get; set; }
        public string url_imagem { get; set; }
        public int id_tipo_usuario { get; set; }
        public bool status { get; set; }

    }
}
