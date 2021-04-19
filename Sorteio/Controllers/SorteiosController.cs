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

        [HttpGet]
        [Route("[controller]/[action]/{idSorteio:int}")]
        public async Task<IActionResult> Editar(int idSorteio)
        {
            ViewBag.CategoriaSorteio = await _categoriaSorteioBusiness.ObterTodosCategoriaSorteioAtivo();
            ViewBag.Usuario = await _usuarioBusiness.ObterTodosUsuarios();

            var resultado = await _sorteiosBusiness.ObterSorteioPorId(idSorteio);
            
            return View(resultado);
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
        public async Task<JsonResult> EditarSorteio([FromBody] SorteioBody body)
        {
            var resultado = await _sorteiosBusiness.EditarSorteio(body);
            return Json(new { erro = resultado.erro, mensagem = resultado.mensagem });
        }

        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<JsonResult> EditarFinalizarSorteio([FromBody] VencedorSorteio body)
        {
            var resultado = await _sorteiosBusiness.EditarFinalizarSorteio(body);

            if (resultado)
                return Json(new { erro = false, mensagem = "Dados atualizados!" });
            else
                return Json(new { erro = true, mensagem = "Erro ao atualizar dados. Tente novamente!" });
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
        public async Task<JsonResult> ObterTodosSorteio()
        {
            var resultado = await _sorteiosBusiness.ObterTodosSorteio();
            return Json(new { resultado });
        }

        [HttpGet]
        [Route("[controller]/[action]/{idCategoria}")]
        public async Task<JsonResult> FiltrarSorteioPorCategoria(int idCategoria)
        {
            var resultado = await _sorteiosBusiness.FiltrarSorteioPorCategoria(idCategoria);
            return Json(resultado);
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<JsonResult> ObterTodosUltimosSorteiosRealizados()
        {
            var resultado = await _sorteiosBusiness.ObterTodosUltimosSorteiosRealizados();
            return Json(resultado);
        }
    }
}
