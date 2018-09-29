using CCSIM.Entity;
using CCSIM.Extension;
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
            pSBQueryText.Append("SELECT * FROM (SELECT A.NAME,A.TELEPHONE AS OBJECTNAME,B.LON,B.LAT,TO_CHAR(B.PASSTIME, 'yyyy-mm-dd hh24:mi:ss') AS PASSTIME,1 AS TYPE,B.ADDRESS,C.BMVALUE AS BELONGDEPTNAME,'' AS OWNER FROM CFG_USERINFO A ");
            pSBQueryText.Append("LEFT JOIN GPS_REAL B ON A.TELEPHONE = B.OBJECTNAME LEFT JOIN SYS_BM_CODE C ON A.BELONGDEPTID = C.BMKEY WHERE A.ISDELETED = 0 AND A.LOGINTYPE=40 ORDER BY A.BELONGDEPTID,A.NAME)");
            pSBQueryText.Append("UNION ALL ");
            pSBQueryText.Append("SELECT * FROM(SELECT A.VEHICLENO AS NAME,TO_CHAR(D.ID) AS OBJECTNAME,B.LON,B.LAT,TO_CHAR(B.PASSTIME, 'yyyy-mm-dd hh24:mi:ss') AS PASSTIME,2 AS TYPE,B.ADDRESS,C.BMVALUE AS BELONGDEPTNAME,A.OWNER FROM CFG_CARINFO A ");
            pSBQueryText.Append("LEFT JOIN CFG_VEHICLEINFO D ON A.CLDWZDSBH = D.CLDWZDSBH LEFT JOIN GPS_REAL B ON D.ID = B.OBJECTNAME AND B.OBJECTTYPE=1 LEFT JOIN SYS_BM_CODE C ON A.BELONGDEPTID = C.BMKEY WHERE A.ISDELETED = 0 ORDER BY A.BELONGDEPTID,A.VEHICLENO)");

            var data = OracleOperateBLL.FillDataTable(pSBQueryText.ToString());
            List<TreeInfo> infoList = new List<TreeInfo>();
            foreach (DataRow dr in data.Rows)
            {
                var name = dr["NAME"].ToString();
                var objectName = dr["OBJECTNAME"].ToString();
                var lon = dr["LON"].ToString();
                var lat = dr["LAT"].ToString();
                var passTime = dr["PASSTIME"].ToString();
                var type = dr["TYPE"].ToString();
                var address = dr["ADDRESS"].ToString();
                var belongDeptName = dr["BELONGDEPTNAME"].ToString();
                var owner = dr["OWNER"].ToString();

                TreeInfo info = new TreeInfo();
                info.Name = name;
                info.ObjectName = objectName;
                info.Lon = lon;
                info.Lat = lat;
                info.PassTime = passTime;
                info.Type = type;
                info.Address = address;
                info.BelongDeptName = belongDeptName;
                info.Owner = owner;

                infoList.Add(info);
            }

            List<TreeNode> treeNodeList = new List<TreeNode>();
            TreeNode userNode = new TreeNode();
            userNode.name = "人员";
            userNode.open = true;
            userNode.@checked = true;
            userNode.id = "1";
            int userOffline = 0;
            int userCount = 0;
            //找到所有人员信息
            var userInfoList = infoList.Where(p => p.Type == "1").ToList();
            var belongDeptList_User = infoList.Where(p => p.Type == "1").Select(p => p.BelongDeptName).Distinct().OrderBy(p => p).ToList();
            foreach (var dept in belongDeptList_User)
            {
                TreeNode deptNode = new TreeNode();
                deptNode.open = false;
                deptNode.@checked = true;
                var id = "1_" + belongDeptList_User.IndexOf(dept);
                deptNode.id = id;

                var userList = infoList.Where(p => p.Type == "1" && p.BelongDeptName == dept).ToList();
                var userOffline_Dept = 0;
                var userCount_Dept = 0;
                foreach (var u in userList)
                {
                    var id_child = id + "_" + userList.IndexOf(u);
                    TreeNode n = new TreeNode();
                    n.objectName = u.ObjectName;
                    if (string.IsNullOrWhiteSpace(u.PassTime)) //实时表没有数据，表示离线
                    {
                        n.name = u.Name + "(离线)";
                        n.id = id_child;
                        n.iconSkin = "dark";
                        userOffline++;
                        userOffline_Dept++;
                    }
                    else
                    {
                        //如果时间大于5min，表示离线
                        if (now.Subtract(DateTime.Parse(u.PassTime)).TotalSeconds >= 300)
                        {
                            n.name = u.Name + "(" + u.PassTime + "后离线)";
                            n.id = id_child;
                            n.iconSkin = "dark";
                            userOffline++;
                            userOffline_Dept++;
                        }
                        else
                        {
                            n.name = u.Name;
                            n.id = id_child;
                            n.iconSkin = "light";
                        }
                    }

                    n.@checked = true;
                    deptNode.children.Add(n);

                    userCount++;
                    userCount_Dept++;
                }
                deptNode.name = dept + "(" + (userCount_Dept - userOffline_Dept) + "/" + userCount_Dept + ")";
                userNode.children.Add(deptNode);
            }
            userNode.name = "人员(" + (userCount - userOffline) + "/" + userCount + ")";

            TreeNode carNode = new TreeNode();
            carNode.name = "车辆";
            carNode.open = true;
            carNode.@checked = true;
            carNode.id = "2";
            int carOffline = 0;
            int carCount = 0;
            //找到所有车辆信息
            var carInfoList = infoList.Where(p => p.Type == "2").ToList();
            var belongDeptList_Car = infoList.Where(p => p.Type == "2").Select(p => p.BelongDeptName).Distinct().OrderBy(p => p).ToList();
            foreach (var dept in belongDeptList_Car)
            {
                TreeNode deptNode = new TreeNode();
                deptNode.open = false;
                deptNode.@checked = true;
                var id = "1_" + belongDeptList_Car.IndexOf(dept);
                deptNode.id = id;

                var carList = infoList.Where(p => p.Type == "2" && p.BelongDeptName == dept).ToList();
                int carOffline_Dept = 0;
                int carCount_Dept = 0;
                foreach (var c in carList)
                {
                    var id_child = id + "_" + carList.IndexOf(c);
                    TreeNode n = new TreeNode();
                    n.objectName = c.ObjectName;
                    if (string.IsNullOrWhiteSpace(c.PassTime)) //实时表没有数据，表示离线
                    {
                        n.name = c.Name + "(" + c.Owner + ")" + "(离线)";
                        n.id = id_child;
                        n.iconSkin = "dark";
                        carOffline++;
                        carOffline_Dept++;
                    }
                    else
                    {
                        //如果时间大于5min，表示离线
                        if (now.Subtract(DateTime.Parse(c.PassTime)).TotalSeconds >= 300)
                        {
                            n.name = c.Name + "(" + c.Owner + ")" + "(" + c.PassTime + "后离线)";
                            n.id = id_child;
                            n.iconSkin = "dark";
                            carOffline++;
                            carOffline_Dept++;
                        }
                        else
                        {
                            n.name = c.Name + "(" + c.Owner + ")";
                            n.id = id_child;
                            n.iconSkin = "light";
                        }
                    }

                    n.@checked = true;
                    deptNode.children.Add(n);

                    carCount++;
                    carCount_Dept++;
                }
                deptNode.name = dept + "(" + (carCount_Dept - carOffline_Dept) + "/" + carCount_Dept + ")";
                carNode.children.Add(deptNode);
            }
            carNode.name = "车辆(" + (carCount - carOffline) + "/" + carCount + ")";

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
            pSBQueryText.Append("LEFT JOIN GPS_REAL B ON A.TELEPHONE = B.OBJECTNAME WHERE A.ISDELETED = 0 AND A.LOGINTYPE=40 ");
            pSBQueryText.Append("UNION ALL ");
            pSBQueryText.Append("SELECT A.VEHICLENO AS NAME,TO_CHAR(D.ID) AS OBJECTNAME,CASE WHEN B.PASSTIME IS NULL THEN '1753-01-01 00:00:00' ELSE TO_CHAR(B.PASSTIME, 'yyyy-mm-dd hh24:mi:ss') END AS PASSTIME FROM CFG_CARINFO A ");
            pSBQueryText.Append("LEFT JOIN CFG_VEHICLEINFO D ON A.CLDWZDSBH = D.CLDWZDSBH LEFT JOIN GPS_REAL B ON A.VEHICLENO = B.OBJECTNAME AND B.OBJECTTYPE=1 WHERE A.ISDELETED = 0 ");

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
            pSBQueryText.Append("SELECT A.OBJECTNAME AS OBJECTID,LON,LAT,CASE WHEN B.BELONGNETID IS NULL THEN C.BELONGNETID ELSE B.BELONGNETID END AS BELONGNETID,");
            pSBQueryText.Append("CASE WHEN B.NAME IS NULL THEN C.VEHICLENO ELSE B.NAME END AS NAME,CASE WHEN B.NAME IS NULL THEN C.OWNER ELSE B.NAME END AS OWNER,TO_CHAR(A.PASSTIME, 'yyyy-mm-dd hh24:mi:ss') AS PASSTIME,A.ADDRESS,CASE WHEN B.NAME IS NULL THEN 2 ELSE 1 END AS TYPE,CASE WHEN B.NAME IS NULL THEN C.VEHICLETYPE ELSE 0 END AS OBJECTTYPE,TO_CHAR(A.LASTALARMTIME,'yyyy-mm-dd hh24:mi:ss') AS LASTALARMTIME FROM GPS_REAL A ");
            pSBQueryText.Append("LEFT JOIN CFG_USERINFO B ON A.OBJECTNAME = B.TELEPHONE ");
            pSBQueryText.Append("LEFT JOIN CFG_VEHICLEINFO D ON A.OBJECTNAME = D.ID AND A.OBJECTTYPE = 1 LEFT JOIN CFG_CARINFO C ON D.CLDWZDSBH=C.CLDWZDSBH WHERE A.OBJECTNAME IN('" + string.Join("','", objectNames) + "')");

            //获取所有需要提醒的用户手机号码
            var phone_Show = OracleOperateBLL.FillDataTable("SELECT DISTINCT PHONE FROM MESSAGE WHERE ISSHOW_PLATFORM=0");
            List<string> phone_ShowList = new List<string>();
            foreach (DataRow dr in phone_Show.Rows)
            {
                phone_ShowList.Add(dr["PHONE"].ToString());
            }
            var data = OracleOperateBLL.FillDataTable(pSBQueryText.ToString());
            List<GpsRealData> dataList = new List<GpsRealData>();
            foreach (DataRow dr in data.Rows)
            {
                var objectId = dr["OBJECTID"].ToString();
                var objectName = dr["NAME"].ToString();
                var lon = dr["LON"].ToString();
                var lat = dr["LAT"].ToString();
                var belongNetId = dr["BELONGNETID"].ToString();
                var passTime = dr["PASSTIME"].ToString();
                var address = dr["ADDRESS"].ToString();
                var type = dr["TYPE"].ToString();
                var owner = dr["OWNER"].ToString();
                var objectType = dr["OBJECTTYPE"].ToString();
                var lastAlarmTime = dr["LASTALARMTIME"].ToString();

                GpsRealData d = new GpsRealData();
                d.ObjectId = objectId;
                d.ObjectType = objectType;
                if (string.IsNullOrWhiteSpace(lastAlarmTime))
                {
                    d.IsNeedAlarm = true;
                }
                else //看最近一次报警时间与当前时间比较，小于一分钟不报警
                {
                    var alarmTime = DateTime.Parse(lastAlarmTime);
                    if (now.Subtract(alarmTime).TotalSeconds >= 60)
                    {
                        d.IsNeedAlarm = true;
                    }
                    else
                    {
                        d.IsNeedAlarm = false;
                    }
                }
                if (type == "1")  //人员
                {
                    if (phone_ShowList.Contains(d.ObjectId))
                    {
                        d.IsNeedShow = true;
                    }
                    else
                    {
                        d.IsNeedShow = false;
                    }
                    d.ObjectName = objectName;
                    var lonAndLat = GpsTranslate.gcj2bd(Convert.ToDouble(lat), Convert.ToDouble(lon));
                    d.Lat = lonAndLat[0].ToString();
                    d.Lon = lonAndLat[1].ToString();
                }
                else
                {
                    d.ObjectName = objectName;
                    d.Lon = lon;
                    d.Lat = lat;
                }
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

        /// <summary>
        /// 更新定位实时信息
        /// </summary>
        /// <param name="objectName"></param>
        /// <returns></returns>
        public static void UpdateGpsInfo(string objectName)
        {
            StringBuilder pSBQueryText = new StringBuilder();
            pSBQueryText.Append("UPDATE GPS_REAL SET LASTALARMTIME=TO_DATE('" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss') WHERE OBJECTNAME='" + objectName + "'");

            try
            {
                OracleOperateBLL.ExecuteSql(pSBQueryText.ToString());
            }
            catch
            {

            }
        }

        /// <summary>
        /// 获取未显示过的消息
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static List<MessageInfo> GetMessageList(string phone)
        {
            List<MessageInfo> infoList = new List<MessageInfo>();
            List<int> ids = new List<int>();
            StringBuilder pSBQueryText = new StringBuilder();
            pSBQueryText.Append("SELECT ID,TITLE,ADDRESS,REMARKS FROM MESSAGE WHERE ISSHOW_PLATFORM=0 and PHONE='" + phone + "'");
            var data = OracleOperateBLL.FillDataTable(pSBQueryText.ToString());
            foreach (DataRow dr in data.Rows)
            {
                MessageInfo info = new MessageInfo();
                info.Id = Convert.ToInt32(dr["ID"]);
                info.Title = dr["TITLE"].ToString();
                info.Address = dr["ADDRESS"].ToString();
                info.Remarks = dr["REMARKS"].ToString();

                ids.Add(info.Id);
                infoList.Add(info);
            }

            UpdateMessage(ids);
            return infoList;
        }

        /// <summary>
        /// 更新消息
        /// </summary>
        /// <param name="ids"></param>
        public static void UpdateMessage(List<int> ids)
        {
            StringBuilder pSBQueryText = new StringBuilder();
            pSBQueryText.Append("UPDATE MESSAGE SET ISSHOW_PLATFORM=1 WHERE ID IN(" + string.Join(",", ids) + ")");

            try
            {
                OracleOperateBLL.ExecuteSql(pSBQueryText.ToString());
            }
            catch
            {

            }
        }
    }
}
