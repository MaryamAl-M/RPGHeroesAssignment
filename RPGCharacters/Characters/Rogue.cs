using RPGHeroesAssignment.RPGCharacters.Attributes;
using RPGHeroesAssignment.RPGCharacters.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGHeroesAssignment.RPGCharacters.Characters;

public class Rogue : Character
{
    public Rogue()
    {
        Type = CharacterType.Rogue;
        // Set Base and Total attributes
        BasePrimaryAttributes = PrimaryAttributesFactory.GetBaseAttributes(Type);
        // Set allowed Weapon and Armor types
        AllowedArmorTypes.AddRange(new List<ArmorType>()
        {
            ArmorType.Leather,
            ArmorType.Mail
        });
        AllowedWeaponTypes.AddRange(new List<WeaponType>()
        {
            WeaponType.Dagger,
            WeaponType.Sword
        });
    }

    public override double CalculateDamage()
    {
        Weapon? equippedWeapon = (Weapon?)Equipment[SlotType.Weapon];
        double damagePerSecond = equippedWeapon?.GetDamagePerSecond() ?? BaseDamage;

        return damagePerSecond * (1 + (double)TotalPrimaryAttributes.Dexterity / 100);
    }
}