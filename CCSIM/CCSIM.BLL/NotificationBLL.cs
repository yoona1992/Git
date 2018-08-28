using CCSIM.DAL.DBContext;
using CCSIM.DAL.Model;
using CCSIM.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        //是否读写分离(可以配置在配置文件中)
        private static readonly bool IsReadWriteSeparation = true;

        #region EF上下文对象(主库)

        protected static DbContext MasterDb => _masterDb.Value;
        private static readonly Lazy<DbContext> _masterDb = new Lazy<DbContext>(() => new DbContextFactory().GetWriteDbContext());

        #endregion EF上下文对象(主库)

        #region EF上下文对象(从库)

        protected static DbContext SlaveDb => IsReadWriteSeparation ? _slaveDb.Value : _masterDb.Value;
        private static readonly Lazy<DbContext> _slaveDb = new Lazy<DbContext>(() => new DbContextFactory().GetReadDbContext());

        #endregion EF上下文对象(从库)

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
            var q = (from n in SlaveDb.Set<NOTIFICATION>()
                     join u in SlaveDb.Set<CFG_USERINFO>() on n.PHONE equals u.TELEPHONE
                     where ((title == "" || title == null) ? true : n.TITLE.Contains(title))
                     && ((username == "" || username == null) ? true : u.NAME.Contains(username))
                     && n.CREATE_DATE >= startTime && n.CREATE_DATE <= endTime
                     select new NotificationInfo
                     {
                         Id = n.ID,
                         Title = n.TITLE,
                         Content=n.CONTENT,
                         UserName=u.NAME,
                         Create_Date = n.CREATE_DATE
                     });
            totalCount = q.Count();
            var data = q.OrderByDescending(p => p.Create_Date).ThenByDescending(p => p.Title).Skip((start - 1) * limit).Take(limit).ToList();
            foreach (var d in data)
            {
                d.NotificateTime = Convert.ToDateTime(d.Create_Date).ToString("yyyy-MM-dd HH:mm:ss");
            }
            return data;
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
            var url = ConfigurationManager.AppSettings["NotificationUrl"] +"phone=" +phone+"&title="+title+"&content="+content;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";

            var msg = "";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                msg=reader.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<NotificationReturnInfo>(msg).stateMsg;
        }

    }
}
