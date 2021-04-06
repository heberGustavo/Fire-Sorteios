using Microsoft.Extensions.DependencyInjection;
using Sorteio.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sorteio.CrossCutting.DependencyGroups
{
    public class DataDependencyInjection
    {
        public static void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<SqlDataContext, SqlDataContext>();
        }
    }
}
