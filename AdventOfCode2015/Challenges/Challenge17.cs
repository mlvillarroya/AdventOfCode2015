using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using AdventOfCode2015.Services;
using AdventOfCode2015.Resources;
using Combinatorics.Collections;


namespace AdventOfCode2015.Challenges
{
    public class Challenge17
    {
        private readonly IFileWrapper _fileWrapper;
        private readonly IFileServer _fileServer;

        public Challenge17(IFileWrapper fileWrapper,
            IFileServer fileServer)
        {
            _fileWrapper = fileWrapper;
            _fileServer = fileServer;
            var text = _fileWrapper.ReadAllTextInArray(_fileServer.GetFilePath(FileNames.CHALLENGE17));
            var bottles = text.Select(s => int.Parse(s)).ToList();
            /*
             /////  PART A  /////
            var totalCombinations = 0;
            for (int i = 1; i <= bottles.Count; i++)
            {
                var partialCombinations = new Combinations<int>(bottles, i).Where(c => c.Sum(ce => ce) == 150);
                totalCombinations += partialCombinations.Count();
            }
            */
            for (int i = 1; i <= bottles.Count; i++)
            {
                var partialCombinations = new Combinations<int>(bottles, i).Where(c => c.Sum(ce => ce) == 150);
                if (partialCombinations.Count() > 0)
                {
                    Console.WriteLine($"Minimum number of bottles: {i}\nNumber of possibilities: {partialCombinations.Count()}");
                    break;
                }
            }

        }
    }
}
