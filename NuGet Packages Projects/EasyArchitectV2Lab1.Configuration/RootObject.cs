﻿namespace EasyArchitectV2Lab1.Configuration
{
    public class Rootobject
    {
        public AppSettings AppSettings { get; set; }
        public Logging Logging { get; set; }
        public string AllowedHosts { get; set; }
        public ConnectionStrings ConnectionStrings { get; set; }
    }
}
