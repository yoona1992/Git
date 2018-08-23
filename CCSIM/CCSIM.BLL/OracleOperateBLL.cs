using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSIM.BLL
{
    public class OracleOperateBLL
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        private static string ORACLE_CONNECTION_STRING = "user id=LSGAADMIN;password=lsga110;data source=122.225.122.106/ORCL";

        /// <summary>
        /// 获得数量
        /// </summary>
        /// <param name="queryCountText"></param>
        /// <returns></returns>
        public static int GetQueryCount(string queryCountText)
        {
            int pCount = 0;
            OracleConnection pConn = new OracleConnection(ORACLE_CONNECTION_STRING);
            pConn.Open();
            OracleCommand pComm = new OracleCommand(queryCountText, pConn);
            OracleDataReader pReader = pComm.ExecuteReader();
            while (pReader.Read())
            {
                pCount = Convert.ToInt32(pReader[0]);
                break;
            }
            pReader.Close();
            pReader.Dispose();
            pComm.Dispose();
            pConn.Close();
            pConn.Dispose();
            return pCount;
        }

        /// <summary>
        /// 填充dataset
        /// </summary>
        /// <param name="queryText"></param>
        /// <returns></returns>
        public static DataTable FillDataTable(string queryText)
        {
            DataSet pDataSet = new DataSet();
            OracleConnection pOleDbConnection = new OracleConnection(ORACLE_CONNECTION_STRING);
            OracleCommand pOleDbCommand = new OracleCommand(queryText, pOleDbConnection);
            OracleDataAdapter pOleDbDataAdapter = new OracleDataAdapter();
            pOleDbDataAdapter.SelectCommand = pOleDbCommand;
            pOleDbDataAdapter.Fill(pDataSet);
            pOleDbDataAdapter.Dispose();
            pOleDbCommand.Dispose();
            pOleDbConnection.Close();
            pOleDbConnection.Dispose();
            DataTable pTable = pDataSet.Tables[0];
            pDataSet.Dispose();
            return pTable;
        }
    }
}
