using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using AdventOfCode2015.Services;
using AdventOfCode2015.Resources;
using Combinatorics.Collections;


namespace AdventOfCode2015.Challenges
{
    public class Challenge16
    {
        private readonly IFileWrapper _fileWrapper;
        private readonly IFileServer _fileServer;
        private readonly Dictionary<string, int> AuntSue;
        private List<Dictionary<string, int>> AuntsList;

        public Challenge16(IFileWrapper fileWrapper,
            IFileServer fileServer)
        {
            _fileWrapper = fileWrapper;
            _fileServer = fileServer;
            var text = _fileWrapper.ReadAllTextInArray(_fileServer.GetFilePath(FileNames.CHALLENGE16));
            CreateAuntsList(text);
            AuntSue = new Dictionary<string, int>() { { "children", 3 }, { "cats", 7 }, { "samoyeds", 2 }, { "pomeranians", 3 }, { "akitas", 0 }, { "vizslas", 0 }, { "goldfish", 5 }, { "trees", 3 }, { "cars", 2 }, { "perfumes", 1 } };
            var auntFound = false;
            foreach (var aunt     in AuntsList)
            {
                foreach (var property in aunt)
                {
                    auntFound = true;
                    if (!property.Key.Equals("Sue"))
                    {
                        switch (property.Key)
                        {
                            case "cats":
                            case "trees":
                                if (property.Value <= AuntSue[property.Key]) auntFound = false;
                                break;
                            case "pomeranians":
                            case "goldfish":
                                if (property.Value >= AuntSue[property.Key]) auntFound = false;
                                break;
                            default:
                                if (property.Value != AuntSue[property.Key]) auntFound = false;
                                break;
                        }
                    }
                    if (!auntFound) break;
                }
                if (auntFound)
                {
                    Console.WriteLine($"Aunt is {aunt["Sue"]}");
                    break;
                }
            }
        }

        private void CreateAuntsList(string[] text)
        {
            AuntsList = new List<Dictionary<string, int>>();
            foreach (var line in text)
            {
                var lineSplitted = line.Split(" ");
                var oneAunt = new Dictionary<string, int>();
                for (int i = 0; i < lineSplitted.Length; i += 2)
                {
                    if (lineSplitted[i][^1].Equals(',') || lineSplitted[i][^1].Equals(':'))
                        lineSplitted[i] = lineSplitted[i][..^1];
                    if (lineSplitted[i + 1][^1].Equals(',') || lineSplitted[i + 1][^1].Equals(':'))
                        lineSplitted[i + 1] = lineSplitted[i + 1][..^1];
                    oneAunt.Add(lineSplitted[i], int.Parse(lineSplitted[i + 1]));
                }

                AuntsList.Add(oneAunt);
            }
        }
    }
}
