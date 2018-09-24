using CCSIM.BLL;
using CCSIM.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CCSIM.Web.App_Start
{/// <summary>
 /// 操作日志拦截器
 /// </summary>
    public class LoggerFilter : FilterAttribute, IActionFilter
    {
        /// <summary>
        /// 日志关键字
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 日志描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Action执行后
        /// </summary>
        void IActionFilter.OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        /// <summary>
        /// Action执行前
        /// </summary>
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            var websession = filterContext.HttpContext.Session[WebConstants.UserSession];
            var paramsNames = filterContext.ActionDescriptor.GetParameters();
            var method = "";
            foreach (var param in paramsNames)
            {
                if (param.ParameterName.IndexOf("_fields") != -1 || param.ParameterName.IndexOf("_pageIndex") != -1 || param.ParameterName.IndexOf("_pageSize") != -1)
                {
                }
                else
                {
                    if (param.ParameterType.Name == "FormCollection")
                    {
                        FormCollection collection = (FormCollection)filterContext.ActionParameters[param.ParameterName];
                        foreach(var key in collection.AllKeys)
                        {
                            method += key + ":" + collection[key] + ";";
                        }
                    }
                    else
                    {
                        method += param.ParameterName + ":" + filterContext.ActionParameters[param.ParameterName] + ";";
                    }
                }
            }
            if (!string.IsNullOrWhiteSpace(method))
            {
                method = method.Substring(0, method.Length - 1);
            }
            if (websession == null)
            {
            }
            else
            {
                UserInfo user = filterContext.HttpContext.Session[WebConstants.UserSession] as UserInfo;
                if (user == null) { }
                else
                {
                    LogInfo info = new LogInfo();
                    info.User_Id = user.Id;
                    info.UserName = user.UserName;
                    info.Operation = Description;
                    info.Method = Key;
                    info.Ip = GetClientIP();
                    info.Params = method;
                    LogBLL.AddLog(info);
                }
            }

        }

        /// <summary>
        /// 获取客户端IP
        /// </summary>
        /// <returns></returns>
        private static string GetClientIP()
        {
            string result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (result == null || result == String.Empty)
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            if (result == null || result == String.Empty)
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }
            if (result.StartsWith("::"))
            {
                result = "127.0.0.1";
            }

            return result;
        }

    }
}