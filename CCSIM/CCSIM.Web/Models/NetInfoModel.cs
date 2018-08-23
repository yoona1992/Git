using CCSIM.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CCSIM.Web.Models
{
    /// <summary>
    /// 网格信息
    /// </summary>
    public class NetInfoModel
    {
        public DropDownListModel dropDownList = new DropDownListModel();
        public Entity.NetInfo netInfo = new Entity.NetInfo();
    }
}