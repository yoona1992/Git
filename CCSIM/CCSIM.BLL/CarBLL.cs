using CCSIM.DAL;
using CCSIM.DAL.Base;
using CCSIM.DAL.DBContext;
using CCSIM.DAL.Model;
using CCSIM.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSIM.BLL
{
    /// <summary>
    /// 车辆类
    /// </summary>
    public class CarBLL
    {
        //是否读写分离(可以配置在配置文件中)
        private static readonly bool IsReadWriteSeparation = true;

        #region EF上下文对象(主库)

        protected static DbContext MasterDb => _masterDb.Value;
        private static readonly Lazy<DbContext> _masterDb = new Lazy<DbContext>(() => new DbContextFactory().GetWriteDbContext());

        #endregion EF上下文对象(主库)

        #region EF上下文对象(从库)

        protected static DbContext SlaveDb => IsReadWriteSeparation ? _slaveDb.Value : _masterDb.Value;
        private static readonly Lazy<DbContext> _slaveDb = new Lazy<DbContext>(() => new DbContextFactory().GetReadDbContext());

        #endregion EF上下文对象(从库)

        /// <summary>
        /// 添加车辆信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static bool Add(CFG_CARINFO info)
        {
            DbBase<CFG_CARINFO> db = new DbBase<CFG_CARINFO>();
            db.Insert(info);
            if (db.SaveChanges() >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 修改车辆信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static bool Update(CFG_CARINFO info)
        {
            DbBase<CFG_CARINFO> db = new DbBase<CFG_CARINFO>();
            db.Update(info);
            if (db.SaveChanges() >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除车辆信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool Delete(int id)
        {
            DbBase<CFG_CARINFO> db = new DbBase<CFG_CARINFO>();
            var info = Get(id);
            info.ISDELETED = 1;
            db.Update(info);
            if (db.SaveChanges() >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool Delete(int[] ids)
        {
            DbBase<CFG_CARINFO> db = new DbBase<CFG_CARINFO>();
            List<CFG_CARINFO> infoList = new List<CFG_CARINFO>();
            foreach (var id in ids)
            {
                var info = Get(id);
                info.ISDELETED = 1;
                infoList.Add(info);
            }
            db.UpdateAll(infoList);
            if (db.SaveChanges() >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取车辆信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static CFG_CARINFO Get(int id)
        {
            DbBase<CFG_CARINFO> db = new DbBase<CFG_CARINFO>();
            return db.FirstOrDefault(p => p.ID == id);
        }
        public static CarInfo GetByVehId(int vehId)
        {
            var q = (from c in SlaveDb.Set<CFG_CARINFO>()
                     join v in SlaveDb.Set<CFG_VEHICLEINFO>() on c.CLDWZDSBH equals v.CLDWZDSBH
                     where v.ID == vehId
                     select new CarInfo
                     {
                         Id = c.ID,
                         VehicleNo = c.VEHICLENO,
                         VehicleType = c.VEHICLETYPE,
                         VehicleBrand = c.VEHICLEBRAND,
                         BelongDeptId = c.BELONGDEPTID,
                         BelongNetId = c.BELONGNETID,
                         Owner = c.OWNER,
                         OwnerType = c.OWNERTYPE,
                         Cldwzdsbh = c.CLDWZDSBH,
                         Remark = c.REMARK,
                         Wlwkhm = c.WLWKHM
                     });
            return q.FirstOrDefault();
        }

        /// <summary>
        /// 获取车辆信息列表
        /// </summary>
        /// <param name="vehicleNo"></param>
        /// <param name="ownerType"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static List<CarInfo> GetList(string vehicleNo, int ownerType, int start, int limit, out int totalCount)
        {
            var q = (from c in SlaveDb.Set<CFG_CARINFO>()
                     join n in SlaveDb.Set<CFG_NETINFO>() on c.BELONGNETID equals n.ID
                     join v in SlaveDb.Set<SYS_BM_CODE>() on c.VEHICLETYPE equals v.BMKEY
                     join d in SlaveDb.Set<SYS_BM_CODE>() on c.BELONGDEPTID equals d.BMKEY
                     join o in SlaveDb.Set<SYS_BM_CODE>() on c.OWNERTYPE equals o.BMKEY into oo
                     from ooo in oo.DefaultIfEmpty()
                     where ((vehicleNo == "" || vehicleNo == null) ? true : c.VEHICLENO.Contains(vehicleNo))
                     && ((ownerType == -1) ? true : c.OWNERTYPE == ownerType) && c.ISDELETED == 0
                     select new CarInfo
                     {
                         Id = c.ID,
                         VehicleNo = c.VEHICLENO,
                         VehicleType=c.VEHICLETYPE,
                         VehicleTypeName = v.BMVALUE,
                         VehicleBrand = c.VEHICLEBRAND,
                         BelongDeptId = c.BELONGDEPTID,
                         BelongDeptName = d.BMVALUE,
                         BelongNetName = n.NAME,
                         Owner = c.OWNER,
                         OwnerType = c.OWNERTYPE,
                         OwnerTypeName = ooo.BMVALUE,
                         Cldwzdsbh = c.CLDWZDSBH,
                         Wlwkhm = c.WLWKHM
                     });
            totalCount = q.Count();
            return q.OrderByDescending(p => p.OwnerType).ThenByDescending(p => p.BelongDeptId).ThenBy(p => p.VehicleNo).Skip((start - 1) * limit).Take(limit).ToList();
        }

    }
}
