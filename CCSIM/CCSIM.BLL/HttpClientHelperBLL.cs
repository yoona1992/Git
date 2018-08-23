using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CCSIM.BLL
{
    public class HttpClientHelperBLL
    {
        /// <summary>
        /// GET请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        public static T GetResponse<T>(string url) where T : class, new()
        {
            string returnValue = string.Empty;
            HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(url));
            webReq.Method = "GET";
            webReq.ContentType = "application/json";
            using (HttpWebResponse response = (HttpWebResponse)webReq.GetResponse())
            {
                using (StreamReader streamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    returnValue = streamReader.ReadToEnd();
                    T result = default(T);
                    result = JsonConvert.DeserializeObject<T>(returnValue);
                    return result;
                }
            }
        }
    }
}
