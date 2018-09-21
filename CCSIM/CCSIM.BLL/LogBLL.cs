using CCSIM.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSIM.BLL
{
    public class LogBLL
    {
        public static void AddLog(LogInfo info)
        {
            StringBuilder pSBQueryText = new StringBuilder();
            pSBQueryText.Append("INSERT INTO SYS_LOG(USER_ID,USERNAME,OPERATION,TIME,METHOD,PARAMS,IP,GMT_CREATE) VALUES('"+info.User_Id+"','"+info.UserName+"','"+info.Operation+"','"+0+"','"+info.Method+"','"+info.Params+"','"+info.Ip+"',"+"sysdate)");

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
