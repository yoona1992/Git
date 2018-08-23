using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSIM.Entity
{
    /// <summary>
    /// 车辆信息
    /// </summary>
    public class CarInfo
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 车牌号码
        /// </summary>
        public string VehicleNo { get; set; }

        /// <summary>
        /// 车辆类型
        /// </summary>
        public int VehicleType { get; set; }
        public string VehicleTypeName { get; set; }

        /// <summary>
        /// 车辆品牌
        /// </summary>
        public string VehicleBrand { get; set; }

        /// <summary>
        /// 所属单位
        /// </summary>
        public int BelongDeptId { get; set; }
        public string BelongDeptName { get; set; }

        /// <summary>
        /// 所属网格
        /// </summary>
        public int BelongNetId { get; set; }
        public string BelongNetName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 所有人
        /// </summary>
        public string Owner { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public int Isdeleted { get; set; }
    }
}
