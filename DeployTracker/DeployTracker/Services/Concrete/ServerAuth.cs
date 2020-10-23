using DeployTracker.Database;
using DeployTracker.Models;
using DeployTracker.Services.Contracts;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeployTracker.Services.Concrete
{
    public class ServerAuth:IServerAuth
    {
        

        public ServerAuth()
        {
           
        }

        public void LoginToServer(ServerPool serverPool)
        {
            switch (serverPool.ConnectionType)
            {
                case "SSH":
                    SSHConnection(serverPool.Hostname, serverPool.Port, serverPool.UserName, serverPool.Password);
                    break;
                default:
                    break;
            }
        }

        private void SSHConnection(string host,int port,string user,string pass)
        {
            //Set up the SSH connection
            using (var client = new SshClient(host,port, user, pass))
            {
                //Start the connection
                client.Connect();
                var output = client.RunCommand("echo test");
                client.Disconnect();
                Console.WriteLine(output.Result);
            }
        }

        private void WinRMConnection()
        {

        }

    }
}
