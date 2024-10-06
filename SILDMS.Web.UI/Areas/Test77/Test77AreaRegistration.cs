using System.Web.Mvc;

namespace SILDMS.Web.UI.Areas.Test77
{
    public class Test77AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Test77";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Test77_default",
                "Test77/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}