namespace AdventOfCode2015.Models
{
    public class LogicWireInstruction
    {
        public string Instruction { get; set; }
        public string Wire1 { get; set; }
        public string Wire2 { get; set; }
        public string WireDestination { get; set; }
        public bool InstructionDone { get; set; }
        public void ResetInstruction()
        {
            InstructionDone = false;
        }
    }
}