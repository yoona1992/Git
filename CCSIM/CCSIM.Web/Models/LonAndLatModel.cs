using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CCSIM.Web.Models
{
    /// <summary>
    /// 经纬度信息
    /// </summary>
    public class LonAndLatModel
    {
        /// <summary>
        /// 经纬度
        /// </summary>
        public string lonAndLat { get; set; }

        /// <summary>
        /// 操作类型（1：新增 2：修改）
        /// </summary>
        public int operatorType { get; set; }

        public int id { get; set; }
    }
}