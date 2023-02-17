using RPGHeroesAssignment.RPGCharacters.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGHeroesAssignment.RPGCharacters.Characters;
public static class CharacterFactory
{
    /// <summary>
    /// Create a character of a certain type e.g Mage, Ranger, Rogue, Warrior.
    /// </summary>
    /// <param name="characterType">Type of character to create.</param>
    /// <returns>New character.</returns>
    /// <exception cref="InvalidCharacterException">If given character type was none of Mage, Ranger, Rogue, Warrior.</exception>
    public static Character CreateCharacter(CharacterType characterType)
    {
        return characterType switch
        {
            CharacterType.Mage => new Mage(),
            CharacterType.Ranger => new Ranger(),
            CharacterType.Rogue => new Rogue(),
            CharacterType.Warrior => new Warrior(),
            _ => throw new InvalidCharacterException($"Invalid character type - {characterType}.")
        };
    }
}