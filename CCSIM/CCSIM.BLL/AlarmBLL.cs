using CCSIM.DAL.Base;
using CCSIM.DAL.DBContext;
using CCSIM.DAL.Model;
using CCSIM.Entity;
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
    /// 报警类
    /// </summary>
    public class AlarmBLL
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
        /// 添加报警信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static bool Add(INFO_ALARMINFO info)
        {
            DbBase<INFO_ALARMINFO> db = new DbBase<INFO_ALARMINFO>();
            db.Insert(info);
            if (db.SaveChanges() >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取报警列表
        /// </summary>
        /// <param name="objectName">对象名称</param>
        /// <param name="alarmType">报警类型</param>
        /// <param name="stTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static List<AlarmInfo> GetList(string objectName,int alarmType, DateTime stTime, DateTime endTime, int start, int limit, out int totalCount)
        {
            StringBuilder pSBQueryCount = new StringBuilder();
            pSBQueryCount.Append("SELECT COUNT(*) FROM INFO_ALARMINFO WHERE ALARMTIME BETWEEN TO_DATE('" + stTime.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss') AND TO_DATE('" + endTime.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss') ");
            if (string.IsNullOrWhiteSpace(objectName) == false)
            {
                pSBQueryCount.Append("AND ALARMOBJECTNAME LIKE '%" + objectName + "%'");
            }
            if (alarmType != -1)
            {
                pSBQueryCount.Append("AND ALARMTYPE = " + alarmType);
            }
            totalCount = OracleOperateBLL.GetQueryCount(pSBQueryCount.ToString());

            int pStartNum = limit * (start - 1) + 1;
            int pEndNum = limit * start;

            StringBuilder pSBQueryText = new StringBuilder();
            pSBQueryText.Append("SELECT ALARMINFO,TO_CHAR(ALARMTIME, 'yyyy-mm-dd hh24:mi:ss') AS ALARMTIME,ALARMADDRESS,ALARMTYPE,ALARMOBJECTNAME FROM (SELECT A.*, rownum r FROM(");
            pSBQueryText.Append("SELECT * FROM INFO_ALARMINFO WHERE ALARMTIME BETWEEN TO_DATE('" + stTime.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss') AND TO_DATE('" + endTime.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss') ");
            if (string.IsNullOrWhiteSpace(objectName) == false)
            {
                pSBQueryText.Append("AND ALARMOBJECTNAME LIKE '%" + objectName + "%'");
            }
            if (alarmType != -1)
            {
                pSBQueryText.Append("AND ALARMTYPE = " + alarmType);
            }
            pSBQueryText.Append(" ORDER BY ALARMTIME DESC) A ");
            pSBQueryText.Append("WHERE rownum<=" + pEndNum + ") B WHERE r>=" + pStartNum);
            var data = OracleOperateBLL.FillDataTable(pSBQueryText.ToString());
            List<AlarmInfo> alarmInfoList = new List<AlarmInfo>();
            foreach (DataRow dr in data.Rows)
            {
                AlarmInfo a = new AlarmInfo();
                a.Info = dr["ALARMINFO"].ToString();
                a.AlarmTimeStr = dr["ALARMTIME"].ToString();
                var type = Convert.ToInt16(dr["ALARMTYPE"].ToString());
                if (type == 1)
                {
                    a.AlarmTypeName = "人员报警";
                }
                else
                {
                    a.AlarmTypeName = "车辆报警";
                }
                a.AlarmAddress = dr["ALARMADDRESS"].ToString();
                a.AlarmObjectName = dr["ALARMOBJECTNAME"].ToString();

                alarmInfoList.Add(a);
            }

            return alarmInfoList;
        }
    }
}
