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
    /// 文件管理
    /// </summary>
    public class FileBLL
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        private static string ORACLE_CONNECTION_STRING = "user id=LSGAADMIN;password=lsga110;data source=122.225.122.106/ORCL";

        /// <summary>
        /// 添加文件
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static bool Add(INFO_FILEINFO info)
        {
            bool isSuccess = true;
            StringBuilder pInsertText = new StringBuilder();
            pInsertText.Append("INSERT INTO INFO_FILEINFO(FILENAME,FILEURL,UPLOADTIME,FILESIZE,UPLOADUSER) VALUES('");
            pInsertText.Append(info.FILENAME + "','" + info.FILEURL + "',TO_DATE('" + info.UPLOADTIME.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss'),'" + info.FILESIZE + "'," + info.UPLOADUSER + ")");

            try
            {
                OracleOperateBLL.ExecuteSql(pInsertText.ToString());
            }
            catch
            {
                isSuccess = false;
            }

            return isSuccess;
            //DbBase<INFO_FILEINFO> db = new DbBase<INFO_FILEINFO>();
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
        /// 获取文件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static INFO_FILEINFO Get(int id)
        {
            INFO_FILEINFO pFileInfo = new INFO_FILEINFO();
            OracleConnection pConn = null;
            OracleCommand pComm = null;
            OracleDataReader pReader = null;

            pConn = new OracleConnection(ORACLE_CONNECTION_STRING);
            pConn.Open();
            string pCmdText = "SELECT ID,FILENAME,FILEURL,TO_CHAR(UPLOADTIME,'yyyy-mm-dd hh24:mi:ss') AS UPLOADTIME,FILESIZE,UPLOADUSER FROM INFO_FILEINFO WHERE ID = " + id + "";
            pComm = new OracleCommand(pCmdText, pConn);
            pReader = pComm.ExecuteReader();
            while (pReader.Read())
            {
                pFileInfo.ID = Convert.ToInt32(pReader["ID"]);
                pFileInfo.FILENAME = Convert.ToString(pReader["FILENAME"]);
                pFileInfo.FILEURL = Convert.ToString(pReader["FILEURL"]);
                pFileInfo.UPLOADTIME = Convert.ToDateTime(pReader["UPLOADTIME"]);
                pFileInfo.FILESIZE = Convert.ToString(pReader["FILESIZE"]);
                pFileInfo.UPLOADUSER = Convert.ToInt32(pReader["UPLOADUSER"]);
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
            return pFileInfo;
            //DbBase<INFO_FILEINFO> db = new DbBase<INFO_FILEINFO>();
            //return db.FirstOrDefault(p => p.ID == id);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static bool Delete(int[] ids)
        {
            bool isSuccess = true;

            foreach (var id in ids)
            {
                StringBuilder pUpdateText = new StringBuilder();
                pUpdateText.Append("DELETE FROM INFO_FILEINFO WHERE ID=" + id);

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

            //DbBase<INFO_FILEINFO> db = new DbBase<INFO_FILEINFO>();
            //List<INFO_FILEINFO> infoList = new List<INFO_FILEINFO>();
            //foreach (var id in ids)
            //{
            //    db.Delete(p => p.ID == id);
            //}
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
        /// 获取文件列表
        /// </summary>
        /// <param name="name"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static List<FileInfos> GetList(string name, int start, int limit, out int totalCount)
        {
            StringBuilder pSBQueryCount = new StringBuilder();
            pSBQueryCount.Append("SELECT COUNT(*) FROM INFO_FILEINFO ");
            if (string.IsNullOrWhiteSpace(name) == false)
            {
                pSBQueryCount.Append("WHERE FILENAME like '%" + name + "%'");
            }
            totalCount = OracleOperateBLL.GetQueryCount(pSBQueryCount.ToString());

            int pStartNum = limit * (start - 1) + 1;
            int pEndNum = limit * start;

            StringBuilder pSBQueryText = new StringBuilder();
            pSBQueryText.Append("SELECT ID,FILENAME,FILEURL,UPLOADTIME,FILESIZE,UPLOADUSER,NAME FROM (SELECT C.*, rownum r FROM(");
            pSBQueryText.Append("SELECT A.ID,A.FILENAME,A.FILEURL,TO_CHAR(A.UPLOADTIME,'yyyy-mm-dd hh24:mi:ss') AS UPLOADTIME,A.FILESIZE,A.UPLOADUSER,B.NAME FROM INFO_FILEINFO A ");
            pSBQueryText.Append("LEFT JOIN CFG_USERINFO B ON A.UPLOADUSER=B.ID ");
            if (string.IsNullOrWhiteSpace(name) == false)
            {
                pSBQueryText.Append("WHERE A.FILENAME like '%" + name + "%' ");
            }
            pSBQueryText.Append(" ORDER BY A.UPLOADTIME DESC) C ");
            pSBQueryText.Append("WHERE rownum<=" + pEndNum + ") D WHERE r>=" + pStartNum);
            var data = OracleOperateBLL.FillDataTable(pSBQueryText.ToString());
            List<FileInfos> fileInfosList = new List<FileInfos>();
            foreach (DataRow dr in data.Rows)
            {
                FileInfos d = new FileInfos();
                d.Id = Convert.ToInt32(dr["ID"].ToString());
                d.FileName = dr["FILENAME"].ToString();
                d.FileUrl = dr["FILEURL"].ToString();
                d.UploadTime = DateTime.Parse(dr["UPLOADTIME"].ToString());
                d.UploadTimeStr = dr["UPLOADTIME"].ToString();
                d.FileSize = dr["FILESIZE"].ToString();
                d.UploadUser = dr["NAME"].ToString();

                fileInfosList.Add(d);
            }

            return fileInfosList;

            //var q = (from f in SlaveDb.Set<INFO_FILEINFO>()
            //         join u in SlaveDb.Set<CFG_USERINFO>() on f.UPLOADUSER equals u.ID into uu
            //         from uuu in uu.DefaultIfEmpty()
            //         where ((name == "" || name == null) ? true : f.FILENAME.Contains(name))
            //         select new FileInfos
            //         {
            //             Id = f.ID,
            //             FileName = f.FILENAME,
            //             FileUrl = f.FILEURL,
            //             UploadTime = f.UPLOADTIME,
            //             FileSize = f.FILESIZE,
            //             UploadUser = uuu.NAME
            //         });
            //totalCount = q.Count();
            //var data = q.OrderByDescending(p => p.UploadTime).Skip((start - 1) * limit).Take(limit).ToList();
            //foreach(var d in data)
            //{
            //    d.UploadTimeStr = d.UploadTime.ToString("yyyy-MM-dd HH:mm:ss");
            //}
            //return data;
        }


    }
}
