using FirebaseNet.DataLogic;
using static System.Console;

namespace Api.Core
{
    class Program
    {
        public static void Main()
        {
            //Instantiating with base URL
            var baseUrl = "https://accessrequestsapi.firebaseio.com/Teams/Team-Awesome";
            FirebaseDB firebaseDB = new FirebaseDB(baseUrl);

            // Referring to Node with name "Teams"
            FirebaseDB firebaseDBTeams = firebaseDB.Node("Users");

            #region data to send
            var data = @"
                        {  
                        
                               'U1': {
                                'UserName': 'FreedomK', 
                               	'CloneUser': 'FakeD', 
                               	'Email': 'Freedom.Khanyile@mail.com', 
                               	'FirstName': 'Freedom',
                               	'Surname': 'Khanyile',
                               	'System': 'Automotive'
                                     } 
                                            
                              
                        } 
                    ";

            #endregion

            //Get Response
            WriteLine("Get Request");
            var getResponse = firebaseDBTeams.Get();
            WriteLine(getResponse.Success);
            if (getResponse.Success)
            {
                WriteLine(getResponse.JSONContent);                
            }
            WriteLine();

          
            //Putt method :
            WriteLine("PUT request");
            var putResponse = firebaseDBTeams.Put(data);
            WriteLine(putResponse.Success);
            WriteLine();

            //Post Method
            //WriteLine("POST Request");
            //var postResponse = firebaseDBTeams.Post(data);
            //WriteLine(postResponse.Success);
            //WriteLine();


            //Still needs work will do ASAP
            WriteLine("PATCH Request");
            var patchResponse = firebaseDBTeams
                // Use of NodePath to refer path lnager than a single Node  
                .NodePath("Team-Awesome/Users/U1")
                .Patch("{\"FreedomK\":\"King.Works@mail.com\"}");
            WriteLine(patchResponse.Success);
            WriteLine();

            WriteLine(firebaseDBTeams.ToString());
            ReadLine();
        }
    }
}
