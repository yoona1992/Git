using CCSIM.DAL;
using CCSIM.DAL.DBContext;
using FineUIMvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Z.EntityFramework.Extensions;

namespace CCSIM.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            string licenseName = "60;100-KAI";
            string licenseKey = "11ED9159EB11121F52FE8ABA629C07F1";
            LicenseManager.AddLicense(licenseName,licenseKey);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            
            ModelBinders.Binders.Add(typeof(JArray), new JArrayModelBinder());
            ModelBinders.Binders.Add(typeof(JObject), new JObjectModelBinder());

            //Database.SetInitializer<BaseReadDbContext>(null);
            //Database.SetInitializer<ReadDbContext>(null);
            //Database.SetInitializer<WriteDbContext>(null);

            using (var dbcontext = new ReadDbContext())
            {
                var objectContext = ((IObjectContextAdapter)dbcontext).ObjectContext;
                var mappingCollection = (StorageMappingItemCollection)objectContext.MetadataWorkspace.GetItemCollection(DataSpace.CSSpace);
                mappingCollection.GenerateViews(new List<EdmSchemaError>());
            }
            using (var dbcontext = new WriteDbContext())
            {
                var objectContext = ((IObjectContextAdapter)dbcontext).ObjectContext;
                var mappingCollection = (StorageMappingItemCollection)objectContext.MetadataWorkspace.GetItemCollection(DataSpace.CSSpace);
                mappingCollection.GenerateViews(new List<EdmSchemaError>());
            }

        }
    }
}
