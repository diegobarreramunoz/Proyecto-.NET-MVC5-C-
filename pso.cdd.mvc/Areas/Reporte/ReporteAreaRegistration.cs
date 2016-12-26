using System.Web.Mvc;

namespace pso.cdd.mvc.Areas.Reporte
{
    public class ReporteAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Reporte";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Reporte_default",
                "Reporte/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}