using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sorteio.Domain.IBusiness;
using Sorteio.Domain.Models.EntityDomain;
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
        private readonly IFormasDePagamentoBusiness _formasDePagamentoBusiness;

        public FormasDePagamentoController(ITipoFormasDePagamentoBusiness tipoFormasDePagamentoBusiness, IFormasDePagamentoBusiness formasDePagamentoBusiness)
        {
            _tipoFormasDePagamentoBusiness = tipoFormasDePagamentoBusiness;
            _formasDePagamentoBusiness = formasDePagamentoBusiness;
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

        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<JsonResult> CriarNovaFormaDePagamento([FromBody] FormasDePagamento body)
        {
            var resultado = await _formasDePagamentoBusiness.CriarNovaFormaDePagamento(body);
            return Json(new { erro = resultado.erro, mensagem = resultado.mensagem });
        }

    }
}
