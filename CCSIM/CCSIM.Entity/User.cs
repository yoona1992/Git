﻿using System;
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
        public Guid Id { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string UserPassword { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int Gender { get; set; }

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