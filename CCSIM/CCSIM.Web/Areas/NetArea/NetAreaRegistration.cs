using System.Web.Mvc;

namespace CCSIM.Web.Areas.NetArea
{
    public class NetAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "NetArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "NetArea_default",
                "NetArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}