using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCSIM.Entity;

namespace CCSIM.BLL
{
    public class BaiduGeocoderBLL
    {
        //百度地图Api  Ak
        public const string BaiduAk = "A6bc8cb4539b007f51e8aa5ef37ed431";

        /// <summary>
        /// 经纬度  逆地理编码 Url  需要Format 0.ak  1.经度  2.纬度
        /// </summary>
        private const string BaiduGeoCoding_ApiUrl = "http://api.map.baidu.com/geocoder/v2/?ak={0}&location={1},{2}&output=json&pois=0";
        
        /// <summary>
        /// /// <summary>
        /// 经纬度  逆地理编码 Url  需要Format 0.经度  1.纬度 
        /// </summary>
        /// </summary>
        public static string Baidu_GeoCoding_ApiUrl
        {
            get
            {
                return string.Format(BaiduGeoCoding_ApiUrl, BaiduAk, "{0}", "{1}");
            }
        }

        /// <summary>
        /// 根据经纬度  获取 地址信息
        /// </summary>
        /// <param name="lat">经度</param>
        /// <param name="lng">纬度</param>
        /// <returns></returns>
        public static BaiDuGeoCoding GeoCoder(string lat, string lng)
        {
            string url = string.Format(Baidu_GeoCoding_ApiUrl, lat, lng);
            var model = HttpClientHelperBLL.GetResponse<BaiDuGeoCoding>(url);
            return model;
        }
    }
}
