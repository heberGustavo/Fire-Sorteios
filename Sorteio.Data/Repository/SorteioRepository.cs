using AutoMapper;
using Sorteio.Data.Repository.Base;
using Sorteio.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sorteio.Data.Repository
{
    public class SorteioRepository : RepositoryBase<Domain.Models.EntityDomain.Sorteio, Data.EntityData.SorteioData>, ISorteioRepository
    {
        public SorteioRepository(SqlDataContext dataContext, IMapper mapper) : base(dataContext, mapper)
        {
        }
    }
}
