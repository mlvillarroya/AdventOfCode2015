using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2015.Services;
using AdventOfCode2015.Resources;
using Combinatorics.Collections;


namespace AdventOfCode2015.Challenges
{
    public class Challenge14b
    {
        private readonly IFileWrapper _fileWrapper;
        private readonly IFileServer _fileServer;
        private readonly int _maxTime = 2503;

        public Challenge14b(IFileWrapper fileWrapper,
            IFileServer fileServer)
        {
            _fileWrapper = fileWrapper;
            _fileServer = fileServer;
            var text = _fileWrapper.ReadAllTextInArray(_fileServer.GetFilePath(FileNames.CHALLENGE14));
            var info = text.Select(l => l.Split(" ")).ToList().Select(l => (l[0], l[3],l[6],l[13])).ToList();
            var hallOfFame = new Dictionary<string,int>();
            InitializeHallOfFame(hallOfFame,info);
            for (int i = 1; i <= _maxTime; i++)
            {
                var winners = CalculateWinners(info, i);
                foreach (var winner in winners)
                {
                    hallOfFame[winner]++;
                }
            }
            Console.WriteLine(hallOfFame.Max(r=>r.Value));
        }

        private List<string> CalculateWinners(List<(string, string, string, string)> info, int time)
        {
            var positions = new Dictionary<string,int>();
            foreach (var line in info)
            {
                positions[line.Item1] = CalculatePosition(line.Item2, line.Item3, line.Item4,time);
            }

            var max = positions.Max(w => w.Value);
            return positions.Where(w => w.Value == max).Select(w => w.Key).ToList();
        }

        private int CalculatePosition(string speed, string timeMoving, string timeResting, int time)
        {
            var cycles = time / (int.Parse(timeMoving) + int.Parse(timeResting));
            var remaining = time % (int.Parse(timeMoving) + int.Parse(timeResting));
            if (remaining > int.Parse(timeMoving))
                return (cycles + 1) * int.Parse(speed) * int.Parse(timeMoving);
            else return (cycles * int.Parse(timeMoving) + remaining) * int.Parse(speed);
        }

        private void InitializeHallOfFame(Dictionary<string, int> hallOfFame, List<(string, string, string, string)> info)
        {
            foreach (var line in info)
            {
                hallOfFame.Add(line.Item1,0);
            }
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
