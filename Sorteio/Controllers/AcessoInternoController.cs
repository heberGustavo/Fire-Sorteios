using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sorteio.Domain.IBusiness;
using Sorteio.Domain.Models.EntityDomain;
using Sorteio.Models;
using Sorteio.Portal.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Sorteio.Controllers
{
    [Authorize(Policy = PolicyKeys.USUARIO_LOGADO)]
    public class AcessoInternoController : Controller
    {
        private readonly ISorteiosBusiness _sorteiosBusiness;
        private readonly IUsuarioBusiness _usuarioBusiness;
        private readonly IFormasDePagamentoBusiness _formasDePagamentoBusiness;

        public AcessoInternoController(ISorteiosBusiness sorteiosBusiness, IUsuarioBusiness usuarioBusiness, IFormasDePagamentoBusiness formasDePagamentoBusiness)
        {
            _sorteiosBusiness = sorteiosBusiness;
            _usuarioBusiness = usuarioBusiness;
            _formasDePagamentoBusiness = formasDePagamentoBusiness;
        }

        public async Task<IActionResult> Index()
        {
            var usuarioLogado = AuthHelper.USUARIO_LOGADO();

            ViewBag.UsuarioLogado = usuarioLogado;

            ViewBag.FormasDePagamentos = await _formasDePagamentoBusiness.ObterTodasFormasDePagamentoAtivo();
            ViewBag.MeusBilhetes = await _sorteiosBusiness.ObterSorteiosBilheteClientePorId(usuarioLogado.id_usuario);
            ViewBag.MeusPremios = await _sorteiosBusiness.ObterMeusPremiosClientePorId(usuarioLogado.id_usuario);

            if (usuarioLogado.data_de_nascimento.Year >= 1761)
                ViewBag.DataDeNascimento = usuarioLogado.data_de_nascimento.ToString("yyyy-MM-dd");
            else
                ViewBag.DataDeNascimento = null;

            return View();
        }

        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<JsonResult> EditarDadosCliente([FromBody] Usuario usuario)
        {
            var resultado = await _usuarioBusiness.EditarDadosCliente(usuario);

            if (resultado == 1)
                return Json(new { erro = false, mensagem = "Dados atualizados com sucesso. Em breve será atualizado no portal!" });
            else
                return Json(new { erro = true, mensagem = "Erro ao atualizar dados. Tente novamente!" });
        }
    }
}
