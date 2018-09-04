using CCSIM.DAL.Model;
using System.Data.Entity;
using System.Diagnostics;
using System.Reflection;

namespace CCSIM.DAL.DBContext
{
    public class WriteDbContext : DbContext
    {
        public WriteDbContext() :
            base("name=connWriteStr")
        {
            this.Database.Log = s => Debug.Print(s);
            //Database.SetInitializer<WriteDbContext>(null);
            //Configuration.AutoDetectChangesEnabled = true;
        }

        public DbSet<CFG_CARINFO> VehicleInfos { get; set; }
        public DbSet<CFG_USERINFO> UserInfos { get; set; }
        public DbSet<CFG_NETINFO> NetInfos { get; set; }
        public DbSet<SYS_BM_CODE> BMCodes { get; set; }
        public DbSet<GPS_DATA> GpsDatas { get; set; }
        public DbSet<MESSAGE> MessageInfos { get; set; }
        public DbSet<INFO_ALARMINFO> AlarmInfos { get; set; }
        public DbSet<NOTIFICATION> Notifications { get; set; }
        public DbSet<CFG_VEHICLEINFO> VehicleInfos_Two { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("LSGAADMIN");
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}