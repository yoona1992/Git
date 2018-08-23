using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSIM.Entity
{
    /// <summary>
    /// 树节点
    /// </summary>
    public class TreeNode
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 对象名称
        /// </summary>
        public string objectName { get; set; }

        /// <summary>
        /// 是否打开
        /// </summary>
        public bool open { get; set; }

        /// <summary>
        /// 是否打开
        /// </summary>
        public bool @checked { get; set; }

        /// <summary>
        /// 子节点
        /// </summary>
        public List<TreeNode> children = new List<TreeNode>();
    }
}
