using CCSIM.BLL;
using CCSIM.DAL.Model;
using CCSIM.Entity;
using FineUIMvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CCSIM.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Hello()
        {
            //ShowNotify("长江中路1号有紧急情况发生！", MessageBoxIcon.Warning, Target.Self, "red", 600);
            return View();
        }

        public ActionResult Map()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult btnHello_Click()
        {
            Alert.Show("你好 FineUI！", MessageBoxIcon.Warning);

            return UIHelper.Result();
        }


        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult btnLogin_Click(string tbxUserName, string tbxPassword)
        {
            if (tbxUserName == "admin" && tbxPassword == "admin")
            {
                ShowNotify("成功登录！", MessageBoxIcon.Success);
            }
            else
            {
                ShowNotify("用户名或密码错误！", MessageBoxIcon.Error);
            }

            return UIHelper.Result();
        }


        // GET: Themes
        public ActionResult Themes()
        {
            return View();
        }

        /// <summary>
        /// 获取人员车辆树结构
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMapTree()
        {
            var data = MapBLL.GetTree();

            return new JsonResult
            {
                Data = data
            };
        }

        /// <summary>
        /// 获取所有信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllInfo()
        {
            var data = MapBLL.GetAllInfo();

            return new JsonResult
            {
                Data = data
            };
        }

        /// <summary>
        /// 获取最新的定位信息
        /// </summary>
        /// <param name="objectNames"></param>
        /// <returns></returns>
        public ActionResult GetNewGpsInfo(string objectNames)
        {
            var data = MapBLL.GetNewGpsInfo(objectNames.Split(';'));

            return new JsonResult
            {
                Data = data
            };
        }

        /// <summary>
        /// 获取网格信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetNetInfo()
        {
            var data = NetManageBLL.GetAll();

            return new JsonResult
            {
                Data = data
            };
        }

        /// <summary>
        /// 新增报警信息
        /// </summary>
        /// <returns></returns>
        public ActionResult AddAlarmInfo(string info, string alarmAddress, string alarmObjectName, string alarmType, string alarmTime)
        {
            INFO_ALARMINFO alarmInfo = new INFO_ALARMINFO();
            alarmInfo.ALARMINFO = info;
            alarmInfo.ALARMADDRESS = alarmAddress;
            alarmInfo.ALARMOBJECTNAME = alarmObjectName;
            alarmInfo.ALARMTYPE = Convert.ToInt32(alarmType);
            alarmInfo.ALARMTIME = DateTime.Parse(alarmTime);
            var isSuccess = AlarmBLL.Add(alarmInfo);
            return new JsonResult
            {
                Data = isSuccess
            };
        }

    }
}