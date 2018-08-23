using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSIM.Entity
{
    /// <summary>
    /// 网格信息
    /// </summary>
    public class NetInfo
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 人口信息
        /// </summary>
        public string PopulationInfo { get; set; }

        /// <summary>
        /// 房屋信息
        /// </summary>
        public string HouseInfo { get; set; }

        /// <summary>
        /// 单位门店信息
        /// </summary>
        public string UnitStoreInfo { get; set; }

        /// <summary>
        /// 所属区域
        /// </summary>
        public string BelongArea { get; set; }

        /// <summary>
        /// 所属部门
        /// </summary>
        public int BelongDeptId { get; set; }
        public string BelongDeptName { get; set; }

        /// <summary>
        /// 网格颜色
        /// </summary>
        public string NetColor { get; set; }

        /// <summary>
        /// 经纬度信息
        /// </summary>
        public string LonAndLat { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public int IsDeleted { get; set; }
    }
}
