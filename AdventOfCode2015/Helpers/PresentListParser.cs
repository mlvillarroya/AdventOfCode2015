using System.Linq;
using AdventOfCode2015.Models;

namespace AdventOfCode2015.Helpers
{
    public class PresentListParser : IPresentListParser
    {
        public PresentList StringArrayToPresentList(string[] text)
        {
            var PresentList = new PresentList();
            PresentList.AddRange(text.Select(packageString => packageString.Split('x')).Select(dimensions => new PresentPackage() {Length = int.Parse(dimensions[0]), Width = int.Parse(dimensions[1]), Height = int.Parse(dimensions[2])}));
            return PresentList;
        }
    }
}