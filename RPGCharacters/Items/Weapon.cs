using RPGHeroesAssignment.RPGCharacters.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGHeroesAssignment.RPGCharacters.Items;

public class Weapon :Item
{
    public int Damage { get; private set; }
    public double AttackSpeed { get; private set; }
    public WeaponType Type { get; private set; }

    public Weapon(String name, int levelRequired, SlotType slot, WeaponType type, int damage, double attackSpeed)
    {
        Name = name;    
        LevelRequired = levelRequired;
        Slot = slot;
        Type = type;
        Damage = damage;
        AttackSpeed = attackSpeed;

    }

    /// <summary>
    /// This method calculates the weapon damage per second, by multiplying damage and attacks per second.
    /// </summary>
    /// <returns>Damage per second of the weapon</returns>
    public double GetDamagePerSecond()
    {
        return Math.Round(Damage * AttackSpeed, 2);
    }
}

