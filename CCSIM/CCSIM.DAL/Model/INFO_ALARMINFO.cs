using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSIM.DAL.Model
{
    /// <summary>
    /// 报警信息
    /// </summary>
    public class INFO_ALARMINFO
    {
        /// <summary>
        /// 主键id
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 报警信息
        /// </summary>
        [MaxLength(255)]
        [Required]
        public string ALARMINFO { get; set; }
        
        /// <summary>
        /// 报警时间
        /// </summary>
        [Required]
        public DateTime ALARMTIME { get; set; }

        /// <summary>
        /// 报警地点
        /// </summary>
        [MaxLength(255)]
        public string ALARMADDRESS { get; set; }
        
        /// <summary>
        /// 报警类型
        /// </summary>
        [Required]
        public int ALARMTYPE { get; set; }

        /// <summary>
        /// 报警对象名称
        /// </summary>
        [MaxLength(50)]
        [Required]
        public string ALARMOBJECTNAME { get; set; }
    }
}
