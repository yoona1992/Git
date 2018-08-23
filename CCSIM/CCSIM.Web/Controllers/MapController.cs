using FineUIMvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CCSIM.Entity;

namespace CCSIM.Web.Controllers
{
    public class MapController : BaseController
    {
        // GET: Map
        public ActionResult Index()
        {
            return View();
        }

        //位置1
        private static string[] m_lonAndLat1 = new string[] {
               "30.7163547580,120.4222765242",
               "30.7161702740,120.4227485930",
               "30.7160411350,120.4233708655",
               "30.7158750988,120.4240575110",
               "30.7157459594,120.4248085295",
               "30.7163178612,120.4250445639",
               "30.7171111387,120.4251733099",
               "30.7178121691,120.4251303946",
               "30.7180150980,120.4246154105",
               "30.7181257864,120.4241647994",
               "30.7182549226,120.4235854422",
               "30.7184947465,120.4230704581",
               "30.7187714657,120.4221692358",
               "30.7182733706,120.4219761168",
               "30.7177752729,120.4218044554",
               "30.7171885415,120.4215677171",
               "30.7166719898,120.4213531404",
               "30.7164838965,120.4218259131",
               "30.7163916547,120.4223408972",
               "30.7161702740,120.4230919157"
        };
        //位置2
        private static string[] m_lonAndLat2 = new string[]
        {
            "30.7134619284,120.4147870927",
            "30.7134434795,120.4149372964",
            "30.7133327858,120.4154308229",
            "30.7132774389,120.4159887224",
            "30.7135726220,120.4160316377",
            "30.7139231507,120.4161603838",
            "30.7142367806,120.4162247568",
            "30.7145135120,120.4162891298",
            "30.7146242044,120.4159887224",
            "30.7146795505,120.4156024843",
            "30.7146979992,120.4151304155",
            "30.7149009347,120.4144652277",
            "30.7150116266,120.4138858705",
            "30.7145873069,120.4138214975",
            "30.7140707414,120.4137571245",
            "30.7135910709,120.4136283784",
            "30.7134434795,120.4140360742",
            "30.7133143368,120.4145725160",
            "30.7135726220,120.4147656351"
        };
        //位置3
        private static string[] m_lonAndLat3 = new string[]
        {
            "30.7067955517,120.4310671088",
            "30.7068131149,120.4324616108",
            "30.7066286125,120.4336632405",
            "30.7067762144,120.4345644627",
            "30.7066286125,120.4359806691",
            "30.7065179109,120.4376543675",
            "30.7064810103,120.4391993199",
            "30.7065179109,120.4407442723",
            "30.7076249213,120.4410017643",
            "30.7088795178,120.4411305104",
            "30.7095806087,120.4391134892",
            "30.7098758034,120.4381264363",
            "30.7103185937,120.4360664998",
            "30.7112434838,120.4316961023",
            "30.7117794507,120.4281988417",
            "30.7123329269,120.4249372755",
            "30.7108200846,120.4245081221",
            "30.7086036553,120.4242864844",
            "30.7072752560,120.4245010611",
            "30.7074966572,120.4267326590",
            "30.7045810000,120.4244870000",
            "30.7071276549,120.4292217490",
            "30.7068500153,120.4322899495"
        };

        private static int m_Index1 = 0;
        private static int m_Index2 = 0;
        private static int m_Index3 = 0;

        public ActionResult GetPersonList()
        {
            List<PersonLocationInfo> infoList = new List<PersonLocationInfo>();
            if (m_Index1 <= m_lonAndLat1.Length - 2)
            {
                m_Index1++;
            }
            else
            {
                m_Index1 = 1;
            }

            if (m_Index2 <= m_lonAndLat2.Length - 2)
            {
                m_Index2++;
            }
            else
            {
                m_Index2 = 1;
            }

            PersonLocationInfo info = new PersonLocationInfo();
            info.Name = "张三";
            var lonAndLat = m_lonAndLat1[m_Index1 - 1];
            info.Lon = Convert.ToDecimal(lonAndLat.Split(',')[1]);
            info.Lat= Convert.ToDecimal(lonAndLat.Split(',')[0]);

            infoList.Add(info);

            PersonLocationInfo info1 = new PersonLocationInfo();
            info1.Name = "李四";
            lonAndLat = m_lonAndLat2[m_Index2 - 1];
            info1.Lon = Convert.ToDecimal(lonAndLat.Split(',')[1]);
            info1.Lat = Convert.ToDecimal(lonAndLat.Split(',')[0]);

            infoList.Add(info1);

            return new JsonResult
            {
                Data = infoList
            };
        }


        public ActionResult GetCarList()
        {
            List<PersonLocationInfo> infoList = new List<PersonLocationInfo>();
            if (m_Index3 <= m_lonAndLat3.Length - 2)
            {
                m_Index3++;
            }
            else
            {
                m_Index3 = 1;
            }
            
            PersonLocationInfo info = new PersonLocationInfo();
            info.Name = "浙E00000";
            var lonAndLat = m_lonAndLat3[m_Index3 - 1];
            info.Lon = Convert.ToDecimal(lonAndLat.Split(',')[1]);
            info.Lat = Convert.ToDecimal(lonAndLat.Split(',')[0]);

            infoList.Add(info);
            return new JsonResult
            {
                Data = infoList
            };
        }
    }
}