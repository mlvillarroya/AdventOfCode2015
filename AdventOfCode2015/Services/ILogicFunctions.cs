using AdventOfCode2015.Models;

namespace AdventOfCode2015.Services
{
    public interface ILogicFunctions
    {
        LogicWireInstruction StringToInstruction(string text);
        public void ExecuteInstruction(IWireList wireList, LogicWireInstruction instruction);
    }
}