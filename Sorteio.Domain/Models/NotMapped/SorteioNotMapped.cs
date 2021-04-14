using System;
using System.Collections.Generic;
using System.Text;

namespace Sorteio.Domain.Models.NotMapped
{
    public class SorteioNotMapped
    {
        public int id_sorteio { get; set; }
        public string nome_sorteio { get; set; }
        public string edicao_sorteio { get; set; }
        public string nome_ganhador { get; set; }
        public bool status { get; set; }
    }
}
