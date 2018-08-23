using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSIM.DAL.Model
{
    /// <summary>
    /// 人员定位数据
    /// </summary>
    public class GPS_DATA
    {
        /// <summary>
        /// 主键id
        /// </summary>
        [Key]
        public Int32 ID { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [MaxLength(50)]
        public string PHONE { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        [MaxLength(50)]
        public string LON { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        [MaxLength(50)]
        public string LAT { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CREATE_TIME { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [MaxLength(255)]
        public string ADDRESS { get; set; }
    }
}
