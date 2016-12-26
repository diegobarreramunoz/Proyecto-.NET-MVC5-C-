using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Circon.Mvc.Helpers;
using System.Security.Cryptography;
using pso.cdd.model;
using System.Web;

namespace pso.cdd.mvc {
    public static class CustomHelper {
        public static ApplicationData AppData(this HtmlHelper html)
        {
            return new ApplicationData();
        }
        public static string MD5Hash(this string text) {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++) {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

        public static usuarios SesionLogin(this HtmlHelper html) { 
            return HttpContext.Current.Session[Settings.GetValue<string>("session")] == null ? new usuarios() : (usuarios)HttpContext.Current.Session[Settings.GetValue<string>("session")];  
        }

        public static decimal DiasDisponibles(/*this Empleado item*/) {
            /*//var dias = item.Licencias.Where(p => p.Periodo == DateTime.Today.Year);
            //var disp = item.SaldoHoy;
            //if (dias.Count() > 0) {
            //    disp = dias.Sum(x => x.SaldoHoy);
            //}
            //return disp ?? 0;
            var _db = new CddEntities();
            var legal = _db.DescansoTipos.FirstOrDefault(p => p.Id == 1);
            return legal.MaxDias;*/
            return 0;
        }
        public static decimal DiasAdicionalesDisponibles(/*this Empleado item*/) { 
            //var _db = new CddEntities();
            //var adicional = _db.DescansoTipos.FirstOrDefault(p => p.Id == 2);
            //return adicional.MaxDias;
            return 0;
        }
        public static int DiasTomados(/*this Empleado item*/) {
            //var tomados = item.Descansos.Count(p => p.FechaDescanso.Year == DateTime.Today.Year && p.DescansoTipo_Id==1);
            //return tomados;
            return 0;
        }
        public static int DiasAdicionales(/*this Empleado item*/) {
            //var tomados = item.Descansos.Count(p => p.FechaDescanso.Year == DateTime.Today.Year && p.DescansoTipo_Id != 1);
            //return tomados;
            return 0;
        }
    }

    public class ApplicationData
    {
        public string TitlePrefix(string texto) {return "Pso | " + texto; }  
        public string Title { get; set; } = Settings.GetValue<string>("title");
    }


}
