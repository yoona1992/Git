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
    /// 文件管理
    /// </summary>
    public class FileBLL
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
        /// 添加文件
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static bool Add(INFO_FILEINFO info)
        {
            DbBase<INFO_FILEINFO> db = new DbBase<INFO_FILEINFO>();
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
        /// 获取文件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static INFO_FILEINFO Get(int id)
        {
            DbBase<INFO_FILEINFO> db = new DbBase<INFO_FILEINFO>();
            return db.FirstOrDefault(p => p.ID == id);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static bool Delete(int[] ids)
        {
            DbBase<INFO_FILEINFO> db = new DbBase<INFO_FILEINFO>();
            List<INFO_FILEINFO> infoList = new List<INFO_FILEINFO>();
            foreach (var id in ids)
            {
                db.Delete(p => p.ID == id);
            }
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
        /// 获取文件列表
        /// </summary>
        /// <param name="name"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static List<FileInfos> GetList(string name, int start, int limit, out int totalCount)
        {
            var q = (from f in SlaveDb.Set<INFO_FILEINFO>()
                     join u in SlaveDb.Set<CFG_USERINFO>() on f.UPLOADUSER equals u.ID into uu
                     from uuu in uu.DefaultIfEmpty()
                     where ((name == "" || name == null) ? true : f.FILENAME.Contains(name))
                     select new FileInfos
                     {
                         Id = f.ID,
                         FileName = f.FILENAME,
                         FileUrl = f.FILEURL,
                         UploadTime = f.UPLOADTIME,
                         FileSize = f.FILESIZE,
                         UploadUser = uuu.NAME
                     });
            totalCount = q.Count();
            var data = q.OrderByDescending(p => p.UploadTime).Skip((start - 1) * limit).Take(limit).ToList();
            foreach(var d in data)
            {
                d.UploadTimeStr = d.UploadTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
            return data;
        }


    }
}
