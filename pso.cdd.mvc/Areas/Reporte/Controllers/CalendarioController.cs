using pso.cdd.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pso.cdd.mvc.Areas.Reporte.Controllers
{
    public class CalendarioController : MasterController {

        // GET: Reporte/Calendario
        public ActionResult CalendarioTareas()
        {
            return PartialView(_db.avisos.ToList());
        }
        public ActionResult CalendarioHorasDias()
        {

            var groupCabTs = LoadData("	select CONVERT( varchar(10) , fecha  , 121) as fecha ,sum(horas) as horas from cab_ts where id_usu=" + SesionLogin.id_usu + " group by CONVERT( varchar(10) , fecha  , 121)");

            ViewBag.TS = groupCabTs;
            return View(groupCabTs);

        }

       

    }
}