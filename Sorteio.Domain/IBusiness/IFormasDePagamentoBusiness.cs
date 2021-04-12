using Sorteio.Domain.IBusiness.Base;
using Sorteio.Domain.Models.Common;
using Sorteio.Domain.Models.EntityDomain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sorteio.Domain.IBusiness
{
    public interface IFormasDePagamentoBusiness : IBusinessBase<FormasDePagamento>
    {
        Task<ResultResponseModel> CriarNovaFormaDePagamento(FormasDePagamento formasDePagamento);
    }
}
