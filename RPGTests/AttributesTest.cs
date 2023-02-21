using RPGHeroesAssignment.RPGCharacters.Attributes;
using RPGHeroesAssignment.RPGCharacters.Characters;

namespace RPGTests;
public class AttributesTest
    {
    #region Base attributes creation for each character type

    [Fact]
    public void GetBaseAttributes_GetBaseAttributesForMage_ShouldReturnBaseAttributesForMage()
    {
        // Arrange
        PrimaryAttributes expected = new PrimaryAttributes() { Strength = 1, Dexterity = 1, Intelligence = 8 };

        // Act
        PrimaryAttributes actual = PrimaryAttributesFactory.GetBaseAttributes(CharacterType.Mage);

        // Assert
        Assert.Equal(expected.Strength, actual.Strength);
        Assert.Equal(expected.Dexterity, actual.Dexterity);
        Assert.Equal(expected.Intelligence, actual.Intelligence);
    }

    [Fact]
    public void GetBaseAttributes_GetBaseAttributesForRanger_ShouldReturnBaseAttributesForRanger()
    {
        // Arrange
        PrimaryAttributes expected = new PrimaryAttributes() { Strength = 1, Dexterity = 7, Intelligence = 1 };

        // Act
        PrimaryAttributes actual = PrimaryAttributesFactory.GetBaseAttributes(CharacterType.Ranger);

        // Assert
        Assert.Equal(expected.Strength, actual.Strength);
        Assert.Equal(expected.Dexterity, actual.Dexterity);
        Assert.Equal(expected.Intelligence, actual.Intelligence);
    }

    [Fact]
    public void GetBaseAttributes_GetBaseAttributesForRogue_ShouldReturnBaseAttributesForRogue()
    {
        // Arrange
        PrimaryAttributes expected = new PrimaryAttributes() { Strength = 2, Dexterity = 6, Intelligence = 1 };

        // Act
        PrimaryAttributes actual = PrimaryAttributesFactory.GetBaseAttributes(CharacterType.Rogue);

        // Assert
        Assert.Equal(expected.Strength, actual.Strength);
        Assert.Equal(expected.Dexterity, actual.Dexterity);
        Assert.Equal(expected.Intelligence, actual.Intelligence);
    }

    [Fact]
    public void GetBaseAttributes_GetBaseAttributesForWarrior_ShouldReturnBaseAttributesForWarrior()
    {
        // Arrange
        PrimaryAttributes expected = new PrimaryAttributes() { Strength = 5, Dexterity = 2, Intelligence = 1 };

        // Act
        PrimaryAttributes actual = PrimaryAttributesFactory.GetBaseAttributes(CharacterType.Warrior);

        // Assert
        Assert.Equal(expected.Strength, actual.Strength);
        Assert.Equal(expected.Dexterity, actual.Dexterity);
        Assert.Equal(expected.Intelligence, actual.Intelligence);
    }

    #endregion
}

