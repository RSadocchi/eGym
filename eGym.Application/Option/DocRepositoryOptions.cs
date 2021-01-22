using System;
using System.Collections.Generic;
using System.Text;

namespace eGym.Application.Option
{
    public class DocRepositoryOptions
    {
        private string _tempPath;
        public string TempPath
        {
            get => _tempPath?.Replace("{{CurrentDirectory}}", Environment.CurrentDirectory);
            set => _tempPath = value;
        }

        private string _destinationPath;
        public string DestinationPath
        {
            get => _destinationPath?.Replace("{{CurrentDirectory}}", Environment.CurrentDirectory);
            set => _destinationPath = value;
        }
    }
}
