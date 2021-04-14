using Sorteio.Domain.Business.Base;
using Sorteio.Domain.IBusiness;
using Sorteio.Domain.IRepository;
using Sorteio.Domain.IRepository.Base;
using Sorteio.Domain.Models.Body;
using Sorteio.Domain.Models.Common;
using Sorteio.Domain.Models.EntityDomain;
using Sorteio.Domain.Models.NotMapped;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sorteio.Domain.Business
{
    public class SorteiosBusiness : BusinessBase<Models.EntityDomain.Sorteio>, ISorteiosBusiness
    {
        private readonly ISorteiosRepository _sorteiosRepository;

        public SorteiosBusiness(ISorteiosRepository sorteiosRepository) : base(sorteiosRepository)
        {
            _sorteiosRepository = sorteiosRepository;
        }

        public Task<ResultResponseModel> CriarNovoSorteio(SorteioBody sorteioBody)
            => _sorteiosRepository.CriarNovoSorteio(sorteioBody);

        public Task<int> FinalizarSorteio(int idSorteio)
            => _sorteiosRepository.FinalizarSorteio(idSorteio);

        public Task<IEnumerable<SorteioNotMapped>> ObterTodosSorteio()
            => _sorteiosRepository.ObterTodosSorteio();
    }
}
