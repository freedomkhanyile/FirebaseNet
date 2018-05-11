using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FirebaseNet.Helpers
{
    public class UtilityHelper
    {
        /// User Agent Header in HTTP Request
        private const string USER_AGENT = "firebase-net/1.0";

        /// Validates a URI 
        public static bool ValidateURI(string url)
        {
            Uri localUrl;
            var isValid = true;
            if(Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out localUrl))
            {
                if (
                    !(localUrl.IsAbsoluteUri &&
                      (localUrl.Scheme == "http" || localUrl.Scheme == "https")) ||
                    !localUrl.IsAbsoluteUri)
                {
                    return isValid=false;
                }
            }
            else
            {
                return isValid=false;
            }
            return isValid;
        }
 
        /// Validates JSON string   
        /// <param name="inJSON">JSON to be validatedd</param>  
        /// <param name="output">Valid JSON or Error Message</param>  
        /// <returns>True if valid</returns>  
        public static bool TryParseJSON(string inJSON, out string output)
        {
            try
            {
                JToken parsedJSON = JToken.Parse(inJSON);
                output = parsedJSON.ToString();
                return true;
            }
            catch (Exception ex)
            {
                output = ex.Message;
                return false;
            }
        }
        /// Makes Asynchronus HTTP requests    
        /// <param name="method">HTTP method</param>  
        /// <param name="uri">URI of resource</param>  
        /// <param name="json">JSON string</param>  
        /// <returns>HTTP Responce as Task</returns>  
        public static Task<HttpResponseMessage> RequestHelper(HttpMethod method, Uri uri, string json = null)
        {
            var client = new HttpClient();
            var msg = new HttpRequestMessage(method, uri);
            msg.Headers.Add("user-agent", USER_AGENT);
            if (json != null)
            {
                msg.Content = new StringContent(
                    json,
                    UnicodeEncoding.UTF8,
                    "application/json");
            }

            return client.SendAsync(msg);
        }
    }
}
