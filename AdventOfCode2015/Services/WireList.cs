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

        public void AddWire(string code, int signal)
        {
            var wire = new Wire(code, signal);
            Wires.Add(wire);
        }
        private Wire GetWire(string code)
        {
            return Wires.FirstOrDefault(w => w.GetCode().Equals(code)) ?? new Wire(code,0);
        }
        public int GetWireValue(string code)
        {
            return GetWire(code).GetValue();
        }
    }
}