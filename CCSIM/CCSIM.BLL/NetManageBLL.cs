using CCSIM.DAL.Base;
using CCSIM.DAL.DBContext;
using CCSIM.DAL.Model;
using CCSIM.Entity;
using CCSIM.Extension;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CCSIM.BLL
{
    public class NetManageBLL
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        private static string ORACLE_CONNECTION_STRING = "user id=LSGAADMIN;password=lsga110;data source=122.225.122.106/ORCL";

        /// <summary>
        /// 添加网格信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static bool Add(CFG_NETINFO info)
        {
            bool isSuccess = true;
            StringBuilder pInsertText = new StringBuilder();
            pInsertText.Append("INSERT INTO CFG_NETINFO(NAME,POPULATIONINFO,HOUSEINFO,UNITSTOREINFO,BELONGAREA,BELONGDEPTID,REMARK,ISDELETED,NETCOLOR,LONANDLAT) VALUES('");
            pInsertText.Append(info.NAME + "','" + info.POPULATIONINFO + "','" + info.HOUSEINFO + "','" + info.UNITSTOREINFO + "','" + info.BELONGAREA + "'," + info.BELONGDEPTID + ",'" + info.REMARK + "',");
            pInsertText.Append(info.ISDELETED + ",'" + info.NETCOLOR + "','" + info.LONANDLAT + "')");

            try
            {
                OracleOperateBLL.ExecuteSql(pInsertText.ToString());
            }
            catch
            {
                isSuccess = false;
            }

            return isSuccess;

            //DbBase<CFG_NETINFO> db = new DbBase<CFG_NETINFO>();
            //db.Insert(info);
            //if (db.SaveChanges() >= 0)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }

        /// <summary>
        /// 修改网格信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static bool Update(CFG_NETINFO info)
        {
            bool isSuccess = true;
            StringBuilder pUpdateText = new StringBuilder();
            pUpdateText.Append("UPDATE CFG_NETINFO SET NAME='" + info.NAME + "',POPULATIONINFO='" + info.POPULATIONINFO + "',HOUSEINFO='" + info.HOUSEINFO + "',UNITSTOREINFO='" + info.UNITSTOREINFO + "',BELONGAREA='" + info.BELONGAREA + "',");
            pUpdateText.Append("BELONGDEPTID=" + info.BELONGDEPTID + ",REMARK='" + info.REMARK + "',NETCOLOR='" + info.NETCOLOR + "',LONANDLAT='" + info.LONANDLAT + "' ");
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

            //DbBase<CFG_NETINFO> db = new DbBase<CFG_NETINFO>();
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

        /// <summary>
        /// 删除网格信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool Delete(int id)
        {
            bool isSuccess = true;
            StringBuilder pUpdateText = new StringBuilder();
            pUpdateText.Append("UPDATE CFG_NETINFO SET ISDELETED=1 WHERE ID=" + id);

            try
            {
                OracleOperateBLL.ExecuteSql(pUpdateText.ToString());
            }
            catch
            {
                isSuccess = false;
            }

            return isSuccess;

            //DbBase<CFG_NETINFO> db = new DbBase<CFG_NETINFO>();
            //var info = Get(id);
            //info.ISDELETED = 1;
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
        public static bool Delete(int[] ids)
        {
            bool isSuccess = true;

            foreach (var id in ids)
            {
                StringBuilder pUpdateText = new StringBuilder();
                pUpdateText.Append("UPDATE CFG_NETINFO SET ISDELETED=1 WHERE ID=" + id);

                try
                {
                    OracleOperateBLL.ExecuteSql(pUpdateText.ToString());
                }
                catch
                {
                    isSuccess = false;
                }
            }
            return isSuccess;

            //DbBase<CFG_NETINFO> db = new DbBase<CFG_NETINFO>();
            //List<CFG_NETINFO> infoList = new List<CFG_NETINFO>();
            //foreach (var id in ids)
            //{
            //    var info = Get(id);
            //    info.ISDELETED = 1;
            //    infoList.Add(info);
            //}
            //db.UpdateAll(infoList);
            //if (db.SaveChanges() >= 0)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }

        /// <summary>
        /// 获取网格信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static CFG_NETINFO Get(int id)
        {
            CFG_NETINFO pNetInfo = new CFG_NETINFO();
            OracleConnection pConn = null;
            OracleCommand pComm = null;
            OracleDataReader pReader = null;

            pConn = new OracleConnection(ORACLE_CONNECTION_STRING);
            pConn.Open();
            string pCmdText = "SELECT ID,NAME,POPULATIONINFO,HOUSEINFO,UNITSTOREINFO,BELONGAREA,BELONGDEPTID,REMARK,ISDELETED,NETCOLOR,LONANDLAT FROM CFG_NETINFO WHERE ID = " + id + "";
            pComm = new OracleCommand(pCmdText, pConn);
            pReader = pComm.ExecuteReader();
            while (pReader.Read())
            {
                pNetInfo.ID = Convert.ToInt32(pReader["ID"]);
                pNetInfo.NAME = Convert.ToString(pReader["NAME"]);
                pNetInfo.POPULATIONINFO = Convert.ToString(pReader["POPULATIONINFO"]);
                pNetInfo.HOUSEINFO = Convert.ToString(pReader["HOUSEINFO"]);
                pNetInfo.UNITSTOREINFO = Convert.ToString(pReader["UNITSTOREINFO"]);
                pNetInfo.BELONGAREA = Convert.ToString(pReader["BELONGAREA"]);
                pNetInfo.BELONGDEPTID = Convert.ToInt32(pReader["BELONGDEPTID"]);
                pNetInfo.REMARK = Convert.ToString(pReader["REMARK"]);
                pNetInfo.ISDELETED = Convert.ToInt32(pReader["ISDELETED"]);
                pNetInfo.NETCOLOR = Convert.ToString(pReader["NETCOLOR"]);
                pNetInfo.LONANDLAT = Convert.ToString(pReader["LONANDLAT"]);
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
            return pNetInfo;

            //DbBase<CFG_NETINFO> db = new DbBase<CFG_NETINFO>();
            //return db.FirstOrDefault(p => p.ID == id);
        }

        /// <summary>
        /// 获取网格信息列表
        /// </summary>
        /// <param name="name"></param>
        /// <param name="belongDeptId"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static List<NetInfo> GetList(string name, int belongDeptId, int start, int limit, out int totalCount)
        {
            StringBuilder pSBQueryCount = new StringBuilder();
            pSBQueryCount.Append("SELECT COUNT(*) FROM CFG_NETINFO WHERE ISDELETED=0 ");
            if (string.IsNullOrWhiteSpace(name) == false)
            {
                pSBQueryCount.Append("AND NAME like '%" + name + "%' ");
            }
            if (belongDeptId != -1)
            {
                pSBQueryCount.Append("AND BELONGDEPTID =" + belongDeptId);
            }
            totalCount = OracleOperateBLL.GetQueryCount(pSBQueryCount.ToString());

            int pStartNum = limit * (start - 1) + 1;
            int pEndNum = limit * start;

            StringBuilder pSBQueryText = new StringBuilder();
            pSBQueryText.Append("SELECT ID,BELONGAREA,BELONGDEPTID,BELONGDEPTNAME,NAME,POPULATIONINFO,HOUSEINFO,UNITSTOREINFO,REMARK,NETCOLOR,LONANDLAT FROM (SELECT C.*, rownum r FROM(");
            pSBQueryText.Append("SELECT A.ID,A.BELONGAREA,A.BELONGDEPTID,B.BMVALUE AS BELONGDEPTNAME,A.NAME,A.POPULATIONINFO,A.HOUSEINFO,A.UNITSTOREINFO,A.REMARK,A.NETCOLOR,A.LONANDLAT FROM CFG_NETINFO A ");
            pSBQueryText.Append("LEFT JOIN SYS_BM_CODE B ON A.BELONGDEPTID=B.BMKEY WHERE A.ISDELETED=0 ");
            if (string.IsNullOrWhiteSpace(name) == false)
            {
                pSBQueryText.Append("AND A.NAME like '%" + name + "%' ");
            }
            if (belongDeptId != -1)
            {
                pSBQueryText.Append("AND A.BELONGDEPTID =" + belongDeptId);
            }
            pSBQueryText.Append(" ORDER BY A.NAME) C ");
            pSBQueryText.Append("WHERE rownum<=" + pEndNum + ") D WHERE r>=" + pStartNum);
            var data = OracleOperateBLL.FillDataTable(pSBQueryText.ToString());
            List<NetInfo> netInfoList = new List<NetInfo>();
            foreach (DataRow dr in data.Rows)
            {
                NetInfo d = new NetInfo();
                d.Id = Convert.ToInt32(dr["ID"].ToString());
                d.BelongArea = dr["BELONGAREA"].ToString();
                d.BelongDeptId = Convert.ToInt32(dr["BELONGDEPTID"].ToString());
                d.BelongDeptName = dr["BELONGDEPTNAME"].ToString();
                d.Name = dr["NAME"].ToString();
                d.PopulationInfo = dr["POPULATIONINFO"].ToString();
                d.HouseInfo = dr["HOUSEINFO"].ToString();
                d.UnitStoreInfo = dr["UNITSTOREINFO"].ToString();
                d.Remark = dr["REMARK"].ToString();
                d.NetColor = dr["NETCOLOR"].ToString();
                d.LonAndLat = dr["LONANDLAT"].ToString();

                netInfoList.Add(d);
            }

            return netInfoList;

            //var q = (from n in SlaveDb.Set<CFG_NETINFO>()
            //         join c in SlaveDb.Set<SYS_BM_CODE>() on n.BELONGDEPTID equals c.BMKEY into cc
            //         where ((name == "" || name == null) ? true : n.NAME.Contains(name))
            //         && (belongDeptId == -1 ? true : n.BELONGDEPTID == belongDeptId) && n.ISDELETED == 0
            //         from ccc in cc.DefaultIfEmpty()
            //         select new NetInfo
            //         {
            //             Id = n.ID,
            //             BelongArea = n.BELONGAREA,
            //             BelongDeptName = ccc.BMVALUE,
            //             HouseInfo = n.HOUSEINFO,
            //             LonAndLat = n.LONANDLAT,
            //             PopulationInfo = n.POPULATIONINFO,
            //             Remark = n.REMARK,
            //             UnitStoreInfo = n.UNITSTOREINFO,
            //             NetColor=n.NETCOLOR,
            //             Name = n.NAME
            //         });
            //totalCount = q.Count();
            //return q.OrderByDescending(p => p.Name).Skip((start - 1) * limit).Take(limit).ToList();
        }
        public static List<NetInfo> GetAll()
        {
            StringBuilder pSBQueryText = new StringBuilder();
            pSBQueryText.Append("SELECT A.ID,A.BELONGAREA,A.BELONGDEPTID,B.BMVALUE AS BELONGDEPTNAME,A.NAME,A.POPULATIONINFO,A.HOUSEINFO,A.UNITSTOREINFO,A.REMARK,A.NETCOLOR,A.LONANDLAT FROM CFG_NETINFO A ");
            pSBQueryText.Append("LEFT JOIN SYS_BM_CODE B ON A.BELONGDEPTID=B.BMKEY WHERE A.ISDELETED=0 ");
            pSBQueryText.Append(" ORDER BY A.NAME");
            var data = OracleOperateBLL.FillDataTable(pSBQueryText.ToString());
            List<NetInfo> netInfoList = new List<NetInfo>();
            foreach (DataRow dr in data.Rows)
            {
                NetInfo d = new NetInfo();
                d.Id = Convert.ToInt32(dr["ID"].ToString());
                d.BelongArea = dr["BELONGAREA"].ToString();
                d.BelongDeptId = Convert.ToInt32(dr["BELONGDEPTID"].ToString());
                d.BelongDeptName = dr["BELONGDEPTNAME"].ToString();
                d.Name = dr["NAME"].ToString();
                d.PopulationInfo = dr["POPULATIONINFO"].ToString();
                d.HouseInfo = dr["HOUSEINFO"].ToString();
                d.UnitStoreInfo = dr["UNITSTOREINFO"].ToString();
                d.Remark = dr["REMARK"].ToString();
                d.NetColor = dr["NETCOLOR"].ToString();
                d.LonAndLat = dr["LONANDLAT"].ToString();

                netInfoList.Add(d);
            }

            return netInfoList;

            //var q = (from n in SlaveDb.Set<CFG_NETINFO>()
            //         join c in SlaveDb.Set<SYS_BM_CODE>() on n.BELONGDEPTID equals c.BMKEY into cc
            //         where n.ISDELETED == 0
            //         from ccc in cc.DefaultIfEmpty()
            //         select new NetInfo
            //         {
            //             Id = n.ID,
            //             BelongArea = n.BELONGAREA,
            //             BelongDeptName = ccc.BMVALUE,
            //             HouseInfo = n.HOUSEINFO,
            //             LonAndLat = n.LONANDLAT,
            //             PopulationInfo = n.POPULATIONINFO,
            //             Remark = n.REMARK,
            //             UnitStoreInfo = n.UNITSTOREINFO,
            //             NetColor=n.NETCOLOR,
            //             Name = n.NAME
            //         });
            //return q.ToList();
        }
    }
}
