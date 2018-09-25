using CCSIM.DAL.Base;
using CCSIM.DAL.Model;
using CCSIM.Extension;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSIM.BLL
{
    public class CodeBLL
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        private static string ORACLE_CONNECTION_STRING = "user id=LSGAADMIN;password=lsga110;data source=122.225.122.106/ORCL";

        /// <summary>
        /// 获取编码列表
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static List<SYS_BM_CODE> GetCodeListByParentCode(string code)
        {
            List<SYS_BM_CODE> codeList = new List<SYS_BM_CODE>();
            OracleConnection pConn = null;
            OracleCommand pComm = null;
            OracleDataReader pReader = null;

            pConn = new OracleConnection(ORACLE_CONNECTION_STRING);
            pConn.Open();
            string pCmdText = "SELECT BMKEY,BMVALUE,BMCODE,BMPARENTKEY,REMARK,ISDELETED,ORDERNUM FROM SYS_BM_CODE WHERE ISDELETED=0 AND BMPARENTKEY=(SELECT BMKEY FROM SYS_BM_CODE WHERE BMCODE='" + code + "') ORDER BY ORDERNUM";
            pComm = new OracleCommand(pCmdText, pConn);
            pReader = pComm.ExecuteReader();
            while (pReader.Read())
            {
                SYS_BM_CODE c = new SYS_BM_CODE();
                c.BMKEY = Convert.ToInt32(pReader["BMKEY"]);
                c.BMVALUE = Convert.ToString(pReader["BMVALUE"]);
                c.BMCODE = Convert.ToString(pReader["BMCODE"]);
                c.BMPARENTKEY = Convert.ToInt32(pReader["BMPARENTKEY"]);
                c.REMARK = Convert.ToString(pReader["REMARK"]);
                c.ISDELETED = Convert.ToInt32(pReader["ISDELETED"]);

                codeList.Add(c);
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

            return codeList;

            //List<SYS_BM_CODE> codeList = new List<SYS_BM_CODE>();

            //DbBase<SYS_BM_CODE> db = new DbBase<SYS_BM_CODE>();
            //var d = db.FirstOrDefault(p => p.BMCODE == code);
            //if (d != null)
            //{
            //    codeList = db.GetAll(p => p.BMPARENTKEY == d.BMKEY&&p.ISDELETED==0,"ORDERNUM");
            //}

            //return codeList;
        }

    }
}
