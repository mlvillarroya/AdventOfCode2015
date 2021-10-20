using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2015.Services;
using AdventOfCode2015.Resources;
using Combinatorics.Collections;


namespace AdventOfCode2015.Challenges
{
    public class Challenge15
    {
        private readonly IFileWrapper _fileWrapper;
        private readonly IFileServer _fileServer;

        public Challenge15(IFileWrapper fileWrapper,
            IFileServer fileServer)
        {
            _fileWrapper = fileWrapper;
            _fileServer = fileServer;
            var text = _fileWrapper.ReadAllTextInArray(_fileServer.GetFilePath(FileNames.CHALLENGE15));
            var ingredients = ParseTextToIngredient(text);
            var variations = GenerateVariations();
            var scores = new List<int>();
            foreach (var variation in variations)
            {
                var capacity = variation[0]*ingredients[0].Item2+variation[1]*ingredients[1].Item2+variation[2]*ingredients[2].Item2+variation[3]*ingredients[3].Item2;
                var durability = variation[0]*ingredients[0].Item3+variation[1]*ingredients[1].Item3+variation[2]*ingredients[2].Item3+variation[3]*ingredients[3].Item3;
                var flavour = variation[0]*ingredients[0].Item4+variation[1]*ingredients[1].Item4+variation[2]*ingredients[2].Item4+variation[3]*ingredients[3].Item4;
                var texture = variation[0]*ingredients[0].Item5+variation[1]*ingredients[1].Item5+variation[2]*ingredients[2].Item5+variation[3]*ingredients[3].Item5;
                var calories = variation[0]*ingredients[0].Item6+variation[1]*ingredients[1].Item6+variation[2]*ingredients[2].Item6+variation[3]*ingredients[3].Item6;
                // challenge A: if (capacity>0 && durability>0 && flavour>0 && texture>0) scores.Add(capacity*durability*flavour*texture);
                /* challenge B: */ if (capacity>0 && durability>0 && flavour>0 && texture>0 && calories == 500) scores.Add(capacity*durability*flavour*texture);
            }
            Console.WriteLine($"Max score: {scores.Max()}");
        }

        private List<(string, int, int, int, int, int)> ParseTextToIngredient(string[] text)
        {
            var names = text.Select(l => (l.Split(":")[0],
                    int.Parse(l.Split(":")[1].Split(" ")[2][..^1]),
                    int.Parse(l.Split(":")[1].Split(" ")[4][..^1]),
                    int.Parse(l.Split(":")[1].Split(" ")[6][..^1]),
                    int.Parse(l.Split(":")[1].Split(" ")[8][..^1]),
                    int.Parse(l.Split(":")[1].Split(" ")[10])))
                .ToList();
            return names;
        }

        private List<IReadOnlyList<int>> GenerateVariations()
        {
            int[] ints = Enumerable.Range(0, 100).ToArray();
            Variations<int> variations = new Variations<int>(ints, 4,GenerateOption.WithRepetition);
            var aa = new List<IReadOnlyList<int>>();
            aa = variations.Where(a => a[0] + a[1] + a[2] + a[3] == 100).ToList();
            return aa;
        }
    }
}
