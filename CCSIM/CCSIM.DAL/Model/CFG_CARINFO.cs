using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSIM.DAL.Model
{
    /// <summary>
    /// 车辆信息
    /// </summary>
    public class CFG_CARINFO
    {
        /// <summary>
        /// 主键id
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 车牌号码
        /// </summary>
        [MaxLength(50)]
        [Required]
        public string VEHICLENO { get; set; }

        /// <summary>
        /// 车辆类型
        /// </summary>
        [Required]
        public int VEHICLETYPE { get; set; }

        /// <summary>
        /// 车辆品牌
        /// </summary>
        [MaxLength(100)]
        public string VEHICLEBRAND { get; set; }

        /// <summary>
        /// 所属单位
        /// </summary>
        [Required]
        public int BELONGDEPTID { get; set; }

        /// <summary>
        /// 所属网格
        /// </summary>
        [Required]
        public int BELONGNETID { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(2000)]
        public string REMARK { get; set; }

        /// <summary>
        /// 所有人
        /// </summary>
        [MaxLength(100)]
        public string OWNER { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [Required]
        public int ISDELETED { get; set; }
    }
}
