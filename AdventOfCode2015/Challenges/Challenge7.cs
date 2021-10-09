using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using AdventOfCode2015.Helpers;
using AdventOfCode2015.Models;
using AdventOfCode2015.Services;
using AdventOfCode2015.Resources;

namespace AdventOfCode2015.Challenges
{
    public class Challenge7
    {
        private readonly IFileWrapper _fileWrapper;
        private readonly IFileServer _fileServer;
        private readonly IWireList _wireList;
        private readonly string[] text;

        public Challenge7(IFileWrapper fileWrapper,
            IFileServer fileServer,
            IWireList wireList)
        {
            _fileWrapper = fileWrapper;
            _fileServer = fileServer;
            _wireList = wireList;
            text = _fileWrapper.ReadAllTextInArray(_fileServer.GetFilePath(FileNames.CHALLENGE7));
        }
    }
}
    