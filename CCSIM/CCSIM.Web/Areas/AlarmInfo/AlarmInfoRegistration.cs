using System.Web.Mvc;

namespace CCSIM.Web.Areas.AlarmInfo
{
    public class AlarmInfoRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AlarmInfo";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AlarmInfo_default",
                "AlarmInfo/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}