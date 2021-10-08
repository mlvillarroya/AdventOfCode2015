using System.Collections.Generic;

namespace AdventOfCode2015.Services
{
    public class LedMatrix : ILedMatrix
    {
        public LedMatrix()
        {
            Matrix = new Dictionary<string, int>();
        }
        private Dictionary<string, int> Matrix;
        private string Coordinates(int x, int y)
        {
            return x.ToString() + "-" + y.ToString();
        }
        private void LedOn(int x, int y)
        {
            
            if (!Matrix.ContainsKey(Coordinates(x,y)))
            {
                Matrix.Add(x.ToString()+"-"+y.ToString(),1);
            }
            else
            {
                Matrix[Coordinates(x,y)] = 1;
            }
        }
        private void LedOff(int x, int y)
        {
            if (!Matrix.ContainsKey(Coordinates(x,y)))
            {
                Matrix.Add(x.ToString()+"-"+y.ToString(),0);
            }
            else
            {
                Matrix[Coordinates(x,y)] = 1;
            }
        }
        private void LedToggle(int x, int y)
        {
            var value = 0;
            if (!Matrix.TryGetValue(Coordinates(x,y), out value))
            {
                Matrix.Add(x.ToString()+"-"+y.ToString(),1);
            }
            else
            {
                Matrix[Coordinates(x,y)] = ToggleValue(value);
            }
        }
        private int ToggleValue(int value)
        {
            return value == 0 ? 1 : 0;
        }
        private int Retrieve(int x, int y)
        {
            return Matrix.ContainsKey(Coordinates(x, y)) ? Matrix[Coordinates(x, y)] : 0;
        }

        public void ReadInstruction(string text)
        {
            var instructions = text.Split(" ");
            switch (instructions[0])
            {
                case "toggle":
                    var xo = int.Parse(instructions[1].Split(",")[0]);
                    var xf = int.Parse(instructions[3].Split(",")[0]);
                    var yo = int.Parse(instructions[1].Split(",")[1]);
                    var yf = int.Parse(instructions[3].Split(",")[1]);
                    for (int i = xo; i <= xf; i++)
                    {
                        for (int j = yo; j < yf; j++)
                        {
                            LedToggle(i,j);
                        }
                    }
                    break;
            }
        }
    }
}