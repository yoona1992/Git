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
    /// 用户
    /// </summary>
    public class UserBLL
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
        /// 添加用户信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static bool Add(CFG_USERINFO info)
        {
            DbBase<CFG_USERINFO> db = new DbBase<CFG_USERINFO>();
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
        /// 修改用户信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static bool Update(CFG_USERINFO info)
        {
            DbBase<CFG_USERINFO> db = new DbBase<CFG_USERINFO>();
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
        /// 删除用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool Delete(int id)
        {
            DbBase<CFG_USERINFO> db = new DbBase<CFG_USERINFO>();
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
            DbBase<CFG_USERINFO> db = new DbBase<CFG_USERINFO>();
            List<CFG_USERINFO> infoList = new List<CFG_USERINFO>();
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
        /// 获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static CFG_USERINFO Get(int id)
        {
            DbBase<CFG_USERINFO> db = new DbBase<CFG_USERINFO>();
            return db.FirstOrDefault(p => p.ID == id);
        }
        public static CFG_USERINFO Get(string phone)
        {
            DbBase<CFG_USERINFO> db = new DbBase<CFG_USERINFO>();
            return db.FirstOrDefault(p => p.TELEPHONE == phone);
        }

        /// <summary>
        /// 获取用户信息列表
        /// </summary>
        /// <param name="name"></param>
        /// <param name="belongDeptId"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static List<UserInfo> GetList(string name, string telephone, int belongDeptId, int userType, int start, int limit, out int totalCount)
        {
            var q = (from u in SlaveDb.Set<CFG_USERINFO>()
                     join n in SlaveDb.Set<CFG_NETINFO>() on u.BELONGNETID equals n.ID into nn
                     join s in SlaveDb.Set<SYS_BM_CODE>() on u.SEX equals s.BMKEY
                     join d in SlaveDb.Set<SYS_BM_CODE>() on u.BELONGDEPTID equals d.BMKEY
                     join t in SlaveDb.Set<SYS_BM_CODE>() on u.USERTYPE equals t.BMKEY
                     from nnn in nn.DefaultIfEmpty()
                     where ((name == "" || name == null) ? true : u.NAME.Contains(name))
                     && ((telephone == "" || telephone == null) ? true : u.TELEPHONE.Contains(telephone))
                     && (userType == -1 ? true : u.USERTYPE == userType)
                     && (belongDeptId == -1 ? true : u.BELONGDEPTID == belongDeptId) && u.ISDELETED == 0
                     select new UserInfo
                     {
                         Id = u.ID,
                         Name = u.NAME,
                         SexName = s.BMVALUE,
                         Age = u.AGE,
                         Telephone = u.TELEPHONE,
                         CertificateNum = u.CERTIFICATENUM,
                         Address = u.ADDRESS,
                         BelongDeptId = u.BELONGDEPTID,
                         BelongDeptName = d.BMVALUE,
                         BelongNetId = u.BELONGNETID,
                         BelongNetName = nnn.NAME,
                         UserType = u.USERTYPE,
                         UserTypeName = t.BMVALUE,
                         VirtualTrumpet = u.VIRTUALTRUMPET
                     });
            totalCount = q.Count();
            return q.OrderByDescending(p => p.UserType).ThenByDescending(p => p.BelongDeptId).ThenByDescending(p => p.BelongNetId).ThenBy(p => p.Name).Skip((start - 1) * limit).Take(limit).ToList();
        }

        /// <summary>
        /// 获取所有人员
        /// </summary>
        /// <returns></returns>
        public static List<CFG_USERINFO> GetAll()
        {
            DbBase<CFG_USERINFO> db = new DbBase<CFG_USERINFO>();
            return db.GetAll(p => p.ISDELETED == 0, "NAME");
        }

        /// <summary>
        /// 登录系统
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="userPwd">密码</param>
        /// <returns></returns>
        public static int Login(string userName, string userPwd, out UserInfo userInfo)
        {
            userInfo =null;
            int pResult = -1;  //-1:不存在该用户  0：密码不正确  1：登录成功
            StringBuilder pSBQueryText = new StringBuilder();
            pSBQueryText.Append("SELECT USERNAME,USERPWD FROM CFG_USERINFO WHERE USERNAME='" + userName + "'");

            try
            {
                var data = OracleOperateBLL.FillDataTable(pSBQueryText.ToString());
                if (data.Rows.Count == 0)
                {
                    pResult = -1;
                }
                else if (data.Rows[0]["USERPWD"].ToString() == userPwd)
                {
                    pResult = 1;
                    userInfo = new UserInfo();
                    userInfo.UserName = data.Rows[0]["USERNAME"].ToString();
                    userInfo.UserPwd = data.Rows[0]["USERPWD"].ToString();
                }
                else
                {
                    pResult = 0;
                }
            }
            catch(Exception error)
            {

            }

            return pResult;
        }
    }
}
