using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2015.Services;
using AdventOfCode2015.Resources;
using Combinatorics.Collections;


namespace AdventOfCode2015.Challenges
{
    public class CoupleRelationship
    {
        public string Person1 { get; set; }
        public string Person2 { get; set; }
        public int Points { get; set; }
    }
    
    public class Challenge13
    {
        private readonly IFileWrapper _fileWrapper;
        private readonly IFileServer _fileServer;
        private readonly List<string[]> textSplitted;
        private readonly List<CoupleRelationship> _coupleRelationships;
        private readonly HashSet<string> _guestList;

        public Challenge13(IFileWrapper fileWrapper,
            IFileServer fileServer)
        {
            _fileWrapper = fileWrapper;
            _fileServer = fileServer;
            var text = _fileWrapper.ReadAllTextInArray(_fileServer.GetFilePath(FileNames.CHALLENGE13));
            textSplitted = CreateTextSplitted(text);
            _guestList = GetGuestList(text);
            //For part two
            _guestList = AddMyselfToGuestList(_guestList);
            _coupleRelationships = CreateCoupleRelationships();
            //For part two
            _coupleRelationships = AddMySelfToCoupleRelationships(_coupleRelationships);
            var guestListPermutations = BuildGuestPermutations();
            var points = new List<int>();
            foreach (var arrangement in guestListPermutations)
            {
                points.Add(CalculateArrangementPoints(arrangement));
            }
            Console.WriteLine(points.Max());
        }

        private List<CoupleRelationship> AddMySelfToCoupleRelationships(List<CoupleRelationship> coupleRelationships)
        {
            foreach (var person in _guestList)
            {
                if (!person.Equals("Me"))
                {
                    coupleRelationships.Add(new CoupleRelationship()
                    {
                        Person1 = "Me",
                        Person2 = person.ToString(),
                        Points = 0
                    });
                }        
            }
            return coupleRelationships;
        }

        private HashSet<string> AddMyselfToGuestList(HashSet<string> guestList)
        {
            guestList.Add("Me");
            return guestList;
        }

        private List<CoupleRelationship> CreateCoupleRelationships()
        {
            var CoupleRelationships = new List<CoupleRelationship>();
            foreach (var line in textSplitted)
            {
                var query = CoupleRelationships.Where(l => l.Person1 == line[10] && l.Person2 == line[0]);
                if (query.Any()) query.FirstOrDefault().Points = line[2] == "gain" ? query.FirstOrDefault().Points+int.Parse(line[3]) : query.FirstOrDefault().Points-int.Parse(line[3]);
                else CoupleRelationships.Add(new CoupleRelationship()
                {
                    Person1 = line[0],
                    Person2 = line[10],
                    Points = line[2] == "gain" ? int.Parse(line[3]) : -int.Parse(line[3])
                });
            }

            return CoupleRelationships;
        }

        private int CalculateArrangementPoints(IReadOnlyList<string> arrangement)
        {
            int points = 0;
            for (int i = 0; i < arrangement.Count; i++)
            {
                points += _coupleRelationships.FirstOrDefault(c =>
                    (c.Person1 == arrangement[i] && c.Person2 == arrangement[(i + 1) % arrangement.Count]) ||
                    (c.Person1 == arrangement[(i + 1) % arrangement.Count] && c.Person2 == arrangement[i])).Points;
            }
            return points;
        }

        private HashSet<string> GetGuestList(string[] text)
        {
            var guestList = new HashSet<string>();
            foreach (var line in textSplitted)
            {
                guestList.Add(line[0]);
                guestList.Add(line[10]);
            }

            return guestList;
        }
        private Permutations<string> BuildGuestPermutations()
        {
            return new Permutations<string>(_guestList);
        }

        private List<string[]> CreateTextSplitted(string[] text)
        {
            var TextSplitted = new List<string[]>();
            foreach (var line in text)
            {
                TextSplitted.Add(line.Replace(".","").Split(" "));
            }
            return TextSplitted;
        }
    }
}
