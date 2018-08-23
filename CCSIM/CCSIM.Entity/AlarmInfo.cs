using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSIM.Entity
{
    /// <summary>
    /// 报警信息
    /// </summary>
    public class AlarmInfo
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 报警信息
        /// </summary>
        public string Info { get; set; }

        /// <summary>
        /// 报警时间
        /// </summary>
        public DateTime AlarmTime { get; set; }
        public string AlarmTimeStr { get; set; }

        /// <summary>
        /// 报警地点
        /// </summary>
        public string AlarmAddress { get; set; }

        /// <summary>
        /// 报警类型
        /// </summary>
        public int AlarmType { get; set; }
        public string AlarmTypeName { get; set; }

        /// <summary>
        /// 报警对象名称
        /// </summary>
        public string AlarmObjectName { get; set; }
    }
}
