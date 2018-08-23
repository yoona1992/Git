using CCSIM.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSIM.BLL
{
    /// <summary>
    /// 地图类
    /// </summary>
    public class MapBLL
    {
        /// <summary>
        /// 获取树
        /// </summary>
        /// <returns></returns>
        public static List<TreeNode> GetTree()
        {
            var now = DateTime.Now;
            StringBuilder pSBQueryText = new StringBuilder();
            pSBQueryText.Append("SELECT * FROM (SELECT A.NAME,A.TELEPHONE AS OBJECTNAME,B.LON,B.LAT,TO_CHAR(B.PASSTIME, 'yyyy-mm-dd hh24:mi:ss') AS PASSTIME,1 AS TYPE,B.ADDRESS FROM CFG_USERINFO A ");
            pSBQueryText.Append("LEFT JOIN GPS_REAL B ON A.TELEPHONE = B.OBJECTNAME WHERE A.ISDELETED = 0 ORDER BY A.NAME DESC)");
            pSBQueryText.Append("UNION ALL ");
            pSBQueryText.Append("SELECT * FROM(SELECT A.VEHICLENO AS NAME,A.VEHICLENO AS OBJECTNAME,B.LON,B.LAT,TO_CHAR(B.PASSTIME, 'yyyy-mm-dd hh24:mi:ss') AS PASSTIME,2 AS TYPE,B.ADDRESS FROM CFG_CARINFO A ");
            pSBQueryText.Append("LEFT JOIN GPS_REAL B ON A.VEHICLENO = B.OBJECTNAME WHERE A.ISDELETED = 0 ORDER BY A.VEHICLENO DESC)");

            var data = OracleOperateBLL.FillDataTable(pSBQueryText.ToString());
            List<TreeNode> treeNodeList = new List<TreeNode>();
            TreeNode userNode = new TreeNode();
            userNode.name = "人员";
            userNode.open = true;
            userNode.@checked = true;
            userNode.id = "1";
            TreeNode carNode = new TreeNode();
            carNode.name = "车辆";
            carNode.open = true;
            carNode.@checked = true;
            carNode.id = "2";
            var index = 0;
            foreach (DataRow dr in data.Rows)
            {
                var name = dr["NAME"].ToString();
                var objectName = dr["OBJECTNAME"].ToString();
                var lon = dr["LON"].ToString();
                var lat = dr["LAT"].ToString();
                var passTime = dr["PASSTIME"].ToString();
                var type = dr["TYPE"].ToString();

                TreeNode n = new TreeNode();
                n.objectName = objectName;
                if (string.IsNullOrWhiteSpace(passTime)) //实时表没有数据，表示离线
                {
                    n.name = name + "(离线)";
                    n.id = type + "_" + index.ToString();
                }
                else
                {
                    //如果时间大于5min，表示离线
                    if (now.Subtract(DateTime.Parse(passTime)).TotalSeconds >= 300)
                    {
                        n.name = name + "(" + passTime + "后离线)";
                        n.id = type + "_" + index.ToString();
                    }
                    else
                    {
                        n.name = name;
                        n.id = type + "_" + index.ToString();
                    }
                }

                n.@checked = true;
                if (type == "1")
                {
                    userNode.children.Add(n);
                }
                else
                {
                    carNode.children.Add(n);
                }
            }

            if (userNode.children.Count > 0)
            {
                treeNodeList.Add(userNode);
            }
            if (carNode.children.Count > 0)
            {
                treeNodeList.Add(carNode);
            }

            return treeNodeList;
        }

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public static string GetAllInfo()
        {
            var info = "";
            var now = DateTime.Now;
            StringBuilder pSBQueryText = new StringBuilder();
            pSBQueryText.Append("SELECT A.NAME,A.TELEPHONE AS OBJECTNAME,CASE WHEN B.PASSTIME IS NULL THEN '1753-01-01 00:00:00' ELSE TO_CHAR(B.PASSTIME, 'yyyy-mm-dd hh24:mi:ss') END AS PASSTIME FROM CFG_USERINFO A ");
            pSBQueryText.Append("LEFT JOIN GPS_REAL B ON A.TELEPHONE = B.OBJECTNAME WHERE A.ISDELETED = 0 ");
            pSBQueryText.Append("UNION ALL ");
            pSBQueryText.Append("SELECT A.VEHICLENO AS NAME,A.VEHICLENO AS OBJECTNAME,CASE WHEN B.PASSTIME IS NULL THEN '1753-01-01 00:00:00' ELSE TO_CHAR(B.PASSTIME, 'yyyy-mm-dd hh24:mi:ss') END AS PASSTIME FROM CFG_CARINFO A ");
            pSBQueryText.Append("LEFT JOIN GPS_REAL B ON A.VEHICLENO = B.OBJECTNAME WHERE A.ISDELETED = 0 ");

            var data = OracleOperateBLL.FillDataTable(pSBQueryText.ToString());
            foreach (DataRow dr in data.Rows)
            {
                var objectName = dr["OBJECTNAME"].ToString();
                var passTime = dr["PASSTIME"].ToString();
                if (string.IsNullOrWhiteSpace(objectName) == false)//&& now.Subtract(DateTime.Parse(passTime)).TotalSeconds <= 300)
                {
                    info += objectName + ";";
                }
            }

            return info;
        }

        /// <summary>
        /// 获取最新定位信息
        /// </summary>
        /// <param name="objectName"></param>
        /// <returns></returns>
        public static List<GpsRealData> GetNewGpsInfo(string[] objectNames)
        {
            var now = DateTime.Now;
            StringBuilder pSBQueryText = new StringBuilder();
            pSBQueryText.Append("SELECT LON,LAT,CASE WHEN B.BELONGNETID IS NULL THEN C.BELONGNETID ELSE B.BELONGNETID END AS BELONGNETID,");
            pSBQueryText.Append("CASE WHEN B.NAME IS NULL THEN C.VEHICLENO ELSE B.NAME END AS NAME,TO_CHAR(A.PASSTIME, 'yyyy-mm-dd hh24:mi:ss') AS PASSTIME,A.ADDRESS,CASE WHEN B.NAME IS NULL THEN 2 ELSE 1 END AS TYPE FROM GPS_REAL A ");
            pSBQueryText.Append("LEFT JOIN CFG_USERINFO B ON A.OBJECTNAME = B.TELEPHONE ");
            pSBQueryText.Append("LEFT JOIN CFG_CARINFO C ON A.OBJECTNAME = C.VEHICLENO WHERE A.OBJECTNAME IN('" + string.Join("','", objectNames) + "')");

            var data = OracleOperateBLL.FillDataTable(pSBQueryText.ToString());
            List<GpsRealData> dataList = new List<GpsRealData>();
            foreach (DataRow dr in data.Rows)
            {
                var objectName = dr["NAME"].ToString();
                var lon = dr["LON"].ToString();
                var lat = dr["LAT"].ToString();
                var belongNetId = dr["BELONGNETID"].ToString();
                var passTime = dr["PASSTIME"].ToString();
                var address= dr["ADDRESS"].ToString();
                var type = dr["TYPE"].ToString();

                GpsRealData d = new GpsRealData();
                d.ObjectName = objectName;
                d.Lon = lon;
                d.Lat = lat;
                d.BelongNetId = belongNetId;
                d.Address = address;
                d.Type = type;
                //如果时间大于5min，表示离线
                if (now.Subtract(DateTime.Parse(passTime)).TotalSeconds >= 300)
                {
                    d.IsOffline = true;
                }
                else
                {
                    d.IsOffline = false;
                }
                dataList.Add(d);
            }

            return dataList;
        }
    }
}
