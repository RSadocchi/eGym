using System;
using System.Collections.Generic;
using System.Text;

namespace eGym.Application.Option
{
    public class ApplicationOptions
    {
        public string Contractor { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string SupportEmail { get; set; }
        public string SupportPhone { get; set; }
        public string Version { get; set; }
        public bool VersionVisible { get; set; }
        public string Logo { get; set; }
        public string SecretKey { get; set; }

        public string Company_Name { get; set; }
        public string Company_URI { get; set; }
        public string Company_Logo { get; set; }

        public string NotifyTo { get; set; }

        public string URI { get; set; }
        public string API_URI { get; set; }
        public string DefaultCulture { get; set; }

        public int AutoUserID { get; set; }
        public int AutoAnagID { get; set; }

        private string _appDataPath;
        public string AppDataPath
        {
            get => _appDataPath?.Replace("{{CurrentDirectory}}", Environment.CurrentDirectory);
            set => _appDataPath = value;
        }

        private string _appLogsPath;
        public string AppLogsPath
        {
            get => _appLogsPath?.Replace("{{CurrentDirectory}}", Environment.CurrentDirectory);
            set => _appLogsPath = value;
        }
    }
}
