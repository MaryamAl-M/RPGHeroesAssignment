using RPGHeroesAssignment.RPGCharacters.Attributes;
using RPGHeroesAssignment.RPGCharacters.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGHeroesAssignment.RPGCharacters.Characters;

public class Mage : Character
{
    public Mage()
    {
        Type = CharacterType.Mage;
        // Set Base and Total attributes
        BasePrimaryAttributes = PrimaryAttributesFactory.GetBaseAttributes(Type);
        // Set allowed Weapon and Armor types
        AllowedArmorTypes.Add(ArmorType.Cloth);
        AllowedWeaponTypes.AddRange(new List<WeaponType>()
        {
            WeaponType.Staff,
            WeaponType.Wand
        });
    }

    public override double CalculateDamage()
    {
        Weapon? equippedWeapon = (Weapon?)Equipment[SlotType.Weapon];
        double damagePerSecond = equippedWeapon?.GetDamagePerSecond() ?? BaseDamage;

        return damagePerSecond * (1 + (double)TotalPrimaryAttributes.Intelligence / 100);
    }
}