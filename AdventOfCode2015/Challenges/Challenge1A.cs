using System;
using System.IO;
using AdventOfCode2015.Services;
using AdventOfCode2015.Resources;

namespace AdventOfCode2015.Challenges
{
    public class Challenge1A
    {
        private readonly IFileWrapper _fileWrapper;
        private readonly IFileServer _fileServer;

        public Challenge1A(IFileWrapper fileWrapper,
                           IFileServer fileServer)
        {
            _fileWrapper = fileWrapper;
            _fileServer = fileServer;
            // Read entire text file content in one string  
            var text = _fileWrapper.ReadAllTextInOneString(_fileServer.GetFilePath(FileNames.CHALLENGE1A));  
            Console.WriteLine(text);  
        }
        
    }
}