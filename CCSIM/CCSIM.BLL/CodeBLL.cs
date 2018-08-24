using CCSIM.DAL.Base;
using CCSIM.DAL.Model;
using CCSIM.Extension;
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
        /// 获取编码列表
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static List<SYS_BM_CODE> GetCodeListByParentCode(string code)
        {
            List<SYS_BM_CODE> codeList = new List<SYS_BM_CODE>();

            DbBase<SYS_BM_CODE> db = new DbBase<SYS_BM_CODE>();
            var d = db.FirstOrDefault(p => p.BMCODE == code);
            if (d != null)
            {
                codeList = db.GetAll(p => p.BMPARENTKEY == d.BMKEY&&p.ISDELETED==0,"ORDERNUM");
            }

            return codeList;
        }

    }
}
