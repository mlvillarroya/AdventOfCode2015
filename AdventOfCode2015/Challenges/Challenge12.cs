using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
using System.Threading.Tasks.Dataflow;
using AdventOfCode2015.Helpers;
using AdventOfCode2015.Models;
using AdventOfCode2015.Services;
using AdventOfCode2015.Resources;
using Combinatorics.Collections;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualBasic.FileIO;
using MoreLinq.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AdventOfCode2015.Challenges
{
    public class Challenge12
    {
        private readonly IFileWrapper _fileWrapper;
        private readonly IFileServer _fileServer;
        private readonly string text;

        public Challenge12(IFileWrapper fileWrapper,
            IFileServer fileServer)
        {
            _fileWrapper = fileWrapper;
            _fileServer = fileServer;
            text = _fileWrapper.ReadAllTextInOneString(_fileServer.GetFilePath(FileNames.CHALLENGE12));

            JToken convertObj = JToken.Parse(text);
            Console.WriteLine(SumTokens(convertObj));
            //Console.WriteLine(PrintValue(convertObj));
        }

        private static long SumTokens(JToken token)
        {
            if (token is JObject)
            {    
                var jo = (JObject)token;
                if (IsRed(jo)) return 0;
                return jo.Properties().Select(p => p.Value).Sum(jt => SumTokens(jt));
            }
            else if (token is JArray)
            {
                var ja = (JArray)token;
                return ja.Sum(jt => SumTokens(jt));
            }
            else if (token is JValue)
            {
                var jv = (JValue)token;
                return (jv.Value is long) ? (long)jv.Value : 0;
            }

            throw new InvalidOperationException();
        }
    
        public static bool IsRed(JObject jobject)
        {
            return jobject.Properties()
                .Select(p => p.Value).OfType<JValue>()
                .Select(j => j.Value).OfType<string>()
                .Any(j => j == "red");
        }
        
    /*
    private int PrintValue(JToken jObject)
    {
        var output = 0;
        if (!jObject.HasValues)
        {
            int value = 0;
            output = int.TryParse(jObject.ToString(),out value) ? value : 0;
        }
        else 
        {foreach (var child in jObject)
            {
                output += PrintValue(child);
            }
        }
        return output;
    }
*/
    }
}
