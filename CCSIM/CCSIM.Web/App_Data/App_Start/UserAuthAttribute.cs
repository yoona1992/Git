using CCSIM.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CCSIM.Web.App_Start
{
    /// <summary>
    /// 用户权限
    /// </summary>
    public class UserAuthAttribute : AuthorizeAttribute
    {
        public bool IsLogin = false;
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool Pass = false;
            try
            {
                var websession = httpContext.Session[WebConstants.UserSession];
                if (websession == null)
                {
                    httpContext.Response.StatusCode = 401;//无权限状态码
                    Pass = false;
                    IsLogin = false;
                }
                else
                {
                    UserInfo user = httpContext.Session[WebConstants.UserSession] as UserInfo;
                    if (user == null)
                    {
                        httpContext.Response.StatusCode = 401;//无权限状态码
                        Pass = false;
                        IsLogin = false;
                    }
                    else
                    {
                        Pass = true;
                        IsLogin = true;
                    }
                }
            }
            catch (Exception)
            {
                return Pass;
            }
            return Pass;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            else
            {
                if (!IsLogin)
                {
                    filterContext.HttpContext.Response.Write("<html>");
                    filterContext.HttpContext.Response.Write("<script>");
                    filterContext.HttpContext.Response.Write("window.open ('" + "../Home/Index','_top')");
                    filterContext.HttpContext.Response.Write("</script>");
                    filterContext.HttpContext.Response.Write("</html>");
                    //filterContext.HttpContext.Response.Redirect("~/Home/Login", true);
                }
            }
        }
    }
}