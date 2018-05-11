using FirebaseNet.Helpers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace FirebaseNet.Contracts
{
    public class FirebaseRequest
    {
        /// Suffix to be added in each resource URI
        private const string JSON_SUFFIX = ".json";   

        /// Initializes a new instance of the <see cref="FirebaseRequest"/> class  
        /// <param name="method">HTTP Method</param>  
        /// <param name="uri">URI of resource</param> 
        /// <param name="jsonString">JSON string</param>  
        public FirebaseRequest(HttpMethod method, string uri, string jsonString = null)
        {
            this.Method = method;
            this.JSON = jsonString;
            if (uri.Replace("/", string.Empty).EndsWith("firebaseio.com"))
            {
                this.Uri = uri + '/' + JSON_SUFFIX;
            }
            else
            {
                this.Uri = uri + JSON_SUFFIX;
            }

        }
       
        /// Gets or sets HTTP Method       
        private HttpMethod Method { get; set; }

 
        /// Gets or sets JSON string      
        private string JSON { get; set; }

 
        /// Gets or sets URI
        private string Uri { get; set; }

        ///Execute a HTTP Request
        public FirebaseResponse Execute()
        {
            Uri requestUri;
            if (UtilityHelper.ValidateURI(this.Uri))
            {
                requestUri = new Uri(this.Uri);
            }
            else
            {
                return new FirebaseResponse(false, "Invalid Url Request");
            }
            string json =null;
            if(this.JSON != null)
            {
                if(!UtilityHelper.TryParseJSON(this.JSON, out json))
                {
                    return new FirebaseResponse(false, string.Format("Invalid JSON : {0}", json));
                }
            }

            var response = UtilityHelper.RequestHelper(this.Method, requestUri, json);
            response.Wait();
            var result = response.Result;

            var firebaseResponse = new FirebaseResponse()
            {
                HttpResponse = result,
                ErrorMessage = result.StatusCode.ToString() + " : " + result.ReasonPhrase,
                Success = response.Result.IsSuccessStatusCode
            };

            if (this.Method.Equals(HttpMethod.Get))
            {
                var content = result.Content.ReadAsStringAsync();
                content.Wait();
                firebaseResponse.JSONContent = content.Result;
            }

            return firebaseResponse;
        }
    }
}
