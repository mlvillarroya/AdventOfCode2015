using AdventOfCode2015.Models;

namespace AdventOfCode2015.Services
{
    public class WireInstruction
    {
        private string Instruction;
        private string Wire1;
        private string Wire2;
        private string WireDestination;

        public void ReadInstruction(string text)
        {
            EraseInstruction();
            var instructions = text.Split(" ");
            if (instructions.Length == 5)
            {
                Wire1 = instructions[0];
                Instruction = instructions[1];
                Wire2 = instructions[2];
                WireDestination = instructions[4];
            }
            else if (instructions.Length == 4)
            {
                Instruction = instructions[0];
                Wire2 = instructions[1];
                WireDestination = instructions[3];
            }
            else if (instructions.Length == 3)
            {
                Wire2 = instructions[0];
                WireDestination = instructions[2];
            }
        }

        private void EraseInstruction()
        {
            Wire1 = null;
            Instruction = null;
            Wire2 = null;
            WireDestination = null;
        }
        
    }
}