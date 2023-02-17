using RPGHeroesAssignment.RPGCharacters.Characters;
using RPGHeroesAssignment.RPGCharacters.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGHeroesAssignment.RPGCharacters.Attributes;

public static class PrimaryAttributesFactory
{
    /// <summary>
    /// Gives base primary attributes for every character type.
    /// </summary>
    /// <param name="characterType">Type of character to get the attributes for.</param>
    /// <returns>New base primary attributes.</returns>
    /// <exception cref="InvalidCharacterException">If given character type was none of Mage, Ranger, Rogue, Warrior.</exception>
    public static PrimaryAttributes GetBaseAttributes(CharacterType characterType)
    {
        return characterType switch
        {
            CharacterType.Mage => new PrimaryAttributes { Strength = 1, Dexterity = 1, Intelligence = 8 },
            CharacterType.Ranger => new PrimaryAttributes { Strength = 1, Dexterity = 7, Intelligence = 1 },
            CharacterType.Rogue => new PrimaryAttributes { Strength = 2, Dexterity = 6, Intelligence = 1 },
            CharacterType.Warrior => new PrimaryAttributes { Strength = 5, Dexterity = 2, Intelligence = 1 },
            _ => throw new InvalidCharacterException($"Invalid character type - {characterType}.")
        };
    }

    /// <summary>
    /// Gives primary attributes for every character type, which are intended after leveling up the character.
    /// </summary>
    /// <param name="characterType">Type of character to get the attributes for.</param>
    /// <returns>New primary attributes, which are intended after leveling up the character.</returns>
    /// <exception cref="InvalidCharacterException">If given character type was none of Mage, Ranger, Rogue, Warrior.</exception>
    public static PrimaryAttributes GetLevelUpAttributes(CharacterType characterType)
    {
        return characterType switch
        {
            CharacterType.Mage => new PrimaryAttributes { Strength = 1, Dexterity = 1, Intelligence = 5 },
            CharacterType.Ranger => new PrimaryAttributes { Strength = 1, Dexterity = 5, Intelligence = 1 },
            CharacterType.Rogue => new PrimaryAttributes { Strength = 1, Dexterity = 4, Intelligence = 1 },
            CharacterType.Warrior => new PrimaryAttributes { Strength = 3, Dexterity = 2, Intelligence = 1 },
            _ => throw new InvalidCharacterException($"Invalid character type - {characterType}.")
        };
    }
}