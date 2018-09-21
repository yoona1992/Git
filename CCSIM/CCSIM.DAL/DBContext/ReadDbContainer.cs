using System.Data.Entity;
using System.Diagnostics;

namespace CCSIM.DAL.DBContext
{
    public class ReadDbContext : BaseReadDbContext
    {
        public ReadDbContext() : base("connReadStr")
        {
            this.Database.Log = s => Debug.Print(s);
            Database.SetInitializer<ReadDbContext>(null);
        }

    }
}