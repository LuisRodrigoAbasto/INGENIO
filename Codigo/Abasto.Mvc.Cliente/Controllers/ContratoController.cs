using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Abasto.Mvc.Cliente.Controllers
{
    public class ContratoController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Contrato";

            return View();
        }
    }
}
