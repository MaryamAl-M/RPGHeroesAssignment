using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using RPGHeroesAssignment;
using RPGHeroesAssignment.RPGCharacters.Characters;
using RPGHeroesAssignment.RPGCharacters.Attributes;
using RPGHeroesAssignment.RPGCharacters.Exceptions;
using RPGHeroesAssignment.RPGCharacters.Items;

namespace RPGTests;

public class CharacterTest
{
    #region User can create a character of type Mage, Warrior, Ranger, Rouger

    [Fact]
    public void CreateCharacter_CreateCharacterMage_ShouldReturnCharacterWithTypeMage()
    {
        // Arrange
        Character newCharacterMage = CharacterFactory.CreateCharacter(CharacterType.Mage);

        // Assert
        Assert.IsType<Mage>(newCharacterMage);
    }

    [Fact]
    public void CreateCharacter_CreateCharacterRogue_ShouldReturnCharacterWithTypeRogue()
    {
        // Arrange
        Character newCharacterRogue = CharacterFactory.CreateCharacter(CharacterType.Rogue);

        // Assert
        Assert.IsType<Rogue>(newCharacterRogue);
    }


    [Fact]
    public void CreateCharacter_CreateCharacterRanger_ShouldReturnCharacterWithTypeRanger()
    {
        // Arrange
        Character newCharacterRanger = CharacterFactory.CreateCharacter(CharacterType.Ranger);

        // Assert
        Assert.IsType<Ranger>(newCharacterRanger);
    }

    [Fact]
    public void CreateCharacter_CreateCharacterWarrior_ShouldReturnCharacterWithTypeWarrior()
    {
        // Arrange
        Character newCharacterWarrior = CharacterFactory.CreateCharacter(CharacterType.Warrior);

        // Assert
        Assert.IsType<Warrior>(newCharacterWarrior);
    }
    #endregion



    #region Character has base primary attributes when created

    [Theory]
    [InlineData(CharacterType.Mage)]
    [InlineData(CharacterType.Ranger)]
    [InlineData(CharacterType.Rogue)]
    [InlineData(CharacterType.Warrior)]
    public void Constructor_CharacterCreated_ShouldHaveBasePrimaryAttributesForItsType(CharacterType characterType)
    {
        // Arrange
        PrimaryAttributes basePrimaryAttributes = PrimaryAttributesFactory.GetBaseAttributes(characterType);
        PrimaryAttributes expected = basePrimaryAttributes;

        // Act
        Character newCharacter = CharacterFactory.CreateCharacter(characterType);
        PrimaryAttributes actual = newCharacter.BasePrimaryAttributes;

        // Assert
        Assert.Equal(expected.Strength, actual.Strength);
        Assert.Equal(expected.Dexterity, actual.Dexterity);
        Assert.Equal(expected.Intelligence, actual.Intelligence);
    }

    #endregion


    #region Character can level up

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public void LevelUp_IncreaseCharactersLevelWithPositiveInteger_ShouldIncreaseCharactersLevel(int levelsToUp)
    {
        // Arrange
        Character ranger = CharacterFactory.CreateCharacter(CharacterType.Ranger);
        int CHARACTER_START_LEVEL = 1;
        int expected = levelsToUp + CHARACTER_START_LEVEL;

        // Act
        ranger.LevelUp(levelsToUp);
        int actual = ranger.Level;

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void LevelUp_IncreaseCharactersLevelWithZeroOrNegativeInteger_ShouldThrowInvalidCharacterLevelException(
        int levelsToUp)
    {
        // Arrange
        Character mage = CharacterFactory.CreateCharacter(CharacterType.Mage);

        // Act and Assert
        Assert.Throws<InvalidCharacterLevelException>(() => { mage.LevelUp(levelsToUp); });
    }

    #endregion



    #region Character attributes increase when level up

    [Theory]
    [InlineData(CharacterType.Mage)]
    [InlineData(CharacterType.Ranger)]
    [InlineData(CharacterType.Rogue)]
    [InlineData(CharacterType.Warrior)]
    public void
        LevelUp_CharacterLevelUpBy1Level_ShouldIncreaseCharacterBasePrimaryAttributesAccordingToCharacterTypeAnd1LevelUp(
            CharacterType characterType)
    {
        // Arrange
        Character character = CharacterFactory.CreateCharacter(characterType);
        PrimaryAttributes expected = PrimaryAttributesFactory.GetBaseAttributes(characterType) +
                                     PrimaryAttributesFactory.GetLevelUpAttributes(characterType);
        // Act
        character.LevelUp();
        PrimaryAttributes actual = character.BasePrimaryAttributes;

        // Assert
        Assert.Equal(expected.Strength, actual.Strength);
        Assert.Equal(expected.Dexterity, actual.Dexterity);
        Assert.Equal(expected.Intelligence, actual.Intelligence);
    }

    [Theory]
    [InlineData(2)]
    [InlineData(3)]
    public void
        LevelUp_MageLevelUpBySeveralLevels_ShouldIncreaseCharacterBasePrimaryAttributesAccordingToCharacterTypeAndSeveralLevelUps(
            int levels)
    {
        // Arrange
        Character mage = CharacterFactory.CreateCharacter(CharacterType.Mage);
        PrimaryAttributes expected = PrimaryAttributesFactory.GetBaseAttributes(CharacterType.Mage) +
                                     PrimaryAttributesFactory.GetLevelUpAttributes(CharacterType.Mage) * levels;
        // Act
        mage.LevelUp(levels);
        PrimaryAttributes actual = mage.BasePrimaryAttributes;

        // Assert
        Assert.Equal(expected.Strength, actual.Strength);
        Assert.Equal(expected.Dexterity, actual.Dexterity);
        Assert.Equal(expected.Intelligence, actual.Intelligence);
    }

    #endregion

    #region Character total attributes calculation

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public void
        GetTotalPrimaryAttributes_GetTotalPrimaryAttributesOfMageWithDifferentLevel_ShouldReturnTotalAttributesAsSumOfBaseAndLevelUpAttributes(
            int levelsToUp)
    {
        // Arrange
        Character mage = CharacterFactory.CreateCharacter(CharacterType.Mage);
        PrimaryAttributes expected = PrimaryAttributesFactory.GetBaseAttributes(CharacterType.Mage) +
                                     PrimaryAttributesFactory.GetLevelUpAttributes(CharacterType.Mage) * levelsToUp;

        // Act
        mage.LevelUp(levelsToUp);
        PrimaryAttributes actual = mage.TotalPrimaryAttributes;

        // Assert
        Assert.Equal(expected.Strength, actual.Strength);
        Assert.Equal(expected.Dexterity, actual.Dexterity);
        Assert.Equal(expected.Intelligence, actual.Intelligence);
    }

    [Fact]
    public void
        GetTotalPrimaryAttributes_GetTotalPrimaryAttributesOfMageLevel2WithArmorEquipped_ShouldReturnTotalAttributesAsSumOfBasePlusLevelUpPlusArmorAttributes()
    {
        // Arrange
        Character mage = CharacterFactory.CreateCharacter(CharacterType.Mage);
        Armor armor = new Armor("Magic Hat", 1, SlotType.Head, ArmorType.Cloth,
            new PrimaryAttributes() { Strength = 0, Dexterity = 0, Intelligence = 10 });
        PrimaryAttributes expected = PrimaryAttributesFactory.GetBaseAttributes(CharacterType.Mage) +
                                     PrimaryAttributesFactory.GetLevelUpAttributes(CharacterType.Mage) +
                                     armor.Attributes;

        // Act
        mage.LevelUp();
        mage.EquipArmor(armor);
        PrimaryAttributes actual = mage.TotalPrimaryAttributes;

        // Assert
        Assert.Equal(expected.Strength, actual.Strength);
        Assert.Equal(expected.Dexterity, actual.Dexterity);
        Assert.Equal(expected.Intelligence, actual.Intelligence);
    }

    #endregion

}


