using CCSIM.BLL;
using CCSIM.Web.App_Start;
using CCSIM.Web.Models;
using FineUIMvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CCSIM.Web.Areas.Trajectory.Controllers
{
    public class TrajectoryController : Controller
    {
        // GET: Trajectory/Trajectory
        public ActionResult Index()
        {
            LoadData();

            #region 下拉框绑定
            var userTypeList = CodeBLL.GetCodeListByParentCode("RYLX");
            var listItems = new List<ListItem>();
            listItems.Add(new ListItem
            {
                Text = "全部",
                Value = "-1"
            });
            foreach (var d in userTypeList)
            {
                listItems.Add(new ListItem
                {
                    Text = d.BMVALUE,
                    Value = d.BMKEY.ToString()
                });
            }

            var userType = new DropDownListModel();
            userType.DropDownList = "VALUE";
            userType.DropDownListItem = listItems;


            var ownerTypeList = CodeBLL.GetCodeListByParentCode("YCBM");
            listItems = new List<ListItem>();
            listItems.Add(new ListItem
            {
                Text = "全部",
                Value = "-1"
            });
            foreach (var d in ownerTypeList)
            {
                listItems.Add(new ListItem
                {
                    Text = d.BMVALUE,
                    Value = d.BMKEY.ToString()
                });
            }

            var ownerType = new DropDownListModel();
            ownerType.DropDownList = "VALUE1";
            ownerType.DropDownListItem = listItems;
            #endregion
            TrajectoryInfoModel model = new TrajectoryInfoModel();
            model.userType = userType;
            model.ownerType = ownerType;
            return View(model);
        }

        public ActionResult Detail_User()
        {
            LoadData_DetailUser();
            return View();
        }

        public ActionResult Detail_Car()
        {
            LoadData_DetailCar();
            return View();
        }

        public ActionResult Map_User()
        {
            return View();
        }
        public ActionResult Map_Car()
        {
            return View();
        }


        #region BindGrid

        private void LoadData()
        {
            var recordCount = 0;
            var data = UserBLL.GetList("", "", -1, -1, 1, 20, out recordCount);
            ViewBag.UserGridRecordCount = recordCount;
            ViewBag.UserGridDataSource = data;

            var recordCount1 = 0;
            var data1 = CarBLL.GetList("", -1, 1, 20, out recordCount1);
            ViewBag.CarGridRecordCount = recordCount1;
            ViewBag.CarGridDataSource = data1;
        }

        private void LoadData_DetailUser()
        {
            var telephone = Request.QueryString["telephone"];
            var recordCount = 0;
            var stTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            var endTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            var data = TrajectoryBLL.GetList_User(telephone, stTime, endTime, 1, 20, out recordCount);
            ViewBag.TrajectoryDetailUserGridRecordCount = recordCount;
            ViewBag.TrajectoryDetailUserGridDataSource = data;
        }

        private void LoadData_DetailCar()
        {
            var cldwzdsbh = Request.QueryString["cldwzdsbh"];
            var recordCount = 0;
            var stTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            var endTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            var data = TrajectoryBLL.GetList_Car(cldwzdsbh, stTime, endTime, 1, 20, out recordCount);
            ViewBag.TrajectoryDetailCarGridRecordCount = recordCount;
            ViewBag.TrajectoryDetailCarGridDataSource = data;
        }

        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserGrid_PageIndexChanged(JArray UserGrid_fields, string userName, string telephone, int userType, int UserGrid_pageIndex, int UserGrid_pageSize)
        {
            var grid1 = UIHelper.Grid("UserGrid");
            var recordCount = 0;
            var data = UserBLL.GetList(userName, telephone, -1, userType, UserGrid_pageIndex + 1, UserGrid_pageSize, out recordCount);

            grid1.RecordCount(recordCount);
            grid1.DataSource(data, UserGrid_fields);

            return UIHelper.Result();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CarGrid_PageIndexChanged(JArray CarGrid_fields, string vehicleNo, int ownerType, int CarGrid_pageIndex, int CarGrid_pageSize)
        {
            var grid1 = UIHelper.Grid("CarGrid");
            var recordCount = 0;
            var data = CarBLL.GetList(vehicleNo, ownerType, CarGrid_pageIndex + 1, CarGrid_pageSize, out recordCount);

            grid1.RecordCount(recordCount);
            grid1.DataSource(data, CarGrid_fields);

            return UIHelper.Result();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TrajectoryDetailUserGrid_PageIndexChanged(JArray TrajectoryDetailUserGrid_fields, string telephone, string passTime, int TrajectoryDetailUserGrid_pageIndex, int TrajectoryDetailUserGrid_pageSize)
        {
            var grid1 = UIHelper.Grid("TrajectoryDetailUserGrid");
            var recordCount = 0;
            var startTimeStr = passTime.Substring(0, 19);
            var endTimeStr = passTime.Substring(22, 19);
            var stTime = DateTime.Parse(startTimeStr);
            var endTime = DateTime.Parse(endTimeStr);
            var data = TrajectoryBLL.GetList_User(telephone, stTime, endTime, TrajectoryDetailUserGrid_pageIndex + 1, TrajectoryDetailUserGrid_pageSize, out recordCount);

            grid1.RecordCount(recordCount);
            grid1.DataSource(data, TrajectoryDetailUserGrid_fields);

            return UIHelper.Result();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TrajectoryDetailCarGrid_PageIndexChanged(JArray TrajectoryDetailCarGrid_fields, string cldwzdsbh, string passTime, int TrajectoryDetailCarGrid_pageIndex, int TrajectoryDetailCarGrid_pageSize)
        {
            var grid1 = UIHelper.Grid("TrajectoryDetailCarGrid");
            var recordCount = 0;
            var startTimeStr = passTime.Substring(0, 19);
            var endTimeStr = passTime.Substring(22, 19);
            var stTime = DateTime.Parse(startTimeStr);
            var endTime = DateTime.Parse(endTimeStr);
            var data = TrajectoryBLL.GetList_Car(cldwzdsbh, stTime, endTime, TrajectoryDetailCarGrid_pageIndex + 1, TrajectoryDetailCarGrid_pageSize, out recordCount);

            grid1.RecordCount(recordCount);
            grid1.DataSource(data, TrajectoryDetailCarGrid_fields);

            return UIHelper.Result();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [LoggerFilter(Key = "Trajectory/btnUserSearch_Click", Description = "用户查询")]
        public ActionResult btnUserSearch_Click(JArray UserGrid_fields, string userName, string telephone, int userType, int UserGrid_pageIndex, int UserGrid_pageSize)
        {
            var grid1 = UIHelper.Grid("UserGrid");
            var recordCount = 0;
            var data = UserBLL.GetList(userName, telephone, -1, userType, UserGrid_pageIndex + 1, UserGrid_pageSize, out recordCount);

            grid1.RecordCount(recordCount);
            grid1.DataSource(data, UserGrid_fields);

            return UIHelper.Result();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [LoggerFilter(Key = "Trajectory/btnCarSearch_Click", Description = "车辆查询")]
        public ActionResult btnCarSearch_Click(JArray CarGrid_fields, string vehicleNo, int ownerType, int CarGrid_pageIndex, int CarGrid_pageSize)
        {
            var grid1 = UIHelper.Grid("CarGrid");
            var recordCount = 0;
            var data = CarBLL.GetList(vehicleNo, ownerType, CarGrid_pageIndex + 1, CarGrid_pageSize, out recordCount);

            grid1.RecordCount(recordCount);
            grid1.DataSource(data, CarGrid_fields);

            return UIHelper.Result();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [LoggerFilter(Key = "Trajectory/btnDetailUserSearch_Click", Description = "用户轨迹查询")]
        public ActionResult btnDetailUserSearch_Click(JArray TrajectoryDetailUserGrid_fields, string telephone, string passTime, int TrajectoryDetailUserGrid_pageIndex, int TrajectoryDetailUserGrid_pageSize)
        {
            var grid1 = UIHelper.Grid("TrajectoryDetailUserGrid");
            var recordCount = 0;
            var startTimeStr = passTime.Substring(0, 19);
            var endTimeStr = passTime.Substring(22, 19);
            var stTime = DateTime.Parse(startTimeStr);
            var endTime = DateTime.Parse(endTimeStr);
            var data = TrajectoryBLL.GetList_User(telephone, stTime, endTime, TrajectoryDetailUserGrid_pageIndex + 1, TrajectoryDetailUserGrid_pageSize, out recordCount);

            grid1.RecordCount(recordCount);
            grid1.DataSource(data, TrajectoryDetailUserGrid_fields);

            return UIHelper.Result();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [LoggerFilter(Key = "Trajectory/btnDetailCarSearch_Click", Description = "车辆轨迹查询")]
        public ActionResult btnDetailCarSearch_Click(JArray TrajectoryDetailCarGrid_fields, string cldwzdsbh, string passTime, int TrajectoryDetailCarGrid_pageIndex, int TrajectoryDetailCarGrid_pageSize)
        {
            var grid1 = UIHelper.Grid("TrajectoryDetailCarGrid");
            var recordCount = 0;
            var startTimeStr = passTime.Substring(0, 19);
            var endTimeStr = passTime.Substring(22, 19);
            var stTime = DateTime.Parse(startTimeStr);
            var endTime = DateTime.Parse(endTimeStr);
            var data = TrajectoryBLL.GetList_Car(cldwzdsbh, stTime, endTime, TrajectoryDetailCarGrid_pageIndex + 1, TrajectoryDetailCarGrid_pageSize, out recordCount);

            grid1.RecordCount(recordCount);
            grid1.DataSource(data, TrajectoryDetailCarGrid_fields);

            return UIHelper.Result();
        }

        public ActionResult GetList_User(string telephone, string passTime)
        {
            var startTimeStr = passTime.Substring(0, 19);
            var endTimeStr = passTime.Substring(22, 19);
            var stTime = DateTime.Parse(startTimeStr);
            var endTime = DateTime.Parse(endTimeStr);
            var data = TrajectoryBLL.GetList_User(telephone, stTime, endTime);

            return new JsonResult
            {
                Data = data
            };
        }
        public ActionResult GetList_Car(string cldwzdsbh, string passTime)
        {
            var startTimeStr = passTime.Substring(0, 19);
            var endTimeStr = passTime.Substring(22, 19);
            var stTime = DateTime.Parse(startTimeStr);
            var endTime = DateTime.Parse(endTimeStr);
            var data = TrajectoryBLL.GetList_Car(cldwzdsbh, stTime, endTime);

            return new JsonResult
            {
                Data = data
            };
        }

    }
}