using RPGHeroesAssignment.RPGCharacters.Attributes;
using RPGHeroesAssignment.RPGCharacters.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGHeroesAssignment.RPGCharacters.Items;

public class Armor : Item
{
    public ArmorType Type { get; set; }
    public PrimaryAttributes Attributes { get; set; }
    public Armor(string name, int levelRequired, SlotType slot, ArmorType type, PrimaryAttributes attributes)
    {
        Name = name;
        LevelRequired = levelRequired;
        Slot = slot;
        Type = type;
        Attributes = attributes;
    }
}