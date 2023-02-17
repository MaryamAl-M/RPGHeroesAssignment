using RPGHeroesAssignment.RPGCharacters.Attributes;
using RPGHeroesAssignment.RPGCharacters.Exceptions;
using RPGHeroesAssignment.RPGCharacters.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGHeroesAssignment.RPGCharacters.Characters
{
    public abstract class Character
    {

        public string Name { get; set; } = "Nameless hero";
        public int Level { get; private set; } = 1;
        public CharacterType Type { get; init; }
        public PrimaryAttributes BasePrimaryAttributes { get; set; } = new PrimaryAttributes();
        public PrimaryAttributes TotalPrimaryAttributes => CalculateTotalAttributes();
        public readonly List<ArmorType> AllowedArmorTypes = new List<ArmorType>();
        public readonly List<WeaponType> AllowedWeaponTypes = new List<WeaponType>();
        public const double BaseDamage = 1.0;

        public Dictionary<SlotType, Item?> Equipment { get; } = new()
    {
        {SlotType.Weapon, null},
        {SlotType.Head, null},
        {SlotType.Body, null},
        {SlotType.Legs, null}
    };

        /// <summary>
        /// An Abstract method that calculates damage that character can deal.
        /// </summary>
        /// <returns>Amount damage to deal.</returns>
        public abstract double CalculateDamage();


        /// <summary>
        /// A method that increases the character's level by the number provided. By default, increase by 1.
        /// </summary>
        public virtual void LevelUp(int levels = 1)
        {
            if (levels < 1)
            {
                throw new InvalidCharacterLevelException("Invalid character level. Must be 1 or greater.");
            }

            // Increase character level
            Level += levels;
            // Increase character base attributes
            BasePrimaryAttributes += PrimaryAttributesFactory.GetLevelUpAttributes(Type) * levels;
        }


        /// <summary>
        /// Equips weapon in weapon slot of character.
        /// </summary>
        /// <param name="weapon">Weapon to equip.</param>
        public virtual string EquipWeapon(Weapon weapon)
        {
            if (!AllowedWeaponTypes.Contains(weapon.Type))
            {
                throw new InvalidWeaponException($"Invalid weapon. Class <{GetType().Name}> can't use <{weapon.Type}>.");
            }

            if (weapon.LevelRequired > Level)
            {
                throw new InvalidCharacterLevelException($"Invalid character level. Required character level - {weapon.LevelRequired}.");
            }

            Equipment[weapon.Slot] = weapon;

            return "New weapon equipped!";
        }


        /// <summary>
        /// Equips armor in armor slot of character.
        /// </summary>
        /// <param name="armor">Armor to equip.</param>
        public virtual string EquipArmor(Armor armor)
        {
            if (!AllowedArmorTypes.Contains(armor.Type))
            {
                throw new InvalidArmorException($"Invalid armor. Class <{GetType().Name}> can't use <{armor.Type}>.");
            }

            if (armor.LevelRequired > Level)
            {
                throw new InvalidCharacterLevelException($"Invalid character level. Required character level - {armor.LevelRequired}.");
            }

            Equipment[armor.Slot] = armor;

            return "New armor equipped!";
        }


        private PrimaryAttributes CalculateTotalAttributes()
        {
            PrimaryAttributes equippedArmorAttributes = new PrimaryAttributes();
            equippedArmorAttributes =
                (from slot in Equipment
                 where slot.Value != null && slot.Key != SlotType.Weapon
                 select (Armor)slot.Value)
                .Aggregate(equippedArmorAttributes, (current, currentSlot) => current + currentSlot.Attributes);

            return BasePrimaryAttributes + equippedArmorAttributes;
        }


        /// <summary>
        /// Displays character statistics to console.
        /// Statistics consist of character properties (Name, Level, Primary Attributes, Damage)
        /// </summary>
        public virtual void DisplayStats()
        {
            {
                StringBuilder stats = new StringBuilder();
                stats.AppendLine("Name: " + Name);
                stats.AppendLine("Level: " + Level);
                stats.AppendLine("Strength: " + TotalPrimaryAttributes.Strength);
                stats.AppendLine("Dexterity: " + TotalPrimaryAttributes.Dexterity);
                stats.AppendLine("Intelligence: " + TotalPrimaryAttributes.Intelligence);
                stats.AppendLine("Damage: " + CalculateDamage());
                Console.WriteLine(stats.ToString());
            }
        }
    }
}
