using System.IO;

namespace AdventOfCode2015.Services
{
    public class FileWrapper : IFileWrapper
    {
        public string ReadAllTextInOneString(string path)
        {
            return File.ReadAllText(path);
        }
    }
}