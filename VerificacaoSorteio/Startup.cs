using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Sorteio.Data;
using Sorteio.Data.Repository;
using Sorteio.Domain.Business;
using Sorteio.Domain.IBusiness;
using Sorteio.Domain.IRepository;
using Sorteio.Migration;
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
            builder.Services.AddScoped<SqlConnectionInFunction>();

            builder.Services.AddTransient<ISorteiosBusiness, SorteiosBusiness>();
            builder.Services.AddTransient<ISorteiosRepository, SorteiosRepository>();
        }
    }
}