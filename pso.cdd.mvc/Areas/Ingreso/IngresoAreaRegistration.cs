using System.Web.Mvc;

namespace pso.cdd.mvc.Areas.Ingreso
{
    public class IngresoAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Ingreso";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Ingreso_default",
                "Ingreso/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}