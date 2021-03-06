﻿using CCSIM.BLL;
using CCSIM.DAL.Model;
using CCSIM.Entity;
using CCSIM.Web.App_Start;
using CCSIM.Web.Models;
using FineUIMvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
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

        public ActionResult UserMessage()
        {
            var phone = Request.QueryString["phone"];
            LoadData_UserMessage(phone);
            return View();
        }

        public ActionResult UserMessageDetail()
        {
            ViewData["Id"] = Request.QueryString["id"];
            return View();
        }

        public ActionResult NetDetail()
        {
            var id = Convert.ToInt32(Request.QueryString["id"]);
            #region 下拉框绑定
            var belongDeptList = CodeBLL.GetCodeListByParentCode("PCS");
            var listItems = new List<ListItem>();
            foreach (var d in belongDeptList)
            {
                listItems.Add(new ListItem
                {
                    Text = d.BMVALUE,
                    Value = d.BMKEY.ToString()
                });
            }

            var model = new DropDownListModel();
            model.DropDownList = "VALUE";
            model.DropDownListItem = listItems;
            #endregion

            var data = NetManageBLL.Get(id);
            //UIHelper.DropDownList("").SelectedValue("");
            NetInfoModel info = new NetInfoModel();
            info.dropDownList = model;
            info.netInfo.Id = data.ID;
            info.netInfo.Name = data.NAME;
            info.netInfo.BelongDeptId = data.BELONGDEPTID;
            info.netInfo.PopulationInfo = data.POPULATIONINFO;
            info.netInfo.HouseInfo = data.HOUSEINFO;
            info.netInfo.BelongArea = data.BELONGAREA;
            info.netInfo.UnitStoreInfo = data.UNITSTOREINFO;
            info.netInfo.NetColor = data.NETCOLOR;
            info.netInfo.LonAndLat = data.LONANDLAT;
            info.netInfo.Remark = data.REMARK;
            return View(info);
        }

        public ActionResult MessageSend()
        {
            return View();
        }

        public ActionResult UserDetail()
        {
            var phone = Request.QueryString["phone"];
            #region 下拉框绑定
            var belongDeptList = CodeBLL.GetCodeListByParentCode("SSBM_RY");
            var listItems = new List<ListItem>();
            foreach (var d in belongDeptList)
            {
                listItems.Add(new ListItem
                {
                    Text = d.BMVALUE,
                    Value = d.BMKEY.ToString()
                });
            }

            var belongDept = new DropDownListModel();
            belongDept.DropDownList = "VALUE";
            belongDept.DropDownListItem = listItems;

            var sexList = CodeBLL.GetCodeListByParentCode("XB");
            listItems = new List<ListItem>();
            foreach (var d in sexList)
            {
                listItems.Add(new ListItem
                {
                    Text = d.BMVALUE,
                    Value = d.BMKEY.ToString()
                });
            }

            var sex = new DropDownListModel();
            sex.DropDownList = "VALUE1";
            sex.DropDownListItem = listItems;

            var certificateTypeList = CodeBLL.GetCodeListByParentCode("ZJLX");
            listItems = new List<ListItem>();
            foreach (var d in certificateTypeList)
            {
                listItems.Add(new ListItem
                {
                    Text = d.BMVALUE,
                    Value = d.BMKEY.ToString()
                });
            }

            var certificateType = new DropDownListModel();
            certificateType.DropDownList = "VALUE2";
            certificateType.DropDownListItem = listItems;

            var belongNetList = NetManageBLL.GetAll();
            listItems = new List<ListItem>();
            foreach (var d in belongNetList)
            {
                listItems.Add(new ListItem
                {
                    Text = d.Name,
                    Value = d.Id.ToString()
                });
            }

            var belongNet = new DropDownListModel();
            belongNet.DropDownList = "VALUE3";
            belongNet.DropDownListItem = listItems;

            var userTypeList = CodeBLL.GetCodeListByParentCode("RYLX");
            listItems = new List<ListItem>();
            foreach (var d in userTypeList)
            {
                listItems.Add(new ListItem
                {
                    Text = d.BMVALUE,
                    Value = d.BMKEY.ToString()
                });
            }

            var userType = new DropDownListModel();
            userType.DropDownList = "VALUE4";
            userType.DropDownListItem = listItems;
            #endregion
            UserInfoModel model = new UserInfoModel();
            model.belongDept = belongDept;
            model.sex = sex;
            model.certificateType = certificateType;
            model.belongNet = belongNet;
            model.userType = userType;
            var data = UserBLL.Get(phone);
            model.userInfo.Id = data.ID;
            model.userInfo.Name = data.NAME;
            model.userInfo.Sex = data.SEX;
            model.userInfo.Age = data.AGE;
            model.userInfo.Telephone = data.TELEPHONE;
            model.userInfo.CertificateType = data.CERTIFICATETYPE;
            model.userInfo.CertificateNum = data.CERTIFICATENUM;
            model.userInfo.BelongDeptId = data.BELONGDEPTID;
            model.userInfo.BelongNetId = data.BELONGNETID;
            model.userInfo.Direction = data.DIRECTION;
            model.userInfo.Address = data.ADDRESS;
            model.userInfo.Remark = data.REMARK;
            model.userInfo.UserType = data.USERTYPE;
            model.userInfo.VirtualTrumpet = data.VIRTUALTRUMPET;
            return View(model);
        }

        public ActionResult CarDetail()
        {
            var id = Convert.ToInt32(Request.QueryString["id"]);
            #region 下拉框绑定
            var belongDeptList = CodeBLL.GetCodeListByParentCode("PCS");
            var listItems = new List<ListItem>();
            foreach (var d in belongDeptList)
            {
                listItems.Add(new ListItem
                {
                    Text = d.BMVALUE,
                    Value = d.BMKEY.ToString()
                });
            }

            var belongDept = new DropDownListModel();
            belongDept.DropDownList = "VALUE";
            belongDept.DropDownListItem = listItems;

            var vehicleTypeList = CodeBLL.GetCodeListByParentCode("CLLX");
            listItems = new List<ListItem>();
            foreach (var d in vehicleTypeList)
            {
                listItems.Add(new ListItem
                {
                    Text = d.BMVALUE,
                    Value = d.BMKEY.ToString()
                });
            }

            var vehicleType = new DropDownListModel();
            vehicleType.DropDownList = "VALUE1";
            vehicleType.DropDownListItem = listItems;

            var belongNetList = NetManageBLL.GetAll();
            listItems = new List<ListItem>();
            foreach (var d in belongNetList)
            {
                listItems.Add(new ListItem
                {
                    Text = d.Name,
                    Value = d.Id.ToString()
                });
            }

            var belongNet = new DropDownListModel();
            belongNet.DropDownList = "VALUE2";
            belongNet.DropDownListItem = listItems;

            var ownerTypeList = CodeBLL.GetCodeListByParentCode("YCBM");
            listItems = new List<ListItem>();
            foreach (var d in ownerTypeList)
            {
                listItems.Add(new ListItem
                {
                    Text = d.BMVALUE,
                    Value = d.BMKEY.ToString()
                });
            }

            var ownerType = new DropDownListModel();
            ownerType.DropDownList = "VALUE3";
            ownerType.DropDownListItem = listItems;
            #endregion
            CarInfoModel model = new CarInfoModel();
            model.belongDept = belongDept;
            model.vehicleType = vehicleType;
            model.belongNet = belongNet;
            model.ownerType = ownerType;
            var data = CarBLL.GetByVehId(id);
            model.carInfo.Id = data.Id;
            model.carInfo.VehicleNo = data.VehicleNo;
            model.carInfo.VehicleType = data.VehicleType;
            model.carInfo.VehicleBrand = data.VehicleBrand;
            model.carInfo.BelongDeptId = data.BelongDeptId;
            model.carInfo.BelongNetId = data.BelongNetId;
            model.carInfo.Owner = data.Owner;
            model.carInfo.OwnerType = data.OwnerType;
            model.carInfo.Remark = data.Remark;
            model.carInfo.Cldwzdsbh = data.Cldwzdsbh;
            model.carInfo.Wlwkhm = data.Wlwkhm;
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult PasswordUpdate()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult UserLogin(string userName, string userPwd)
        {
            UserInfo info = new UserInfo();
            var data = UserBLL.Login(userName, userPwd, out info);
            if (data == 1)
            {
                //添加日志
                LogInfo log = new LogInfo();
                log.User_Id = info.Id;
                log.UserName = info.UserName;
                log.Operation = "用户登录";
                log.Method = "Home/Login";
                log.Ip = GetClientIP();
                log.Params = "userName:" + userName + ";userPwd:" + userPwd;
                LogBLL.AddLog(log);
                Session[WebConstants.UserSession] = info;
                //return RedirectToAction("Index", "Home");
                return new JsonResult
                {
                    Data = info
                };
            }
            else
            {
                return new JsonResult
                {
                    Data = data
                };
            }
        }

        public ActionResult UserLogout()
        {
            Session[WebConstants.UserSession] = null;
            return Redirect("../Home/Index");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LoggerFilter(Key = "Home/btnPasswordUpdateSave_Click", Description = "用户密码修改")]
        public ActionResult btnPasswordUpdateSave_Click(FormCollection values)
        {
            var isSuccess = UserBLL.UpdateUserPwd(Convert.ToInt32(values["UserId"]), values["Password"]);
            ActiveWindow.HidePostBack();

            if (isSuccess)
            {
                ShowNotify("密码修改成功！");
            }
            else
            {
                ShowNotify("密码修改失败！");
            }
            return UIHelper.Result();
        }

        /// <summary>
        /// 获取客户端IP
        /// </summary>
        /// <returns></returns>
        private string GetClientIP()
        {
            string result = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (result == null || result == String.Empty)
            {
                result = Request.ServerVariables["REMOTE_ADDR"];
            }
            if (result == null || result == String.Empty)
            {
                result = Request.UserHostAddress;
            }
            if (result.StartsWith("::"))
            {
                result = "127.0.0.1";
            }

            return result;
        }

        [AllowAnonymous]
        public ActionResult IsTimeOut()
        {
            bool isTimeOut = false;
            if (Session[WebConstants.UserSession] == null)
            {
                isTimeOut = true;
            }
            return new JsonResult
            {
                Data = isTimeOut
            };
        }

        #region BindGrid

        private void LoadData_UserMessage(string phone)
        {
            var recordCount = 0;
            //近一个星期数据
            var stTime = DateTime.Parse(DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd") + " 00:00:00");
            var endTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            var data = MessageBLL.GetList_Map(phone, "", 0, stTime, endTime, 1, 20, out recordCount);
            ViewBag.UserMessageRecordCount = recordCount;
            ViewBag.UserMessageDataSource = data;
        }

        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserMessageGrid_PageIndexChanged(JArray UserMessageGrid_fields, string phone, int type, DateTime startTime, DateTime endTime, int UserMessageGrid_pageIndex, int UserMessageGrid_pageSize)
        {
            var grid1 = UIHelper.Grid("UserMessageGrid");
            var recordCount = 0;
            var stTime = DateTime.Parse(startTime.ToString("yyyy-MM-dd") + " 00:00:00");
            var edTime = DateTime.Parse(endTime.ToString("yyyy-MM-dd") + " 23:59:59");
            var data = MessageBLL.GetList_Map(phone, "", type, stTime, edTime, UserMessageGrid_pageIndex + 1, UserMessageGrid_pageSize, out recordCount);

            grid1.RecordCount(recordCount);
            grid1.DataSource(data, UserMessageGrid_fields);

            return UIHelper.Result();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [LoggerFilter(Key = "Home/btnUserMessageSearch_Click", Description = "用户消息查询")]
        public ActionResult btnUserMessageSearch_Click(JArray UserMessageGrid_fields, string phone, int type, DateTime startTime, DateTime endTime, int UserMessageGrid_pageIndex, int UserMessageGrid_pageSize)
        {
            var grid1 = UIHelper.Grid("UserMessageGrid");
            var recordCount = 0;
            var stTime = DateTime.Parse(startTime.ToString("yyyy-MM-dd") + " 00:00:00");
            var edTime = DateTime.Parse(endTime.ToString("yyyy-MM-dd") + " 23:59:59");
            var data = MessageBLL.GetList_Map(phone, "", type, stTime, edTime, UserMessageGrid_pageIndex + 1, UserMessageGrid_pageSize, out recordCount);

            grid1.RecordCount(recordCount);
            grid1.DataSource(data, UserMessageGrid_fields);

            return UIHelper.Result();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [LoggerFilter(Key = "Home/btnSend_Click", Description = "用户消息发送")]
        public ActionResult btnSend_Click(FormCollection values)
        {
            var msg = NotificationBLL.SendMessage(values["Phone"], values["Title"], values["Content"]);
            ActiveWindow.HidePostBack();
            ShowNotify(msg);
            return UIHelper.Result();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Window1_Close()
        {
            return UIHelper.Result();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Window2_Close()
        {
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
        public ActionResult AddAlarmInfo(string info, string alarmAddress, string alarmObjectName, string alarmType, string alarmTime, string objectName)
        {
            INFO_ALARMINFO alarmInfo = new INFO_ALARMINFO();
            alarmInfo.ALARMINFO = info;
            alarmInfo.ALARMADDRESS = alarmAddress;
            alarmInfo.ALARMOBJECTNAME = alarmObjectName;
            alarmInfo.ALARMTYPE = Convert.ToInt32(alarmType);
            alarmInfo.ALARMTIME = DateTime.Parse(alarmTime);
            var isSuccess = AlarmBLL.Add(alarmInfo);
            MapBLL.UpdateGpsInfo(objectName);
            return new JsonResult
            {
                Data = isSuccess
            };
        }

        public ActionResult GetMessageList(string phone)
        {
            var data = MapBLL.GetMessageList(phone);

            return new JsonResult
            {
                Data = data
            };
        }

        // GET: Home/DownloadApp
        public ActionResult DownloadApp()
        {
            string filePath = Server.MapPath("~/File/app-release.apk");
            return File(filePath, "application/vnd.android", "app-release.apk");
        }
    }
}