using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sorteio.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Sorteio.Controllers
{
    public class AcessoInternoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Sorteios()
        {
            return View();
        }

        public IActionResult NovoSorteio()
        {
            return View();
        }

    }
}
