using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
using AdventOfCode2015.Helpers;
using AdventOfCode2015.Models;
using AdventOfCode2015.Services;
using AdventOfCode2015.Resources;
using Combinatorics.Collections;
using Microsoft.VisualBasic.FileIO;

namespace AdventOfCode2015.Challenges
{
    public class Challenge9
    {
        private readonly IFileWrapper _fileWrapper;
        private readonly IFileServer _fileServer;
        private readonly string[] text;

        public Challenge9(IFileWrapper fileWrapper,
            IFileServer fileServer)
        {
            _fileWrapper = fileWrapper;
            _fileServer = fileServer;
            text = _fileWrapper.ReadAllTextInArray(_fileServer.GetFilePath(FileNames.CHALLENGE9));
            var cityList = BuildCityList(text);
            var cityListPermutations= new Permutations<string>(cityList);
            var cityListPermutationsWithDistance = GetDistances(text, cityListPermutations);
            var miniumum = cityListPermutationsWithDistance.Min();
            var maximum = cityListPermutationsWithDistance.Max();
            Console.WriteLine($"Minimum distance: {miniumum}");
            Console.WriteLine($"Maximum distance: {maximum}");

        }

        private List<string> BuildCityList(string[] text)
        {
            List<string> CityList = new List<string>();
            foreach (var line in text)
            {
                var instruction = line.Split(" ");
                if (!CityList.Contains(instruction[0])) CityList.Add(instruction[0]);
                if (!CityList.Contains(instruction[2])) CityList.Add(instruction[2]);
            }
            return CityList;
        }

        private List<int> GetDistances(string[] text, Permutations<string> CityListPermutations)
        {
            var CityListPermutationsWithDistance = new List<int>();
            var distance = 0;
            foreach (var CityListPermutation in CityListPermutations)
            {
                distance = 0;
                for (int j = 0; j < CityListPermutation.Count - 1; j++)
                {   
                    foreach (var line in text)
                    {
                        var instruction = line.Split(" ");
                        if ((CityListPermutation[j].Equals(instruction[0]) && CityListPermutation[j+1].Equals(instruction[2])) || (CityListPermutation[j].Equals(instruction[2]) && CityListPermutation[j+1].Equals(instruction[0])))
                            distance += int.Parse(instruction[4]);
                    }
                }
                CityListPermutationsWithDistance.Add(distance);
            }
            return CityListPermutationsWithDistance;
        }
    }
}