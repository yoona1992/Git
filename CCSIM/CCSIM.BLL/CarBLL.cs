using CCSIM.DAL;
using CCSIM.DAL.Base;
using CCSIM.DAL.DBContext;
using CCSIM.DAL.Model;
using CCSIM.Entity;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSIM.BLL
{
    /// <summary>
    /// 车辆类
    /// </summary>
    public class CarBLL
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        private static string ORACLE_CONNECTION_STRING = "user id=LSGAADMIN;password=lsga110;data source=122.225.122.106/ORCL";
        
        /// <summary>
        /// 添加车辆信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static bool Add(CFG_CARINFO info)
        {
            bool isSuccess = true;
            StringBuilder pInsertText = new StringBuilder();
            pInsertText.Append("INSERT INTO CFG_CARINFO(VEHICLENO,VEHICLETYPE,VEHICLEBRAND,BELONGDEPTID,BELONGNETID,REMARK,ISDELETED,OWNER,OWNERTYPE,CLDWZDSBH,WLWKHM) VALUES('");
            pInsertText.Append(info.VEHICLENO + "'," + info.VEHICLETYPE + ",'" + info.VEHICLEBRAND + "'," + info.BELONGDEPTID + "," + info.BELONGNETID + ",'" + info.REMARK + "'," + info.ISDELETED + ",'");
            pInsertText.Append(info.OWNER + "'," + info.OWNERTYPE + ",'" + info.CLDWZDSBH + "','" + info.WLWKHM + "')");

            try
            {
                OracleOperateBLL.ExecuteSql(pInsertText.ToString());
            }
            catch
            {
                isSuccess = false;
            }

            return isSuccess;
            //DbBase<CFG_CARINFO> db = new DbBase<CFG_CARINFO>();
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
        /// 修改车辆信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static bool Update(CFG_CARINFO info)
        {
            bool isSuccess = true;
            StringBuilder pUpdateText = new StringBuilder();
            pUpdateText.Append("UPDATE CFG_CARINFO SET VEHICLENO='" + info.VEHICLENO + "',VEHICLETYPE=" + info.VEHICLETYPE + ",VEHICLEBRAND='" + info.VEHICLEBRAND + "',BELONGDEPTID=" + info.BELONGDEPTID + ",BELONGNETID=" + info.BELONGNETID + ",");
            pUpdateText.Append("REMARK='" + info.REMARK + "',OWNER='" + info.OWNER + "',OWNERTYPE=" + info.OWNERTYPE + ",CLDWZDSBH='" + info.CLDWZDSBH + "',WLWKHM='" + info.WLWKHM + "' ");
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

            //DbBase<CFG_CARINFO> db = new DbBase<CFG_CARINFO>();
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
        /// 删除车辆信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool Delete(int id)
        {
            bool isSuccess = true;
            StringBuilder pUpdateText = new StringBuilder();
            pUpdateText.Append("UPDATE CFG_CARINFO SET ISDELETED=1 WHERE ID=" + id);

            try
            {
                OracleOperateBLL.ExecuteSql(pUpdateText.ToString());
            }
            catch
            {
                isSuccess = false;
            }

            return isSuccess;

            //DbBase<CFG_CARINFO> db = new DbBase<CFG_CARINFO>();
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
                pUpdateText.Append("UPDATE CFG_CARINFO SET ISDELETED=1 WHERE ID=" + id);

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

            //DbBase<CFG_CARINFO> db = new DbBase<CFG_CARINFO>();
            //List<CFG_CARINFO> infoList = new List<CFG_CARINFO>();
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
        /// 获取车辆信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static CFG_CARINFO Get(int id)
        {
            CFG_CARINFO pCarInfo = new CFG_CARINFO();
            OracleConnection pConn = null;
            OracleCommand pComm = null;
            OracleDataReader pReader = null;

            pConn = new OracleConnection(ORACLE_CONNECTION_STRING);
            pConn.Open();
            string pCmdText = "SELECT ID,VEHICLENO,VEHICLETYPE,VEHICLEBRAND,BELONGDEPTID,BELONGNETID,REMARK,ISDELETED,OWNER,OWNERTYPE,CLDWZDSBH,WLWKHM FROM CFG_CARINFO WHERE ID = " + id + "";
            pComm = new OracleCommand(pCmdText, pConn);
            pReader = pComm.ExecuteReader();
            while (pReader.Read())
            {
                pCarInfo.ID = Convert.ToInt32(pReader["ID"]);
                pCarInfo.VEHICLENO = Convert.ToString(pReader["VEHICLENO"]);
                pCarInfo.VEHICLETYPE = Convert.ToInt32(pReader["VEHICLETYPE"]);
                pCarInfo.VEHICLEBRAND = Convert.ToString(pReader["VEHICLEBRAND"]);
                pCarInfo.BELONGDEPTID = Convert.ToInt32(pReader["BELONGDEPTID"]);
                pCarInfo.BELONGNETID = Convert.ToInt32(pReader["BELONGNETID"]);
                pCarInfo.REMARK = Convert.ToString(pReader["REMARK"]);
                pCarInfo.ISDELETED = Convert.ToInt32(pReader["ISDELETED"]);
                pCarInfo.OWNER = Convert.ToString(pReader["OWNER"]);
                pCarInfo.OWNERTYPE = Convert.ToInt32(pReader["OWNERTYPE"]);
                pCarInfo.CLDWZDSBH = Convert.ToString(pReader["CLDWZDSBH"]);
                pCarInfo.WLWKHM = Convert.ToString(pReader["WLWKHM"]);
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
            return pCarInfo;
            //DbBase<CFG_CARINFO> db = new DbBase<CFG_CARINFO>();
            //return db.FirstOrDefault(p => p.ID == id);
        }
        public static CarInfo GetByVehId(int vehId)
        {
            CarInfo pCarInfo = new CarInfo();
            OracleConnection pConn = null;
            OracleCommand pComm = null;
            OracleDataReader pReader = null;

            pConn = new OracleConnection(ORACLE_CONNECTION_STRING);
            pConn.Open();
            string pCmdText = "SELECT A.ID,A.VEHICLENO,A.VEHICLETYPE,A.VEHICLEBRAND,A.BELONGDEPTID,A.BELONGNETID,A.REMARK,A.OWNER,A.OWNERTYPE,A.CLDWZDSBH,A.WLWKHM FROM CFG_CARINFO A LEFT JOIN CFG_VEHICLEINFO B ON A.CLDWZDSBH=B.CLDWZDSBH WHERE B.ID = " + vehId + "";
            pComm = new OracleCommand(pCmdText, pConn);
            pReader = pComm.ExecuteReader();
            while (pReader.Read())
            {
                pCarInfo.Id = Convert.ToInt32(pReader["ID"]);
                pCarInfo.VehicleNo = Convert.ToString(pReader["VEHICLENO"]);
                pCarInfo.VehicleType = Convert.ToInt32(pReader["VEHICLETYPE"]);
                pCarInfo.VehicleBrand = Convert.ToString(pReader["VEHICLEBRAND"]);
                pCarInfo.BelongDeptId = Convert.ToInt32(pReader["BELONGDEPTID"]);
                pCarInfo.BelongNetId = Convert.ToInt32(pReader["BELONGNETID"]);
                pCarInfo.Remark = Convert.ToString(pReader["REMARK"]);
                pCarInfo.Owner = Convert.ToString(pReader["OWNER"]);
                pCarInfo.OwnerType = Convert.ToInt32(pReader["OWNERTYPE"]);
                pCarInfo.Cldwzdsbh = Convert.ToString(pReader["CLDWZDSBH"]);
                pCarInfo.Wlwkhm = Convert.ToString(pReader["WLWKHM"]);
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
            return pCarInfo;

            //var q = (from c in SlaveDb.Set<CFG_CARINFO>()
            //         join v in SlaveDb.Set<CFG_VEHICLEINFO>() on c.CLDWZDSBH equals v.CLDWZDSBH
            //         where v.ID == vehId
            //         select new CarInfo
            //         {
            //             Id = c.ID,
            //             VehicleNo = c.VEHICLENO,
            //             VehicleType = c.VEHICLETYPE,
            //             VehicleBrand = c.VEHICLEBRAND,
            //             BelongDeptId = c.BELONGDEPTID,
            //             BelongNetId = c.BELONGNETID,
            //             Owner = c.OWNER,
            //             OwnerType = c.OWNERTYPE,
            //             Cldwzdsbh = c.CLDWZDSBH,
            //             Remark = c.REMARK,
            //             Wlwkhm = c.WLWKHM
            //         });
            //return q.FirstOrDefault();
        }

        /// <summary>
        /// 获取车辆信息列表
        /// </summary>
        /// <param name="vehicleNo"></param>
        /// <param name="ownerType"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static List<CarInfo> GetList(string vehicleNo, int ownerType, int start, int limit, out int totalCount)
        {
            StringBuilder pSBQueryCount = new StringBuilder();
            pSBQueryCount.Append("SELECT COUNT(*) FROM CFG_CARINFO WHERE ISDELETED=0 ");
            if (string.IsNullOrWhiteSpace(vehicleNo) == false)
            {
                pSBQueryCount.Append("AND VEHICLENO like '%" + vehicleNo + "%' ");
            }
            if (ownerType != -1)
            {
                pSBQueryCount.Append("AND OWNERTYPE =" + ownerType);
            }
            totalCount = OracleOperateBLL.GetQueryCount(pSBQueryCount.ToString());

            int pStartNum = limit * (start - 1) + 1;
            int pEndNum = limit * start;

            StringBuilder pSBQueryText = new StringBuilder();
            pSBQueryText.Append("SELECT ID,VEHICLENO,VEHICLETYPE,VEHICLETYPENAME,VEHICLEBRAND,BELONGDEPTID,BELONGDEPTNAME,BELONGNETID,BELONGNETNAME,OWNER,OWNERTYPE,OWNERTYPENAME,CLDWZDSBH,WLWKHM FROM (SELECT F.*, rownum r FROM(");
            pSBQueryText.Append("SELECT A.ID,A.VEHICLENO,A.VEHICLETYPE,C.BMVALUE AS VEHICLETYPENAME,A.VEHICLEBRAND,A.BELONGDEPTID,D.BMVALUE AS BELONGDEPTNAME,A.BELONGNETID,B.NAME AS BELONGNETNAME,A.OWNER,A.OWNERTYPE,E.BMVALUE AS OWNERTYPENAME,A.CLDWZDSBH,A.WLWKHM FROM CFG_CARINFO A ");
            pSBQueryText.Append("LEFT JOIN CFG_NETINFO B ON A.BELONGNETID = B.ID ");
            pSBQueryText.Append("LEFT JOIN SYS_BM_CODE C ON A.VEHICLETYPE = C.BMKEY ");
            pSBQueryText.Append("LEFT JOIN SYS_BM_CODE D ON A.BELONGDEPTID = D.BMKEY ");
            pSBQueryText.Append("LEFT JOIN SYS_BM_CODE E ON A.OWNERTYPE = E.BMKEY WHERE A.ISDELETED=0 ");
            if (string.IsNullOrWhiteSpace(vehicleNo) == false)
            {
                pSBQueryText.Append("AND A.VEHICLENO like '%" + vehicleNo + "%' ");
            }
            if (ownerType != -1)
            {
                pSBQueryText.Append("AND A.OWNERTYPE =" + ownerType);
            }
            pSBQueryText.Append(" ORDER BY A.OWNERTYPE DESC,A.BELONGDEPTID DESC,A.VEHICLENO DESC) F ");
            pSBQueryText.Append("WHERE rownum<=" + pEndNum + ") G WHERE r>=" + pStartNum);
            var data = OracleOperateBLL.FillDataTable(pSBQueryText.ToString());
            List<CarInfo> carInfoList = new List<CarInfo>();
            foreach (DataRow dr in data.Rows)
            {
                CarInfo d = new CarInfo();
                d.Id = Convert.ToInt32(dr["ID"].ToString());
                d.VehicleNo = dr["VEHICLENO"].ToString();
                d.VehicleType = Convert.ToInt32(dr["VEHICLETYPE"].ToString());
                d.VehicleTypeName = dr["VEHICLETYPENAME"].ToString();
                d.VehicleBrand = dr["VEHICLEBRAND"].ToString();
                d.BelongDeptId = Convert.ToInt32(dr["BELONGDEPTID"].ToString());
                d.BelongDeptName = dr["BELONGDEPTNAME"].ToString();
                d.BelongNetId = Convert.ToInt32(dr["BELONGNETID"].ToString());
                d.BelongNetName = dr["BELONGNETNAME"].ToString();
                d.Owner = dr["OWNER"].ToString();
                d.OwnerType = Convert.ToInt32(dr["OWNERTYPE"].ToString());
                d.OwnerTypeName = dr["OWNERTYPENAME"].ToString();
                d.Cldwzdsbh = dr["CLDWZDSBH"].ToString();
                d.Wlwkhm = dr["WLWKHM"].ToString();

                carInfoList.Add(d);
            }

            return carInfoList;
            //var q = (from c in SlaveDb.Set<CFG_CARINFO>()
            //         join n in SlaveDb.Set<CFG_NETINFO>() on c.BELONGNETID equals n.ID
            //         join v in SlaveDb.Set<SYS_BM_CODE>() on c.VEHICLETYPE equals v.BMKEY
            //         join d in SlaveDb.Set<SYS_BM_CODE>() on c.BELONGDEPTID equals d.BMKEY
            //         join o in SlaveDb.Set<SYS_BM_CODE>() on c.OWNERTYPE equals o.BMKEY into oo
            //         from ooo in oo.DefaultIfEmpty()
            //         where ((vehicleNo == "" || vehicleNo == null) ? true : c.VEHICLENO.Contains(vehicleNo))
            //         && ((ownerType == -1) ? true : c.OWNERTYPE == ownerType) && c.ISDELETED == 0
            //         select new CarInfo
            //         {
            //             Id = c.ID,
            //             VehicleNo = c.VEHICLENO,
            //             VehicleType = c.VEHICLETYPE,
            //             VehicleTypeName = v.BMVALUE,
            //             VehicleBrand = c.VEHICLEBRAND,
            //             BelongDeptId = c.BELONGDEPTID,
            //             BelongDeptName = d.BMVALUE,
            //             BelongNetName = n.NAME,
            //             Owner = c.OWNER,
            //             OwnerType = c.OWNERTYPE,
            //             OwnerTypeName = ooo.BMVALUE,
            //             Cldwzdsbh = c.CLDWZDSBH,
            //             Wlwkhm = c.WLWKHM
            //         });
            //totalCount = q.Count();
            //return q.OrderByDescending(p => p.OwnerType).ThenByDescending(p => p.BelongDeptId).ThenBy(p => p.VehicleNo).Skip((start - 1) * limit).Take(limit).ToList();

        }

    }
}
