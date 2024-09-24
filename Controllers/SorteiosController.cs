using SorteioWebApplication.Models;
using SorteioWebApplication.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace SorteioWebApplication.Controllers
{
    public class SorteiosController : Controller
    {
        // GET: SorteiosController
        public ActionResult Index()
        {
            List<Sorteios> listaSorteios;
            ExecSorteios.RunAsync(ExecSorteios.enum_opcao.enumGet).GetAwaiter().GetResult();

            listaSorteios = ExecSorteios.ListaSorteiosItens.Select(i => i).ToList<Sorteios>();

            return View(listaSorteios);
        }

        // GET: SorteiosController/Details/5
        public ActionResult Details(int id)
        {
            ExecSorteios.RunAsync(ExecSorteios.enum_opcao.enumGet).GetAwaiter().GetResult();
            Sorteios? Sorteios = ExecSorteios.ListaSorteiosItens
                                .Select(i => i)
                                .ToList<Sorteios>()
                                .Where(i => i.NumeroDoSorteio == id)
                                .FirstOrDefault<Sorteios>();
            return View(Sorteios);
        }

    }
}
