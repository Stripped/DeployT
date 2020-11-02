﻿namespace DeployTracker.Models
{
    public class ServerPool
    {
        public string Hostname { get; set; }
        public string ConnectionType { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
