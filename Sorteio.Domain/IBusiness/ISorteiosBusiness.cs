using Sorteio.Domain.IBusiness.Base;
using Sorteio.Domain.Models.Body;
using Sorteio.Domain.Models.Common;
using Sorteio.Domain.Models.EntityDomain;
using Sorteio.Domain.Models.NotMapped;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sorteio.Domain.IBusiness
{
    public interface ISorteiosBusiness : IBusinessBase<Models.EntityDomain.Sorteio>
    {
        Task<ResultResponseModel> CriarNovoSorteio(SorteioBody sorteioBody);
        Task<IEnumerable<SorteioNotMapped>> ObterTodosSorteio();
        Task<ResultResponseModel> FinalizarSorteio(VencedorSorteio vencedorSorteio);
        Task<IEnumerable<InformacoesSorteio>> ObterInformacoesSorteio();
        Task<SorteioNotMapped> ObterSorteioPorId(int idSorteio);
        Task<bool> EditarFinalizarSorteio(VencedorSorteio body);
        Task<ResultResponseModel> EditarSorteio(SorteioBody body);
    }
}
