using CCSIM.Web.App_Start;
using System.Web;
using System.Web.Mvc;

namespace CCSIM.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new UserAuthAttribute());//注册
        }
    }
}
