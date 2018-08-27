using CCSIM.BLL;
using CCSIM.DAL.Model;
using CCSIM.Web.Models;
using FineUIMvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CCSIM.Web.Areas.Car.Controllers
{
    public class CarInfoController : CCSIM.Web.Controllers.BaseController
    {
        // GET: CarInfo/CarInfo
        public ActionResult Index()
        {
            LoadData();
            #region 下拉框绑定
            var ownerTypeList = CodeBLL.GetCodeListByParentCode("YCBM");
            var listItems = new List<ListItem>();
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
            ownerType.DropDownList = "VALUE";
            ownerType.DropDownListItem = listItems;
            #endregion
            CarInfoModel model = new CarInfoModel();
            model.ownerType = ownerType;
            return View(model);
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
            var data = CarBLL.Get(id);
            model.carInfo.Id = data.ID;
            model.carInfo.VehicleNo = data.VEHICLENO;
            model.carInfo.VehicleType = data.VEHICLETYPE;
            model.carInfo.VehicleBrand = data.VEHICLEBRAND;
            model.carInfo.BelongDeptId = data.BELONGDEPTID;
            model.carInfo.BelongNetId = data.BELONGNETID;
            model.carInfo.Owner = data.OWNER;
            model.carInfo.OwnerType = data.OWNERTYPE;
            model.carInfo.Remark = data.REMARK;
            return View(model);
        }


        #region BindGrid

        private void LoadData()
        {
            var recordCount = 0;
            var data = CarBLL.GetList("",-1, 1, 20, out recordCount);
            ViewBag.Grid1RecordCount = recordCount;
            ViewBag.Grid1DataSource = data;
        }

        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CarGrid_PageIndexChanged(JArray CarGrid_fields, string vehicleNo,int ownerType, int CarGrid_pageIndex, int CarGrid_pageSize)
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
        public ActionResult DeleteRows(JArray selectedRows, JArray CarGrid_fields, string vehicleNo,int ownerType, int CarGrid_pageIndex, int CarGrid_pageSize)
        {
            var ids = new List<int>();
            foreach (var id in selectedRows)
            {
                ids.Add(Convert.ToInt32(id));
            }

            var isSuccess = CarBLL.Delete(ids.ToArray());

            if (isSuccess)
            {
                ShowNotify("数据删除成功！");
            }
            else
            {
                ShowNotify("数据删除失败！");
            }

            var grid1 = UIHelper.Grid("CarGrid");
            var recordCount = 0;
            var data = CarBLL.GetList(vehicleNo, ownerType, CarGrid_pageIndex + 1, CarGrid_pageSize, out recordCount);

            grid1.RecordCount(recordCount);
            grid1.DataSource(data, CarGrid_fields);
            return UIHelper.Result();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult btnSearch_Click(JArray CarGrid_fields, string vehicleNo,int ownerType, int CarGrid_pageIndex, int CarGrid_pageSize)
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
        public ActionResult btnAdd_Click(FormCollection values)
        {
            CFG_CARINFO info = new CFG_CARINFO();
            info.VEHICLENO = values["VehicleNo"];
            info.VEHICLETYPE = Convert.ToInt32(values["VehicleType"]);
            info.VEHICLEBRAND = values["VehicleBrand"];
            info.BELONGDEPTID = Convert.ToInt32(values["BelongDeptId"]);
            info.BELONGNETID = Convert.ToInt32(values["BelongNetId"]);
            info.REMARK = values["Remark"];
            info.OWNER = values["Owner"];
            info.OWNERTYPE = Convert.ToInt32(values["OwnerType"]);
            info.ISDELETED = 0;
            var isSuccess = CarBLL.Add(info);
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
            CFG_CARINFO info = new CFG_CARINFO();
            info.ID = Convert.ToInt32(values["Id"]);
            info.VEHICLENO = values["VehicleNo"];
            info.VEHICLETYPE = Convert.ToInt32(values["VehicleType"]);
            info.VEHICLEBRAND = values["VehicleBrand"];
            info.BELONGDEPTID = Convert.ToInt32(values["BelongDeptId"]);
            info.BELONGNETID = Convert.ToInt32(values["BelongNetId"]);
            info.OWNERTYPE = Convert.ToInt32(values["OwnerType"]);
            info.REMARK = values["Remark"];
            info.OWNER = values["Owner"];
            var isSuccess = CarBLL.Update(info);
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

    }
}