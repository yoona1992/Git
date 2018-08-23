using CCSIM.DAL.DBContext;
using System.Data.Entity;

namespace CCSIM.DAL.Strategy
{
    /// <summary>
    /// 单一策略
    /// </summary>
    public class SingleStrategy : IReadDbStrategy
    {
        public DbContext GetDbContext()
        {
            return new ReadDbContext();
        }
    }
}