using CCSIM.DAL.Base;
using CCSIM.DAL.DBContext;
using CCSIM.DAL.Model;
using CCSIM.Entity;
using CCSIM.Extension;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CCSIM.BLL
{
    public class NetManageBLL
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
        /// 添加网格信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static bool Add(CFG_NETINFO info)
        {
            DbBase<CFG_NETINFO> db = new DbBase<CFG_NETINFO>();
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
        /// 修改网格信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static bool Update(CFG_NETINFO info)
        {
            DbBase<CFG_NETINFO> db = new DbBase<CFG_NETINFO>();
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
        /// 删除网格信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool Delete(int id)
        {
            DbBase<CFG_NETINFO> db = new DbBase<CFG_NETINFO>();
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
            DbBase<CFG_NETINFO> db = new DbBase<CFG_NETINFO>();
            List<CFG_NETINFO> infoList = new List<CFG_NETINFO>();
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
        /// 获取网格信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static CFG_NETINFO Get(int id)
        {
            DbBase<CFG_NETINFO> db = new DbBase<CFG_NETINFO>();
            return db.FirstOrDefault(p => p.ID == id);
        }

        /// <summary>
        /// 获取网格信息列表
        /// </summary>
        /// <param name="name"></param>
        /// <param name="belongDeptId"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static List<NetInfo> GetList(string name, int belongDeptId, int start, int limit, out int totalCount)
        {
            var q = (from n in SlaveDb.Set<CFG_NETINFO>()
                     join c in SlaveDb.Set<SYS_BM_CODE>() on n.BELONGDEPTID equals c.BMKEY into cc
                     where ((name == "" || name == null) ? true : n.NAME.Contains(name))
                     && (belongDeptId == -1 ? true : n.BELONGDEPTID == belongDeptId) && n.ISDELETED == 0
                     from ccc in cc.DefaultIfEmpty()
                     select new NetInfo
                     {
                         Id = n.ID,
                         BelongArea = n.BELONGAREA,
                         BelongDeptName = ccc.BMVALUE,
                         HouseInfo = n.HOUSEINFO,
                         LonAndLat = n.LONANDLAT,
                         PopulationInfo = n.POPULATIONINFO,
                         Remark = n.REMARK,
                         UnitStoreInfo = n.UNITSTOREINFO,
                         NetColor=n.NETCOLOR,
                         Name = n.NAME
                     });
            totalCount = q.Count();
            return q.OrderByDescending(p => p.Name).Skip((start - 1) * limit).Take(limit).ToList();
        }
        public static List<NetInfo> GetAll()
        {
            var q = (from n in SlaveDb.Set<CFG_NETINFO>()
                     join c in SlaveDb.Set<SYS_BM_CODE>() on n.BELONGDEPTID equals c.BMKEY into cc
                     where n.ISDELETED == 0
                     from ccc in cc.DefaultIfEmpty()
                     select new NetInfo
                     {
                         Id = n.ID,
                         BelongArea = n.BELONGAREA,
                         BelongDeptName = ccc.BMVALUE,
                         HouseInfo = n.HOUSEINFO,
                         LonAndLat = n.LONANDLAT,
                         PopulationInfo = n.POPULATIONINFO,
                         Remark = n.REMARK,
                         UnitStoreInfo = n.UNITSTOREINFO,
                         NetColor=n.NETCOLOR,
                         Name = n.NAME
                     });
            return q.ToList();
        }
    }
}
