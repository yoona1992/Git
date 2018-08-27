using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSIM.Entity
{
    /// <summary>
    /// 通知返回消息
    /// </summary>
    public class NotificationReturnInfo
    {
        /// <summary>
        /// 状态消息
        /// </summary>
        public string stateMsg { get; set; }

        /// <summary>
        /// 状态编码
        /// </summary>
        public string stateCode { get; set; }
    }
}
