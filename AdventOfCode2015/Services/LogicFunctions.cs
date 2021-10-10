using System.Diagnostics;
using AdventOfCode2015.Models;

namespace AdventOfCode2015.Services
{
    public class LogicFunctions : ILogicFunctions
    {
        public LogicWireInstruction StringToInstruction(string text)
        {
            var logicWireInstruction = new LogicWireInstruction();
            var instructions = text.Split(" ");
            if (instructions.Length == 5)
            {
                logicWireInstruction.Wire1 = instructions[0];
                logicWireInstruction.Instruction = instructions[1];
                logicWireInstruction.Wire2 = instructions[2];
                logicWireInstruction.WireDestination = instructions[4];
            }
            else if (instructions.Length == 4)
            {
                logicWireInstruction.Instruction = instructions[0];
                logicWireInstruction.Wire2 = instructions[1];
                logicWireInstruction.WireDestination = instructions[3];
            }
            else if (instructions.Length == 3)
            {
                logicWireInstruction.Instruction = "ASSIGN";
                logicWireInstruction.Wire2 = instructions[0];
                logicWireInstruction.WireDestination = instructions[2];
            }
            return logicWireInstruction;
        }

        public void ExecuteInstruction(IWireList wireList, LogicWireInstruction instruction)
        {
            int value1;
            int value2;
            switch (instruction.Instruction)
            {
                case "ASSIGN":
                    //Are we putting a number or another wire??
                    if (wireList.WireIsSet(instruction.Wire2) || int.TryParse(instruction.Wire2, out value1))
                    {
                        wireList.SetWire(instruction.WireDestination,
                            int.TryParse(instruction.Wire2, out value1)
                                ? value1
                                : wireList.GetWire(instruction.Wire2).GetValue());
                        instruction.InstructionDone = true;
                    }
                    break;
                case "AND":
                    if ((wireList.WireIsSet(instruction.Wire1) || int.TryParse(instruction.Wire1, out value1)) && (wireList.WireIsSet(instruction.Wire2) || int.TryParse(instruction.Wire2, out value2)))
                    {
                        value1 = int.TryParse(instruction.Wire1, out value1)
                            ? value1
                            : wireList.GetWire(instruction.Wire1).GetValue();
                        value2 = int.TryParse(instruction.Wire2, out value2)
                            ? value2
                            : wireList.GetWire(instruction.Wire2).GetValue();
                        wireList.SetWire(instruction.WireDestination, value1 & value2);
                        instruction.InstructionDone = true;
                    }
                    break;
                case "OR":
                    if ((wireList.WireIsSet(instruction.Wire1) || int.TryParse(instruction.Wire1, out value1)) && (wireList.WireIsSet(instruction.Wire2) || int.TryParse(instruction.Wire2, out value2)))
                    {
                        value1 = int.TryParse(instruction.Wire1, out value1)
                            ? value1
                            : wireList.GetWire(instruction.Wire1).GetValue();
                        value2 = int.TryParse(instruction.Wire2, out value2)
                            ? value2
                            : wireList.GetWire(instruction.Wire2).GetValue();
                        wireList.SetWire(instruction.WireDestination, value1 | value2);
                        instruction.InstructionDone = true;
                    }
                    break;
                case "LSHIFT":
                    if ((wireList.WireIsSet(instruction.Wire1) || int.TryParse(instruction.Wire1, out value1)) && (wireList.WireIsSet(instruction.Wire2) || int.TryParse(instruction.Wire2, out value2)))
                    {
                        value1 = int.TryParse(instruction.Wire1, out value1)
                            ? value1
                            : wireList.GetWire(instruction.Wire1).GetValue();
                        value2 = int.TryParse(instruction.Wire2, out value2)
                            ? value2
                            : wireList.GetWire(instruction.Wire2).GetValue();
                        wireList.SetWire(instruction.WireDestination, value1 << value2);
                        instruction.InstructionDone = true;
                    }
                    break;
                case "RSHIFT":
                    if ((wireList.WireIsSet(instruction.Wire1) || int.TryParse(instruction.Wire1, out value1)) && (wireList.WireIsSet(instruction.Wire2) || int.TryParse(instruction.Wire2, out value2)))
                    {
                        value1 = int.TryParse(instruction.Wire1, out value1)
                            ? value1
                            : wireList.GetWire(instruction.Wire1).GetValue();
                        value2 = int.TryParse(instruction.Wire2, out value2)
                            ? value2
                            : wireList.GetWire(instruction.Wire2).GetValue();
                        wireList.SetWire(instruction.WireDestination, value1 >> value2);
                        instruction.InstructionDone = true;
                    }
                    break;
                case "NOT":
                    if (wireList.WireIsSet(instruction.Wire2) || int.TryParse(instruction.Wire2, out value1))
                    {
                        value1 = int.TryParse(instruction.Wire2, out value1)
                            ? value1
                            : wireList.GetWire(instruction.Wire2).GetValue();
                        wireList.SetWire(instruction.WireDestination, (ushort) ~value1);
                        instruction.InstructionDone = true;
                    }
                    break;
            }
        }
    }
}