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
    public class Challenge6
    {
        private readonly IFileWrapper _fileWrapper;
        private readonly IFileServer _fileServer;
        private readonly ILedMatrix _ledMatrix;
        private readonly string[] text;

        public Challenge6(IFileWrapper fileWrapper,
            IFileServer fileServer,
            ILedMatrix ledMatrix)
        {
            _fileWrapper = fileWrapper;
            _fileServer = fileServer;
            _ledMatrix = ledMatrix;
            text = _fileWrapper.ReadAllTextInArray(_fileServer.GetFilePath(FileNames.CHALLENGE6));
            foreach (var line in text)
            {
                ledMatrix.ReadInstruction(line,2);
            }
            //Console.WriteLine($"Total leds on: {ledMatrix.TotalLedOn()}");
            Console.WriteLine($"Total brightness: {ledMatrix.TotalBrightness()}");
        }
    }
}
    