using CCSIM.DAL;
using CCSIM.DAL.Base;
using CCSIM.DAL.Model;
using CCSIM.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSIM.BLL
{
    /// <summary>
    /// 车辆类
    /// </summary>
    public class VehicleBLL
    {
        /// <summary>
        /// 添加车辆信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static Guid Add(VehicleModel info)
        {
            DbBase<VehicleModel> db = new DbBase<VehicleModel>();
            db.Insert(info);
            db.SaveChanges();

            return Guid.Empty;
        }

        /// <summary>
        /// 删除车辆信息
        /// </summary>
        /// <param name="plateNo"></param>
        /// <returns></returns>
        public static bool Delete(string plateNo)
        {
            DbBase<VehicleModel> db = new DbBase<VehicleModel>();
            db.Delete(p=>p.PlateNo== plateNo);
            db.SaveChanges();

            return true;
        }

        /// <summary>
        /// 获取车辆信息
        /// </summary>
        /// <param name="plateNo"></param>
        /// <returns></returns>
        public static List<VehicleModel> GetAll()
        {
            DbBase<VehicleModel> db = new DbBase<VehicleModel>();
            var data = db.GetAll("PlateNo");

            return data;
        }
    }
}
