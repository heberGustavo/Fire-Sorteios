using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sorteio.Domain.IBusiness;
using Sorteio.Models;
using Sorteio.Portal.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Sorteio.Controllers
{
    public class AcessoInternoController : Controller
    {
        private readonly ISorteiosBusiness _sorteiosBusiness;

        public AcessoInternoController(ISorteiosBusiness sorteiosBusiness)
        {
            _sorteiosBusiness = sorteiosBusiness;
        }

        public async Task<IActionResult> Index()
        {
            var usuarioLogado = AuthHelper.USUARIO_LOGADO();

            ViewBag.NomeUsuarioLogado = usuarioLogado.nome;
            ViewBag.MeusBilhetes = await _sorteiosBusiness.ObterSorteiosBilheteClientePorId(usuarioLogado.id_usuario);
            ViewBag.MeusPremios = await _sorteiosBusiness.ObterMeusPremiosClientePorId(usuarioLogado.id_usuario);

            return View();
        }
    }
}
