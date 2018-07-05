using FirebaseNet.DataLogic;
using Newtonsoft.Json;
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
            user.Email = "Nehemiah.Mackenzie@mail.com";
            //user.Email = "Freedom.Khanyile@mail.com";
            user.Status = "Active";
            user.Role = "Developer";

            #region data to send
            //var data = @"
            //            {                          
            //                'U1': {                                
            //             	'Email': 'Freedom.Khanyile@mail.com', 
            //             	'Status': 'Active',
            //             	'Role': 'Developer'                            
            //                   }                                           
            //            } 
            //        ";

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
                    var userList = JsonConvert.DeserializeObject<UserJson[]>(getResponse.JSONContent);
                    WriteLine("node number is: {0}"  );
                }
                else
                {
                    WriteLine("No Users Yet");
                    var putResponse1 = firebaseDBTeams.NodePath("U2").Put(data);
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

public class UserJson
{
    [JsonProperty("User")]
    public User User { get; set; }
}

