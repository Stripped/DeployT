using DeployTracker.Models;
using DeployTracker.Services.Contracts;
using Naos.WinRM;
using Renci.SshNet;
using System.Net;
using System.Security;
using System.Threading.Tasks;

namespace DeployTracker.Services.Concrete
{
    public class ServerAuth:IServerAuth
    {
        

        public ServerAuth()
        {
           
        }

        public Task<string> LoginToServerAsync(ServerPool serverPool)
        {
            return serverPool.ConnectionType switch
            {
                "SSH" => Task.FromResult(SshConnection(serverPool.Hostname, serverPool.Port, serverPool.UserName, serverPool.Password)),
                "WinRM" => Task.FromResult(WinRMConnection(serverPool.Hostname,serverPool.UserName,serverPool.Password)),
                _ => Task.FromResult("Connection Unsuccessfull"),
            };
        }

        private string SshConnection(string host,int port,string user,string pass)
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

        private string WinRMConnection(string host, string user, string pass)
        {
            SecureString theSecureString = new NetworkCredential("", "pass").SecurePassword;
            // this is the entrypoint to interact with the system (interfaced for testing).
            var machineManager = new MachineManager(
                host,
                user,
                theSecureString,               
                true);
            // can run random script blocks WITH parameters.
            var script = machineManager.RunScript(
                "{ ls ./ }");
            return script.ToString();
        }

    }
}
