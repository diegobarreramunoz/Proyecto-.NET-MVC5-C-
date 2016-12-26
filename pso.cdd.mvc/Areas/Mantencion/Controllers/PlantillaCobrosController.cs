using pso.cdd.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;

namespace pso.cdd.mvc.Areas.Mantencion.Controllers
{
    public class PlantillaCobrosController : MasterController
    {
        // GET: Mantencion/PlantillaCobros
        public ActionResult Index()
        {
            //validacion de permisos
            if (tienePermiso("PERUSUMANPLC")) { return PartialView("denegaPermiso"); }
            //carga variable con un List array de objetos valor_hh
            var valor_hh = _db.valor_hh.Where(item => item.estado==true).Include(v => v.cargos).Include(v => v.empresas).Include(v => v.unidades);
            return View(valor_hh.ToList());
        }


        // GET: Ingreso/IngresoHoras
        public ActionResult Create()
        {
            //validacion de permisos
            if (tienePermiso("PERUSUMANPLC")) { return PartialView("denegaPermiso"); }
            //carga objetos SelectList, para elementos select html, en el "modelo" asociado a la vista "Form" 
            ViewBag.id_cargo = new SelectList(_db.cargos.OrderBy(item => item.nom_cargo), "id_cargo", "nom_cargo");
            ViewBag.id_emp = new SelectList(_db.empresas.OrderBy(item => item.nom_emp), "id_emp", "nom_emp");
            ViewBag.id_unidad = new SelectList(_db.unidades.OrderBy(item => item.nom_unidad), "id_unidad", "nom_unidad");
            return PartialView("Form");
        }



        //se declara explicitamente que este action sera invocado de manera POST
        [HttpPost]
        //se realiza validacion de token, asociado a la cookie del cliente (Seguridad)
        [ValidateAntiForgeryToken]
        public ActionResult Create(valor_hh model)
        {
            //validacion de permisos
            if (tienePermiso("PERUSUMANPLC")) { return PartialView("denegaPermiso"); }

            //se realiza validacion de campos del modelo obtenidos desde el formulario
            ModelState.Remove("valor");
            if (ModelState.IsValid)
            {
                //se modifican atributos del modelo
                model.usuario_mod = SesionLogin.Nom_cor_usu;
                model.fecha_mod = DateTime.Now;
                model.estado = true;
                try
                {
                    //se agrega modelo a la entidad
                    _db.valor_hh.Add(model);
                    //se guardan los cambios
                    _db.SaveChanges();
                }
                catch (Exception err)
                {
                    //captura excepcion en caso de existir registro duplicado (primary key) en la tabla de la base de datos
                    if ((err.InnerException.InnerException).GetType().ToString().Equals("System.Data.SqlClient.SqlException") && ((SqlException)(err.InnerException.InnerException)).ErrorCode == -2146232060)
                    {
                        return JsonError("Ya existe un registro con estos datos");
                    }
                    //captura cualquier otra excepcion
                    return JsonError("Opps, ocurrio un problema");
                }
                return JsonExito();
            }
            //en caso de existir un campo invalido, lo captura la validacion del modelo, recarga los selectList y carga nuevamente el formulario
            Selectores(model);
            return JsonError("Opps, ocurrio un problema");
        }

        public ActionResult Edit(int id)
        {
            //carga formulario en caso de edicion
            if (tienePermiso("PERUSUMANPLC")) { return PartialView("denegaPermiso"); }
            var model = _db.valor_hh.FirstOrDefault(p => p.id_valor == id);
            if (model == null) return RedirectToAction("Create");
            Selectores(model);
            //retorna el formulario con el modelo asociado, para edicion
            return PartialView("Form", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(valor_hh model)
        {
            //edita y guarda cambios al enviarse el formulario
            if (tienePermiso("PERUSUMANPLC")) { return PartialView("denegaPermiso"); }
            if (ModelState.IsValid)
            {
                _db.valor_hh.Attach(model);
                _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
                return JsonExito();
            }
            Selectores(model);
            return JsonError("Opps, ocurrio un problema");
        }

        public ActionResult Delete(int id)
        {
            if (tienePermiso("PERUSUMANPLC")) { return PartialView("denegaPermiso"); }
            var model = _db.valor_hh.FirstOrDefault(p => p.id_valor == id);
            if (model == null) return JsonError("No existe el identificador solicitado");
            try
            {

                /*Eliminacion Logica*/
                model.estado = false;

                /*Eliminacion Fisica*/
                //_db.valor_hh.Remove(model);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                return JsonError("Opps, ocurrio un problema");
            }
            return JsonExito();
        }

        private void Selectores(valor_hh model)
        {
            //carga objetos SelectList, para elementos select html, en el "modelo" asociado a la vista "Form"
            ViewBag.id_cargo = new SelectList(_db.cargos.OrderBy(item => item.nom_cargo), "id_cargo", "nom_cargo");
            ViewBag.id_emp = new SelectList(_db.empresas.OrderBy(item => item.nom_emp), "id_emp", "nom_emp");
            ViewBag.id_unidad = new SelectList(_db.unidades.OrderBy(item => item.nom_unidad), "id_unidad", "nom_unidad");
        }

        //Metodo para obtener especificamente 1 Country by Id valor retornado como Json
        //public JsonResult GetStateByIdCountry(int IdCountry)
        //{
        //    return Json(new SelectList(lstState.Where(x => x.IdCountry == IdCountry).ToList(), "IdState", "Name"));
        //}
    }
}