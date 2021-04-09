using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sorteio.Domain.IBusiness;
using Sorteio.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Sorteio.Controllers
{
    public class FormasDePagamentoController : Controller
    {
        private readonly ITipoFormasDePagamentoBusiness _tipoFormasDePagamentoBusiness;

        public FormasDePagamentoController(ITipoFormasDePagamentoBusiness tipoFormasDePagamentoBusiness)
        {
            _tipoFormasDePagamentoBusiness = tipoFormasDePagamentoBusiness;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> NovaFormaDePagamento()
        {
            ViewBag.TipoFormaDePagamento = await _tipoFormasDePagamentoBusiness.ObterTodasFormasDePagamentoAtiva();
            return View();
        }

    }
}
