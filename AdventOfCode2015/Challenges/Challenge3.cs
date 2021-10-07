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
        private readonly string text;

        public Challenge3(IFileWrapper fileWrapper,
            IFileServer fileServer,
            IPresentListParser presentListParser)
        {
            _fileWrapper = fileWrapper;
            _fileServer = fileServer;
            _presentListParser = presentListParser;
            text = _fileWrapper.ReadAllTextInOneString(_fileServer.GetFilePath(FileNames.CHALLENGE3));

            CalculateSantaAndRobotTrip();
        }

        public void CalculateSantaTrip()
        {
            var houses = new HashSet<string>();
            var santaPosition = new SantaPosition();
            houses.Add(santaPosition.ToString());
            foreach (var movement in text)
            {
                santaPosition.ChangePosition(movement);
                houses.Add(santaPosition.ToString());
            }

            Console.WriteLine($"String length: {text.Length}");
            Console.WriteLine($"Number of houses visited: {houses.Count}");
        }
        public void CalculateSantaAndRobotTrip()
        {
            var houses = new HashSet<string>();
            var santaPosition = new SantaPosition();
            var robotPosition = new SantaPosition();
            houses.Add(santaPosition.ToString());
            for (int i = 0; i < text.Length; i++)
            {
                if (i % 2 == 0)
                {
                    santaPosition.ChangePosition(text[i]);
                    houses.Add(santaPosition.ToString());
                }
                else
                {
                    robotPosition.ChangePosition(text[i]);
                    houses.Add(robotPosition.ToString());
                }
            }

            Console.WriteLine($"String length: {text.Length}");
            Console.WriteLine($"Number of houses visited: {houses.Count}");
        }

    }
}
    