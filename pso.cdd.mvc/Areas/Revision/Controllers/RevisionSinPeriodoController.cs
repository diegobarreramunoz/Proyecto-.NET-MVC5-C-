using pso.cdd.model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pso.cdd.mvc.Areas.Revision.Controllers
{
    public class RevisionSinPeriodoController : MasterController
    {
        // GET: Revision/RevisionSinPeriodo
        public ActionResult Index()
        {
            if (tienePermiso("PERUSUREVFAC")) { return PartialView("denegaPermiso"); }
            string jerarquia = SesionLogin.jerarquia;
            var list = LoadData("select c.*, (c.valor_hh*c.horas_comerciales) as sub_total, (c.valor_hh*(c.horas_comerciales - c.descuento)) as total, " +
                                        "ISNULL(emp.nom_emp, '') as nom_emp, " +
                                        "ISNULL(cat.nom_cat, '') as nom_cat, " +
                                        "ISNULL(tip.nom_tser, '') as nom_tser, " +
                                        "ISNULL(usu.Nom_usu, '') as Nom_usu, " +
                                        "un.nom_unidad " +
                                                        "from cab_ts c " +
                                                        "inner "+
                                                        "join (select * from " +
                                                        "    (select *, " +
                                                        "        (rank() over(partition by id_tser order by fecha_ini desc, fecha_fin desc)) as 'rank_' " +
                                                        "    from periodos " +
                                                        "    where not(getdate() between fecha_ini and fecha_fin) " +
                                                        "           and getdate() > fecha_fin) as tb " +
                                                        "where tb.rank_ < 2) " +
                                                        " p on (c.fecha between p.fecha_ini and p.fecha_fin) and(p.id_tser = c.id_tser) "+
                                                         "left join empresas emp on emp.id_emp = c.id_emp "+
                                                         "left join categorias cat on cat.id_cat = c.id_cat "+
                                                         "left join tipos_servicios tip on tip.id_tser = c.id_tser "+
                                                         "left join usuarios usu on usu.id_usu = c.id_usu "+
                                                         "left join unidades un on un.id_unidad = usu.id_unidad " +
                                                        "where((c.estado = 'ANALI' and 'REVI1' = '" + jerarquia + "') " +
                                                        "    	or (c.estado in ('REVI1','ANALI') and 'REVI2' = '" + jerarquia + "') "+
                                                        "    or(c.estado = 'REVI2' and 'REVI3' = '" + jerarquia + "') " +
                                                        "    or(c.estado = 'REVI3' and 'REVI4' = '" + jerarquia + "') " +
                                                        "    or(c.estado = 'REVI4' and 'REVI5' = '" + jerarquia + "'))and  c.id_tser = 2 and  c.horas_comerciales > 0  ");
            ViewBag.jerarquia = jerarquia;
            return View(list);
        }

        public ActionResult Aprove(string id_string, string[] ids_string)
        {
            if (tienePermiso("PERUSUREVFAC")) { return PartialView("denegaPermiso"); }
            if (id_string == null)
            {
                if (ids_string != null || !ids_string.Any())
                {
                    foreach (var item in ids_string)
                    {
                        if (!aprove_change(item)) return JsonError("Opps, ocurrio un problema");
                    }
                    return JsonExito();
                }
                return JsonError("Opps, ocurrio un problema");

            }
            else if(!id_string.Equals(""))
            {
                if (aprove_change(id_string)) return JsonExito();
                else return JsonError("Opps, ocurrio un problema");
            }
            else
            {
                return JsonError("Opps, ocurrio un problema");
            }
        }


        private Boolean aprove_change(string id_string) {
            try
            {
                int id = Int32.Parse(id_string);
                cab_ts cab_ts = _db.cab_ts.Single(item => item.ts_id == id);
                string jerarquia = SesionLogin.jerarquia;
                cab_ts.estado = jerarquia;
                if (ModelState.IsValid)
                {
                    _db.cab_ts.Attach(cab_ts);
                    _db.Entry(cab_ts).State = System.Data.Entity.EntityState.Modified;
                    _db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public ActionResult Create()
        {
            //validacion de permisos
            if (tienePermiso("PERUSUREVFAC")) { return PartialView("denegaPermiso"); }
            int usuario_id = SesionLogin.id_usu;
            ViewBag.id_emp_list = new SelectList(_db.empresas.OrderBy(item => item.nom_emp), "id_emp", "nom_emp");
            ViewBag.id_cat_list = new SelectList(Enumerable.Empty<SelectListItem>(), "id_cat", "nom_cat");
            ViewBag.id_usuarios_list = new MultiSelectList(_db.usuarios.Where(item => item.id_usu != SesionLogin.id_usu), "id_usu", "Nom_usu");
            ViewBag.id_tser_list = new SelectList(_db.tipos_servicios, "id_tser", "nom_tser");
            ViewBag.usu_id_unidad = SesionLogin.id_unidad;
            ViewBag.jerarquia = SesionLogin.jerarquia;
            return PartialView("Form");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(cab_ts model/*, string[] usuarios_grupales*/, string horas)
        {
            if (tienePermiso("PERUSUREVFAC")) { return PartialView("denegaPermiso"); }
            try
            {
                DateTime fecha_actual = DateTime.Now;
                DateTime fecha_ingreso = model.fecha;
                int grupo;
                var periodos_vigentes = get_ultimo_periodos_no_vigente();
                if (!periodos_vigentes.Where(item => item.id_tser == model.id_tser).Any())
                {
                    return JsonError("No existen periodos vigentes para la Fecha Ingresada");
                }
                var ultimo_cab_ts = (_db.cab_ts.OrderByDescending(item => item.grupo)).Take(1).SingleOrDefault();
                if (ultimo_cab_ts == null) { grupo = 0; } else { grupo = ultimo_cab_ts.grupo + 1; }

                try
                {
                    model.horas = (Int32.Parse(horas.Split(':')[0]) * 60) + Int32.Parse(horas.Split(':')[1]);
                    model.horas_comerciales = (Int32.Parse(horas.Split(':')[0]) * 60) + Int32.Parse(horas.Split(':')[1]);
                }
                catch (Exception ex)
                {
                    model.horas = 0;
                    model.horas_comerciales = 0;
                }
                model.id_usu = SesionLogin.id_usu;
                model.descuento = 0;
                model.estado = SesionLogin.jerarquia;
                model.usuario_mod = SesionLogin.Nom_cor_usu;
                model.fecha_mod = fecha_actual;
                model.descr_ing = "";
                model.grupo = grupo;
                model.dueño_grupo = "S";
                model.valor_hh = valorhh(model.id_emp, SesionLogin.id_usu);
                var usuario_grupal = model;
                ModelState.Remove("id_cat");
                ModelState.Remove("horas");
                ModelState.Remove("horas_comerciales");
                if (ModelState.IsValid)
                {
                    _db.cab_ts.Add(model);
                    _db.SaveChanges();
                    //if (usuarios_grupales != null)
                    //{
                    //    foreach (string item in usuarios_grupales)
                    //    {
                    //        try
                    //        {
                    //            int id_usu_grup = Int32.Parse(item);
                    //            usuario_grupal.dueño_grupo = "N";
                    //            usuario_grupal.id_usu = id_usu_grup;
                    //            usuario_grupal.valor_hh = valorhh(usuario_grupal.id_emp, id_usu_grup);
                    //            _db.cab_ts.Add(usuario_grupal);
                    //            _db.SaveChanges();
                    //        }
                    //        catch (Exception ex)
                    //        {

                    //        }
                    //    }

                    //}
                    return JsonExito();
                }
                return JsonError("Opps, ocurrio un problema");
            }
            catch (Exception err)
            {
                return JsonError("Opps, ocurrio un problema");
            }
        }

        public ActionResult Edit(int id)
        {
            //carga formulario en caso de edicion
            if (tienePermiso("PERUSUREVFAC")) { return PartialView("denegaPermiso"); }
            var model = _db.cab_ts.FirstOrDefault(p => p.ts_id == id);
            if (model == null) return RedirectToAction("Create");
            Selectores(model);
            try
            {
                int tserv = model.id_tser;
                var periodos_vigentes = get_ultimo_periodos_no_vigente();
                DateTime fecha_ini = (periodos_vigentes.Single(item => item.id_tser == tserv)).fecha_ini;
                DateTime fecha_fin = (DateTime)(periodos_vigentes.Single(item => item.id_tser == tserv)).fecha_fin;
                ViewBag.fecha_ini_per = fecha_ini.ToString("MM-dd-yyyy");
                ViewBag.fecha_fin_per = fecha_fin.ToString("MM-dd-yyyy");
            }
            catch (Exception e)
            {

            }
            //retorna el formulario con el modelo asociado, para edicion
            ViewBag.usu_id_unidad = SesionLogin.id_unidad;
            ViewBag.jerarquia = SesionLogin.jerarquia;
            return PartialView("Form", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(cab_ts model, string horas)
        {
            //edita y guarda cambios al enviarse el formulario
            if (tienePermiso("PERUSUREVFAC")) { return PartialView("denegaPermiso"); }
            cab_ts model_to_edit = (cab_ts)_db.cab_ts.SingleOrDefault(item => item.ts_id == model.ts_id);
            if (SesionLogin.jerarquia.Equals("REVI3"))
            {
                model_to_edit.descr_ing = model.descr_ing;
            }else
            {
                model_to_edit.id_tser = model.id_tser;
                model_to_edit.id_emp = model.id_emp;
                model_to_edit.id_cat = model.id_cat;
                model_to_edit.fecha = model.fecha;
                try
                {
                    model_to_edit.horas_comerciales = (Int32.Parse(horas.Split(':')[0]) * 60) + Int32.Parse(horas.Split(':')[1]);
                }
                catch (Exception ex)
                {
                    model_to_edit.horas_comerciales = 0;
                }
                model_to_edit.descr_esp = model.descr_esp;
                model_to_edit.fecha_mod = DateTime.Now;
                model_to_edit.usuario_mod = SesionLogin.Nom_cor_usu;
                model_to_edit.descuento = model.descuento;
            }
            ModelState.Remove("id_cat");
            ModelState.Remove("horas");
            ModelState.Remove("horas_comerciales");
            if (ModelState.IsValid)
            {
                _db.cab_ts.Attach(model_to_edit);
                _db.Entry(model_to_edit).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
                return JsonExito();
            }

            return JsonError("Opps, ocurrio un problema");
        }


        //Metodo para obtener especificamente 1 Country by Id valor retornado como Json
        public JsonResult GetCategoria(string id_emp, string id_unidad, string tser)
        {
            try
            {
                int _id_unidad = Int32.Parse(id_unidad);
                int _id_emp = Int32.Parse(id_emp);
                int _id_tser = Int32.Parse(tser);
                if (_id_unidad < 0) { _id_unidad = SesionLogin.id_unidad; }
                var Lista = (from cat in _db.categorias
                             join ect in _db.emp_car_tsrv on cat.id_cat equals ect.id_cat
                             join uni in _db.unidades on cat.id_unidad equals uni.id_unidad
                             where ect.id_emp == _id_emp
                             && ect.id_tser == _id_tser
                             && uni.id_unidad == _id_unidad
                             select cat);
                if (Lista.Any())
                {
                    return Json(new SelectList(Lista.ToList(), "id_cat", "nom_cat"));
                }
                categorias selListItem = new categorias();
                List<categorias> newList = new List<categorias>();
                newList.Add(selListItem);
                return Json(new SelectList(newList));
            }
            catch (Exception e)
            {
                categorias selListItem = new categorias();
                List<categorias> newList = new List<categorias>();
                newList.Add(selListItem);
                return Json(new SelectList(newList));
            }


        }

        public JsonResult GetFechasPorPeriodoVigente(string tser)
        {
            try
            {
                int tserv = Int32.Parse(tser);
                var periodos_vigente = get_ultimo_periodos_no_vigente();
                DateTime fecha_ini = (periodos_vigente.Single(item => item.id_tser == tserv)).fecha_ini;
                DateTime fecha_fin = (DateTime)(periodos_vigente.Single(item => item.id_tser == tserv)).fecha_fin;
                List<string> fechas = new List<string>();
                fechas.Add(fecha_ini.ToString("MM-dd-yyyy"));
                fechas.Add(fecha_fin.ToString("MM-dd-yyyy"));
                return Json(fechas);
            }
            catch (Exception e)
            {
                return Json(new List<string>());
            }
        }

        private void Selectores(cab_ts model)
        {
            ViewBag.id_emp_list = new SelectList(_db.empresas.OrderBy(item => item.nom_emp), "id_emp", "nom_emp", model.id_emp);

            var lista_usu_selecionados = _db.cab_ts.Where(item => item.grupo == model.grupo && item.dueño_grupo == "N").Select(item => item.id_usu).ToArray();
            ViewBag.id_usuarios_list = new MultiSelectList(_db.usuarios.Where(item => item.id_usu != SesionLogin.id_usu), "id_usu", "Nom_usu", null, lista_usu_selecionados);
            ViewBag.id_tser_list = new SelectList(_db.tipos_servicios, "id_tser", "nom_tser");

            var Lista = (from cat in _db.categorias
                         join ect in _db.emp_car_tsrv on cat.id_cat equals ect.id_cat
                         join uni in _db.unidades on cat.id_unidad equals uni.id_unidad
                         where ect.id_emp == model.id_emp
                         && ect.id_tser == model.id_tser
                         && uni.id_unidad == model.usuarios.id_unidad
                         select cat);

            ViewBag.id_cat_list = new SelectList(Lista, "id_cat", "nom_cat", model.id_cat);
        }

        private decimal valorhh(int emp, int usu)
        {
            try
            {
                var valor_hh = (from v in _db.valor_hh
                                join u in _db.usuarios on new
                                {
                                    property1 = v.id_unidad,
                                    property2 = v.id_cargo
                                } equals new
                                {
                                    property1 = u.id_unidad,
                                    property2 = u.id_cargo
                                }
                                where v.id_emp == emp && u.id_usu == usu
                                select v);
                if (valor_hh.Any())
                {
                    return ((valor_hh)(valor_hh)).valor;
                }
                return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        private IQueryable<periodos> get_ultimo_periodos_no_vigente()
        {
            var lista = _db.Database.SqlQuery<periodos>("(select * from "+
                                                        "    (select *, " +
                                                        "        (rank() over(partition by id_tser order by fecha_ini desc, fecha_fin desc)) as 'rank_' " +
                                                        "    from periodos " +
                                                        "    where not(getdate() between fecha_ini and fecha_fin) "+
                                                        "           and getdate() > fecha_fin) as tb " +
                                                        "where tb.rank_ < 2)");
            return lista.AsQueryable();
        }
        public ActionResult Delete(cab_ts model, int? id)
        {
			if (tienePermiso("PERUSUREVFAC")) { return PartialView("denegaPermiso"); }
            if (id == null)
            {
                return JsonError("Opps, ocurrio un problema");
            }
            cab_ts model_to_edit = (cab_ts)_db.cab_ts.SingleOrDefault(item => item.ts_id == id);
            model_to_edit.horas_comerciales = 0;
            if (ModelState.IsValid)
            {
                _db.cab_ts.Attach(model_to_edit);
                _db.Entry(model_to_edit).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
                return JsonExito();
            }
            return JsonError("Opps, ocurrio un problema");
        }
    }
}