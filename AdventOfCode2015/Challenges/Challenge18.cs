using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using AdventOfCode2015.Services;
using AdventOfCode2015.Resources;
using Combinatorics.Collections;


namespace AdventOfCode2015.Challenges
{
    public class Challenge18
    {
        private readonly IFileWrapper _fileWrapper;
        private readonly IFileServer _fileServer;

        public Challenge18(IFileWrapper fileWrapper,
            IFileServer fileServer)
        {
            _fileWrapper = fileWrapper;
            _fileServer = fileServer;
            var text = _fileWrapper.ReadAllTextInArray(_fileServer.GetFilePath(FileNames.CHALLENGE18)).Select(l=>l.ToArray()).ToArray();
            var result = text.Select(a => a.ToArray()).ToArray();
            for (int i = 0; i < 100; i++)
            {
                ChangeLights(text, result);
                text = result.Select(l => l.ToArray()).ToArray();
            }

            Console.WriteLine(result.Sum(l => l.Sum(c => LightValue(c))));
        }
        
        private static void PrintLights(char[][] result)
        {
            for (int i = 0; i < result.Length; i++)
            {
                for (int j = 0; j < result[i].Length; j++)
                {
                    Console.Write(result[i][j]);
                }

                Console.WriteLine("");
            }
        }

        private void ChangeLights(char[][] text, char[][] result)
        {
            var lenghtI = text.Length;
            for (int i = 0; i < lenghtI; i++)
            {
                var lengthJ = text[i].Length;
                for (int j = 0; j < lengthJ; j++)
                {
                    var value = CheckEnvironment(text, i, j);
                    if ((i == 0 && j == 0) || 
                        (i == lenghtI - 1 && j == 0) || 
                        (i == 0 && j == lengthJ - 1) ||
                        (i == lenghtI - 1 && j == lengthJ - 1))
                    {
                        result[i][j] = SetLight(1);
                    }
                    else if (LightValue(text[i][j]) == 1)
                    {
                        if (value is 2 or 3) result[i][j] = SetLight(1);
                        else result[i][j] = SetLight(0);
                    }
                    else
                    {
                        if (value == 3) result[i][j] = SetLight(1);
                        else result[i][j] = SetLight(0);
                    }
                }
            }
        }

        private int CheckEnvironment(char[][] lightArray, int i, int j)
        {
            var total = 0;
            if (i == 0) // TOP
            {
                // upper left corner
                if (j == 0)
                {
                    return LightValue(lightArray[i][j+1]) + LightValue(lightArray[i+1][j]) + LightValue(lightArray[i+1][j+1]);
                } 
                // upper right corner
                else if (j == lightArray[0].Length - 1)
                {
                    return LightValue(lightArray[i][j-1]) + LightValue(lightArray[i+1][j-1]) + LightValue(lightArray[i+1][j]);
                }
                // upper border
                else
                {
                    return LightValue(lightArray[i][j-1]) + LightValue(lightArray[i+1][j-1]) + LightValue(lightArray[i+1][j]) + LightValue(lightArray[i+1][j+1]) + LightValue(lightArray[i][j+1]);
                }
            }
            else if (i == lightArray.Length-1) // BOTTOM
            {
                // bottom left corner
                if (j == 0)
                {
                    return LightValue(lightArray[i-1][j]) + LightValue(lightArray[i-1][j+1]) + LightValue(lightArray[i][j+1]);
                }
                // bottom right corner
                if (j == lightArray[0].Length - 1)
                {
                    return LightValue(lightArray[i-1][j-1]) + LightValue(lightArray[i-1][j]) + LightValue(lightArray[i][j-1]);
                }
                // right border
                else
                {
                    return LightValue(lightArray[i-1][j-1]) + LightValue(lightArray[i-1][j]) + LightValue(lightArray[i-1][j+1]) + LightValue(lightArray[i][j-1]) + LightValue(lightArray[i][j+1]);
                }
            }
            else if (j == 0)
            {
                return  LightValue(lightArray[i-1][j]) + LightValue(lightArray[i-1][j+1]) + LightValue(lightArray[i][j+1]) +  LightValue(lightArray[i+1][j]) + LightValue(lightArray[i+1][j+1]) ;
            }
            else if (j == lightArray[0].Length - 1)
            {
                return LightValue(lightArray[i-1][j-1]) + LightValue(lightArray[i-1][j]) + LightValue(lightArray[i][j-1]) + LightValue(lightArray[i+1][j-1]) + LightValue(lightArray[i+1][j]);
            }
            // generic case
            return LightValue(lightArray[i-1][j-1]) + LightValue(lightArray[i-1][j]) + LightValue(lightArray[i-1][j+1]) + LightValue(lightArray[i][j-1]) + LightValue(lightArray[i][j+1]) + LightValue(lightArray[i+1][j-1]) + LightValue(lightArray[i+1][j]) + LightValue(lightArray[i+1][j+1]) ;

        }

        private int LightValue(char value)
        {
            if (value.Equals('.')) return 0;
            else return 1;
        }

        private char SetLight(int value)
        {
            if (value == 1) return '#';
            else return '.';
        }
    }
}
