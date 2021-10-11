using AdventOfCode2015.Models;

namespace AdventOfCode2015.Services
{
    public interface IWireList
    {
        void SetWire(string code, int signal);
        Wire GetWire(string code);
        public bool WireIsSet(string code);
        public void ResetWireList();
    }
}