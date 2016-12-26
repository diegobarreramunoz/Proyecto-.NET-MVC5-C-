using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using pso.cdd.model;
using Circon.Mvc.Helpers;
using System.IO;

namespace pso.cdd.mvc {
    public class MasterController : Controller {
        
        protected TSEntities _db = new TSEntities();

        protected usuarios SesionLogin {
            get { return Session[Settings.GetValue<string>("session")] == null ? new usuarios() : (usuarios)Session[Settings.GetValue<string>("session")]; }
            set { Session[Settings.GetValue<string>("session")] = value; }
        }

        protected List<perfil_usuario> SesionPermisos
        {
            get { return Session[Settings.GetValue<string>("permisos")] == null ? new List<perfil_usuario>() : (List<perfil_usuario>)Session[Settings.GetValue<string>("permisos")];}
            set { Session[Settings.GetValue<string>("permisos")] = value; }
        }

        protected JsonResult JsonExito() {
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        } 
        protected JsonResult JsonError(string msg) {
            return Json(new { success = false, error = true, mensaje = msg }, JsonRequestBehavior.AllowGet);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext) {
            Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            Response.Cache.SetValidUntilExpires(false);
            Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            var controller = (string)filterContext.RouteData.Values["controller"];
            var action = (string)filterContext.RouteData.Values["action"];
            var area = (string)filterContext.RouteData.DataTokens["area"];

            ViewBag.ConSesion = (filterContext.HttpContext.Session != null && SesionLogin.id_usu > 0);

            var paginas = new[] { "LOGIN", "LOGOUT" };

            if (controller.ToUpper().Equals("HOME") && (paginas.Contains(action.ToUpper()))) return;

            if (ViewBag.ConSesion) return;
            //sin sesion
            FormsAuthentication.SignOut();

            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
            {
                controller = "Home",
                action = "Login",
                Area = ""
            }));
        }

        protected Boolean tienePermiso(string permiso)
        {
            var tiene = SesionPermisos.Find(p => p.perfil == permiso);
            if (null != tiene) return false;
            else return true;
        }


        protected List<Dictionary<string, object>> LoadData(string sqlSelect, params object[] sqlParameters)
        {
            var table = new List<Dictionary<string, object>>();
            _db.Database.Connection.Open();
            using (var cmd = _db.Database.Connection.CreateCommand())
            {
                cmd.CommandText = sqlSelect;
                foreach (var param in sqlParameters)
                    cmd.Parameters.Add(param);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var row = new Dictionary<string, object>();
                        for (int i = 0; i < reader.FieldCount; i++)
                            row[reader.GetName(i)] = reader[i];
                        table.Add(row);
                    }
                }
            }
            _db.Database.Connection.Close();
            return table;
        }

        protected void LogError(Exception ex)
        {
            string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            message += string.Format("Message: {0}", ex.Message);
            message += Environment.NewLine;
            message += string.Format("StackTrace: {0}", ex.StackTrace);
            message += Environment.NewLine;
            message += string.Format("Source: {0}", ex.Source);
            message += Environment.NewLine;
            message += string.Format("TargetSite: {0}", ex.TargetSite.ToString());
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            string path = Server.MapPath("~/ErrorLog/ErrorLog.txt");
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(message);
                writer.Close();
            }
        }
    }
}