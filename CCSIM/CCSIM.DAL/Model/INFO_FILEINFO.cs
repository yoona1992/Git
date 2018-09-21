using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSIM.DAL.Model
{
    /// <summary>
    /// 文件信息
    /// </summary>
    public class INFO_FILEINFO
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(255)]
        public string FILENAME { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [MaxLength(255)]
        public string FILEURL { get; set; }

        /// <summary>
        /// 上传时间
        /// </summary>
        public DateTime UPLOADTIME { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        [MaxLength(50)]
        public string FILESIZE { get; set; }
        
        /// <summary>
        /// 上传人
        /// </summary>
        public int UPLOADUSER { get; set; }
    }
}
