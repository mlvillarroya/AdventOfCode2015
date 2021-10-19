using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2015.Services;
using AdventOfCode2015.Resources;
using Combinatorics.Collections;


namespace AdventOfCode2015.Challenges
{
    public class Challenge14
    {
        private readonly IFileWrapper _fileWrapper;
        private readonly IFileServer _fileServer;

        public Challenge14(IFileWrapper fileWrapper,
            IFileServer fileServer)
        {
            _fileWrapper = fileWrapper;
            _fileServer = fileServer;
            var text = _fileWrapper.ReadAllTextInArray(_fileServer.GetFilePath(FileNames.CHALLENGE14));
            var info = text.Select(l => l.Split(" ")).ToList().Select(l => (l[0], l[3],l[6],l[13])).ToList();
            var distances = new List<int>();
            foreach (var reindeer in info)
            {
                  distances.Add(ComputeDistance(reindeer,2503));      
            }

            Console.WriteLine($"Longest distance, {distances.Max()}");
        }

        private int ComputeDistance((string, string, string, string) reindeer, int time)
        {
            var cycles = time / (int.Parse(reindeer.Item3) + int.Parse(reindeer.Item4));
            var remaining = time % (int.Parse(reindeer.Item3) + int.Parse(reindeer.Item4));
            if (remaining > int.Parse(reindeer.Item3))
                return (cycles + 1) * int.Parse(reindeer.Item2) * int.Parse(reindeer.Item3);
            else return (cycles * int.Parse(reindeer.Item3) + remaining) * int.Parse(reindeer.Item2);
        }
    }
}
