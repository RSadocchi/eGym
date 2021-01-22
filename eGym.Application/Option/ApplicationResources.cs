using System;
using System.Collections.Generic;
using System.Text;

namespace eGym.Application.Option
{
    public class ApplicationResources
    {
        private string _comuniItalianiJson;
        public string ComuniItalianiJson
        {
            get => _comuniItalianiJson?.Replace("{{CurrentDirectory}}", Environment.CurrentDirectory);
            set => _comuniItalianiJson = value;
        }

        private string _comuniItalianiCsv;
        public string ComuniItalianiCsv
        {
            get => _comuniItalianiCsv?.Replace("{{CurrentDirectory}}", Environment.CurrentDirectory);
            set => _comuniItalianiCsv = value;
        }

        private string _fatturePath;
        public string FatturePath
        {
            get => _fatturePath?.Replace("{{CurrentDirectory}}", Environment.CurrentDirectory);
            set => _fatturePath = value;
        }
    }
}
