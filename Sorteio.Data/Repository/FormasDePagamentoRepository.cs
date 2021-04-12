using AutoMapper;
using Dapper;
using Sorteio.Data.EntityData;
using Sorteio.Data.Repository.Base;
using Sorteio.Domain.IRepository;
using Sorteio.Domain.Models.Common;
using Sorteio.Domain.Models.EntityDomain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sorteio.Data.Repository
{
    public class FormasDePagamentoRepository : RepositoryBase<FormasDePagamento, FormasDePagamentoData>, IFormasDePagamentoRepository
    {
        public FormasDePagamentoRepository(SqlDataContext dataContext, IMapper mapper) : base(dataContext, mapper)
        {
        }

        public Task<int> ExcluirFormaDePagamento(int idFormaDePagamento)
            => _dataContext.Connection.ExecuteAsync(@"UPDATE FormasDePagamento 
                                                      SET status = 1
                                                      WHERE id_forma_de_pagamento = @idFormaDePagamento", 
                                                      new { idFormaDePagamento});
    }
}
