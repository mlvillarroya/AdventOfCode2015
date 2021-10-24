using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
using AdventOfCode2015.Services;
using AdventOfCode2015.Resources;


namespace AdventOfCode2015.Challenges
{
    public class Challenge19
    {
        private readonly IFileWrapper _fileWrapper;
        private readonly IFileServer _fileServer;
        private string OriginalString;

        public Challenge19(IFileWrapper fileWrapper,
            IFileServer fileServer)
        {
            _fileWrapper = fileWrapper;
            _fileServer = fileServer;
            Random rng = new Random();
            OriginalString =
                "CRnCaCaCaSiRnBPTiMgArSiRnSiRnMgArSiRnCaFArTiTiBSiThFYCaFArCaCaSiThCaPBSiThSiThCaCaPTiRnPBSiThRnFArArCaCaSiThCaSiThSiRnMgArCaPTiBPRnFArSiThCaSiRnFArBCaSiRnCaPRnFArPMgYCaFArCaPTiTiTiBPBSiThCaPTiBPBSiRnFArBPBSiRnCaFArBPRnSiRnFArRnSiRnBFArCaFArCaCaCaSiThSiThCaCaPBPTiTiRnFArCaPTiBSiAlArPBCaCaCaCaCaSiRnMgArCaSiThFArThCaSiThCaSiRnCaFYCaSiRnFYFArFArCaSiRnFYFArCaSiRnBPMgArSiThPRnFArCaSiRnFArTiRnSiRnFYFArCaSiRnBFArCaSiRnTiMgArSiThCaSiThCaFArPRnFArSiRnFArTiTiTiTiBCaCaSiRnCaCaFYFArSiThCaPTiBPTiBCaSiThSiRnMgArCaF";
            var text = _fileWrapper.ReadAllTextInArray(_fileServer.GetFilePath(FileNames.CHALLENGE19)).Select(l=>l.Split(" => ")).ToList();
            // Part A
            // var finalList = CreateStringSubstitutionLists(text,OriginalString);
            // Console.WriteLine($"Number of different strings generated: {finalList.Count}");
            // Part B
            var mutations = 0;
            var target = OriginalString;
            while (!target.Equals("e"))
            {
                var temp = target;
                foreach (var line in text)
                {
                    //target = Regex.Replace(target, "([.*]*)"+line[1]+"([.*]*)", "$1"+line[0]+"$2");
                    //mutations++;
                    var a = line[0]; 
                    var b = line[1];
                    var index = target.IndexOf(b);
                    if (index >= 0)
                    {
                        target = target.Substring(0, index) + a + target.Substring(index + b.Length);
                        mutations++;
                    }
                }
                if (temp.Equals(target))
                {
                    target = OriginalString;
                    mutations = 0;
                    text = text.OrderBy(a => rng.Next()).ToList();
                }
            }

            Console.WriteLine(mutations);
        }
        
        private HashSet<string> CreateStringSubstitutionLists(List<string[]> substitutions, string originalString)
        {
            var finalList = new HashSet<string>();
            foreach (var line in substitutions)
            {
                finalList.UnionWith(SubstituteAllOcurrences(originalString, line[0], line[1]));
            }

            return finalList;
        }

        //https://stackoverflow.com/questions/17325768/how-do-i-replace-a-specific-occurrence-of-a-string-in-a-string
        private List<string> SubstituteAllOcurrences(string textWhere, string textOut, string textIn)
        {
            var outputList = new List<string>();
            for (int i = 0; i < Regex.Matches(textWhere,textOut).Count; i++)
            {
                var found = 0;
                outputList.Add(Regex.Replace(textWhere, textOut, x => found++ == i ? textIn : x.Value));
            }
            return outputList;
        }
    }
}
