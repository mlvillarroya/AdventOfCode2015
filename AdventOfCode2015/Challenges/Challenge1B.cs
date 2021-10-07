using System;
using System.IO;
using AdventOfCode2015.Services;
using AdventOfCode2015.Resources;

namespace AdventOfCode2015.Challenges
{
    public class Challenge1B
    {
        private readonly IFileWrapper _fileWrapper;
        private readonly IFileServer _fileServer;

        public Challenge1B(IFileWrapper fileWrapper,
                           IFileServer fileServer)
        {
            _fileWrapper = fileWrapper;
            _fileServer = fileServer;
            // Read entire text file content in one string  
            var text = _fileWrapper.ReadAllTextInOneString(_fileServer.GetFilePath(FileNames.CHALLENGE1A));
            Console.WriteLine(CalculateStepsToBasement(text));
        }

        private int CalculateStepsToBasement(string text)
        {
            var floor = 0;
            for (int counter = 0; counter < text.Length; counter++)
            {
                floor = ComputeFloorChar(text[counter], floor);
                if (floor == -1) return counter + 1;
            }
            return 0;
        }

        private int ComputeFloorChar(char c, int floor)
        {
            switch (c)
            {
                case '(':
                    floor++;
                    break;
                case ')':
                    floor--;
                    break;   
                default:
                    break;
            }
            return floor;
        }
    }
}