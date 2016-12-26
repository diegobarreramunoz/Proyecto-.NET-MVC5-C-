using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pso.cdd.mvc.Areas.Mantencion.Controllers
{
    public class MantenedoresController : MasterController
    {
        // GET: Mantencion/Mantenedores
        public ActionResult Index()
        {
            return View();
        }
    }
}