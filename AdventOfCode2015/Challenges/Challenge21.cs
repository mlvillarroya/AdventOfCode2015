using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
using System.Xml;
using AdventOfCode2015.Services;
using AdventOfCode2015.Resources;
using Combinatorics.Collections;


namespace AdventOfCode2015.Challenges
{
    public class Challenge21
    {
        private readonly IFileWrapper _fileWrapper;
        private readonly IFileServer _fileServer;
        private List<IReadOnlyList<int>> possibleCombinations;
        private List<Item> swords;
        private List<Item> shields;
        private List<Item> rings;
        public Challenge21(IFileWrapper fileWrapper,
            IFileServer fileServer)
        {
            _fileWrapper = fileWrapper;
            _fileServer = fileServer;
            var text = _fileWrapper.ReadAllTextInArray(_fileServer.GetFilePath(FileNames.CHALLENGE21_WEAPONS)).
                Select(c=>Regex.Replace(c,@"\s\+",@"+")).
                Select(c=>Regex.Replace(c,@"\s+",@" ")).
                Select(c=>c.Split(" ")).
                ToList();
            // PART A
            possibleCombinations = CreatePossibleInventories();
            (swords, shields, rings) = LoadWeaponList(text);
            var prizes = new List<CombinationPrizeWin>();
            foreach (var combination in possibleCombinations)
            {
                prizes.Add(new CombinationPrizeWin()
                {
                    Combination = combination,
                    Prize = CalculatePrize(combination,swords,shields,rings),
                    Win = YouWin(100,YourDamage(combination,swords,shields,rings),YourArmor(combination,swords,shields,rings),103,9,2)
                });
            }
            Console.WriteLine(prizes.Where(i => i.Win == false).OrderBy(i => i.Prize).Last().Prize);
        }

        private (int,int,int) SumProduct(IReadOnlyList<int> possibility, List<(string, int, int, int)> items)
        {
            var result1 = 0;
            var result2 = 0;
            var result3 = 0;
            for (int i = 0; i < possibility.Count; i++)
            {
                result1 += possibility[i] * items[i].Item2;
                result2 += possibility[i] * items[i].Item3;
                result3 += possibility[i] * items[i].Item4;
            }
            return (result1,result2,result3);
        }

        private int CalculatePrize(IReadOnlyList<int> combination, List<Item> swords, List<Item> shields, List<Item> rings)
        {
            return
                (combination[0] != 0 ? swords[combination[0]-1].Cost : 0) +
                (combination[1] != 0 ? shields[combination[1] - 1].Cost : 0) +
                (combination[2] != 0 ? rings[combination[2] - 1].Cost : 0) +
                (combination[3] != 0 ? rings[combination[3] - 1].Cost : 0);
        }

        private int YourDamage(IReadOnlyList<int> combination,List<Item> swords, List<Item> shields, List<Item> rings)
        {
            return
                (combination[0] != 0 ? swords[combination[0]-1].Damage : 0) +
                (combination[1] != 0 ? shields[combination[1] - 1].Damage : 0) +
                (combination[2] != 0 ? rings[combination[2] - 1].Damage : 0) +
                (combination[3] != 0 ? rings[combination[3] - 1].Damage : 0);
        }
        private int YourArmor(IReadOnlyList<int> combination,List<Item> swords, List<Item> shields, List<Item> rings)
        {
            return
                (combination[0] != 0 ? swords[combination[0]-1].Armor : 0) +
                (combination[1] != 0 ? shields[combination[1] - 1].Armor : 0) +
                (combination[2] != 0 ? rings[combination[2] - 1].Armor : 0) +
                (combination[3] != 0 ? rings[combination[3] - 1].Armor : 0);
        }
        private bool? YouWin(int YourHit, int YourDamage, int YourArmor, int BossHit, int BossDamage, int BossArmor)
        {
            if ((YourDamage - BossArmor) <= 0)
            {
                if ((BossDamage - YourArmor) <= 0) return null;
                else  return false;
            }
            else if ((BossDamage - YourArmor) <= 0) 
            {
                if ((YourDamage - BossArmor) <= 0) return null;
                else  return true;
            }
            else return (BossHit / (YourDamage - BossArmor)) + (BossHit % (YourDamage - BossArmor) != 0 ? 1 : 0 )
                   <= 
                   (YourHit / (BossDamage - YourArmor)) + (YourHit % (BossDamage - YourArmor) != 0 ? 1 : 0 );
        }

        private List<IReadOnlyList<int>> CreatePossibleInventories()
        {
            int[] numeros = { 0,1,2,3,4,5,6};
            var combinations = new Variations<int>(numeros,4,GenerateOption.WithRepetition).ToList();
            //sword can't be 0 nor 6
            combinations = combinations.Where(c => (c[0] != 0 && c[0] != 6)).ToList();
            //shield can't be 6
            combinations = combinations.Where(c =>  c[1] != 6).ToList();
            //two rings cannot be equals, except if they're 0
            combinations = combinations.Where(c =>  ((c[2] == c[3] && c[2] == 0)) || c[2] != c[3]).ToList();
            return combinations;
        }

        private (List<Item> Swords, List<Item> Shields, List<Item> Rings) LoadWeaponList(List<string[]> text)
        {
            int i;
            int j;
            var swords = new List<Item>();
            var shields = new List<Item>();
            var rings = new List<Item>();
            (i, j) = Delimite("Weapons", text);
            for (int k = i+1; k < j; k++)
            {
                swords.Add(new Item()
                {
                    Name = text[k][0],
                    Cost = int.Parse(text[k][1]),
                    Damage = int.Parse(text[k][2]),
                    Armor = int.Parse(text[k][3])
                });
            }
            (i, j) = Delimite("Armor", text);
            for (int k = i+1; k < j; k++)
            {
                shields.Add(new Item()
                {
                    Name = text[k][0],
                    Cost = int.Parse(text[k][1]),
                    Damage = int.Parse(text[k][2]),
                    Armor = int.Parse(text[k][3])
                });
            }
            (i, j) = Delimite("Rings", text);
            for (int k = i+1; k < j; k++)
            {
                rings.Add(new Item()
                {
                    Name = text[k][0],
                    Cost = int.Parse(text[k][1]),
                    Damage = int.Parse(text[k][2]),
                    Armor = int.Parse(text[k][3])
                });
            }
            return (swords, shields, rings);
        }

        private (int i, int j) Delimite(string title, List<string[]> text)
        {
            int i;
            int j;
            i = text.FindIndex(c => c[0].Contains(title));
            text = text.GetRange(i, text.Count - i);
            j = text.FindIndex(c => c[0] == "") != -1 ? text.FindIndex(c => c[0] == "") + i : text.Count + i;
            return (i, j);
        }
    }

    public class Item
    {
        public string Name { get; set; }
        public int Cost { get; set; }
        public int Damage { get; set; }
        public int Armor { get; set; }
    }

    public class CombinationPrizeWin
    {
        public IReadOnlyList<int> Combination { get; set; }
        public int Prize { get; set; }
        public bool? Win { get; set; }
    }

    public class PrizeWin
    {
        public int Prize { get; set; }
        public bool Win { get; set; }
    }
}
