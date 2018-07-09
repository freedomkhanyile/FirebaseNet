using FirebaseNet.Contracts;
using FirebaseNet.DataLogic;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirebaseNet.Interfaces
{
    public interface IFirebase
    {
        FirebaseResponse Get();
        FirebaseResponse Put(string jsonData);

        FirebaseResponse Post(string jsonData);

        FirebaseResponse Patch(string jsonData);

        FirebaseResponse Delete();

        FirebaseDB Node(string node);
    }
}
