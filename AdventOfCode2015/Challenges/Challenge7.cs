using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using AdventOfCode2015.Helpers;
using AdventOfCode2015.Models;
using AdventOfCode2015.Services;
using AdventOfCode2015.Resources;
using Microsoft.VisualBasic.FileIO;

namespace AdventOfCode2015.Challenges
{
    public class Challenge7
    {
        private readonly IFileWrapper _fileWrapper;
        private readonly IFileServer _fileServer;
        private readonly IWireList _wireList;
        private readonly ILogicFunctions _logicFunctions;
        private readonly string[] text;

        public Challenge7(IFileWrapper fileWrapper,
            IFileServer fileServer,
            IWireList wireList,
            ILogicFunctions logicFunctions)
        {
            _fileWrapper = fileWrapper;
            _fileServer = fileServer;
            _wireList = wireList;
            _logicFunctions = logicFunctions;
            text = _fileWrapper.ReadAllTextInArray(_fileServer.GetFilePath(FileNames.CHALLENGE7));
            List<LogicWireInstruction> instructions = LoadInstructions();
            ProcessInstructions(instructions);
            var finalValuePartA = _wireList.GetWire("a").GetValue();
            Console.WriteLine("Part A -> Wire a: " + finalValuePartA);
            // PART B
            _wireList.ResetWireList();
            _wireList.SetWire("b",finalValuePartA);
            instructions = instructions.Select(i => {i.ResetInstruction(); return i;}).ToList();
            ProcessInstructions(instructions);
            var finalValuePartB = _wireList.GetWire("a").GetValue();
            Console.WriteLine("Part B -> Wire a: " + finalValuePartB);

        }

        private void ProcessInstructions(List<LogicWireInstruction> instructions)
        {
            while (instructions.Count > 0)
            {
                foreach (var instruction in instructions)
                {
                    _logicFunctions.ExecuteInstruction(_wireList,instruction);
                }
                instructions = instructions.Where(instruction => instruction.InstructionDone == false).ToList();
            }
        }

        List<LogicWireInstruction> LoadInstructions()
        {
            var instructions = new List<LogicWireInstruction>();
            foreach (var line in text)
            {
                instructions.Add(_logicFunctions.StringToInstruction(line));
            }
            return instructions;
        }
    }
}