using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSIM.Entity
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int Sex { get; set; }
        public string SexName { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public string Age { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// 证件类型
        /// </summary>
        public int CertificateType { get; set; }

        /// <summary>
        /// 证件号码
        /// </summary>
        public string CertificateNum { get; set; }

        /// <summary>
        /// 去向
        /// </summary>
        public string Direction { get; set; }

        /// <summary>
        /// 家庭地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 所属部门
        /// </summary>
        public int BelongDeptId { get; set; }
        public string BelongDeptName { get; set; }

        /// <summary>
        /// 所属网格
        /// </summary>
        public int? BelongNetId { get; set; }
        public string BelongNetName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public int Isdeleted { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public int UserType { get; set; }
        public string UserTypeName { get; set; }

        /// <summary>
        /// 虚拟短号
        /// </summary>
        public string VirtualTrumpet { get; set; }

        public string UserName { get; set; }

        public string UserPwd { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public int? LoginType { get; set; }
    }
}
