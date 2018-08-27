using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSIM.Entity
{
    /// <summary>
    /// 通知
    /// </summary>
    public class NotificationInfo
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 正文
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 是否已读
        /// </summary>
        public int Is_Read { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Create_Date { get; set; }

        /// <summary>
        /// 通知时间
        /// </summary>
        public string NotificateTime { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 阅读时间
        /// </summary>
        public DateTime Read_Date { get; set; }
    }
}
