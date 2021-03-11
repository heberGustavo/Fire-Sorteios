using Dapper.Contrib.Extensions;

namespace Sorteio.Data.EntityData
{
    [Table("Sorteio")]
    public class SorteioData
    {
        public int id_sorteio { get; set; }
        public string nome_sorteio { get; set; }
    }
}
