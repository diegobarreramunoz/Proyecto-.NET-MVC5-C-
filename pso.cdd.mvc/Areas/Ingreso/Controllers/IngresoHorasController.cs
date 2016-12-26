using pso.cdd.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;
using System.Net;

namespace pso.cdd.mvc.Areas.Ingreso.Controllers
{
    public class IngresoHorasController : MasterController
    {
        // GET: Ingreso/IngresoHoras
        public ActionResult Index()
        {
            if (tienePermiso("PERUSUINGHH")) { return PartialView("denegaPermiso"); }
            var cab_ts_list = (from cab in _db.cab_ts
                               join upe in (from p in _db.periodos
                                            join t in _db.tipos_servicios on p.id_tser equals t.id_tser
                                            group new { p, t } by new { t.nom_tser, t.id_tser } into g
                                            where (DateTime.Now >= g.Max(d => d.p.fecha_ini)) && (DateTime.Now <= g.Max(d => d.p.fecha_fin))
                                            select new { id_tser = g.Key.id_tser, nom_tser = g.Key.nom_tser, fecha_ini = g.Max(d => d.p.fecha_ini), fecha_fin = g.Max(d => d.p.fecha_fin) })
                               on cab.id_tser equals upe.id_tser
                               where cab.id_usu == SesionLogin.id_usu
                                    && cab.id_tser == upe.id_tser
                                    && (cab.fecha <= upe.fecha_fin && cab.fecha >= upe.fecha_ini)
                               select cab).OrderByDescending(x => x.ts_id);
            
            return View(cab_ts_list);
        }

        public ActionResult Create()
        {
            //validacion de permisos
            if (tienePermiso("PERUSUINGHH")) { return PartialView("denegaPermiso"); }
            int usuario_id = SesionLogin.id_usu;
            ViewBag.id_emp_list = new SelectList(_db.empresas.OrderBy(item => item.nom_emp), "id_emp", "nom_emp");
            ViewBag.id_cat_list = new SelectList(Enumerable.Empty<SelectListItem>(), "id_cat", "nom_cat");
            ViewBag.id_usuarios_list = new MultiSelectList(_db.usuarios.Where(item => item.id_usu != SesionLogin.id_usu), "id_usu", "Nom_usu");
            ViewBag.id_tser_list = new SelectList(_db.tipos_servicios, "id_tser", "nom_tser");
            ViewBag.usu_id_unidad = SesionLogin.id_unidad;
            return PartialView("Form");
        }

        public ActionResult Edit(int id)
        {
            //carga formulario en caso de edicion
            if (tienePermiso("PERUSUINGHH")) { return PartialView("denegaPermiso"); }
            var model = _db.cab_ts.FirstOrDefault(p => p.ts_id == id);
            if (model == null) return RedirectToAction("Create");
            Selectores(model);
            try
            {
                int tserv = model.id_tser;
                var periodos_vigentes = get_periodos_vigentes();
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
            return PartialView("Form", model);
        }


        [HttpPost]
        
        [ValidateAntiForgeryToken]
        public ActionResult Create(cab_ts model, string tipo, string[] usuarios_grupales, string horas)
        {
            if (tienePermiso("PERUSUINGHH")) { return PartialView("denegaPermiso"); }
            try
            {
                DateTime fecha_actual = DateTime.Now;
                DateTime fecha_ingreso = model.fecha;
                int grupo;

                var periodos_vigentes = get_periodos_vigentes();

                if (!periodos_vigentes.Where(item => item.id_tser == model.id_tser).Any())
                {
                    return JsonError("No existen periodos vigentes para la Fecha Ingresada");
                }

                var ultimo_cab_ts = (_db.cab_ts.OrderByDescending(item => item.grupo)).Take(1).SingleOrDefault();
                if (ultimo_cab_ts == null) { grupo = 0; } else { grupo = ultimo_cab_ts.grupo + 1; }

                try
                {
                    model.horas = (Int32.Parse(horas.Split(':')[0])*60)+ Int32.Parse(horas.Split(':')[1]);
                    model.horas_comerciales = (Int32.Parse(horas.Split(':')[0]) * 60) + Int32.Parse(horas.Split(':')[1]);

                }
                catch (Exception ex)
                {
                    model.horas = 0;
                    model.horas_comerciales = 0;
                }
                model.id_usu = SesionLogin.id_usu;
                model.descuento = 0;
                model.estado = "ANALI";
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
                    if (usuarios_grupales != null)
                    { 
                        foreach (string item in usuarios_grupales)
                        {
                            try
                            {
                                int id_usu_grup = Int32.Parse(item);
                                usuario_grupal.dueño_grupo = "N";
                                usuario_grupal.id_usu = id_usu_grup;
                                usuario_grupal.valor_hh = valorhh(usuario_grupal.id_emp, id_usu_grup);
                                _db.cab_ts.Add(usuario_grupal);
                                _db.SaveChanges();
                            }
                            catch (Exception ex)
                            {

                            }
                        }

                    }
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(cab_ts model, string horas)
        {
            //edita y guarda cambios al enviarse el formulario
            if (tienePermiso("PERUSUINGHH")) { return PartialView("denegaPermiso"); }
            cab_ts model_to_edit = (cab_ts)_db.cab_ts.SingleOrDefault(item => item.ts_id == model.ts_id);
            model_to_edit.id_tser = model.id_tser;
            model_to_edit.id_emp = model.id_emp;
            model_to_edit.id_cat = model.id_cat;
            model_to_edit.fecha = model.fecha;
            try
            {
                // model_to_edit.horas = Decimal.Parse(horas[0]) + Decimal.Round(Decimal.Parse(horas[1]) / 60, 1);
                model_to_edit.horas = (Int32.Parse(horas.Split(':')[0]) * 60) + Int32.Parse(horas.Split(':')[1]);
                model_to_edit.horas_comerciales = (Int32.Parse(horas.Split(':')[0]) * 60) + Int32.Parse(horas.Split(':')[1]);
            }
            catch (Exception ex)
            {
                model_to_edit.horas = 0;
                model_to_edit.horas_comerciales = 0;
            }
            model_to_edit.descr_esp = model.descr_esp;
            model_to_edit.fecha_mod = DateTime.Now;
            model_to_edit.usuario_mod = SesionLogin.Nom_cor_usu;

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
            Selectores(model);
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
                    return Json(new SelectList(Lista.ToList(),"id_cat","nom_cat"));
                }
                categorias selListItem = new categorias();
                List<categorias> newList = new List<categorias>();
                newList.Add(selListItem);
                return Json(new SelectList(newList));
            }
            catch(Exception e)
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
                var periodos_vigente = get_periodos_vigentes();
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
            ViewBag.id_emp_list = new SelectList(_db.empresas.OrderBy(item => item.nom_emp), "id_emp", "nom_emp",model.id_emp);
            
            var lista_usu_selecionados = _db.cab_ts.Where(item => item.grupo == model.grupo && item.dueño_grupo == "N").Select(item => item.id_usu).ToArray();
            ViewBag.id_usuarios_list = new MultiSelectList(_db.usuarios.Where(item => item.id_usu != SesionLogin.id_usu), "id_usu", "Nom_usu",null, lista_usu_selecionados);
            ViewBag.id_tser_list = new SelectList(_db.tipos_servicios, "id_tser", "nom_tser");

            var Lista = (from cat in _db.categorias
                         join ect in _db.emp_car_tsrv on cat.id_cat equals ect.id_cat
                         join uni in _db.unidades on cat.id_unidad equals uni.id_unidad
                         where ect.id_emp == model.id_emp
                         && ect.id_tser == model.id_tser
                         && uni.id_unidad == SesionLogin.id_unidad
                         select cat);

            ViewBag.id_cat_list = new SelectList(Lista, "id_cat", "nom_cat", model.id_cat);
            ViewBag.usu_id_unidad = SesionLogin.id_unidad;
        }

        private decimal valorhh (int emp, int usu)
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
            catch(Exception ex)
            {
                return 0;
            }
        }

        private IQueryable<periodos> get_periodos_vigentes()
        {
            var Anonymus = (from p in _db.periodos
             join t in _db.tipos_servicios on p.id_tser equals t.id_tser
             group new { p, t } by new { t.nom_tser, t.id_tser } into g
             where (DateTime.Now >= g.Max(d => d.p.fecha_ini)) && (DateTime.Now <= g.Max(d => d.p.fecha_fin))
             select new {id_periodo = g.FirstOrDefault(d => d.p.id_tser == g.Key.id_tser).p.id_periodo, id_tser = g.Key.id_tser, nom_tser = g.Key.nom_tser, fecha_ini = g.Max(d => d.p.fecha_ini), fecha_fin = g.Max(d => d.p.fecha_fin) });

            List <periodos> lista_periodos = new List<periodos>();

            foreach (var item in Anonymus)
            {
                periodos periodo = new periodos
                {
                    id_periodo = item.id_periodo,
                    id_tser = item.id_tser,
                    fecha_ini = item.fecha_ini,
                    fecha_fin = item.fecha_fin,
                    estado = null,
                    fecha_mod = null,
                    usuario_mod = null,
                    tipos_servicios = null
                };

                lista_periodos.Add(periodo);

            }
            return lista_periodos.AsQueryable();
        }

       
        public ActionResult Delete(cab_ts model, int? id)
        {
            if (id == null)
            {
                return JsonError("Opps, ocurrio un problema");
            }
			if (tienePermiso("PERUSUINGHH")) { return PartialView("denegaPermiso"); }
            cab_ts model_to_edit = (cab_ts)_db.cab_ts.SingleOrDefault(item => item.ts_id == id);
            if (ModelState.IsValid)
            {
                _db.cab_ts.Remove(model_to_edit);
                _db.SaveChanges();
                return JsonExito();
            }
            return JsonError("Opps, ocurrio un problema");
        }

    }
}
