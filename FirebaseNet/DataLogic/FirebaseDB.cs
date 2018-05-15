using FirebaseNet.Contracts;
using System;
using System.Net.Http;

namespace FirebaseNet.DataLogic
{
    public class FirebaseDB
    {
        /// Gets or sets Represents current full path of a Firebase Database resource 
        private string RootNode { get; set; }

        /// Initializes a new instance of the <see cref="FirebaseDB"/> class with base url of 
        /// <param name="baseURL">Firebase Database URL</param>  
        public FirebaseDB(string url)
        {
            this.RootNode = url;
        }

        #region Nodes
        /// Adds more node to base URL  
        /// <param name="node">Single node of Firebase DB</param>  
        public FirebaseDB Node (string node)
        {
            if (node.Contains("/"))
            {
                throw new FormatException(" '/' is not a valid Node instrument");
            }
            return new FirebaseDB(this.RootNode + '/' + node);
        }

        /// <param name="nodePath">Nodepath of Firebase DB</param> 
        public FirebaseDB NodePath(string nodePath)
        {
            return new FirebaseDB(this.RootNode + '/' + nodePath);
        }
        #endregion


        public FirebaseResponse Get()
        {
            return new FirebaseRequest(HttpMethod.Get, this.RootNode).Execute();
        }

        public FirebaseResponse Put(string jsonData)
        {
            return new FirebaseRequest(HttpMethod.Put, this.RootNode, jsonData).Execute();
        }

        public FirebaseResponse Post(string jsonData)
        {
            return new FirebaseRequest(HttpMethod.Post, this.RootNode, jsonData).Execute();
        }
        public FirebaseResponse Patch(string jsonData)
        {
            return new FirebaseRequest(new HttpMethod("PATCH"), this.RootNode, jsonData).Execute();
        }

        public FirebaseResponse Delete()
        {
            return new FirebaseRequest(HttpMethod.Delete, this.RootNode).Execute();
        }

        public override string ToString()
        {
            return this.RootNode;
        }
    }
}
