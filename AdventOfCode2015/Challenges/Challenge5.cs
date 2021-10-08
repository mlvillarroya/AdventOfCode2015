using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using AdventOfCode2015.Helpers;
using AdventOfCode2015.Models;
using AdventOfCode2015.Services;
using AdventOfCode2015.Resources;

namespace AdventOfCode2015.Challenges
{
    public class Challenge5
    {
        private readonly IFileWrapper _fileWrapper;
        private readonly IFileServer _fileServer;
        private readonly string[] text;

        public Challenge5(IFileWrapper fileWrapper,
            IFileServer fileServer,
            IPresentListParser presentListParser,
            IEncrypting encrypting)
        {
            _fileWrapper = fileWrapper;
            _fileServer = fileServer;
            text = _fileWrapper.ReadAllTextInArray(_fileServer.GetFilePath(FileNames.CHALLENGE5));
            Console.WriteLine($"Nice string: {NiceStrings(text).ToString()}");
            Console.WriteLine($"Nice string2: {NiceStrings2(text).ToString()}");
        }

        private int NiceStrings(string[] strings)
        {
            var niceStrings = 0;
            foreach (var text in strings)
            {
                if (HasThreeVowels(text) && HasTwoSameLetters(text) && !HasForbiddenSubString(text)) niceStrings++;
            }
            return niceStrings;
        }

        private bool HasThreeVowels(string text)
        {
            int points = 0;
            points += text.Count(l => l.Equals('a'));
            points += text.Count(l => l.Equals('e'));
            points += text.Count(l => l.Equals('i'));
            points += text.Count(l => l.Equals('o'));
            points += text.Count(l => l.Equals('u'));
            return points >= 3;
        }

        private bool HasTwoSameLetters(string text)
        {
            for (int i = 1; i < text.Length; i++)
            {
                if (text[i - 1] == text[i]) return true;
            }
            return false;
        }

        private bool HasForbiddenSubString(string text)
        {
            return text.Contains("ab") || text.Contains("cd") || text.Contains("pq") || text.Contains("xy");
        }
        
        private int NiceStrings2(string[] strings)
        {
            var niceStrings = 0;
            foreach (var text in strings)
            {
                if (HasTwoEqualGroupsOfTwoLetters(text) && HasGroupOfThreeWithEqualLimits(text)) niceStrings++;
            }
            return niceStrings;
        }

        private bool HasTwoEqualGroupsOfTwoLetters(string text)
        {
            var group = string.Empty;
            var rest = string.Empty;
            for (int i = 0; i + 2 < text.Length; i++)
            {
                group = text.Substring(i,2);
                rest = text.Substring(i + 2);
                if (rest.Contains(group)) return true;
            }
            return false;
        }

        private bool HasGroupOfThreeWithEqualLimits(string text)
        {
            for (int i = 1; i < text.Length-1; i++)
            {
                if (text[i-1].Equals(text[i+1])) return true;
            }
            return false;
        }
    }
}
    