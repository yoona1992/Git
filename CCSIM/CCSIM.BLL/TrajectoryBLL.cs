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
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSIM.BLL
{
    /// <summary>
    /// 轨迹
    /// </summary>
    public class TrajectoryBLL
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
        /// 获取人员轨迹列表
        /// </summary>
        /// <param name="telephone">电话号码</param>
        /// <param name="stTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static List<GpsData> GetList_User(string telephone, DateTime stTime, DateTime endTime, int start, int limit, out int totalCount)
        {
            StringBuilder pSBQueryCount = new StringBuilder();
            pSBQueryCount.Append("SELECT COUNT(*) FROM GPS_DATA WHERE CREATE_TIME BETWEEN TO_DATE('" + stTime.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss') AND TO_DATE('" + endTime.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss') ");
            if (string.IsNullOrWhiteSpace(telephone) == false)
            {
                pSBQueryCount.Append("AND PHONE='" + telephone + "'");
            }
            totalCount = OracleOperateBLL.GetQueryCount(pSBQueryCount.ToString());

            int pStartNum = limit * (start - 1) + 1;
            int pEndNum = limit * start;

            StringBuilder pSBQueryText = new StringBuilder();
            pSBQueryText.Append("SELECT LON,LAT,CREATE_TIME,ADDRESS FROM (SELECT A.*, rownum r FROM(");
            pSBQueryText.Append("SELECT * FROM GPS_DATA WHERE CREATE_TIME BETWEEN TO_DATE('" + stTime.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss') AND TO_DATE('" + endTime.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss') ");
            if (string.IsNullOrWhiteSpace(telephone) == false)
            {
                pSBQueryText.Append("AND PHONE='" + telephone + "' ");
            }
            pSBQueryText.Append(" ORDER BY CREATE_TIME DESC) A ");
            pSBQueryText.Append("WHERE rownum<=" + pEndNum + ") B WHERE r>=" + pStartNum);
            var data = OracleOperateBLL.FillDataTable(pSBQueryText.ToString());
            List<GpsData> gpsDataList = new List<GpsData>();
            foreach (DataRow dr in data.Rows)
            {
                GpsData d = new GpsData();
                d.Address = dr["ADDRESS"].ToString();
                d.Create_Time = DateTime.Parse(dr["CREATE_TIME"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                d.Lat = dr["LAT"].ToString();
                d.Lon = dr["LON"].ToString();

                gpsDataList.Add(d);
            }

            return gpsDataList;
        }
        public static List<GpsData> GetList_User(string telephone, DateTime stTime, DateTime endTime)
        {
            StringBuilder pSBQueryText = new StringBuilder();
            pSBQueryText.Append("SELECT LON,LAT,CREATE_TIME,ADDRESS FROM GPS_DATA WHERE CREATE_TIME BETWEEN TO_DATE('" + stTime.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss') AND TO_DATE('" + endTime.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss') ");
            if (string.IsNullOrWhiteSpace(telephone) == false)
            {
                pSBQueryText.Append("AND PHONE='" + telephone + "' ");
            }
            pSBQueryText.Append(" ORDER BY CREATE_TIME");
            var data = OracleOperateBLL.FillDataTable(pSBQueryText.ToString());
            List<GpsData> gpsDataList = new List<GpsData>();
            foreach (DataRow dr in data.Rows)
            {
                GpsData d = new GpsData();
                d.Address = dr["ADDRESS"].ToString();
                d.Create_Time = DateTime.Parse(dr["CREATE_TIME"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                var lonAndLat = GpsTranslate.gcj2bd(Convert.ToDouble(dr["LAT"].ToString()), Convert.ToDouble(dr["LON"].ToString()));
                d.Lat = lonAndLat[0].ToString();
                d.Lon = lonAndLat[1].ToString();

                gpsDataList.Add(d);
            }

            return gpsDataList;
        }

        /// <summary>
        /// 获取车辆轨迹列表
        /// </summary>
        /// <param name="cldwzdsbh"></param>
        /// <param name="stTime"></param>
        /// <param name="endTime"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static List<GpsData> GetList_Car(string cldwzdsbh, DateTime stTime, DateTime endTime, int start, int limit, out int totalCount)
        {
            StringBuilder pSBQueryCount = new StringBuilder();
            pSBQueryCount.Append("SELECT COUNT(*) FROM GPS_VEHICLEDATA A LEFT JOIN CFG_VEHICLEINFO B ON A.VEHID=B.ID WHERE A.PASSTIME BETWEEN TO_DATE('" + stTime.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss') AND TO_DATE('" + endTime.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss') ");
            if (string.IsNullOrWhiteSpace(cldwzdsbh) == false)
            {
                pSBQueryCount.Append("AND B.CLDWZDSBH='" + cldwzdsbh + "'");
            }
            totalCount = OracleOperateBLL.GetQueryCount(pSBQueryCount.ToString());

            int pStartNum = limit * (start - 1) + 1;
            int pEndNum = limit * start;

            StringBuilder pSBQueryText = new StringBuilder();
            pSBQueryText.Append("SELECT LONGITUDE_BAIDU,LATITUDE_BAIDU,PASSTIME FROM (SELECT C.*, rownum r FROM(");
            pSBQueryText.Append("SELECT * FROM GPS_VEHICLEDATA A LEFT JOIN CFG_VEHICLEINFO B ON A.VEHID=B.ID WHERE A.PASSTIME BETWEEN TO_DATE('" + stTime.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss') AND TO_DATE('" + endTime.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss') ");
            if (string.IsNullOrWhiteSpace(cldwzdsbh) == false)
            {
                pSBQueryText.Append("AND B.CLDWZDSBH='" + cldwzdsbh + "' ");
            }
            pSBQueryText.Append(" ORDER BY A.PASSTIME DESC) C ");
            pSBQueryText.Append("WHERE rownum<=" + pEndNum + ") D WHERE r>=" + pStartNum);
            var data = OracleOperateBLL.FillDataTable(pSBQueryText.ToString());
            List<GpsData> gpsDataList = new List<GpsData>();
            foreach (DataRow dr in data.Rows)
            {
                GpsData d = new GpsData();
                //d.Address = dr["ADDRESS"].ToString();
                d.Create_Time = DateTime.Parse(dr["PASSTIME"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                d.Lat = Convert.ToDecimal(dr["LATITUDE_BAIDU"]).ToString("f6");
                d.Lon = Convert.ToDecimal(dr["LONGITUDE_BAIDU"]).ToString("f6");

                gpsDataList.Add(d);
            }

            return gpsDataList;
        }
        public static List<GpsData> GetList_Car(string cldwzdsbh, DateTime stTime, DateTime endTime)
        {
            StringBuilder pSBQueryText = new StringBuilder();
            pSBQueryText.Append("SELECT LONGITUDE_BAIDU,LATITUDE_BAIDU,PASSTIME FROM GPS_VEHICLEDATA A LEFT JOIN CFG_VEHICLEINFO B ON A.VEHID=B.ID WHERE A.PASSTIME BETWEEN TO_DATE('" + stTime.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss') AND TO_DATE('" + endTime.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss') ");
            if (string.IsNullOrWhiteSpace(cldwzdsbh) == false)
            {
                pSBQueryText.Append("AND B.CLDWZDSBH='" + cldwzdsbh + "' ");
            }
            pSBQueryText.Append(" ORDER BY A.PASSTIME DESC");
            var data = OracleOperateBLL.FillDataTable(pSBQueryText.ToString());
            List<GpsData> gpsDataList = new List<GpsData>();
            foreach (DataRow dr in data.Rows)
            {
                GpsData d = new GpsData();
                //d.Address = dr["ADDRESS"].ToString();
                d.Create_Time = DateTime.Parse(dr["PASSTIME"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                d.Lat = Convert.ToDecimal(dr["LATITUDE_BAIDU"]).ToString("f6");
                d.Lon = Convert.ToDecimal(dr["LONGITUDE_BAIDU"]).ToString("f6");

                gpsDataList.Add(d);
            }

            return gpsDataList;
        }

    }
}
