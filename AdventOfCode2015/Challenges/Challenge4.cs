using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using AdventOfCode2015.Helpers;
using AdventOfCode2015.Models;
using AdventOfCode2015.Services;
using AdventOfCode2015.Resources;

namespace AdventOfCode2015.Challenges
{
    public class Challenge4
    {
        private readonly IFileWrapper _fileWrapper;
        private readonly IFileServer _fileServer;
        private readonly IPresentListParser _presentListParser;
        private readonly IEncrypting _encrypting;
        private const string PuzzleInput = "yzbqklnj";

        public Challenge4(IFileWrapper fileWrapper,
            IFileServer fileServer,
            IPresentListParser presentListParser,
            IEncrypting encrypting)
        {
            _fileWrapper = fileWrapper;
            _fileServer = fileServer;
            _presentListParser = presentListParser;
            _encrypting = encrypting;

            Console.WriteLine(SearchForFiveZerosHash());
        }

        int SearchForFiveZerosHash()
        {
            var i = 0;
            while (true)
            {
                if (_encrypting.GetMD5Hash(PuzzleInput + i.ToString()).Substring(0,6) == "000000") return i;
                i++;
            }
        }
    }
}
    