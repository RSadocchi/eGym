using System.IO;

namespace eGym.Common.Security.FileProtector
{
    public struct ContentFile
    {
        /// <summary>
        /// Filename widthout extension
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// Directory path
        /// </summary>
        public string FilePath { get; set; }

        public override string ToString() => Path.Combine(FilePath, $"{FileName}.c");
    }
}
