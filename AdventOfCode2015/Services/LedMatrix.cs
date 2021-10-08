using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;

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
        private void LedOn2(int x, int y)
        {
            var value = 0;
            if (!Matrix.TryGetValue(Coordinates(x,y), out value))
            {
                Matrix.Add(x.ToString()+"-"+y.ToString(),1);
            }
            else
            {
                value++;
                Matrix[Coordinates(x,y)] = value;
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
                Matrix[Coordinates(x,y)] = 0;
            }
        }
        private void LedOff2(int x, int y)
        {
            var value = 0;
            if (!Matrix.TryGetValue(Coordinates(x,y), out value))
            {
                Matrix.Add(Coordinates(x,y),0);
            }
            else
            {
                value = value <= 0 ? 0 : value-1;
                Matrix[Coordinates(x,y)] = value;
            }
        }

        private void LedToggle(int x, int y)
        {
            var value = 0;
            if (!Matrix.TryGetValue(Coordinates(x,y), out value))
            {
                Matrix.Add(Coordinates(x,y),1);
            }
            else
            {
                Matrix[Coordinates(x,y)] = ToggleValue(value);
            }
        }
        private void LedToggle2(int x, int y)
        {
            var value = 0;
            if (!Matrix.TryGetValue(Coordinates(x,y), out value))
            {
                Matrix.Add(Coordinates(x,y),2);
            }
            else
            {
                value+=2;
                Matrix[Coordinates(x,y)] = value;
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

        public void ReadInstruction(string text,int option)
        {
            var instructions = text.Split(" ");
            int xo, xf, yo, yf;
            switch (instructions[0])
            {
                case "toggle":
                    xo = int.Parse(instructions[1].Split(",")[0]);
                    xf = int.Parse(instructions[3].Split(",")[0]);
                    yo = int.Parse(instructions[1].Split(",")[1]);
                    yf = int.Parse(instructions[3].Split(",")[1]);
                    for (int i = xo; i <= xf; i++)
                    {
                        for (int j = yo; j <= yf; j++)
                        {
                            if (option == 1) LedToggle(i, j);
                            else LedToggle2(i, j);
                        }
                    }
                    break;
                default:
                    xo = int.Parse(instructions[2].Split(",")[0]);
                    xf = int.Parse(instructions[4].Split(",")[0]);
                    yo = int.Parse(instructions[2].Split(",")[1]);
                    yf = int.Parse(instructions[4].Split(",")[1]);
                    for (int i = xo; i <= xf; i++)
                    {
                        for (int j = yo; j <= yf; j++)
                        {
                            if (instructions[1].Equals("on"))
                            {
                                if (option == 1) LedOn(i, j);
                                else LedOn2(i,j);
                            }
                            else
                            {
                                if (option == 1) LedOff(i, j);
                                else LedOff2(i,j);
                            }
                        }
                    }
                    break;
            }
            Console.WriteLine(TotalBrightness());
        }
        
        public int TotalLedOn()
        {
            return Matrix.Count(e => e.Value == 1);
        }

        public int TotalBrightness()
        {
            return Matrix.Sum(e => e.Value);
        }
    }
}