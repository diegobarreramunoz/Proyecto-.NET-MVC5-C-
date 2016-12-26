using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;

namespace pso.cdd.mvc.Areas.Mantencion.Controllers
{
    public class AvisosController : MasterController
    {

        // GET: avisos/avisos
        public ActionResult Index()
        {
            if (tienePermiso("PERUSUMANAVI")) { return PartialView("denegaPermiso"); }
            return View(_db.avisos.ToList());
            //return View();
        }

        public ActionResult Avisos_Create()
        {
            if (tienePermiso("PERUSUMANAVI")) { return PartialView("denegaPermiso"); }
            return View("Form");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(cdd.model.avisos model)
        {
            if (tienePermiso("PERUSUMANAVI")) { return PartialView("denegaPermiso"); }
            model.usuario_mod = SesionLogin.Nom_cor_usu.ToString();
            model.fecha_mod = System.DateTime.Now;
            model.msg = model.msg.Replace("\r\n", "</br>");
            model.estado = true;
            if (ModelState.IsValid)
            {
                model.usuario_mod = SesionLogin.Nom_cor_usu.ToString();
                model.fecha_mod = System.DateTime.Now;
                model.estado = true;
                _db.avisos.Add(model);
                _db.SaveChanges();
                return JsonExito();
            }
            //Selectores(model);
            return JsonError("Opps, ocurrio un problema");
        }

        public ActionResult Edit(int id)
        {
            if (tienePermiso("PERUSUMANAVI")) { return PartialView("denegaPermiso"); }
            var model = _db.avisos.FirstOrDefault(p => p.id_aviso == id);
            if (model == null) return RedirectToAction("Create");
            //model.fecha_ini = model.fecha_ini.ToLocalTime();
            //Selectores(model);
            return PartialView("Form", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(cdd.model.avisos model)
        {
            if (tienePermiso("PERUSUMANAVI")) { return PartialView("denegaPermiso"); }
            model.usuario_mod = SesionLogin.Nom_cor_usu.ToString();
            model.fecha_mod = System.DateTime.Now;
            model.msg = model.msg.Replace("\r\n", "</br>");
            model.estado = true;
            if (ModelState.IsValid)
            {
                _db.avisos.Attach(model);
                _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
                return JsonExito();
            }
            // Selectores(model);
            return JsonError("Opps, ocurrio un problema");
        }


        public ActionResult Delete(int id)
        {
            if (tienePermiso("PERUSUMANAVI")) { return PartialView("denegaPermiso"); }
            var model = _db.avisos.FirstOrDefault(p => p.id_aviso == id);
            if (model == null) return JsonError("No existe el identificador solicitado");
            _db.avisos.Remove(model);
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




    }
}