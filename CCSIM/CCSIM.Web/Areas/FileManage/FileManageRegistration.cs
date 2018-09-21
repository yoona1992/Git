using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CCSIM.Web.Areas.FileManage
{
    public class FileManageRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "FileManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "FileManage_default",
                "FileManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}