using CCSIM.DAL.Model;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace CCSIM.DAL.DBContext
{
    public class BaseReadDbContext : DbContext
    {

        public BaseReadDbContext(string connReadStr) : base(connReadStr)
        {
            Database.SetInitializer<BaseReadDbContext>(null);
            Configuration.AutoDetectChangesEnabled = false;
        }

        public DbSet<CFG_CARINFO> VehicleInfos { get; set; }
        public DbSet<CFG_USERINFO> UserInfos { get; set; }
        public DbSet<CFG_NETINFO> NetInfos { get; set; }
        public DbSet<SYS_BM_CODE> BMCodes { get; set; }
        public DbSet<GPS_DATA> GpsDatas { get; set; }
        public DbSet<MESSAGE> MessageInfos { get; set; }
        public DbSet<INFO_ALARMINFO> AlarmInfos { get; set; }
        public DbSet<NOTIFICATION> Notifications { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("LSGAADMIN");
        }

    }

    /// <summary>
    /// CodeFirst使用
    /// </summary>
    public class MyContextFactory : IDbContextFactory<BaseReadDbContext>
    {
        public BaseReadDbContext Create()
        {
            return new BaseReadDbContext(ConfigurationManager.AppSettings["DbAddress_Oracle"]);
        }
    }
}