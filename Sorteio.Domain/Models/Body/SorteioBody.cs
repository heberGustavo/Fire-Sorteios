using Sorteio.Domain.Models.EntityDomain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sorteio.Domain.Models.Body
{
    public class SorteioBody
    {
        public Sorteio.Domain.Models.EntityDomain.Sorteio sorteio { get; set; }
        public IEnumerable<GaleriaFotos> linkImagens { get; set; }
    }
}
