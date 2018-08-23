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
    public class VehicleModel
    {
        /// <summary>
        /// 主键id
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// 车牌号码
        /// </summary>
        [MaxLength(255)]
        [Required]
        [Display(Name ="车牌号码")]
        public string PlateNo { get; set; }

        /// <summary>
        /// 车辆类型
        /// </summary>
        public int VehicleType { get; set; }

        /// <summary>
        /// 车辆品牌
        /// </summary>
        public int VehicleBrand { get; set; }

        /// <summary>
        /// 所属单位
        /// </summary>
        public Guid BelongDept { get; set; }

        /// <summary>
        /// 所属网格
        /// </summary>
        public Guid BelongNet { get; set; }



        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 备注1
        /// </summary>
        public string Remark1 { get; set; }
    }
}
