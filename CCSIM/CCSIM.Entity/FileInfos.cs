using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSIM.Entity
{
    /// <summary>
    /// 文件信息
    /// </summary>
    public class FileInfos
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string FileUrl { get; set; }

        /// <summary>
        /// 上传时间
        /// </summary>
        public DateTime UploadTime { get; set; }
        public string UploadTimeStr { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public string FileSize { get; set; }

        /// <summary>
        /// 上传人
        /// </summary>
        public string UploadUser { get; set; }
    }
}
