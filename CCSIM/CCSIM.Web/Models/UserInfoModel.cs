using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CCSIM.Web.Models
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserInfoModel
    {
        public DropDownListModel belongDept = new DropDownListModel();
        public DropDownListModel belongNet = new DropDownListModel();
        public DropDownListModel sex = new DropDownListModel();
        public DropDownListModel certificateType = new DropDownListModel();
        public DropDownListModel userType = new DropDownListModel();
        public Entity.UserInfo userInfo = new Entity.UserInfo();
    }
}