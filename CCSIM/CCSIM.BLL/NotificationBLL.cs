using CCSIM.DAL.DBContext;
using CCSIM.DAL.Model;
using CCSIM.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CCSIM.BLL
{
    public class NotificationBLL
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        private static string ORACLE_CONNECTION_STRING = "user id=LSGAADMIN;password=lsga110;data source=122.225.122.106/ORCL";

        /// <summary>
        /// 获取消息列表
        /// </summary>
        /// <param name="username">用户名称</param>
        /// <param name="title">标题</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static List<NotificationInfo> GetList(string username, string title, DateTime startTime, DateTime endTime, int start, int limit, out int totalCount)
        {
            StringBuilder pSBQueryCount = new StringBuilder();
            pSBQueryCount.Append("SELECT COUNT(*) FROM NOTIFICATION A LEFT JOIN CFG_USERINFO B ON A.PHONE=B.TELEPHONE WHERE TO_CHAR(A.CREATE_DATE,'yyyymmddhh24miss') BETWEEN '" + startTime.ToString("yyyyMMddHHmmss") + "' AND '" + endTime.ToString("yyyyMMddHHmmss") + "' ");
            if (string.IsNullOrWhiteSpace(title) == false)
            {
                pSBQueryCount.Append("AND A.TITLE like '%" + title + "%' ");
            }
            if (string.IsNullOrWhiteSpace(username) == false)
            {
                pSBQueryCount.Append("AND B.NAME like '%" + username + "%' ");
            }
            totalCount = OracleOperateBLL.GetQueryCount(pSBQueryCount.ToString());

            int pStartNum = limit * (start - 1) + 1;
            int pEndNum = limit * start;

            StringBuilder pSBQueryText = new StringBuilder();
            pSBQueryText.Append("SELECT ID,TITLE,CONTENT,CREATE_DATE,NAME FROM (SELECT C.*, rownum r FROM(");
            pSBQueryText.Append("SELECT A.ID,A.TITLE,A.CONTENT,TO_CHAR(A.CREATE_DATE,'yyyy-mm-dd hh24:mi:ss') AS CREATE_DATE,B.NAME FROM NOTIFICATION A ");
            pSBQueryText.Append("LEFT JOIN CFG_USERINFO B ON A.PHONE=B.TELEPHONE WHERE TO_CHAR(A.CREATE_DATE,'yyyymmddhh24miss') BETWEEN '" + startTime.ToString("yyyyMMddHHmmss") + "' AND '" + endTime.ToString("yyyyMMddHHmmss") + "' ");
            if (string.IsNullOrWhiteSpace(title) == false)
            {
                pSBQueryText.Append("AND A.TITLE like '%" + title + "%' ");
            }
            if (string.IsNullOrWhiteSpace(username) == false)
            {
                pSBQueryText.Append("AND B.NAME like '%" + username + "%' ");
            }
            pSBQueryText.Append(" ORDER BY A.CREATE_DATE DESC,A.TITLE DESC) C ");
            pSBQueryText.Append("WHERE rownum<=" + pEndNum + ") D WHERE r>=" + pStartNum);
            var data = OracleOperateBLL.FillDataTable(pSBQueryText.ToString());
            List<NotificationInfo> notificationInfoList = new List<NotificationInfo>();
            foreach (DataRow dr in data.Rows)
            {
                NotificationInfo d = new NotificationInfo();
                d.Id = Convert.ToInt32(dr["ID"].ToString());
                d.Title = dr["TITLE"].ToString();
                d.Content = dr["CONTENT"].ToString();
                d.UserName = dr["NAME"].ToString();
                d.Create_Date = Convert.ToDateTime(dr["CREATE_DATE"].ToString());
                d.NotificateTime = dr["CREATE_DATE"].ToString();

                notificationInfoList.Add(d);
            }

            return notificationInfoList;
            //var q = (from n in SlaveDb.Set<NOTIFICATION>()
            //         join u in SlaveDb.Set<CFG_USERINFO>() on n.PHONE equals u.TELEPHONE into uu
            //         from uuu in uu.DefaultIfEmpty()
            //         where ((title == "" || title == null) ? true : n.TITLE.Contains(title))
            //         && ((username == "" || username == null) ? true : uuu.NAME.Contains(username))
            //         && n.CREATE_DATE >= startTime && n.CREATE_DATE <= endTime
            //         select new NotificationInfo
            //         {
            //             Id = n.ID,
            //             Title = n.TITLE,
            //             Content = n.CONTENT,
            //             UserName = uuu.NAME,
            //             Create_Date = n.CREATE_DATE
            //         });
            //totalCount = q.Count();
            //var data = q.OrderByDescending(p => p.Create_Date).ThenByDescending(p => p.Title).Skip((start - 1) * limit).Take(limit).ToList();
            //foreach (var d in data)
            //{
            //    d.NotificateTime = Convert.ToDateTime(d.Create_Date).ToString("yyyy-MM-dd HH:mm:ss");
            //}
            //return data;
        }


        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="phone">手机号码</param>
        /// <param name="title">标题</param>
        /// <param name="content">正文</param>
        /// <returns></returns>
        public static string SendMessage(string phone, string title, string content)
        {
            var url = ConfigurationManager.AppSettings["NotificationUrl"] + "phone=" + phone + "&title=" + title + "&content=" + content;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";

            var msg = "";
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    msg = reader.ReadToEnd();
                }
                msg = JsonConvert.DeserializeObject<NotificationReturnInfo>(msg).stateMsg;
            }
            catch
            {
                msg = "发送失败！";
            }

            return msg;
        }

    }
}
