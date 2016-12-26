using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pso.cdd.mvc.Areas.Mantencion.Controllers
{
    public class UsuariosController : MasterController
    {
        // GET: Mantencion/Usuarios
        public ActionResult Index()
        {
            if (tienePermiso("PERUSUMANUSU")) { return PartialView("denegaPermiso"); }
            return View();
        }
    }
}