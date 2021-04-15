using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sorteio.Domain.IBusiness;
using Sorteio.Domain.Models.Body;
using Sorteio.Domain.Models.EntityDomain;
using Sorteio.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Sorteio.Controllers
{
    public class SorteiosController : Controller
    {
        private readonly ICategoriaSorteioBusiness _categoriaSorteioBusiness;
        private readonly ISorteiosBusiness _sorteiosBusiness;
        private readonly IUsuarioBusiness _usuarioBusiness;

        public SorteiosController(ICategoriaSorteioBusiness categoriaSorteioBusiness, ISorteiosBusiness sorteiosBusiness, IUsuarioBusiness usuarioBusiness)
        {
            _categoriaSorteioBusiness = categoriaSorteioBusiness;
            _sorteiosBusiness = sorteiosBusiness;
            _usuarioBusiness = usuarioBusiness;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Usuario = await _usuarioBusiness.ObterTodosUsuarios();

            var resultado = await _sorteiosBusiness.ObterTodosSorteio();
            return View(resultado);
        }

        public async Task<IActionResult> Novo()
        {
            ViewBag.CategoriaSorteio = await _categoriaSorteioBusiness.ObterTodosCategoriaSorteioAtivo();

            return View();
        }

        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<JsonResult> CriarNovoSorteio([FromBody] SorteioBody body)
        {
            var resultado = await _sorteiosBusiness.CriarNovoSorteio(body);

            return Json(new { erro = resultado.erro, mensagem = resultado.mensagem});
        }

        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<JsonResult> FinalizarSorteio([FromBody] VencedorSorteio body)
        {
            var resultado = await _sorteiosBusiness.FinalizarSorteio(body);

            return Json(new { erro = resultado.erro, mensagem = resultado.mensagem });
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<JsonResult> ObterTodosSorteio(int idSorteio)
        {
            var resultado = await _sorteiosBusiness.ObterTodosSorteio();
            return Json(new { resultado });
        }
    }
}
