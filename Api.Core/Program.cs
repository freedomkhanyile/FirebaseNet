using FirebaseNet.DataLogic;
using static System.Console;

namespace Api.Core
{
    class Program
    {
        public static void Main()
        {
            //Instantiating with base URL
            var baseUrl = "https://accessrequestsapi.firebaseio.com/-LCE8eBb9e53-opXVkS6/Team-Awesome";
            FirebaseDB firebaseDB = new FirebaseDB(baseUrl);

            // Referring to Node with name "Teams"
            FirebaseDB firebaseDBTeams = firebaseDB.Node("Users");

            #region data to send
            //var data = @"
            //            {  
            //                'Team - Awesome': {  
            //                   'Users': {
            //                   'U1': {
            //                       'UserName': 'FakeD', 
            //                   	'CloneUser': 'Master', 
            //                   	'Email': 'Fake.Doe@mail.com', 
            //                   	'FirstName': 'Fake',
            //                   	'Surname': 'Doe',
            //                   	'System': 'Automotive'
            //                         } 
                                            
            //                      }
            //                    }
            //            } 
            //        ";

            #endregion

            WriteLine("Get Request");
            var response = firebaseDBTeams.Get();
            WriteLine(response.Success);
            if (response.Success)
            {
                WriteLine(response.JSONContent);                
            }
            WriteLine();

            WriteLine(firebaseDBTeams.ToString());
            ReadLine();
        }
    }
}
