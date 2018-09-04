using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSIM.DAL.Model
{
    public class CFG_VEHICLEINFO
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public int? ID { get; set; }

        /// <summary>
        /// 车辆定位终端识别号
        /// </summary>
        public string CLDWZDSBH { get; set; }
    }
}
