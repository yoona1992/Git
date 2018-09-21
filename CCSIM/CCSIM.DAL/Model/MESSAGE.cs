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
    /// 上传文字、照片资料信息
    /// </summary>
    [Table("MESSAGE")]
    public class MESSAGE
    {

        /// <summary>
        /// 主键id
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [MaxLength(255)]
        public string TITLE { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [MaxLength(50)]
        public string PHONE { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [MaxLength(255)]
        public string ADDRESS { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(255)]
        public string REMARKS { get; set; }

        /// <summary>
        /// 上传时间
        /// </summary>
        [Required]
        public DateTime CREATE_DATE { get; set; }

        /// <summary>
        /// 照片地址
        /// </summary>
        [MaxLength(2000)]
        public string PHOTO { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int STATUS { get; set; }

        /// <summary>
        /// 是否已读
        /// </summary>
        public int? ISREAD_PLATFORM { get; set; }
    }
}
