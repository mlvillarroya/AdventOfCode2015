using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
using System.Xml;
using AdventOfCode2015.Services;
using AdventOfCode2015.Resources;
using Combinatorics.Collections;


namespace AdventOfCode2015.Challenges
{
    public class Challenge22
    {
        private readonly IFileWrapper _fileWrapper;
        private readonly IFileServer _fileServer;

        public Challenge22(IFileWrapper fileWrapper,
            IFileServer fileServer)
        {
            _fileWrapper = fileWrapper;
            _fileServer = fileServer;
            var text = _fileWrapper.ReadAllTextInArray(_fileServer.GetFilePath(FileNames.CHALLENGE21_WEAPONS));
            var prueba = GetMonthNames("es");
        }
        
        private IEnumerable GetMonthNames(string culture)
        {
            for (var i = 0; i < 12; i++)
            {
                yield return new DateTime(2020, i + 1, 1)
                    .ToString("MMMM", CultureInfo.CreateSpecificCulture(culture));
                for (int j = 0; j < 12; j++)
                {
                    yield return j;
                }
            }
        }
    }
}
