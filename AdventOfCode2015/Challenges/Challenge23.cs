using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Xml;
using AdventOfCode2015.Services;
using AdventOfCode2015.Resources;
using Combinatorics.Collections;


namespace AdventOfCode2015.Challenges
{
    public class Challenge23
    {
        private readonly IFileWrapper _fileWrapper;
        private readonly IFileServer _fileServer;
        private int position;
        public int a;
        public int b;
        private List<string[]> instructions;

        public Challenge23(IFileWrapper fileWrapper,
            IFileServer fileServer)
        {
            _fileWrapper = fileWrapper;
            _fileServer = fileServer;
            instructions = _fileWrapper.ReadAllTextInArray(_fileServer.GetFilePath(FileNames.CHALLENGE23)).Select(l=>l.Replace(",","").Split(" ")).ToList();
            position = 0;
            a = 1;
            b = 0;
            while (position < instructions.Count)
            {
                executeInstruction();
            }

            Console.WriteLine(b);
        }

        private void executeInstruction()
        {
            var instruction = instructions[position];
            switch (instruction[0])
            {
                /*
                 hlf r sets register r to half its current value, then continues with the next instruction.
                tpl r sets register r to triple its current value, then continues with the next instruction.
                inc r incre ments register r, adding 1 to it, then continues with the next instruction.
                jmp offset is a jump; it continues with the instruction offset away relative to itself.
                jie r, offset is like jmp, but only jumps if register r is even ("jump if even").
                jio r, offset is like jmp, but only jumps if register r is 1 ("jump if one", not odd).
                 */
                case "hlf":
                    if (GetIntByVariableName(instruction[1]) != null)
                        SetIntInVariableByName(instruction[1],(int)GetIntByVariableName(instruction[1])/2);
                    position++;
                    break;
                case "tpl":
                    if (GetIntByVariableName(instruction[1]) != null)
                        SetIntInVariableByName(instruction[1],(int)GetIntByVariableName(instruction[1])*3);
                    position++;
                    break;
                case "inc":
                    if (GetIntByVariableName(instruction[1]) != null)
                        SetIntInVariableByName(instruction[1],(int)GetIntByVariableName(instruction[1])+1);
                    position++;
                    break;
                case "jmp":
                    position+=int.Parse(instruction[1]);
                    break;
                case "jie":
                    if (GetIntByVariableName(instruction[1]) != null && GetIntByVariableName(instruction[1]) % 2 == 0)
                        position+=int.Parse(instruction[2]);
                    else position++;
                    break;
                case "jio":
                    if (GetIntByVariableName(instruction[1]) != null && GetIntByVariableName(instruction[1]) == 1)
                        position += int.Parse(instruction[2]);
                    else position++;
                    break;
            }
        }

        private int? GetIntByVariableName(string variableName)
        {
            return this.GetType().GetField(variableName) != null
                ? (int)this.GetType().GetField(variableName).GetValue(this)
                : null;
        }

        private void SetIntInVariableByName(string variableName, int value)
        {
            this.GetType().GetField(variableName).SetValue(this, value);
        }
    }
}
