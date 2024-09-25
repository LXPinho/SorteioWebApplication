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

        // GET: SorteiosController
        public ActionResult Create()
        {
            ExecSorteios.RunAsync(ExecSorteios.enum_opcao.enumCreate).GetAwaiter().GetResult();
            return RedirectToAction("Index");
        }

        // GET: SorteiosController
        public ActionResult Details(int id)
        {
            ExecSorteios.RunAsync(ExecSorteios.enum_opcao.enumGet).GetAwaiter().GetResult();
            Sorteios? sorteios = ExecSorteios.ListaSorteiosItens
                                .Select(i => i)
                                .ToList<Sorteios>()
                                .Where(i => i.NumeroDoSorteio == id)
                                .FirstOrDefault<Sorteios>();
            return View(sorteios);
        }

        // GET: SorteiosController
        public ActionResult Delete(int id)
        {
            ExecSorteios.LimpaSorteosItem();
            ExecSorteios.SorteiosItem.NumeroDoSorteio = id;
            ExecSorteios.RunAsync(ExecSorteios.enum_opcao.enumDelete).GetAwaiter().GetResult();
            return RedirectToAction("Index");
        }
    }
}
