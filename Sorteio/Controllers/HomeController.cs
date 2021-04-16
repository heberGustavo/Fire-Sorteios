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

        public HomeController(ISorteiosBusiness sorteiosBusiness)
        {
            _sorteiosBusiness = sorteiosBusiness;
        }

        public async Task<IActionResult> Index()
        {
            ////// AQUIII
            //var resultado = await _sorteiosBusiness.ObterInformacoesSorteio();
            return View();
        }
        
        [Route("Sorteio")]
        public IActionResult Sorteio()
        {
            return View();
        }
    }
}
