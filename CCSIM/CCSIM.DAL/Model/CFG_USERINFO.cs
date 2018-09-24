using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSIM.DAL.Model
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class CFG_USERINFO
    {
        /// <summary>
        /// 主键id
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [MaxLength(50)]
        [Required]
        public string NAME { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [Required]
        public int SEX { get;set; }

        /// <summary>
        /// 年龄
        /// </summary>
        [MaxLength(50)]
        public string AGE { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [MaxLength(20)]
        [Required]
        public string TELEPHONE { get; set; }

        /// <summary>
        /// 证件类型
        /// </summary>
        [Required]
        public int CERTIFICATETYPE { get; set; }

        /// <summary>
        /// 证件号码
        /// </summary>
        [MaxLength(50)]
        [Required]
        public string CERTIFICATENUM { get; set; }

        /// <summary>
        /// 去向
        /// </summary>
        [MaxLength(255)]
        public string DIRECTION { get; set; }


        /// <summary>
        /// 家庭地址
        /// </summary>
        [MaxLength(255)]
        public string ADDRESS { get; set; }

        /// <summary>
        /// 所属部门
        /// </summary>
        [Required]
        public int BELONGDEPTID { get; set; }

        /// <summary>
        /// 所属网格
        /// </summary>
        [Required]
        public int? BELONGNETID { get; set; }

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

        /// <summary>
        /// 用户类型
        /// </summary>
        [Required]
        public int USERTYPE { get; set; }

        /// <summary>
        /// 虚拟短号
        /// </summary>
        [MaxLength(50)]
        public string VIRTUALTRUMPET { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [MaxLength(100)]
        public string USERNAME { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [MaxLength(100)]
        public string USERPWD { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public int? LOGINTYPE { get; set; }
    }
}
