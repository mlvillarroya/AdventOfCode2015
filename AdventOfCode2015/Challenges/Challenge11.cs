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
using Microsoft.Extensions.Primitives;
using Microsoft.VisualBasic.FileIO;
using MoreLinq.Extensions;

namespace AdventOfCode2015.Challenges
{
    public class Challenge11
    {
        public Challenge11()
        {
            string pattern = "hxbxxyzz";
            var goodPassword = false;
            while (!goodPassword)
            {
                pattern = IncrementString(pattern);
                goodPassword = !StringHasForbiddenLetter(pattern) && StringHasThreeConsecutiveLetters(pattern) &&
                               StringHasTwoOrMoreRepeatedLetterPatterns(pattern);
            }

            Console.WriteLine(pattern);
        }

        private string IncrementString(string text)
        {
            Char last = text[text.Length - 1];
            if (last == 'z') return IncrementString(text.Substring(0, text.Length - 1)) + 'a';
            else
            {
                return text.Substring(0,text.Length-1) + (Char)(Convert.ToUInt16(last)+1);
            }
        }

        private bool StringHasThreeConsecutiveLetters(string text)
        {
            for (int i = 0; i < text.Length-2; i++)
            {
                if (Convert.ToUInt16(text[i]) +1 == Convert.ToUInt16(text[i + 1]) &&
                    Convert.ToUInt16(text[i + 1]) +1 == Convert.ToUInt16(text[i + 2])) return true;
            }
            return false;
        }

        private bool StringHasForbiddenLetter(string text)
        {
            return text.Contains('i') || text.Contains('o') || text.Contains('l');
        }

        private bool StringHasTwoOrMoreRepeatedLetterPatterns(String text)
        {
            return Regex.Matches(text, @"([a-z])\1{1}").Count >= 2;
        }
    }
}
