using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSIM.Entity
{
    /// <summary>
    /// GPS实时数据
    /// </summary>
    public class GpsRealData
    {

        public string ObjectId { get; set; }

        public string ObjectName { get; set; }

        public string Lon { get; set; }

        public string Lat { get; set; }

        public string BelongNetId { get; set; }

        public string Address { get; set; }

        public string Type { get; set; }

        public bool IsOffline { get; set; }
    }
}
