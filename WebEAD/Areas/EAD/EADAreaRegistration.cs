using System.Web.Mvc;

namespace WebEAD.Areas.EAD
{
    public class EADAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "EAD";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "EAD_default",
                "EAD/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}