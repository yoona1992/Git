using CCSIM.BLL;
using CCSIM.DAL.Model;
using CCSIM.Entity;
using CCSIM.Web.App_Start;
using CCSIM.Web.Controllers;
using FineUIMvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CCSIM.Web.Areas.FileManage.Controllers
{
    public class FileManageController : BaseController
    {
        // GET: FileManage/FileManage
        public ActionResult Index()
        {
            LoadData();
            return View();
        }
        
        public ActionResult QrCodeDownload()
        {
            return View();
        }

        #region BindGrid

        private void LoadData()
        {
            var recordCount = 0;
            var data = FileBLL.GetList("", 1, 20, out recordCount);
            ViewBag.Grid1RecordCount = recordCount;
            ViewBag.Grid1DataSource = data;
        }

        #endregion


        [HttpPost]
        [ValidateAntiForgeryToken]
        [LoggerFilter(Key = "FileManage/btnUpload_Click", Description = "文件上传")]
        public ActionResult btnUpload_Click(HttpPostedFileBase uploadFile, FormCollection values)
        {
            if (uploadFile != null)
            {
                string fileName = uploadFile.FileName;
                fileName = fileName.Replace(":", "_").Replace(" ", "_").Replace("\\", "_").Replace("/", "_");
                string truthFileName = DateTime.Now.Ticks.ToString() + "_" + fileName;
                uploadFile.SaveAs(Server.MapPath("~/upload/" + truthFileName));

                //保存文件
                INFO_FILEINFO info = new INFO_FILEINFO();
                info.FILENAME = fileName;
                info.FILEURL = truthFileName;
                info.UPLOADTIME = DateTime.Now;

                if (Session[WebConstants.UserSession] == null)
                {
                    info.UPLOADUSER = -1;
                }
                else
                {
                    UserInfo user = Session[WebConstants.UserSession] as UserInfo;
                    info.UPLOADUSER = user.Id;
                }
                if (System.IO.File.Exists(Server.MapPath("~/upload/" + truthFileName)))
                {
                    System.IO.FileInfo fileInfo = new System.IO.FileInfo(Server.MapPath("~/upload/" + truthFileName));
                    info.FILESIZE = System.Math.Ceiling(fileInfo.Length / 1024.0) + "KB";
                }

                var isSuccess = FileBLL.Add(info);

                if (isSuccess)
                {
                    ShowNotify("数据上传成功！");
                }
                else
                {
                    ShowNotify("数据上传失败！");
                }
            }

            // 调用父页面定义的函数 reload
            PageContext.RegisterStartupScript("reload();");
            return UIHelper.Result();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FileManageGrid_PageIndexChanged(JArray FileManageGrid_fields, string fileName, int FileManageGrid_pageIndex, int FileManageGrid_pageSize)
        {
            var grid1 = UIHelper.Grid("FileManageGrid");
            var recordCount = 0;
            var data = FileBLL.GetList(fileName, FileManageGrid_pageIndex + 1, FileManageGrid_pageSize, out recordCount);

            grid1.RecordCount(recordCount);
            grid1.DataSource(data, FileManageGrid_fields);

            return UIHelper.Result();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [LoggerFilter(Key = "FileManage/DeleteRows", Description = "文件删除")]
        public ActionResult DeleteRows(JArray selectedRows, JArray FileManageGrid_fields, string fileName, int FileManageGrid_pageIndex, int FileManageGrid_pageSize)
        {
            var ids = new List<int>();
            foreach (var id in selectedRows)
            {
                ids.Add(Convert.ToInt32(id));
            }

            var isSuccess = FileBLL.Delete(ids.ToArray());

            if (isSuccess)
            {
                ShowNotify("数据删除成功！");
            }
            else
            {
                ShowNotify("数据删除失败！");
            }

            var grid1 = UIHelper.Grid("FileManageGrid");
            var recordCount = 0;
            var data = FileBLL.GetList(fileName, FileManageGrid_pageIndex + 1, FileManageGrid_pageSize, out recordCount);

            grid1.RecordCount(recordCount);
            grid1.DataSource(data, FileManageGrid_fields);
            return UIHelper.Result();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [LoggerFilter(Key = "FileManage/btnSearch_Click", Description = "文件查询")]
        public ActionResult btnSearch_Click(JArray FileManageGrid_fields, string fileName, int FileManageGrid_pageIndex, int FileManageGrid_pageSize)
        {
            var grid1 = UIHelper.Grid("FileManageGrid");
            var recordCount = 0;
            var data = FileBLL.GetList(fileName, FileManageGrid_pageIndex + 1, FileManageGrid_pageSize, out recordCount);

            grid1.RecordCount(recordCount);
            grid1.DataSource(data, FileManageGrid_fields);

            return UIHelper.Result();
        }

        [AllowAnonymous]
        // GET: FileManage/Download
        [LoggerFilter(Key = "FileManage/Download", Description = "文件下载")]
        public ActionResult Download(int id)
        {
            var data = FileBLL.Get(id);
            string filePath = Server.MapPath("~/upload/" + data.FILEURL);
            return File(filePath, "application/vnd.android", data.FILENAME);
        }
    }
}