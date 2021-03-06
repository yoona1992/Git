﻿using CCSIM.BLL;
using CCSIM.Entity;
using CCSIM.Web.App_Start;
using FineUIMvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CCSIM.Web.Areas.Message.Controllers
{
    public class MessageController : CCSIM.Web.Controllers.BaseController
    {
        // GET: Message/Message
        public ActionResult Index()
        {
            LoadData();
            return View();
        }
        public ActionResult Detail()
        {
            ViewData["Id"] = Request.QueryString["id"];
            return View();
        }

        #region BindGrid

        private void LoadData()
        {
            var recordCount = 0;
            var stTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            var endTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            var data = MessageBLL.GetList("", "", stTime, endTime, 1, 20, out recordCount);
            ViewBag.Grid1RecordCount = recordCount;
            ViewBag.Grid1DataSource = data;
        }

        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MessageGrid_PageIndexChanged(JArray MessageGrid_fields, string username, string title, DateTime startTime, DateTime endTime, int MessageGrid_pageIndex, int MessageGrid_pageSize)
        {
            var grid1 = UIHelper.Grid("MessageGrid");
            var recordCount = 0;
            var stTime = DateTime.Parse(startTime.ToString("yyyy-MM-dd") + " 00:00:00");
            var edTime = DateTime.Parse(endTime.ToString("yyyy-MM-dd") + " 23:59:59");
            var data = MessageBLL.GetList(username, title, stTime, edTime, MessageGrid_pageIndex + 1, MessageGrid_pageSize, out recordCount);

            grid1.RecordCount(recordCount);
            grid1.DataSource(data, MessageGrid_fields);

            return UIHelper.Result();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [LoggerFilter(Key = "Message/btnSearch_Click", Description = "资料数据查询")]
        public ActionResult btnSearch_Click(JArray MessageGrid_fields, string username, string title, DateTime startTime, DateTime endTime, int MessageGrid_pageIndex, int MessageGrid_pageSize)
        {
            var grid1 = UIHelper.Grid("MessageGrid");
            var recordCount = 0;
            var stTime = DateTime.Parse(startTime.ToString("yyyy-MM-dd") + " 00:00:00");
            var edTime = DateTime.Parse(endTime.ToString("yyyy-MM-dd") + " 23:59:59");
            var data = MessageBLL.GetList(username, title, stTime, edTime, MessageGrid_pageIndex + 1, MessageGrid_pageSize, out recordCount);

            grid1.RecordCount(recordCount);
            grid1.DataSource(data, MessageGrid_fields);

            return UIHelper.Result();
        }

        [LoggerFilter(Key = "Message/GetFileList", Description = "资料附件查看")]
        public ActionResult GetFileList(int id)
        {
            var data = MessageBLL.Get(id);
            List<FileInfo> fileList = new List<FileInfo>();
            if (string.IsNullOrWhiteSpace(data.PHOTO))
            {
            }
            else
            {
                var files = data.PHOTO.Split('|');
                foreach (var file in files)
                {
                    FileInfo info = new FileInfo();
                    info.FileName = file.Substring(0, file.LastIndexOf("."));
                    info.FileUrl = ConfigurationManager.AppSettings["FileUrl"] + file;
                    fileList.Add(info);
                }
            }

            //修改消息为已读
            data.ISREAD_PLATFORM = 1;
            MessageBLL.Update(data);
            return new JsonResult
            {
                Data = fileList
            };
        }

    }
}