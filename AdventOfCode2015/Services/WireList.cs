using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2015.Models;

namespace AdventOfCode2015.Services
{
    public class WireList : IWireList
    {
        private List<Wire> Wires;

        public WireList()
        {
            Wires = new List<Wire>();
        }

        public void SetWire(string code, int signal)
        {
            var wire = new Wire(code, signal);
            Wires.Add(wire);
        }

        public bool WireIsSet(string code)
        {
            return Wires.FirstOrDefault(w => w.GetCode().Equals(code)) != null;
        }

        public Wire GetWire(string code)
        {
            return Wires.FirstOrDefault(w => w.GetCode().Equals(code));
        }
    }
}