using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSIM.Entity
{
    public class VehicleInfo
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 车牌号码
        /// </summary>
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
    }
}
