using pso.cdd.model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pso.cdd.mvc.Areas.Mantencion.Controllers
{
    public class PerfilUsuarioController : MasterController
    {
        // GET: Mantencion/PerfilUsuario
        public ActionResult Index()
        {
            if (tienePermiso("PERUSUMANUSU")) { return PartialView("denegaPermiso"); }
            var perfiles_usuarios = _db.perfil_usuario.OrderByDescending(item => item.perfil);
            return View(perfiles_usuarios.ToList());
        }


        public ActionResult Create(string tipo)
        {
            //validacion de permisos
            if (tienePermiso("PERUSUMANUSU")) { return PartialView("denegaPermiso"); }
            ViewBag.perfil = new SelectList(_db.parametros.Where(item => item.grupo== "PERUSU").OrderByDescending(item => item.texto), "valor", "texto");
            ViewBag.id_usuario = new SelectList(_db.usuarios.OrderByDescending(item => item.Nom_usu), "id_usu", "Nom_usu");
            return PartialView("Form");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(perfil_usuario model)
        {
            if (tienePermiso("PERUSUMANUSU")) { return PartialView("denegaPermiso"); }
            try
            {
                if (ModelState.IsValid)
                {
                    _db.perfil_usuario.Add(model);
                    _db.SaveChanges();
                    return JsonExito();
                }
                Selectores(model);
                return JsonError("Opps, ocurrio un problema");
            }
            catch (Exception err)
            {
                return JsonError("Opps, ocurrio un problema");
            }

        }

        private void Selectores(perfil_usuario model)
        {
            ViewBag.perfil = new SelectList(_db.parametros.Where(item => item.grupo == "PERUSU").OrderByDescending(item => item.texto), "valor", "texto",model.perfil);
            ViewBag.id_usuario = new SelectList(_db.usuarios.OrderByDescending(item => item.Nom_usu), "id_usu", "Nom_usu",model.id_usuario);
        }

    }
}