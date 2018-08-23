using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSIM.DAL.Model
{
    /// <summary>
    /// 编码
    /// </summary>
    public class SYS_BM_CODE
    {
        /// <summary>
        /// 编码KEY
        /// </summary>
        [Key]
        public int BMKEY { get; set; }

        /// <summary>
        /// 编码值
        /// </summary>
        [MaxLength(255)]
        [Required]
        public string BMVALUE { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [MaxLength(255)]
        [Required]
        public string BMCODE { get; set; }

        /// <summary>
        /// 父编码KEY
        /// </summary>
        public int? BMPARENTKEY { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(255)]
        public string REMARK { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [Required]
        public int ISDELETED { get; set; }
    }
}
