using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2015.Helpers;
using AdventOfCode2015.Models;
using AdventOfCode2015.Services;
using AdventOfCode2015.Resources;

namespace AdventOfCode2015.Challenges
{
    public class Challenge3
    {
        private readonly IFileWrapper _fileWrapper;
        private readonly IFileServer _fileServer;
        private readonly IPresentListParser _presentListParser;

        public Challenge3(IFileWrapper fileWrapper,
            IFileServer fileServer,
            IPresentListParser presentListParser)
        {
            _fileWrapper = fileWrapper;
            _fileServer = fileServer;
            _presentListParser = presentListParser;
            var text = _fileWrapper.ReadAllTextInOneString(_fileServer.GetFilePath(FileNames.CHALLENGE3));
        }
    }
}