using AutoMapper;
using Sorteio.Data.EntityData;
using Sorteio.Domain.Models.EntityDomain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sorteio.CrossCutting.MappingGroups
{
    public class DomainToData : Profile
    {
        public DomainToData()
        {
            CreateMap<CategoriaSorteio, CategoriaSorteioData>();
            CreateMap<TipoFormaDePagamento, TipoFormaDePagamentoData>();
        }
    }
}
