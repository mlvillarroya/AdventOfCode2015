using AdventOfCode2015.Models;

namespace AdventOfCode2015.Helpers
{
    public interface IPresentListParser
    {
        PresentList StringArrayToPresentList(string[] text);
    }
}