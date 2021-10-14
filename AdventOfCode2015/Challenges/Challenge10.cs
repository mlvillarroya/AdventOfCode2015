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
using MoreLinq.Extensions;

namespace AdventOfCode2015.Challenges
{
    public class Challenge10
    {

        
        public Challenge10()
        {
            var solution = Enumerable.Range(1, 50)
                .Aggregate("1321131112".Select(c => c - '0').ToArray(),
                    (acc, _) => acc
                        .GroupAdjacent(n => n)
                        .SelectMany(g => new int[] {g.Count(), g.First()})
                        .ToArray())
                .Count();
        }
            /*
            var number = "1321131112";
            for (int i = 0; i < 1; i++)
            {
                number = LookAndSay(number);
                Console.WriteLine(i + 1);
            }

            Console.WriteLine(number);
        }



        string LookAndSay(string input)
        {
            var output = string.Empty;
            for (int i = 0; i < input.Length; i++)
            {
                var j = i + 1;
                while ((j < input.Length) && (input[i] == input[j]))
                {
                    j++;
                }

                output += (j - i).ToString() + input[i];
                i = j - 1;
            }

            return output;
        }
    */
    }
}
