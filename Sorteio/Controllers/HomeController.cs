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
    public class HomeController : Controller
    {
        private readonly ISorteiosBusiness _sorteiosBusiness;
        private readonly ICategoriaSorteioBusiness _categoriaSorteioBusiness;

        public HomeController(ISorteiosBusiness sorteiosBusiness, ICategoriaSorteioBusiness categoriaSorteioBusiness)
        {
            _sorteiosBusiness = sorteiosBusiness;
            _categoriaSorteioBusiness = categoriaSorteioBusiness;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.CategoriaSorteio = await _categoriaSorteioBusiness.ObterTodosCategoriaSorteioAtivo();

            var resultado = await _sorteiosBusiness.ObterInformacoesSorteio();
            return View(resultado);
        }
        
        [Route("Sorteio")]
        public IActionResult Sorteio()
        {
            return View();
        }
    }
}
