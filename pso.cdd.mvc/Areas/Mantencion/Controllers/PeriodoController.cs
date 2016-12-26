using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using pso.cdd.model;

namespace pso.cdd.mvc.Areas.Mantencion.Controllers
{
    public class PeriodoController : MasterController
    {
        // GET: periodo/Periodo
        public ActionResult Index()
        {
            if (tienePermiso("PERUSUMANPER")) { return PartialView("denegaPermiso"); }

            var periodos_vigentes = LoadData("(select id_periodo, id_tser, CONVERT(varchar(10),fecha_ini, 110) as fecha_ini, CONVERT(varchar(10),fecha_fin, 110) as fecha_fin, rank_, vigente from " +
                                        "(select *, " +
                                        "    (rank() over(partition by id_tser order by fecha_ini desc, fecha_fin desc)) as 'rank_', " +
                                        "    case when(not(getdate() between fecha_ini and fecha_fin) " +

                                        "and getdate() > fecha_fin) then 'N' " +

                                        "when(getdate() between fecha_ini and fecha_fin) then 'S' " +
                                        "else '' end as 'vigente' " +
                                        "from periodos " +
                                        "where (not(getdate() between fecha_ini and fecha_fin) " +

                                        "and getdate() > fecha_fin) " +

                                        "or(getdate() between fecha_ini and fecha_fin)) as tb " +
                                    "where tb.rank_ < 2)");
            ViewBag.periodos_vigentes = periodos_vigentes;
            foreach(var item in periodos_vigentes)
            {
            }
            return View(_db.periodos.ToList());
            // return View();
        }


        // GET: periodo/Periodo/Create
        public ActionResult Create()
        {
            if (tienePermiso("PERUSUMANPER")) { return PartialView("denegaPermiso"); }

            ViewBag.Fec_Max = ((DateTime)((_db.periodos.Where(p => p.id_tser == 1)).Max(x => x.fecha_fin))).AddDays(1);
            ViewBag.id_tser = new SelectList(_db.tipos_servicios.OrderBy(item => item.nom_tser), "id_tser", "nom_tser");
            return PartialView("Create");
        }

        // POST: periodo/Periodo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(cdd.model.periodos model)
        {
            if (tienePermiso("PERUSUMANPER")) { return PartialView("denegaPermiso"); }
            model.usuario_mod = SesionLogin.Nom_cor_usu.ToString();
            model.fecha_mod = System.DateTime.Now;
            model.estado = true;
            ((DateTime)model.fecha_fin).AddMilliseconds(86400000-1);
            if (ModelState.IsValid)
            {
                model.usuario_mod = SesionLogin.Nom_cor_usu.ToString();
                model.fecha_mod = System.DateTime.Now;
                model.estado = true;
                _db.periodos.Add(model);
                _db.SaveChanges();
                return JsonExito();
            }
            Selectores(model);
            return JsonError("");
        }

        // GET: periodo/Periodo/Edit/5
        public ActionResult Edit(int id)
        {
            if (tienePermiso("PERUSUMANPER")) { return PartialView("denegaPermiso"); }
            var model = _db.periodos.FirstOrDefault(p => p.id_periodo == id);
            if (model == null) return RedirectToAction("Create");
            //model.fecha_ini = model.fecha_ini.ToLocalTime();
            Selectores(model);
            return PartialView("Create", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(periodos model)
        {
            if (tienePermiso("PERUSUMANPER")) { return PartialView("denegaPermiso"); }
            model.usuario_mod = SesionLogin.Nom_cor_usu.ToString();
            model.fecha_mod = System.DateTime.Now;
            model.estado = true;
            if (ModelState.IsValid)
            {
                _db.periodos.Attach(model);
                _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
                return JsonExito();
            }
            Selectores(model);
            return JsonError("");
        }





        // GET: periodo/Periodo/Delete
        public ActionResult Delete(int id)
        {
            if (tienePermiso("PERUSUMANPER")) { return PartialView("denegaPermiso"); }
            var model = _db.periodos.FirstOrDefault(p => p.id_periodo == id);
            if (model == null) return JsonError("No existe el identificador solicitado");
            _db.periodos.Remove(model);
            _db.SaveChanges();
            return JsonExito();
        }



        private void DisplaySuccessMessage(string msgText)
        {
            TempData["SuccessMessage"] = msgText;
        }

        private void DisplayErrorMessage()
        {
            TempData["ErrorMessage"] = "Save changes was unsuccessful.";
        }

        private void Selectores(periodos model)
        {
            //carga objetos SelectList, para elementos select html, en el "modelo" asociado a la vista "Form"
            ViewBag.id_tser = new SelectList(_db.tipos_servicios.OrderBy(item => item.nom_tser), "id_tser", "nom_tser");
        }

        public JsonResult GetFechaMax(string id_tser)
        {
            try
            {
                int _id_tser = Int32.Parse(id_tser);
                string Lista = (((DateTime)((_db.periodos.Where(p => p.id_tser == _id_tser)).Max(x => x.fecha_fin))).AddDays(1)).ToString("yyyy/MM/dd 00:00:00");
                return Json(Lista);
            }
            catch (Exception e)
            {
                string date = "2016-01-01";
                string fecha = Convert.ToDateTime(date).ToString();
                return Json(date);
            }
            
        }
        public JsonResult GetFechas(string id_tser)
        {
            try
            {
                var vars = LoadData("(select id_periodo, id_tser, CONVERT(varchar(10),fecha_ini, 110) as fecha_ini, CONVERT(varchar(10),fecha_fin, 110) as fecha_fin, rank_, vigente from " +
                                        "(select *, " +
                                        "    (rank() over(partition by id_tser order by fecha_ini desc, fecha_fin desc)) as 'rank_', " +
                                        "    case when(not(getdate() between fecha_ini and fecha_fin) " +

                                        "and getdate() > fecha_fin) then 'N' " +

                                        "when(getdate() between fecha_ini and fecha_fin) then 'S' " +
                                        "else '' end as 'vigente' " +
                                        "from periodos " +
                                        "where (not(getdate() between fecha_ini and fecha_fin) " +

                                        "and getdate() > fecha_fin) " +

                                        "or(getdate() between fecha_ini and fecha_fin)) as tb " +
                                    "where tb.rank_ < 2  and id_tser = "+Int32.Parse(id_tser)+")");
                return Json(vars);
            }catch(Exception e)
            {
                return Json("");
            }
        }

    }
}