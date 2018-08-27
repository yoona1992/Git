using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSIM.DAL.Model
{
    /// <summary>
    /// 通知
    /// </summary>
    [Table("NOTIFICATION")]
    public class NOTIFICATION
    {
        /// <summary>
        /// 主键id
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [MaxLength(2000)]
        public string TITLE { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public int TYPE { get; set; }

        /// <summary>
        /// 正文
        /// </summary>
        [MaxLength(2000)]
        public string CONTENT { get; set; }

        /// <summary>
        /// 是否已读
        /// </summary>
        public int IS_READ { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CREATE_DATE { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [MaxLength(50)]
        public string PHONE { get; set; }

        /// <summary>
        /// 阅读时间
        /// </summary>
        public DateTime READ_DATE { get; set; }
    }
}
