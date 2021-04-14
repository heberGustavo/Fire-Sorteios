using Sorteio.Domain.IRepository.Base;
using Sorteio.Domain.Models.Body;
using Sorteio.Domain.Models.Common;
using Sorteio.Domain.Models.EntityDomain;
using Sorteio.Domain.Models.NotMapped;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sorteio.Domain.IRepository
{
    public interface ISorteiosRepository : IRepositoryBase<Models.EntityDomain.Sorteio>
    {
        Task<ResultResponseModel> CriarNovoSorteio(SorteioBody sorteioBody);
        Task<IEnumerable<SorteioNotMapped>> ObterTodosSorteio();
        Task<int> FinalizarSorteio(int idSorteio);
    }
}
