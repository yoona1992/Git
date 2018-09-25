using CCSIM.DAL.Base;
using CCSIM.DAL.DBContext;
using CCSIM.DAL.Model;
using CCSIM.Entity;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CCSIM.BLL
{
    /// <summary>
    /// 资料信息
    /// </summary>
    public class MessageBLL
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
        public static List<MessageInfo> GetList(string username, string title, DateTime startTime, DateTime endTime, int start, int limit, out int totalCount)
        {
            StringBuilder pSBQueryCount = new StringBuilder();
            pSBQueryCount.Append("SELECT COUNT(*) FROM MESSAGE A LEFT JOIN CFG_USERINFO B ON A.PHONE=B.TELEPHONE WHERE TO_CHAR(A.CREATE_DATE,'yyyymmddhh24miss') BETWEEN '" + startTime.ToString("yyyyMMddHHmmss")+"' AND '"+endTime.ToString("yyyyMMddHHmmss")+"' ");
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
            pSBQueryText.Append("SELECT ID,TITLE,PHONE,ADDRESS,REMARKS,CREATE_DATE,NAME FROM (SELECT C.*, rownum r FROM(");
            pSBQueryText.Append("SELECT A.ID,A.TITLE,A.PHONE,A.ADDRESS,A.REMARKS,TO_CHAR(A.CREATE_DATE,'yyyy-mm-dd hh24:mi:ss') AS CREATE_DATE,B.NAME FROM MESSAGE A ");
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
            List<MessageInfo> messageInfoList = new List<MessageInfo>();
            foreach (DataRow dr in data.Rows)
            {
                MessageInfo d = new MessageInfo();
                d.Id = Convert.ToInt32(dr["ID"].ToString());
                d.Title = dr["TITLE"].ToString();
                d.Phone = dr["PHONE"].ToString();
                d.Address = dr["ADDRESS"].ToString();
                d.Remarks = dr["REMARKS"].ToString();
                d.Name = dr["NAME"].ToString();
                d.Create_Date = Convert.ToDateTime(dr["CREATE_DATE"].ToString());
                d.UploadDate = dr["CREATE_DATE"].ToString();

                messageInfoList.Add(d);
            }

            return messageInfoList;
            //var q = (from m in SlaveDb.Set<MESSAGE>()
            //         join u in SlaveDb.Set<CFG_USERINFO>() on m.PHONE equals u.TELEPHONE into uu
            //         from uuu in uu.DefaultIfEmpty()
            //         where ((title == "" || title == null) ? true : m.TITLE.Contains(title))
            //         && ((username == "" || username == null) ? true : uuu.NAME.Contains(username))
            //         && m.CREATE_DATE >= startTime && m.CREATE_DATE <= endTime
            //         select new MessageInfo
            //         {
            //             Id = m.ID,
            //             Title = m.TITLE,
            //             Phone = m.PHONE,
            //             Address = m.ADDRESS,
            //             Name = uuu.NAME,
            //             Create_Date = m.CREATE_DATE
            //         });
            //totalCount = q.Count();
            //var data = q.OrderByDescending(p => p.Create_Date).ThenByDescending(p => p.Title).Skip((start - 1) * limit).Take(limit).ToList();
            //foreach (var d in data)
            //{
            //    d.UploadDate = d.Create_Date.ToString("yyyy-MM-dd HH:mm:ss");
            //}
            //return data;
        }
        public static List<MessageInfo> GetList_Map(string phone, string title, int type, DateTime startTime, DateTime endTime, int start, int limit, out int totalCount)
        {
            StringBuilder pSBQueryCount = new StringBuilder();
            pSBQueryCount.Append("SELECT COUNT(*) FROM MESSAGE A LEFT JOIN CFG_USERINFO B ON A.PHONE=B.TELEPHONE WHERE TO_CHAR(A.CREATE_DATE,'yyyymmddhh24miss') BETWEEN '" + startTime.ToString("yyyyMMddHHmmss") + "' AND '" + endTime.ToString("yyyyMMddHHmmss") + "' ");
            if (string.IsNullOrWhiteSpace(title) == false)
            {
                pSBQueryCount.Append("AND A.TITLE like '%" + title + "%' ");
            }
            if (string.IsNullOrWhiteSpace(phone) == false)
            {
                pSBQueryCount.Append("AND B.TELEPHONE like '%" + phone + "%' ");
            }
            if (type != -1)
            {
                pSBQueryCount.Append("AND A.ISREAD_PLATFORM=" + type + " ");
            }
            totalCount = OracleOperateBLL.GetQueryCount(pSBQueryCount.ToString());

            int pStartNum = limit * (start - 1) + 1;
            int pEndNum = limit * start;

            StringBuilder pSBQueryText = new StringBuilder();
            pSBQueryText.Append("SELECT ID,TITLE,PHONE,ADDRESS,REMARKS,CREATE_DATE,NAME,ISREAD_PLATFORM FROM (SELECT C.*, rownum r FROM(");
            pSBQueryText.Append("SELECT A.ID,A.TITLE,A.PHONE,A.ADDRESS,A.REMARKS,TO_CHAR(A.CREATE_DATE,'yyyy-mm-dd hh24:mi:ss') AS CREATE_DATE,A.ISREAD_PLATFORM,B.NAME FROM MESSAGE A ");
            pSBQueryText.Append("LEFT JOIN CFG_USERINFO B ON A.PHONE=B.TELEPHONE WHERE TO_CHAR(A.CREATE_DATE,'yyyymmddhh24miss') BETWEEN '" + startTime.ToString("yyyyMMddHHmmss") + "' AND '" + endTime.ToString("yyyyMMddHHmmss") + "' ");
            if (string.IsNullOrWhiteSpace(title) == false)
            {
                pSBQueryText.Append("AND A.TITLE like '%" + title + "%' ");
            }
            if (string.IsNullOrWhiteSpace(phone) == false)
            {
                pSBQueryText.Append("AND B.TELEPHONE like '%" + phone + "%' ");
            }
            if (type != -1)
            {
                pSBQueryText.Append("AND A.ISREAD_PLATFORM=" + type + " ");
            }
            pSBQueryText.Append(" ORDER BY ISREAD_PLATFORM,A.CREATE_DATE DESC,A.TITLE DESC) C ");
            pSBQueryText.Append("WHERE rownum<=" + pEndNum + ") D WHERE r>=" + pStartNum);
            var data = OracleOperateBLL.FillDataTable(pSBQueryText.ToString());
            List<MessageInfo> messageInfoList = new List<MessageInfo>();
            foreach (DataRow dr in data.Rows)
            {
                MessageInfo d = new MessageInfo();
                d.Id = Convert.ToInt32(dr["ID"].ToString());
                d.Title = dr["TITLE"].ToString();
                d.Phone = dr["PHONE"].ToString();
                d.Address = dr["ADDRESS"].ToString();
                d.Remarks = dr["REMARKS"].ToString();
                d.Name = dr["NAME"].ToString();
                d.Create_Date = Convert.ToDateTime(dr["CREATE_DATE"].ToString());
                d.UploadDate = dr["CREATE_DATE"].ToString();
                d.IsRead_Platform= Convert.ToInt32(dr["ISREAD_PLATFORM"].ToString());
                messageInfoList.Add(d);
            }

            return messageInfoList;

            //var q = (from m in SlaveDb.Set<MESSAGE>()
            //         join u in SlaveDb.Set<CFG_USERINFO>() on m.PHONE equals u.TELEPHONE into uu
            //         from uuu in uu.DefaultIfEmpty()
            //         where ((title == "" || title == null) ? true : m.TITLE.Contains(title))
            //         && ((phone == "" || phone == null) ? true : uuu.TELEPHONE.Contains(phone))
            //         && (type == -1 ? true : m.ISREAD_PLATFORM == type)
            //         && m.CREATE_DATE >= startTime && m.CREATE_DATE <= endTime
            //         select new MessageInfo
            //         {
            //             Id = m.ID,
            //             Title = m.TITLE,
            //             Phone = m.PHONE,
            //             Address = m.ADDRESS,
            //             Name = uuu.NAME,
            //             Create_Date = m.CREATE_DATE,
            //             Remarks=m.REMARKS,
            //             IsRead_Platform = m.ISREAD_PLATFORM
            //         });
            //totalCount = q.Count();
            //var data = q.OrderByDescending(p => p.Create_Date).ThenByDescending(p => p.Title).Skip((start - 1) * limit).Take(limit).ToList();
            //foreach (var d in data)
            //{
            //    d.UploadDate = d.Create_Date.ToString("yyyy-MM-dd HH:mm:ss");
            //}
            //return data;
        }


        /// <summary>
        /// 获取车辆信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static MESSAGE Get(int id)
        {
            MESSAGE pMessage = new MESSAGE();
            OracleConnection pConn = null;
            OracleCommand pComm = null;
            OracleDataReader pReader = null;

            pConn = new OracleConnection(ORACLE_CONNECTION_STRING);
            pConn.Open();
            string pCmdText = "SELECT ID,TITLE,PHONE,ADDRESS,REMARKS,TO_CHAR(CREATE_DATE,'yyyy-mm-dd hh24:mi:ss') AS CREATE_DATE,PHOTO,STATUS,ISREAD_PLATFORM,ISSHOW_PLATFORM,ISREPLY FROM MESSAGE WHERE ID = " + id + "";
            pComm = new OracleCommand(pCmdText, pConn);
            pReader = pComm.ExecuteReader();
            while (pReader.Read())
            {
                pMessage.ID = Convert.ToInt32(pReader["ID"]);
                pMessage.TITLE = Convert.ToString(pReader["TITLE"]);
                pMessage.PHONE = Convert.ToString(pReader["PHONE"]);
                pMessage.ADDRESS = Convert.ToString(pReader["ADDRESS"]);
                pMessage.REMARKS = Convert.ToString(pReader["REMARKS"]);
                pMessage.CREATE_DATE = Convert.ToDateTime(pReader["CREATE_DATE"]);
                pMessage.PHOTO = Convert.ToString(pReader["PHOTO"]);
                pMessage.STATUS = Convert.ToInt32(pReader["STATUS"]);
                pMessage.ISREAD_PLATFORM = Convert.ToInt32(pReader["ISREAD_PLATFORM"]);
            }
            if (pReader != null)
            {
                pReader.Close();
                pReader.Dispose();
            }
            if (pComm != null)
            {
                pComm.Dispose();
            }
            if (pConn != null)
            {
                pConn.Close();
                pConn.Dispose();
            }
            return pMessage;
            //DbBase<MESSAGE> db = new DbBase<MESSAGE>();
            //return db.FirstOrDefault(p => p.ID == id);
        }

        /// <summary>
        /// 修改是否已读
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static bool Update(MESSAGE info)
        {
            bool isSuccess = true;
            StringBuilder pUpdateText = new StringBuilder();
            pUpdateText.Append("UPDATE MESSAGE SET ISREAD_PLATFORM= "+info.ISREAD_PLATFORM+" ");
            pUpdateText.Append("WHERE ID=" + info.ID);

            try
            {
                OracleOperateBLL.ExecuteSql(pUpdateText.ToString());
            }
            catch
            {
                isSuccess = false;
            }

            return isSuccess;

            //DbBase<MESSAGE> db = new DbBase<MESSAGE>();
            //db.Update(info);
            //if (db.SaveChanges() >= 0)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }
    }
}
