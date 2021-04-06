using Microsoft.Extensions.DependencyInjection;
using Sorteio.Domain.IBusiness.Migration;
using Sorteio.Migration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sorteio.CrossCutting.DependencyGroups
{
    public class DomainDependencyInjection
    {
        public static void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IMigrationBusiness, MigrationBusiness>();
        }
    }
}
