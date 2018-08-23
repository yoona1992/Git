using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSIM.DAL.Model
{
    /// <summary>
    /// 网格信息
    /// </summary>
    public class CFG_NETINFO
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string NAME { get; set; }

        /// <summary>
        /// 人口信息
        /// </summary>
        [MaxLength(200)]
        public string POPULATIONINFO { get; set; }

        /// <summary>
        /// 房屋信息
        /// </summary>
        [MaxLength(200)]
        public string HOUSEINFO { get; set; }

        /// <summary>
        /// 单位门店信息
        /// </summary>
        [MaxLength(200)]
        public string UNITSTOREINFO { get; set; }

        /// <summary>
        /// 所属区域
        /// </summary>
        [MaxLength(200)]
        public string BELONGAREA { get; set; }

        /// <summary>
        /// 所属部门
        /// </summary>
        [Required]
        public int BELONGDEPTID { get; set; }

        /// <summary>
        /// 网格颜色
        /// </summary>
        [MaxLength(7)]
        public string NETCOLOR { get; set; }

        /// <summary>
        /// 经纬度信息
        /// </summary>
        [Required]
        public string LONANDLAT { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(2000)]
        public string REMARK { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [Required]
        public int ISDELETED { get; set; }
    }
}
