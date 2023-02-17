using RPGHeroesAssignment.RPGCharacters.Attributes;
using RPGHeroesAssignment.RPGCharacters.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGHeroesAssignment.RPGCharacters.Characters;

public class Warrior : Character
{
    public Warrior()
    {
        Type = CharacterType.Warrior;
        // Set Base and Total attributes
        BasePrimaryAttributes = PrimaryAttributesFactory.GetBaseAttributes(Type);
        // Set allowed Weapon and Armor types
        AllowedArmorTypes.AddRange(new List<ArmorType>()
        {
            ArmorType.Mail,
            ArmorType.Plate
        });
        AllowedWeaponTypes.AddRange(new List<WeaponType>()
        {
            WeaponType.Axe,
            WeaponType.Hammer,
            WeaponType.Sword
        });
    }

    public override double CalculateDamage()
    {
        Weapon? equippedWeapon = (Weapon?)Equipment[SlotType.Weapon];
        double damagePerSecond = equippedWeapon?.GetDamagePerSecond() ?? BaseDamage;

        return damagePerSecond * (1 + (double)TotalPrimaryAttributes.Strength / 100);
    }
}