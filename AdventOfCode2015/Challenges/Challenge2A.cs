using System;
using System.IO;
using System.Linq;
using AdventOfCode2015.Helpers;
using AdventOfCode2015.Models;
using AdventOfCode2015.Services;
using AdventOfCode2015.Resources;

namespace AdventOfCode2015.Challenges
{
    public class Challenge2A
    {
        private readonly IFileWrapper _fileWrapper;
        private readonly IFileServer _fileServer;
        private readonly IPresentListParser _presentListParser;

        public Challenge2A(IFileWrapper fileWrapper,
                           IFileServer fileServer,
                           IPresentListParser presentListParser)
        {
            _fileWrapper = fileWrapper;
            _fileServer = fileServer;
            _presentListParser = presentListParser;
            var text = _fileWrapper.ReadAllTextInArray(_fileServer.GetFilePath(FileNames.CHALLENGE2));
            var presentList = _presentListParser.StringArrayToPresentList(text);
            Console.WriteLine("Total wrapping paper: " + presentList.Sum(present => present.ComputeNeededPaper()).ToString());
            Console.WriteLine("Total ribbon: " + presentList.Sum(p => p.ComputeNeededRibbon()).ToString());
        }
    }
}