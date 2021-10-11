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
using Microsoft.VisualBasic.FileIO;

namespace AdventOfCode2015.Challenges
{
    public class Challenge8
    {
        private readonly IFileWrapper _fileWrapper;
        private readonly IFileServer _fileServer;
        private readonly string[] text;

        public Challenge8(IFileWrapper fileWrapper,
            IFileServer fileServer)
        {
            _fileWrapper = fileWrapper;
            _fileServer = fileServer;
            text = _fileWrapper.ReadAllTextInArray(_fileServer.GetFilePath(FileNames.CHALLENGE8));
            int total1 = text.Select(s => new 
            {
                Escaped = s, 
                Unescaped = Regex.Replace(
                        s.Substring(1, s.Length - 2)
                            .Replace("\\\"", "\"")
                            .Replace("\\\\", "?"),
                        @"\\x[0-9a-f]{2}", "?")
            })
            .Sum(s => s.Escaped.Length - s.Unescaped.Length);

            Console.WriteLine($"Total escaped - unescaped: {total1}");

            var total2 = text.Select(s => new
            {
                old = s,
                changed = "\"" +
                          s.Replace("\\", "\\\\")
                              .Replace("\"", "\\\"")
                              
                          + "\""
            })
                .Sum(s => s.changed.Length - s.old.Length);
            Console.WriteLine($"Total escaped - unescaped: {total2}");
        }
    }
}