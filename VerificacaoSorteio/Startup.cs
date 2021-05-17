using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Sorteio.Domain.Business;
using Sorteio.Domain.IBusiness;
using System;
using System.Collections.Generic;
using System.Text;
using VerificacaoSorteio;

[assembly: FunctionsStartup(typeof(Startup))]
namespace VerificacaoSorteio
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<ISorteiosBusiness, SorteiosBusiness>();
        }
    }
}
