using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
using System.Xml;
using AdventOfCode2015.Services;
using AdventOfCode2015.Resources;


namespace AdventOfCode2015.Challenges
{
    public class Challenge20
    {

        private int NumberOfPresents = 33100000;

        public Challenge20(IFileWrapper fileWrapper,
            IFileServer fileServer)
        {
            var i = 1;
            var presents = 0;

            while (presents < NumberOfPresents)
            {
                i*=10;
                presents = GetPresents2(i);
                if (i % 100 == 0) Console.WriteLine($"House : {i}, Number of presents: {presents}");
            }
            i /= 15;
            presents = GetPresents2(i);
            while (presents < NumberOfPresents)
            {
                i+=1;
                presents = GetPresents2(i);
                if (i % 100 == 0) Console.WriteLine($"House : {i}, Number of presents: {presents}");
            }
            Console.WriteLine($"FINAL RESULT: ¡¡¡¡¡ {i} !!!!");
        }

        private List<int> GetDivisors(int input)
        {
            var i = input;
            var output = new List<int>();
            while (i > 0)
            {
                if (input % i == 0) output.Add(i);
                i--;
            }
            return output;
        }

        private int GetPresents(int houseNumber)
        {
            var output = 0;
            foreach (var number in GetDivisors(houseNumber))
            {
                output += houseNumber / number * 10;
            }

            return output;
        }
        private int GetPresents2(int houseNumber)
        {
            var output = 0;
            foreach (var number in GetDivisors(houseNumber))
            {
                if (houseNumber/number <= 50) output += number * 11;
            }

            return output;
        }

    }
}
