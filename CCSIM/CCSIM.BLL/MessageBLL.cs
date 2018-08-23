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
    /// 资料信息
    /// </summary>
    public class MessageBLL
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
       /// 获取消息列表
       /// </summary>
       /// <param name="telephone"></param>
       /// <param name="title"></param>
       /// <param name="startTime"></param>
       /// <param name="endTime"></param>
       /// <param name="start"></param>
       /// <param name="limit"></param>
       /// <param name="totalCount"></param>
       /// <returns></returns>
        public static List<MessageInfo> GetList(string telephone, string title,DateTime startTime,DateTime endTime, int start, int limit, out int totalCount)
        {
            var q = (from m in SlaveDb.Set<MESSAGE>()
                     where ((title == "" || title == null) ? true : m.TITLE.Contains(title))
                     && ((telephone == "" || telephone == null) ? true : m.PHONE==telephone)
                     &&m.CREATE_DATE>=startTime&&m.CREATE_DATE<=endTime
                     select new MessageInfo
                     {
                         Id = m.ID,
                         Title=m.TITLE,
                         Phone=m.PHONE,
                         Address=m.ADDRESS,
                         Create_Date=m.CREATE_DATE
                     });
            totalCount = q.Count();
            var data= q.OrderByDescending(p => p.Create_Date).ThenByDescending(p=>p.Title).Skip((start - 1) * limit).Take(limit).ToList();
            foreach(var d in data)
            {
                d.UploadDate = d.Create_Date.ToString("yyyy-MM-dd HH:mm:ss");
            }
            return data;
        }

    }
}
