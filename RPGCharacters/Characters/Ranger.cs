using RPGHeroesAssignment.RPGCharacters.Attributes;
using RPGHeroesAssignment.RPGCharacters.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGHeroesAssignment.RPGCharacters.Characters;

public class Ranger : Character
{
    public Ranger()
    {
        Type = CharacterType.Ranger;
        // Set Base attributes
        BasePrimaryAttributes = PrimaryAttributesFactory.GetBaseAttributes(Type);
        // Set allowed Weapon and Armor types
        AllowedArmorTypes.AddRange(new List<ArmorType>()
        {
            ArmorType.Leather,
            ArmorType.Mail
        });
        AllowedWeaponTypes.Add(WeaponType.Bow);
    }

    public override double CalculateDamage()
    {
        Weapon? equippedWeapon = (Weapon?)Equipment[SlotType.Weapon];
        double damagePerSecond = equippedWeapon?.GetDamagePerSecond() ?? baseDamage;

        return damagePerSecond * (1 + (double)TotalPrimaryAttributes.Dexterity / 100);
    }
}