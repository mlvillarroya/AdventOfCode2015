using AdventOfCode2015.Models;

namespace AdventOfCode2015.Services
{
    public interface IWireList
    {
        void AddWire(string code, int signal);
        int GetWireValue(string code);
    }
}