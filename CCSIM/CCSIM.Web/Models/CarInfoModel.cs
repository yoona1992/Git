using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CCSIM.Web.Models
{
    /// <summary>
    /// 车辆信息
    /// </summary>
    public class CarInfoModel
    {
        public DropDownListModel belongDept = new DropDownListModel();
        public DropDownListModel vehicleType = new DropDownListModel();
        public DropDownListModel belongNet = new DropDownListModel();
        public DropDownListModel ownerType = new DropDownListModel();
        public Entity.CarInfo carInfo = new Entity.CarInfo();
    }
}