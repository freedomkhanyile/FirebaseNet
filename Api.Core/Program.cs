using FirebaseNet.DataLogic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace Api.Core
{
    class Program
    {
        public static void Main()
        {
            //Instantiating with base URL
            var baseUrl = "https://accessrequestsapi.firebaseio.com/";
            FirebaseDB firebaseDB = new FirebaseDB(baseUrl);

            // Referring to Node with name "Teams"
            FirebaseDB firebaseDBTeams = firebaseDB.Node("Users");
            var user = new User();
            user.Email = "Jane.Doe@mail.com";
            //user.Email = "Freedom.Khanyile@mail.com";
            user.Status = "Active";
            user.Role = "Manager";

            #region data to send            
            string data = JsonConvert.SerializeObject(user);

            #endregion

            //Get Response
            WriteLine("Get Request");
            var getResponse = firebaseDBTeams.Get();
            WriteLine(getResponse.Success);
            if (getResponse.Success)
            {
                if (getResponse.JSONContent != "null")
                {
                    WriteLine(getResponse.JSONContent);

                    dynamic tempData = JsonConvert.DeserializeObject<dynamic>(getResponse.JSONContent);
                    var userList = ((IDictionary<string, JToken>)tempData)
                            .Select(d => JsonConvert.DeserializeObject<User>(d.Value.ToString())).ToList();
                    var number = userList.Count + 1;
                    var nodeNumber = "U" + number.ToString();

                    var putResponse = firebaseDBTeams.NodePath(nodeNumber).Put(data);
                    WriteLine(putResponse.Success);

                }
                else
                {
                    WriteLine("No Users Yet");
                    var putResponse1 = firebaseDBTeams.NodePath("U1").Put(data);
                    WriteLine(putResponse1.Success);
                }
                            
            }
            WriteLine();


            //Put method :
            //WriteLine("PUT request");
            //var putResponse = firebaseDBTeams.NodePath("U1").Put(data);
            //WriteLine(putResponse.Success);
            //WriteLine();

            //Post Method
            //WriteLine("POST Request");
            //var postResponse = firebaseDBTeams.Post(data);
            //WriteLine(postResponse.Success);
            //WriteLine();


            //Still needs work will do ASAP
            //WriteLine("PATCH Request");
            //var patchResponse = firebaseDBTeams
            //    // Use of NodePath to refer path lnager than a single Node  
            //    .NodePath("U1")
            //    .Patch("{\"Email\":\"Freedom.Khanyile@mail.com\"}");
            //WriteLine(patchResponse.Success);
            //WriteLine();

            WriteLine(firebaseDBTeams.ToString());
            ReadLine();
        }
    }
}
public class User
{
    [JsonProperty("Email")]
    public string Email { get; set; }
    [JsonProperty("Status")]
    public string Status { get; set; }
    [JsonProperty("Role")]
    public string Role { get; set; }
}



