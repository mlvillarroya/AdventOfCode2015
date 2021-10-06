using AdventOfCode2015.Services;

namespace AdventOfCode2015.Challenges
{
    public class Challenge1A
    {
        private readonly IWebReader _webReader;
        private const string Url1A = "https://adventofcode.com/2015/day/1/input";

        public Challenge1A(IWebReader webReader)
        {
            _webReader = webReader;
            var code = _webReader.Content(Url1A);
        }
        
    }
}