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
    /// 用户
    /// </summary>
    public class UserBLL
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        private static string ORACLE_CONNECTION_STRING = "user id=LSGAADMIN;password=lsga110;data source=122.225.122.106/ORCL";

        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static bool Add(CFG_USERINFO info)
        {
            bool isSuccess = true;
            StringBuilder pInsertText = new StringBuilder();
            pInsertText.Append("INSERT INTO CFG_USERINFO(NAME,SEX,AGE,TELEPHONE,CERTIFICATETYPE,CERTIFICATENUM,DIRECTION,BELONGDEPTID,BELONGNETID,ADDRESS,REMARK,ISDELETED,USERTYPE,VIRTUALTRUMPET,USERNAME,USERPWD,LOGINTYPE) VALUES('");
            pInsertText.Append(info.NAME + "'," + info.SEX + ",'" + info.AGE + "','" + info.TELEPHONE + "'," + info.CERTIFICATETYPE + ",'" + info.CERTIFICATENUM + "','" + info.DIRECTION + "',");
            pInsertText.Append(info.BELONGDEPTID + "," + info.BELONGNETID + ",'" + info.ADDRESS + "','" + info.REMARK + "'," + info.ISDELETED + "," + info.USERTYPE + ",'");
            pInsertText.Append(info.VIRTUALTRUMPET + "','" + info.USERNAME + "','" + info.USERPWD + "'," + info.LOGINTYPE + ")");

            try
            {
                OracleOperateBLL.ExecuteSql(pInsertText.ToString());
            }
            catch
            {
                isSuccess = false;
            }

            return isSuccess;

            //DbBase<CFG_USERINFO> db = new DbBase<CFG_USERINFO>();
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
        /// 修改用户信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static bool Update(CFG_USERINFO info)
        {
            bool isSuccess = true;
            StringBuilder pUpdateText = new StringBuilder();
            pUpdateText.Append("UPDATE CFG_USERINFO SET NAME='" + info.NAME + "',SEX=" + info.SEX + ",AGE='" + info.AGE + "',TELEPHONE='" + info.TELEPHONE + "',CERTIFICATETYPE=" + info.CERTIFICATETYPE + ",");
            pUpdateText.Append("CERTIFICATENUM='" + info.CERTIFICATENUM + "',DIRECTION='" + info.DIRECTION + "',BELONGDEPTID=" + info.BELONGDEPTID + ",BELONGNETID=" + info.BELONGNETID + ",ADDRESS='" + info.ADDRESS + "', ");
            pUpdateText.Append("REMARK='" + info.REMARK + "',USERTYPE=" + info.USERTYPE + ",VIRTUALTRUMPET='" + info.VIRTUALTRUMPET + "',USERNAME='" + info.USERNAME + "',USERPWD='" + info.USERPWD + "',LOGINTYPE= " + info.LOGINTYPE + " ");
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

            //DbBase<CFG_USERINFO> db = new DbBase<CFG_USERINFO>();
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
        /// 删除用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool Delete(int id)
        {
            bool isSuccess = true;
            StringBuilder pUpdateText = new StringBuilder();
            pUpdateText.Append("DELETE FROM CFG_USERINFO WHERE ID=" + id);

            try
            {
                OracleOperateBLL.ExecuteSql(pUpdateText.ToString());
            }
            catch
            {
                isSuccess = false;
            }

            return isSuccess;

            //DbBase<CFG_USERINFO> db = new DbBase<CFG_USERINFO>();
            //db.Delete(p => p.ID == id);
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
                pUpdateText.Append("DELETE FROM CFG_USERINFO WHERE ID=" + id);

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

            //DbBase<CFG_USERINFO> db = new DbBase<CFG_USERINFO>();
            //List<CFG_USERINFO> infoList = new List<CFG_USERINFO>();
            //foreach (var id in ids)
            //{
            //    db.Delete(p => p.ID == id);
            //    //var info = Get(id);
            //    //info.ISDELETED = 1;
            //    //infoList.Add(info);
            //}
            ////db.UpdateAll(infoList);
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
        /// 获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static CFG_USERINFO Get(int id)
        {
            CFG_USERINFO pUserInfo = new CFG_USERINFO();
            OracleConnection pConn = null;
            OracleCommand pComm = null;
            OracleDataReader pReader = null;

            pConn = new OracleConnection(ORACLE_CONNECTION_STRING);
            pConn.Open();
            string pCmdText = "SELECT ID,NAME,SEX,AGE,TELEPHONE,CERTIFICATETYPE,CERTIFICATENUM,DIRECTION,BELONGDEPTID,BELONGNETID,ADDRESS,REMARK,ISDELETED,USERTYPE,VIRTUALTRUMPET,USERNAME,USERPWD,LOGINTYPE FROM CFG_USERINFO WHERE ID = " + id + "";
            pComm = new OracleCommand(pCmdText, pConn);
            pReader = pComm.ExecuteReader();
            while (pReader.Read())
            {
                pUserInfo.ID = Convert.ToInt32(pReader["ID"]);
                pUserInfo.NAME = Convert.ToString(pReader["NAME"]);
                pUserInfo.SEX = Convert.ToInt32(pReader["SEX"]);
                pUserInfo.AGE = Convert.ToString(pReader["AGE"]);
                pUserInfo.TELEPHONE = Convert.ToString(pReader["TELEPHONE"]);
                pUserInfo.CERTIFICATETYPE = Convert.ToInt32(pReader["CERTIFICATETYPE"]);
                pUserInfo.CERTIFICATENUM = Convert.ToString(pReader["CERTIFICATENUM"]);
                pUserInfo.DIRECTION = Convert.ToString(pReader["DIRECTION"]);
                pUserInfo.BELONGDEPTID = Convert.ToInt32(pReader["BELONGDEPTID"]);
                pUserInfo.BELONGNETID = Convert.ToInt32(pReader["BELONGNETID"]);
                pUserInfo.ADDRESS = Convert.ToString(pReader["ADDRESS"]);
                pUserInfo.REMARK = Convert.ToString(pReader["REMARK"]);
                pUserInfo.ISDELETED = Convert.ToInt32(pReader["ISDELETED"]);
                pUserInfo.USERTYPE = Convert.ToInt32(pReader["USERTYPE"]);
                pUserInfo.VIRTUALTRUMPET = Convert.ToString(pReader["VIRTUALTRUMPET"]);
                pUserInfo.USERNAME = Convert.ToString(pReader["USERNAME"]);
                pUserInfo.USERPWD = Convert.ToString(pReader["USERPWD"]);
                pUserInfo.LOGINTYPE = Convert.ToInt32(pReader["LOGINTYPE"]);
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
            return pUserInfo;
            //DbBase<CFG_USERINFO> db = new DbBase<CFG_USERINFO>();
            //return db.FirstOrDefault(p => p.ID == id);
        }

        public static CFG_USERINFO Get(string phone)
        {
            CFG_USERINFO pUserInfo = new CFG_USERINFO();
            OracleConnection pConn = null;
            OracleCommand pComm = null;
            OracleDataReader pReader = null;

            pConn = new OracleConnection(ORACLE_CONNECTION_STRING);
            pConn.Open();
            string pCmdText = "SELECT ID,NAME,SEX,AGE,TELEPHONE,CERTIFICATETYPE,CERTIFICATENUM,DIRECTION,BELONGDEPTID,BELONGNETID,ADDRESS,REMARK,ISDELETED,USERTYPE,VIRTUALTRUMPET,USERNAME,USERPWD,LOGINTYPE FROM CFG_USERINFO WHERE TELEPHONE = '" + phone + "'";
            pComm = new OracleCommand(pCmdText, pConn);
            pReader = pComm.ExecuteReader();
            while (pReader.Read())
            {
                pUserInfo.ID = Convert.ToInt32(pReader["ID"]);
                pUserInfo.NAME = Convert.ToString(pReader["NAME"]);
                pUserInfo.SEX = Convert.ToInt32(pReader["SEX"]);
                pUserInfo.AGE = Convert.ToString(pReader["AGE"]);
                pUserInfo.TELEPHONE = Convert.ToString(pReader["TELEPHONE"]);
                pUserInfo.CERTIFICATETYPE = Convert.ToInt32(pReader["CERTIFICATETYPE"]);
                pUserInfo.CERTIFICATENUM = Convert.ToString(pReader["CERTIFICATENUM"]);
                pUserInfo.DIRECTION = Convert.ToString(pReader["DIRECTION"]);
                pUserInfo.BELONGDEPTID = Convert.ToInt32(pReader["BELONGDEPTID"]);
                pUserInfo.BELONGNETID = Convert.ToInt32(pReader["BELONGNETID"]);
                pUserInfo.ADDRESS = Convert.ToString(pReader["ADDRESS"]);
                pUserInfo.REMARK = Convert.ToString(pReader["REMARK"]);
                pUserInfo.ISDELETED = Convert.ToInt32(pReader["ISDELETED"]);
                pUserInfo.USERTYPE = Convert.ToInt32(pReader["USERTYPE"]);
                pUserInfo.VIRTUALTRUMPET = Convert.ToString(pReader["VIRTUALTRUMPET"]);
                pUserInfo.USERNAME = Convert.ToString(pReader["USERNAME"]);
                pUserInfo.USERPWD = Convert.ToString(pReader["USERPWD"]);
                pUserInfo.LOGINTYPE = Convert.ToInt32(pReader["LOGINTYPE"]);
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
            return pUserInfo;

            //DbBase<CFG_USERINFO> db = new DbBase<CFG_USERINFO>();
            //return db.FirstOrDefault(p => p.TELEPHONE == phone);
        }

        /// <summary>
        /// 获取用户信息列表
        /// </summary>
        /// <param name="name"></param>
        /// <param name="belongDeptId"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static List<UserInfo> GetList(string name, string telephone, int belongDeptId, int userType, int start, int limit, out int totalCount)
        {
            StringBuilder pSBQueryCount = new StringBuilder();
            pSBQueryCount.Append("SELECT COUNT(*) FROM CFG_USERINFO WHERE ISDELETED=0 ");
            if (string.IsNullOrWhiteSpace(name) == false)
            {
                pSBQueryCount.Append("AND NAME like '%" + name + "%' ");
            }
            if (string.IsNullOrWhiteSpace(telephone) == false)
            {
                pSBQueryCount.Append("AND TELEPHONE like '%" + telephone + "%' ");
            }
            if (userType != -1)
            {
                pSBQueryCount.Append("AND USERTYPE =" + userType + " ");
            }
            if (belongDeptId != -1)
            {
                pSBQueryCount.Append("AND BELONGDEPTID =" + belongDeptId);
            }
            totalCount = OracleOperateBLL.GetQueryCount(pSBQueryCount.ToString());

            int pStartNum = limit * (start - 1) + 1;
            int pEndNum = limit * start;

            StringBuilder pSBQueryText = new StringBuilder();
            pSBQueryText.Append("SELECT ID,NAME,SEXNAME,AGE,TELEPHONE,CERTIFICATENUM,ADDRESS,BELONGDEPTID,BELONGDEPTNAME,BELONGNETID,BELONGNETNAME,USERTYPE,USERTYPENAME,VIRTUALTRUMPET FROM (SELECT F.*, rownum r FROM(");
            pSBQueryText.Append("SELECT A.ID,A.NAME,C.BMVALUE AS SEXNAME,A.AGE,A.TELEPHONE,A.CERTIFICATENUM,A.ADDRESS,A.BELONGDEPTID,D.BMVALUE AS BELONGDEPTNAME,A.BELONGNETID,B.NAME AS BELONGNETNAME,A.USERTYPE,E.BMVALUE AS USERTYPENAME,A.VIRTUALTRUMPET FROM CFG_USERINFO A ");
            pSBQueryText.Append("LEFT JOIN CFG_NETINFO B ON A.BELONGNETID=B.ID ");
            pSBQueryText.Append("LEFT JOIN SYS_BM_CODE C ON A.SEX=C.BMKEY ");
            pSBQueryText.Append("LEFT JOIN SYS_BM_CODE D ON A.BELONGDEPTID=D.BMKEY ");
            pSBQueryText.Append("LEFT JOIN SYS_BM_CODE E ON A.USERTYPE=E.BMKEY WHERE A.ISDELETED=0 ");
            if (string.IsNullOrWhiteSpace(name) == false)
            {
                pSBQueryText.Append("AND A.NAME like '%" + name + "%' ");
            }
            if (string.IsNullOrWhiteSpace(telephone) == false)
            {
                pSBQueryText.Append("AND A.TELEPHONE like '%" + telephone + "%' ");
            }
            if (userType != -1)
            {
                pSBQueryText.Append("AND A.USERTYPE =" + userType + " ");
            }
            if (belongDeptId != -1)
            {
                pSBQueryText.Append("AND A.BELONGDEPTID =" + belongDeptId);
            }
            pSBQueryText.Append(" ORDER BY A.USERTYPE DESC,A.BELONGDEPTID DESC,A.BELONGNETID DESC,A.NAME DESC) F ");
            pSBQueryText.Append("WHERE rownum<=" + pEndNum + ") G WHERE r>=" + pStartNum);
            var data = OracleOperateBLL.FillDataTable(pSBQueryText.ToString());
            List<UserInfo> userInfoList = new List<UserInfo>();
            foreach (DataRow dr in data.Rows)
            {
                UserInfo d = new UserInfo();
                d.Id = Convert.ToInt32(dr["ID"].ToString());
                d.Name = dr["NAME"].ToString();
                d.SexName = dr["SEXNAME"].ToString();
                d.Age = dr["AGE"].ToString();
                d.Telephone = dr["TELEPHONE"].ToString();
                d.CertificateNum = dr["CERTIFICATENUM"].ToString();
                d.Address = dr["ADDRESS"].ToString();
                d.BelongDeptId = Convert.ToInt32(dr["BELONGDEPTID"].ToString());
                d.BelongDeptName = dr["BELONGDEPTNAME"].ToString();
                d.BelongNetId = Convert.ToInt32(dr["BELONGNETID"].ToString());
                d.BelongNetName = dr["BELONGNETNAME"].ToString();
                d.UserType = Convert.ToInt32(dr["USERTYPE"].ToString());
                d.UserTypeName = dr["USERTYPENAME"].ToString();
                d.VirtualTrumpet = dr["VIRTUALTRUMPET"].ToString();

                userInfoList.Add(d);
            }

            return userInfoList;

            //var q = (from u in SlaveDb.Set<CFG_USERINFO>()
            //         join n in SlaveDb.Set<CFG_NETINFO>() on u.BELONGNETID equals n.ID into nn
            //         join s in SlaveDb.Set<SYS_BM_CODE>() on u.SEX equals s.BMKEY
            //         join d in SlaveDb.Set<SYS_BM_CODE>() on u.BELONGDEPTID equals d.BMKEY
            //         join t in SlaveDb.Set<SYS_BM_CODE>() on u.USERTYPE equals t.BMKEY
            //         from nnn in nn.DefaultIfEmpty()
            //         where ((name == "" || name == null) ? true : u.NAME.Contains(name))
            //         && ((telephone == "" || telephone == null) ? true : u.TELEPHONE.Contains(telephone))
            //         && (userType == -1 ? true : u.USERTYPE == userType)
            //         && (belongDeptId == -1 ? true : u.BELONGDEPTID == belongDeptId) && u.ISDELETED == 0
            //         select new UserInfo
            //         {
            //             Id = u.ID,
            //             Name = u.NAME,
            //             SexName = s.BMVALUE,
            //             Age = u.AGE,
            //             Telephone = u.TELEPHONE,
            //             CertificateNum = u.CERTIFICATENUM,
            //             Address = u.ADDRESS,
            //             BelongDeptId = u.BELONGDEPTID,
            //             BelongDeptName = d.BMVALUE,
            //             BelongNetId = u.BELONGNETID,
            //             BelongNetName = nnn.NAME,
            //             UserType = u.USERTYPE,
            //             UserTypeName = t.BMVALUE,
            //             VirtualTrumpet = u.VIRTUALTRUMPET
            //         });
            //totalCount = q.Count();
            //return q.OrderByDescending(p => p.UserType).ThenByDescending(p => p.BelongDeptId).ThenByDescending(p => p.BelongNetId).ThenBy(p => p.Name).Skip((start - 1) * limit).Take(limit).ToList();
        }

        /// <summary>
        /// 获取所有人员
        /// </summary>
        /// <returns></returns>
        public static List<CFG_USERINFO> GetAll()
        {
            StringBuilder pSBQueryText = new StringBuilder();
            pSBQueryText.Append("SELECT ID,NAME,SEX,AGE,TELEPHONE,CERTIFICATETYPE,CERTIFICATENUM,DIRECTION,BELONGDEPTID,BELONGNETID,ADDRESS,REMARK,ISDELETED,USERTYPE,VIRTUALTRUMPET,USERNAME,USERPWD,LOGINTYPE FROM CFG_USERINFO WHERE ISDELETED=0");
            var data = OracleOperateBLL.FillDataTable(pSBQueryText.ToString());
            List<CFG_USERINFO> userInfoList = new List<CFG_USERINFO>();
            foreach (DataRow dr in data.Rows)
            {
                CFG_USERINFO d = new CFG_USERINFO();
                d.ID = Convert.ToInt32(dr["ID"]);
                d.NAME = Convert.ToString(dr["NAME"]);
                d.SEX = Convert.ToInt32(dr["SEX"]);
                d.AGE = Convert.ToString(dr["AGE"]);
                d.TELEPHONE = Convert.ToString(dr["TELEPHONE"]);
                d.CERTIFICATETYPE = Convert.ToInt32(dr["CERTIFICATETYPE"]);
                d.CERTIFICATENUM = Convert.ToString(dr["CERTIFICATENUM"]);
                d.DIRECTION = Convert.ToString(dr["DIRECTION"]);
                d.BELONGDEPTID = Convert.ToInt32(dr["BELONGDEPTID"]);
                d.BELONGNETID = Convert.ToInt32(dr["BELONGNETID"]);
                d.ADDRESS = Convert.ToString(dr["ADDRESS"]);
                d.REMARK = Convert.ToString(dr["REMARK"]);
                d.ISDELETED = Convert.ToInt32(dr["ISDELETED"]);
                d.USERTYPE = Convert.ToInt32(dr["USERTYPE"]);
                d.VIRTUALTRUMPET = Convert.ToString(dr["VIRTUALTRUMPET"]);
                d.USERNAME = Convert.ToString(dr["USERNAME"]);
                d.USERPWD = Convert.ToString(dr["USERPWD"]);
                d.LOGINTYPE = Convert.ToInt32(dr["LOGINTYPE"]);
                userInfoList.Add(d);
            }

            return userInfoList;

            //DbBase<CFG_USERINFO> db = new DbBase<CFG_USERINFO>();
            //return db.GetAll(p => p.ISDELETED == 0, "NAME");
        }

        /// <summary>
        /// 登录系统
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="userPwd">密码</param>
        /// <returns></returns>
        public static int Login(string userName, string userPwd, out UserInfo userInfo)
        {
            userInfo = null;
            int pResult = -1;  //-1:不存在该用户  0：密码不正确  1：登录成功
            StringBuilder pSBQueryText = new StringBuilder();
            pSBQueryText.Append("SELECT ID,USERNAME,USERPWD,NAME,LOGINTYPE FROM CFG_USERINFO WHERE USERNAME='" + userName + "' AND ISDELETED=0");

            try
            {
                var data = OracleOperateBLL.FillDataTable(pSBQueryText.ToString());
                if (data.Rows.Count == 0)
                {
                    pResult = -1;
                }
                else if (data.Rows[0]["USERPWD"].ToString() == userPwd)
                {
                    //看是不是平台用户
                    if (Convert.ToInt32(data.Rows[0]["LOGINTYPE"]) == 39)
                    {
                        pResult = 1;
                        userInfo = new UserInfo();
                        userInfo.Id = Convert.ToInt32(data.Rows[0]["ID"]);
                        userInfo.UserName = data.Rows[0]["USERNAME"].ToString();
                        userInfo.UserPwd = data.Rows[0]["USERPWD"].ToString();
                        userInfo.Name = data.Rows[0]["NAME"].ToString();
                    }
                    else
                    {
                        pResult = -2;
                    }
                }
                else
                {
                    pResult = 0;
                }
            }
            catch (Exception error)
            {

            }

            return pResult;
        }

        /// <summary>
        /// 获取人员列表
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, Dictionary<string, string>> GetUserList()
        {
            Dictionary<string, Dictionary<string, string>> m_dic = new Dictionary<string, Dictionary<string, string>>();
            Dictionary<string, bool> m_userDic = new Dictionary<string, bool>();
            StringBuilder pSBQueryText = new StringBuilder();
            pSBQueryText.Append("SELECT A.TELEPHONE,A.ID,A.NAME,A.CERTIFICATENUM,B.BMVALUE FROM CFG_USERINFO A LEFT JOIN SYS_BM_CODE B ON A.USERTYPE=B.BMKEY WHERE A.ISDELETED = 0 ORDER BY USERTYPE, NAME");
            try
            {
                var data = OracleOperateBLL.FillDataTable(pSBQueryText.ToString());
                foreach (DataRow dr in data.Rows)
                {
                    var userName = dr["NAME"].ToString();
                    if (m_userDic.ContainsKey(userName) == false)
                    {
                        m_userDic.Add(userName, false);
                    }
                    else
                    {
                        m_userDic[userName] = true;
                    }

                }
                foreach (DataRow dr in data.Rows)
                {
                    var userType = dr["BMVALUE"].ToString();
                    var userId = dr["ID"].ToString();
                    var userTelephone = dr["TELEPHONE"].ToString();
                    var userName = dr["NAME"].ToString();
                    var userCertificateNum = dr["CERTIFICATENUM"].ToString();
                    if (string.IsNullOrWhiteSpace(userType))
                    {
                        userType = "未分类";
                    }

                    //看是不是已经存在用户类型
                    if (m_dic.ContainsKey(userType))
                    {
                        var value = m_dic[userType];
                        if (value.ContainsKey(userId + ";" + userTelephone) == false)
                        {
                            //看是不是同名
                            if (m_userDic.ContainsKey(userName))
                            {
                                var isSame = m_userDic[userName];
                                if (isSame)
                                {
                                    value.Add(userId + ";" + userTelephone, userName + "(" + userCertificateNum + ")");
                                }
                                else
                                {
                                    value.Add(userId + ";" + userTelephone, userName);
                                }
                            }
                            else
                            {
                                value.Add(userId + ";" + userTelephone, userName);
                            }
                        }
                    }
                    else
                    {
                        m_dic.Add(userType, new Dictionary<string, string>());

                        var value = m_dic[userType];
                        if (value.ContainsKey(userId + ";" + userTelephone) == false)
                        {
                            //看是不是同名
                            if (m_userDic.ContainsKey(userName))
                            {
                                var isSame = m_userDic[userName];
                                if (isSame)
                                {
                                    value.Add(userId + ";" + userTelephone, userName + "(" + userCertificateNum + ")");
                                }
                                else
                                {
                                    value.Add(userId + ";" + userTelephone, userName);
                                }
                            }
                            else
                            {
                                value.Add(userId + ";" + userTelephone, userName);
                            }
                        }
                    }
                }
            }
            catch
            {

            }

            return m_dic;
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        public static bool UpdateUserPwd(int userId, string password)
        {
            bool success = true;
            StringBuilder pSBQueryText = new StringBuilder();
            pSBQueryText.Append("UPDATE CFG_USERINFO SET USERPWD='" + password + "' WHERE ID=" + userId + "");

            try
            {
                OracleOperateBLL.ExecuteSql(pSBQueryText.ToString());
            }
            catch
            {
                success = false;
            }

            return success;
        }
    }
}
