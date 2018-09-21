using CCSIM.BLL;
using CCSIM.DAL.Model;
using CCSIM.Web.App_Start;
using CCSIM.Web.Models;
using FineUIMvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CCSIM.Web.Areas.User.Controllers
{
    public class UserInfoController : CCSIM.Web.Controllers.BaseController
    {
        // GET: User/UserInfo
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

            #endregion
            UserInfoModel model = new UserInfoModel();
            model.userType = userType;
            return View(model);
        }

        public ActionResult Add()
        {
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
            belongNet.DropDownList = "";
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
            return View(model);
        }

        public ActionResult Edit()
        {
            var id = Convert.ToInt32(Request.QueryString["id"]);
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
            belongNet.DropDownList = "";
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
            var data = UserBLL.Get(id);
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


        #region BindGrid

        private void LoadData()
        {
            var recordCount = 0;
            var data = UserBLL.GetList("","", -1, -1, 1, 20, out recordCount);
            ViewBag.Grid1RecordCount = recordCount;
            ViewBag.Grid1DataSource = data;
        }

        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserGrid_PageIndexChanged(JArray UserGrid_fields, string userName, string telephone,int userType, int UserGrid_pageIndex, int UserGrid_pageSize)
        {
            var grid1 = UIHelper.Grid("UserGrid");
            var recordCount = 0;
            var data = UserBLL.GetList(userName, telephone, -1,userType, UserGrid_pageIndex + 1, UserGrid_pageSize, out recordCount);

            grid1.RecordCount(recordCount);
            grid1.DataSource(data, UserGrid_fields);

            return UIHelper.Result();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [LoggerFilter(Key = "UserInfo/DeleteRows", Description = "用户删除")]
        public ActionResult DeleteRows(JArray selectedRows, JArray UserGrid_fields, string userName, string telephone,int userType, int UserGrid_pageIndex, int UserGrid_pageSize)
        {
            var ids = new List<int>();
            foreach (var id in selectedRows)
            {
                ids.Add(Convert.ToInt32(id));
            }

            var isSuccess = UserBLL.Delete(ids.ToArray());

            if (isSuccess)
            {
                ShowNotify("数据删除成功！");
            }
            else
            {
                ShowNotify("数据删除失败！");
            }

            var grid1 = UIHelper.Grid("UserGrid");
            var recordCount = 0;
            var data = UserBLL.GetList(userName,telephone, -1, userType, UserGrid_pageIndex + 1, UserGrid_pageSize, out recordCount);

            grid1.RecordCount(recordCount);
            grid1.DataSource(data, UserGrid_fields);
            return UIHelper.Result();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [LoggerFilter(Key = "UserInfo/btnSearch_Click", Description = "用户查询")]
        public ActionResult btnSearch_Click(JArray UserGrid_fields, string userName, string telephone,int userType, int UserGrid_pageIndex, int UserGrid_pageSize)
        {
            var grid1 = UIHelper.Grid("UserGrid");
            var recordCount = 0;
            var data = UserBLL.GetList(userName,telephone, -1,userType, UserGrid_pageIndex+1, UserGrid_pageSize, out recordCount);

            grid1.RecordCount(recordCount);
            grid1.DataSource(data, UserGrid_fields);

            return UIHelper.Result();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [LoggerFilter(Key = "UserInfo/btnAdd_Click", Description = "用户添加")]
        public ActionResult btnAdd_Click(FormCollection values)
        {
            CFG_USERINFO info = new CFG_USERINFO();
            info.NAME = values["Name"];
            info.SEX = Convert.ToInt32(values["Sex"]);
            info.AGE = values["Age"];
            info.TELEPHONE = values["Telephone"];
            info.CERTIFICATETYPE = Convert.ToInt32(values["CertificateType"]);
            info.CERTIFICATENUM = values["CertificateNum"];
            info.DIRECTION = values["Direction"];
            info.ADDRESS = values["Address"];
            info.BELONGDEPTID = Convert.ToInt32(values["BelongDeptId"]);
            info.BELONGNETID = string.IsNullOrWhiteSpace(values["BelongNetId"])?-1:Convert.ToInt32(values["BelongNetId"]);
            info.USERTYPE = Convert.ToInt32(values["UserType"]);
            info.REMARK = values["Remark"];
            info.VIRTUALTRUMPET = values["VirtualTrumpet"];
            info.ISDELETED = 0;
            var isSuccess = UserBLL.Add(info);
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
        [LoggerFilter(Key = "UserInfo/btnSave_Click", Description = "用户修改")]
        public ActionResult btnSave_Click(FormCollection values)
        {
            CFG_USERINFO info = new CFG_USERINFO();
            info.ID = Convert.ToInt32(values["Id"]);
            info.NAME = values["Name"];
            info.SEX = Convert.ToInt32(values["Sex"]);
            info.AGE = values["Age"];
            info.TELEPHONE = values["Telephone"];
            info.CERTIFICATETYPE = Convert.ToInt32(values["CertificateType"]);
            info.CERTIFICATENUM = values["CertificateNum"];
            info.DIRECTION = values["Direction"];
            info.ADDRESS = values["Address"];
            info.BELONGDEPTID = Convert.ToInt32(values["BelongDeptId"]);
            info.BELONGNETID = string.IsNullOrWhiteSpace(values["BelongNetId"]) ? -1 : Convert.ToInt32(values["BelongNetId"]);
            info.USERTYPE = Convert.ToInt32(values["UserType"]);
            info.REMARK = values["Remark"];
            info.VIRTUALTRUMPET = values["VirtualTrumpet"];
            var isSuccess = UserBLL.Update(info);
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