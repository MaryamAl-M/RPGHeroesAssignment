using RPGHeroesAssignment;
using RPGHeroesAssignment.RPGCharacters.Characters;
using RPGHeroesAssignment.RPGCharacters.Items;

namespace RPGTests;

public class Weapontest
{
    #region Weapon damage per second calculation

    [Theory]
    [InlineData("Axe", 1, SlotType.Weapon, WeaponType.Axe, 2, 1.0, 2.0)]
    [InlineData("Bow", 1, SlotType.Weapon, WeaponType.Bow, 5, 1.5, 7.5)]
    [InlineData("Dagger", 1, SlotType.Weapon, WeaponType.Dagger, 3, 1.3, 3.9)]
    [InlineData("Hammer", 1, SlotType.Weapon, WeaponType.Hammer, 5, 0.5, 2.5)]
    public void GetDamagePerSecond_GetDamagePerSecondOfWeaponAxe_ShouldReturnDamagePerSecond(string name,
        int levelRequired, SlotType slot, WeaponType type, int damage, double attackSpeed, double damagePerSecond)
    {
        // Arrange 
        Weapon weapon = new Weapon(name, levelRequired, slot, type, damage, attackSpeed);
        double expected = damagePerSecond;

        // Act
        double actual = weapon.GetDamagePerSecond();

        // Assert
        Assert.Equal(expected, actual);
    }

    #endregion
}
