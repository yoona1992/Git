using CCSIM.BLL;
using CCSIM.DAL.Model;
using CCSIM.Web.Controllers;
using CCSIM.Web.Models;
using FineUIMvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CCSIM.Web.Areas.NetArea.Controllers
{
    public class NetAreaController : BaseController
    {
        // GET: NetArea/NetArea
        public ActionResult Index()
        {
            LoadData();
            return View();
        }

        public ActionResult Add()
        {
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

            return View(model);
        }

        public ActionResult Edit()
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
            info.netInfo.Id =data.ID;
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

        public ActionResult LonAndLat()
        {
            LonAndLatModel model = new LonAndLatModel();
            model.operatorType = Convert.ToInt32(Request.QueryString["type"]);
            model.id = Convert.ToInt32(Request.QueryString["id"]);
            return View(model);
        }


        #region BindGrid

        private void LoadData()
        {
            var recordCount = 0;
            var data = NetManageBLL.GetList("", -1, 1, 20, out recordCount);
            ViewBag.Grid1RecordCount = recordCount;
            ViewBag.Grid1DataSource = data;
        }

        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NetGrid_PageIndexChanged(JArray NetGrid_fields, string netName, int NetGrid_pageIndex, int NetGrid_pageSize)
        {
            var grid1 = UIHelper.Grid("NetGrid");
            var recordCount = 0;
            var data = NetManageBLL.GetList(netName, -1, NetGrid_pageIndex + 1, NetGrid_pageSize, out recordCount);

            grid1.RecordCount(recordCount);
            grid1.DataSource(data, NetGrid_fields);

            return UIHelper.Result();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRows(JArray selectedRows, JArray NetGrid_fields, string netName, int NetGrid_pageIndex, int NetGrid_pageSize)
        {
            var ids = new List<int>();
            foreach (var id in selectedRows)
            {
                ids.Add(Convert.ToInt32(id));
            }

            var isSuccess = NetManageBLL.Delete(ids.ToArray());

            if (isSuccess)
            {
                ShowNotify("数据删除成功！");
            }
            else
            {
                ShowNotify("数据删除失败！");
            }

            var grid1 = UIHelper.Grid("NetGrid");
            var recordCount = 0;
            var data = NetManageBLL.GetList(netName, -1, NetGrid_pageIndex + 1, NetGrid_pageSize, out recordCount);

            grid1.RecordCount(recordCount);
            grid1.DataSource(data, NetGrid_fields);
            return UIHelper.Result();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult btnSearch_Click(JArray NetGrid_fields, string netName, int NetGrid_pageIndex, int NetGrid_pageSize)
        {
            var grid1 = UIHelper.Grid("NetGrid");
            var recordCount = 0;
            var data = NetManageBLL.GetList(netName, -1, NetGrid_pageIndex + 1, NetGrid_pageSize, out recordCount);

            grid1.RecordCount(recordCount);
            grid1.DataSource(data, NetGrid_fields);

            return UIHelper.Result();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult btnAdd_Click(FormCollection values)
        {
            CFG_NETINFO info = new CFG_NETINFO();
            info.NAME = values["Name"];
            info.BELONGDEPTID = Convert.ToInt32(values["BelongDeptId"]);
            info.POPULATIONINFO = values["PopulationInfo"];
            info.HOUSEINFO = values["HouseInfo"];
            info.BELONGAREA = values["BelongArea"];
            info.UNITSTOREINFO = values["UnitStoreInfo"];
            info.NETCOLOR = values["NetColor"];
            info.LONANDLAT = values["LonAndLat"];
            info.REMARK = values["Remark"];
            info.ISDELETED = 0;
            var isSuccess = NetManageBLL.Add(info);
            ActiveWindow.HidePostBack();

            if (isSuccess)
            {
                ShowNotify("数据新增成功！");
            }
            else
            {
                ShowNotify("数据新增失败！");
            }
            return UIHelper.Result();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult btnSave_Click(FormCollection values)
        {
            CFG_NETINFO info = new CFG_NETINFO();
            info.ID = Convert.ToInt32(values["Id"]);
            info.NAME = values["Name"];
            info.BELONGDEPTID = Convert.ToInt32(values["BelongDeptId"]);
            info.POPULATIONINFO = values["PopulationInfo"];
            info.HOUSEINFO = values["HouseInfo"];
            info.BELONGAREA = values["BelongArea"];
            info.UNITSTOREINFO = values["UnitStoreInfo"];
            info.NETCOLOR = values["NetColor"];
            info.LONANDLAT = values["LonAndLat"];
            info.REMARK = values["Remark"];
            var isSuccess = NetManageBLL.Update(info);
            ActiveWindow.HidePostBack();

            if (isSuccess)
            {
                ShowNotify("数据修改成功！");
            }
            else
            {
                ShowNotify("数据修改失败！");
            }
            return UIHelper.Result();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Window1_Close()
        {
            // 调用父页面定义的函数 reload
            PageContext.RegisterStartupScript("reload();");

            return UIHelper.Result();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Window2_Close()
        {
            // 调用父页面定义的函数 reload
            PageContext.RegisterStartupScript("reload();");

            return UIHelper.Result();
        }
        public ActionResult GetLonAndLat(int id)
        {
            var data = NetManageBLL.Get(id);

            return new JsonResult
            {
                Data = data.LONANDLAT
            };
        }
    }
}