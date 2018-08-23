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
    public class UserModel
    {
        /// <summary>
        /// 主键id
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [MaxLength(50)]
        [Required]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [MaxLength(255)]
        [Required]
        public string UserPassword { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int Gender { get;set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string TelePhone { get; set; }

        /// <summary>
        /// 证件类型
        /// </summary>
        public int CredentialType { get; set; }

        /// <summary>
        /// 去向
        /// </summary>
        public string Direction { get; set; }

        /// <summary>
        /// 证件号码
        /// </summary>
        public string CredentialNum { get; set; }

        /// <summary>
        /// 家庭地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 所属部门
        /// </summary>
        public Guid BelongDept { get; set; }

        /// <summary>
        /// 所属网格
        /// </summary>
        public Guid BelongGrid { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
