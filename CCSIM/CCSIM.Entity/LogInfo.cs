using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSIM.Entity
{
    /// <summary>
    /// 用户操作日志
    /// </summary>
    public class LogInfo
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int User_Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 操作内容
        /// </summary>
        public string Operation { get; set; }

        /// <summary>
        /// 次数
        /// </summary>

        public int Time { get; set; }

        /// <summary>
        /// 方法
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        public string Params { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string Gmt_Create { get; set; }
    }
}
