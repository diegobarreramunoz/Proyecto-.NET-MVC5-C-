using Newtonsoft.Json;
using pso.cdd.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace pso.cdd.mvc.Areas.Reporte.Controllers
{
    public class ReportesController : MasterController
    {
        // GET: Reporte/Reportes
        public ActionResult Index()
        {
            if (tienePermiso("PERUSUREPSIM")) { return PartialView("denegaPermiso"); }
            var groupCabTs = LoadData ("select DATENAME(mm,fecha) mes,t.nom_tser, sum (horas) horas, COUNT(ts_id) cuenta from cab_ts c inner join tipos_servicios t on c.id_tser=t.id_tser  group by t.nom_tser,DATENAME(mm,fecha)");
            return View(groupCabTs);
        }


        public ActionResult full()
        {
            if (tienePermiso("PERUSUREPFUL")) { return PartialView("denegaPermiso"); }
            return View(_db.cab_ts);
        }


        public ActionResult estados()
        {
            if (tienePermiso("PERUSUESTSER")) { return PartialView("denegaPermiso"); }
            return View(_db.cab_ts);
        }

        public ActionResult Factura()
        {
            ViewBag.id_periodo_ = new SelectList(_db.periodos.Where(p => p.id_tser == 2).Select(p => new SelectListItem
            {
                Text = p.fecha_ini.Day+"/"+p.fecha_ini.Month+"/"+ p.fecha_ini.Year +" Hasta "+ p.fecha_fin.Day + "/" + p.fecha_fin.Month + "/" + p.fecha_fin.Year,
                Value = p.id_periodo.ToString()
            }), "Value", "Text");
            return View();
        }

        public JsonResult Get_tsXperiodo(string ID)
        {
            int _id_periodo = Int32.Parse(ID);
            //int _id_emp = Int32.Parse(id_emp);
            //int _id_tser = Int32.Parse(tser);
            //if (_id_unidad < 0) { _id_unidad = SesionLogin.id_unidad; }
            //var lista = (from ts in _db.cab_ts
            //             from p in _db.periodos
            //             where p.id_periodo== _id_periodo
            //             where ts.fecha>= p.fecha_ini
            //             where ts.fecha <= p.fecha_fin
            //             select ts);

            var carga_datos = LoadData("	select *,(ts.valor_hh*(ts.horas_comerciales - ts.descuento)) as total, CONVERT(VARCHAR(24),ts.fecha,103)as fec," +
                "               case ts.estado when  'ANALI' then '1' end as ANALI, " +
                "               case ts.estado when  'REVI1' then '1' end as REVI1, " +
                "               case ts.estado when  'REVI2' then '1' end as REVI2  " +
                "                from cab_ts ts  " +
                "				inner join empresas emp on ts.id_emp = emp.id_emp " +
                " 				inner join usuarios usu on ts.id_usu = usu.id_usu" +
                "               inner join categorias cat on ts.id_cat = cat.id_cat" +
                "				 CROSS JOIN periodos per " +
                "			where per.id_periodo = "+ID.ToString() +" and ts.fecha >= per.fecha_ini and ts.fecha <= per.fecha_fin ");

            return Json(new { data = carga_datos });
            //return JsonError("Opps, ocurrio un problema");
        }




        public ActionResult ReporteSemanal()
        {
            if (tienePermiso("PERUSUREPSEM")) { return PartialView("denegaPermiso"); }
            return View(new List<Dictionary<string,object>>());
        }

        public JsonResult getReporteSemanal(string group_by1, string group_by2, string order_by, string fecha_fin, string fecha_inicio, string[] filtro_estados)
        {
            if (tienePermiso("PERUSUREPSEM")) { return Json(""); }
            try
            {
                string filtro = "'" + string.Join("','", filtro_estados) + "'";
                var carga_datos = LoadData("select isnull(id_tser,'Todos') as id_tser, " +
                                            "        isnull(grupo, 'Todos') as grupo, " +
                                            "        suma_horas, " +
                                            "        suma_horas_comerciales, " +
                                            "        total_uf, " +
                                            "        cantidad_registros   from( " +
                                            "select case '"+ group_by1 + "' when 'S' then t.nom_tser end as id_tser, " +
                                            "	case '"+ group_by2 + "' when 'U' then u.Nom_usu " +
                                            "        when 'E' then e.nom_emp " +
                                             "       when 'C' then ca.nom_cat end as grupo " +
                                            ", CONVERT(DECIMAL(16,2), sum(c.horas)/60.0)  as suma_horas " +
                                            ", CONVERT(DECIMAL(16,2), sum(case when c.id_tser = 2 then c.horas_comerciales else 0 end)/60.0) as suma_horas_comerciales " +
                                            ", sum(c.valor_hh * c.horas_comerciales) as total_uf " +
                                            ", count(c.ts_id) as cantidad_registros " +
                                            "from cab_ts c " +
                                            "left " +
                                            "join usuarios u on u.id_usu = c.id_usu " +
                                            "left " +
                                            "join empresas e on e.id_emp = c.id_emp " +
                                            "left " +
                                            "join tipos_servicios t on t.id_tser = c.id_tser " +
                                            "left " +
                                            "join categorias ca on ca.id_cat = c.id_cat " +
                                            "where (c.fecha between '"+ fecha_inicio + "' and '"+ fecha_fin + "') " +
                                            "and c.estado in ("+ filtro + ") " +
                                            "group by case '" + group_by1 + "' when 'S' then t.nom_tser end, " +
                                            "		case '" + group_by2 + "' when 'U' then u.Nom_usu " +
                                            "        when 'E' then e.nom_emp " +
                                            "        when 'C' then ca.nom_cat end " +
                                            ") as tb " +
                                            "order by case when '"+ order_by + "' = 'S' and '" + group_by1 + "' = 'S' then id_tser end");
                //return new JavaScriptSerializer().Serialize(new { data = carga_datos });
                return Json(new {data = carga_datos } );
            }
            catch(Exception ex)
            {
                return Json("");
            }
        }

    }
}