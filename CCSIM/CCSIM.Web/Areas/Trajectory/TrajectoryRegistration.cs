using System.Web.Mvc;

namespace CCSIM.Web.Areas.Trajectory
{
    public class TrajectoryRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Trajectory";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Trajectory_default",
                "Trajectory/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}