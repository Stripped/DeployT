﻿using DeployTracker.Database;
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

        public string LoginToServer(ServerPool serverPool)
        {
            switch (serverPool.ConnectionType)
            {
                case "SSH":
                    return SSHConnection(serverPool.Hostname, serverPool.Port, serverPool.UserName, serverPool.Password);
                default:
                    return "Connection unsuccesfull";
            }
        }

        private string SSHConnection(string host,int port,string user,string pass)
        {
            //Set up the SSH connection
            using (var client = new SshClient(host,port, user, pass))
            {
                //Start the connection
                client.Connect();
                var output = client.RunCommand("echo test");
                client.Disconnect();
                return output.Result;
            }
        }

        private void WinRMConnection()
        {

        }

    }
}