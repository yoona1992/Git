using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CCSIM.DAL
{
    public class MysqlOperator
    {
        private DbContext _dbContext;
        public DbContext CurrentContext
        {
            get
            {
                if (_dbContext == null)
                {
                    _dbContext = new MySqlContext();
                }
                return _dbContext;
            }
        }

        /// <summary>
        /// 执行Sql查询
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="strSql"></param>
        /// <param name="paramObjects"></param>
        /// <returns></returns>
        public List<TEntity> SqlQuery<TEntity>(string strSql, params Object[] paramObjects) where TEntity : class
        {
            if (paramObjects == null)
            {
                paramObjects = new object[0];
            }
            return this.CurrentContext.Database.SqlQuery<TEntity>(strSql, paramObjects).ToList();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IQueryable<TEntity> Search<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return CurrentContext.Set<TEntity>().Where(predicate);
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> FindAll<TEntity>() where TEntity : class
        {
            return CurrentContext.Set<TEntity>();
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isSave"></param>
        public void Insert<TEntity>(TEntity entity, bool isSave = true) where TEntity : class
        {
            CurrentContext.Set<TEntity>().Add(entity);
            if (isSave)
            {
                CurrentContext.SaveChanges();
            }
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="entitys"></param>
        /// <param name="isSave"></param>
        public void Insert<TEntity>(List<TEntity> entitys, bool isSave = true) where TEntity : class
        {
            foreach (var entity in entitys)
            {
                CurrentContext.Set<TEntity>().Add(entity);
            }
            if (isSave)
            {
                CurrentContext.SaveChanges();
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isSave"></param>
        //public void Update<TEntity>(TEntity entity, bool isSave = true) where TEntity : class
        //{
        //    var local = FindLocal(CurrentContext, entity);
        //    if (local == null)
        //    {
        //        throw new Exception("要更新的实体不存在");
        //    }
        //    ObjectMapper.CopyProperties(entity, local);
        //    if (isSave)
        //    {
        //        CurrentContext.SaveChanges();
        //    }
        //}

        ///// <summary>
        ///// 批量更新
        ///// </summary>
        ///// <param name="entities"></param>
        ///// <param name="isSave"></param>
        //public void Update<TEntity>(List<TEntity> entities, bool isSave = true) where TEntity : class
        //{
        //    foreach (var entity in entities)
        //    {
        //        var local = FindLocal(CurrentContext, entity);
        //        if (local == null)
        //        {
        //            throw new Exception("要更新的实体不存在");
        //        }
        //        ObjectMapper.CopyProperties(entity, local);
        //    }
        //    if (isSave)
        //    {
        //        CurrentContext.SaveChanges();
        //    }
        //}

        //private TEntity FindLocal<TEntity>(DbContext currentContext, TEntity entity) where TEntity : class
        //{
        //    throw new NotImplementedException();
        //}

        ///// <summary>
        ///// 删除
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <param name="isSave"></param>
        //public void Delete<TEntity>(TEntity entity, bool isSave = true) where TEntity : class
        //{
        //    var local = FindLocal(CurrentContext, entity);
        //    if (local == null)
        //    {
        //        throw new Exception("要删除的实体不存在");
        //    }
        //    CurrentContext.Entry<TEntity>(local).State = EntityState.Deleted;
        //    if (isSave)
        //    {
        //        CurrentContext.SaveChanges();
        //    }
        //}

    }
}
