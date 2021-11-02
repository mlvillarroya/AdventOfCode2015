using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
using System.Xml;
using AdventOfCode2015.Services;
using AdventOfCode2015.Resources;
using Combinatorics.Collections;


namespace AdventOfCode2015.Challenges
{
    public class Challenge22
    {
        private Warrior me;
        private Boss boss;
        private List<Spell> _spells;
        private int minManaSpent;
        Random rnd = new Random();

        public Challenge22()
        {

            minManaSpent = 99999999;
            CreateSpells();
            var manaNeeded = _spells.Select(s => s.Cost).Min();
            for (int i = 0; i < 1000000; i++)
            {
                me = new Warrior(50,0,500);
                boss = new Boss(58, 9);
                int round = 0;
                int manaSpent = 0;
                while (me.Hit >= 0 && boss.Hit >= 0)
                {
                    if (round % 2 == 0 && me.Mana >= manaNeeded)
                    {
                        var spell = RandomSpell();
                        AttackToBoss(spell);
                        manaSpent += spell.Cost;
                    }
                    else
                    {
                        AttackToWarrior();
                    }
                    round++;
                }
                if (me.Hit >= 0 && manaSpent < minManaSpent) minManaSpent = manaSpent;
            }

            Console.WriteLine(minManaSpent);
        }

        private Spell RandomSpell()
        {
            var spell = new Spell();
            while (true)
            {
                spell = _spells[rnd.Next(0,_spells.Count)];
                if (me.Mana >= spell.Cost)
                {
                    return spell;
                }
            }
        }
        private void AttackToWarrior()
        {
            me.Hit -= (boss.Damage - me.Armor);
            me.Armor = 0;
            // for part B
            me.Hit--;
            if (me.Hit < 0) return;
            //
            TurnSpellEfects();
        }

        private void AttackToBoss(Spell spell)
        {
            boss.Hit -= spell.Damage;
            me.Mana -= spell.Cost;
            me.Armor = 0;
            me.Hit = spell.Name == "Drain" ? me.Hit + spell.Effect : me.Hit;

            boss.PoisonEffect = spell.Name == "Poison" ? boss.PoisonEffect + spell.Turns : boss.PoisonEffect;
            boss.RechargeEffect = spell.Name == "Recharge" ? boss.RechargeEffect + spell.Turns : boss.RechargeEffect;
            me.ShieldEffect = spell.Name == "Shield" ? me.ShieldEffect + spell.Turns : me.ShieldEffect;
            
            TurnSpellEfects();
        }

        private void TurnSpellEfects()
        {
            if (me.ShieldEffect > 0)
            {
                me.Armor = _spells.Where(s => s.Name == "Shield").FirstOrDefault().Effect;
                me.ShieldEffect--;
            }
            if (boss.PoisonEffect > 0)
            {
                boss.Hit -= _spells.Where(s => s.Name == "Poison").FirstOrDefault().Effect;
                boss.PoisonEffect--;
            }

            if (boss.RechargeEffect > 0)
            {
                me.Mana += _spells.Where(s => s.Name == "Recharge").FirstOrDefault().Effect;
                boss.RechargeEffect--;
            }
        }

        private void CreateSpells()
        {
            _spells = new List<Spell>
            {
                new Spell("Magic Missile", 53, 0, 4),
                new Spell("Drain", 73, 0, 2, 2),
                new Spell("Shield",113,6,0,7),
                new Spell("Poison",173,6,0,3),
                new Spell("Recharge",229,5,0,101)
            };
        }
    }

    public class Spell
    {
        public string Name { get; }
        public int Cost { get; }
        public int Turns { get; }
        public int Damage { get; }
        public int Effect { get; }

        public Spell(string name = "", int cost = 0, int turns = 0, int damage = 0, int effect = 0)
        {
            Name = name;
            Cost = cost;
            Turns = turns;
            Damage = damage;
            Effect = effect;
        }
    }

    public class Warrior
    {
        public int Hit { get; set; }
        public int Armor { get; set; }
        public int Mana { get; set; }
        public int ShieldEffect { get; set;}


        public Warrior(int hit, int armor, int mana, int shieldEffect = 0)
        {
            Hit = hit;
            Armor = armor;
            Mana = mana;
            ShieldEffect = shieldEffect;
        }
    }

    public class Boss
    {
        public int Hit { get; set; }
        public int Damage { get; set;}
        public int PoisonEffect { get; set;}
        public int RechargeEffect { get; set;}

        public Boss(int hit, int damage, int poisonEffect = 0, int rechargeEffect = 0)
        {
            Hit = hit;
            Damage = damage;
            PoisonEffect = poisonEffect;
            RechargeEffect = rechargeEffect;
        }
    }
}
