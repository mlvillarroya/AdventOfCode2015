using System;
using System.IO;

namespace AdventOfCode2015.Resources
{
    public class FileServer : IFileServer
    {
        private string GetProjectFolderPath()
        {
            var parent = Directory.GetParent(Environment.CurrentDirectory)?.Parent;
            var directoryInfo = parent?.Parent;
            if (directoryInfo != null)
                return directoryInfo?.FullName;
            else throw new Exception("Trying to access to a null Directory");
        }

        public string GetFilePath(string fileName)
        {
            return GetProjectFolderPath() + "/Resources/Files/" + fileName;
        }
    }
}