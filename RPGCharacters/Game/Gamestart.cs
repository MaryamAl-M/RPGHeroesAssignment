using RPGHeroesAssignment.RPGCharacters.Attributes;
using RPGHeroesAssignment.RPGCharacters.Characters;
using RPGHeroesAssignment.RPGCharacters.Exceptions;
using RPGHeroesAssignment.RPGCharacters.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGHeroesAssignment.RPGCharacters.Game;

public class Gamestart
{
    private Character? PlayerCharacter { get; set; }

    /// <summary>
    /// Starts the game script:
    /// <list type="bullet">
    /// <item><description>the First thing the player see the main menu.</description></item>
    /// <item><description>If player pressed "Play", script goes to create character script. Where player can choose the type of the character</description></item>
    /// <item><description>Then player must pick a name for the character.</description></item>
    /// <item><description>Finally, player begins their story where they can "Do a quest", "Visit an armor shop", "Visit an weapon shop", "Check your stats", "Check your equipment","Exit game"</description></item>
    /// </list>
    /// </summary>
    public void StartNewGame()
    {
        RunMainMenu();
        CreateCharacter();
        PickCharacterName();
        BeginStory();
    }

    /// <summary>
    /// This is the first method that runs in the application. When the main menu runs the player can chose between the following options:
    /// Play - which starts a new game.
    /// About - Shows information about the developer.
    /// Exit - which exits the game. 
    /// </summary>
    private void RunMainMenu()
    {
        string title = "Welcome to RPG Hereos! Shall we play?\n" +
            "(To cycle through the options please use the arrow keys. To select an option press ENTER)";
        string[] options = { "Play", "About", "Exit" };
        ConsoleMenu mainMenu = new ConsoleMenu(title, options);
        int selectedOption = mainMenu.Run();

        switch (selectedOption)
        {
            case 0:
                return;
            case 1:
                DisplayAbout();
                RunMainMenu();
                break;
            case 2:
                ExitGame();
                break;
        }
    }

    /// <summary>
    /// This method exits the game and shows a farewell message. 
    /// </summary>
    private void ExitGame()
    {
        DisplayInfo("Thank you for this time! Until next time!");
        Environment.Exit(0);
    }

    /// <summary>
    /// Displays console screen with given text. Clears the console before displaying.
    /// </summary>
    /// <param name="text">Text to display.</param>
    private void DisplayInfo(string text)
    {
        Console.Clear();
        Console.WriteLine(text);
    }

    /// <summary>
    /// Displays a tip on the existing console screen and waits for player's confirmation by pressing any key.
    /// </summary>
    /// <param name="text">Tip text to display.</param>
    private void DisplayTip(string text = "")
    {
        Console.WriteLine(text);
        Console.WriteLine("\nPress anything to continue...");
        Console.ReadKey(true);
    }
    /// <summary>
    /// this is the about method that shows information about the developer and the purpose of this application.
    /// </summary>
    private void DisplayAbout()
    {
        DisplayInfo(
            "This game was created by Maryam Almashhadi." +
            "\nMade for Noroff C# course, Assignment #4."
            );
        DisplayTip();
    }

    /// <summary>
    /// This method helps the player create a new Character. The player can choose between between:
    /// <list type="bullet">
    /// <item><description>Mage</description></item>
    /// <item><description>Ranger</description></item>
    /// <item><description>Rogue</description></item>
    /// <item><description>Warrior</description></item>
    /// </list>
    /// </summary>
    /// <exception cref="InvalidCharacterException">If the player tries to create a character with a type that doesn't exist</exception>
    private void CreateCharacter()
    {
        string promt = "Let's create a character. Who are you?";
        string[] options = { "Mage", "Ranger", "Rogue", "Warrior" };
        ConsoleMenu characterCreationMenu = new ConsoleMenu(promt, options);
        int selectedOption = characterCreationMenu.Run();
        string playerChoice = options[selectedOption];
        if (!ConfirmChoice(playerChoice))
        {
            CreateCharacter();
        }

        PlayerCharacter = selectedOption switch
        {
            0 => CharacterFactory.CreateCharacter(CharacterType.Mage),
            1 => CharacterFactory.CreateCharacter(CharacterType.Ranger),
            2 => CharacterFactory.CreateCharacter(CharacterType.Rogue),
            3 => CharacterFactory.CreateCharacter(CharacterType.Warrior),
            _ => throw new InvalidCharacterException("Invalid character type. Can't create this character type.")
        };
    }

    /// <summary>
    /// Displays a console dialog message about given subject and player must either confirm or refuse their choice.
    /// <list type="bullet">
    /// <item><description>Press ENTER - confirm</description></item>
    /// <item><description>Any other key - refuse</description></item>
    /// </list>
    /// </summary>
    /// <param name="subject">String that describes what player should confirm.</param>
    /// <returns>The result of the player's choice. Return true if the player has confirmed the choice, or false if not.</returns>
    private bool ConfirmChoice(string subject)
    {
        DisplayInfo($"You chose '{subject}', are you sure?\nPress ENTER to confirm or any other key to refuse.");
        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
        ConsoleKey keyPressed = keyInfo.Key;

        return keyPressed == ConsoleKey.Enter;
    }

    /// <summary>
    /// Displays a console screen, where player can pick a name for their character.
    /// </summary>
    private void PickCharacterName()
    {
        DisplayInfo($"Ok, now you are a {PlayerCharacter?.Type}.\nCall your name.\n(Type your character name and press ENTER)");
        Console.CursorVisible = true;
        string? characterName = Console.ReadLine();
        if (!ConfirmChoice($"name - {characterName}"))
        {
            PickCharacterName();
        }

        PlayerCharacter!.Name = characterName!;
        DisplayInfo(
            $"Done! From now until the end of the days, the name of your {PlayerCharacter?.Type.ToString().ToLower()} is {characterName}.");
        DisplayTip();
    }

    /// <summary>
    /// Displays weapon shop menu, where player can pick a weapon to equip.
    /// Player can't equip unfamiliar weapon type or high level weapon.
    /// </summary>
    private void DisplayWeaponShop()
    {
        Weapon[] weapons = new[]
        {
            // Axes
            new Weapon("Old axe", 1, SlotType.Weapon, WeaponType.Axe, 15, 1.0),
            new Weapon("Rare axe", 5, SlotType.Weapon, WeaponType.Axe, 25, 0.9),
            new Weapon("Enchanted axe", 9, SlotType.Weapon, WeaponType.Axe, 35, 0.8),

            // Bows
            new Weapon("Old bow", 1, SlotType.Weapon, WeaponType.Bow, 20, 1.0),
            new Weapon("Rare bow", 5, SlotType.Weapon, WeaponType.Bow, 30, 1.5),
            new Weapon("Enchanted bow", 9, SlotType.Weapon, WeaponType.Bow, 40, 2.0),

            // Daggers
            new Weapon("Old dagger", 1, SlotType.Weapon, WeaponType.Dagger, 10, 2.0),
            new Weapon("Rare dagger", 5, SlotType.Weapon, WeaponType.Dagger, 15, 2.5),
            new Weapon("Enchanted dagger", 9, SlotType.Weapon, WeaponType.Dagger, 20, 3.0),

            // Hammers
            new Weapon("Old hammer", 1, SlotType.Weapon, WeaponType.Hammer, 30, 0.6),
            new Weapon("Rare hammer", 5, SlotType.Weapon, WeaponType.Hammer, 45, 0.7),
            new Weapon("Enchanted hammer", 9, SlotType.Weapon, WeaponType.Hammer, 60, 0.75),

            // Staff
            new Weapon("Old staff", 1, SlotType.Weapon, WeaponType.Staff, 30, 1.0),
            new Weapon("Rare staff", 5, SlotType.Weapon, WeaponType.Staff, 40, 1.0),
            new Weapon("Enchanted staff", 9, SlotType.Weapon, WeaponType.Staff, 50, 1.0),

            // Swords
            new Weapon("Old sword", 1, SlotType.Weapon, WeaponType.Sword, 22, 1.2),
            new Weapon("Rare sword", 5, SlotType.Weapon, WeaponType.Sword, 42, 1.5),
            new Weapon("Enchanted sword", 9, SlotType.Weapon, WeaponType.Sword, 62, 2.0),

            // Wand
            new Weapon("Old wand", 1, SlotType.Weapon, WeaponType.Wand, 9, 2.0),
            new Weapon("Rare wand", 5, SlotType.Weapon, WeaponType.Wand, 14, 2.5),
            new Weapon("Enchanted wand", 9, SlotType.Weapon, WeaponType.Wand, 19, 3.0),
        };

        string promt = "This is the WEAPON SHOP!\n"
                       + "(Use the arrow keys to cycle through weapons and press ENTER to equip a weapon)\n"
                       + $"    |{"Name",20}|{"Type",10}|{"LevelRequired",17}|{"Damage",10}|{"AttackSpeed",15}|";
        string[] options = new string[weapons.Length + 1];

        foreach (var (weapon, index) in weapons.Select((value, index) => (value, index)))
        {
            options[index] =
                $"|{weapon.Name,20}|{weapon.Type,10}|{weapon.LevelRequired,17}|{weapon.Damage,10}|{weapon.AttackSpeed,15}|";
        }

        options[weapons.Length] = "Leave the shop";
        ConsoleMenu weaponShopMenu = new ConsoleMenu(promt, options);
        int selectedOption = weaponShopMenu.Run();
        if (selectedOption == weapons.Length)
        {
            return;
        }

        Weapon selectedWeapon = weapons[selectedOption];

        try
        {
            PlayerCharacter!.EquipWeapon(selectedWeapon);
            DisplayInfo($"You equipped {selectedWeapon.Name}! Looks good.");
        }
        catch (InvalidWeaponException e)
        {
            DisplayInfo(e.Message + $"\nYou can use {string.Join(", ", PlayerCharacter!.AllowedWeaponTypes)}.");
        }
        catch (InvalidCharacterLevelException e)
        {
            DisplayInfo(e.Message +
                        $"\nThis weapon has level {selectedWeapon.LevelRequired} and your level is {PlayerCharacter!.Level}. Do some quests to gain level.");
        }

        DisplayTip();
        DisplayWeaponShop();
    }

    /// <summary>
    /// Displays armor shop menu, where player can pick an armor to equip.
    /// Player can't equip unfamiliar armor type or high level armor.
    /// </summary>
    private void DisplayArmorShop()
    {
        Armor[] armors = new[]
        {
            // Cloth
            new Armor("Old hood", 1, SlotType.Head, ArmorType.Cloth, new PrimaryAttributes() {Intelligence = 1}),
            new Armor("Ancient cap", 5, SlotType.Head, ArmorType.Cloth, new PrimaryAttributes() {Intelligence = 5}),
            new Armor("Enchanted hat", 9, SlotType.Head, ArmorType.Cloth, new PrimaryAttributes() {Intelligence = 9}),

            new Armor("Old dress", 1, SlotType.Body, ArmorType.Cloth, new PrimaryAttributes() {Intelligence = 1}),
            new Armor("Ancient mantle", 5, SlotType.Body, ArmorType.Cloth, new PrimaryAttributes() {Intelligence = 5}),
            new Armor("Enchanted robe", 9, SlotType.Body, ArmorType.Cloth, new PrimaryAttributes() {Intelligence = 9}),

            new Armor("Old pants", 1, SlotType.Legs, ArmorType.Cloth, new PrimaryAttributes() {Intelligence = 1}),
            new Armor("Ancient pants", 5, SlotType.Legs, ArmorType.Cloth, new PrimaryAttributes() {Intelligence = 5}),
            new Armor("Enchanted pants", 9, SlotType.Legs, ArmorType.Cloth, new PrimaryAttributes() {Intelligence = 9}),

            // Leather
            new Armor("Rawhide hood", 1, SlotType.Head, ArmorType.Leather, new PrimaryAttributes() {Dexterity = 2}),
            new Armor("Elven hood", 5, SlotType.Head, ArmorType.Leather, new PrimaryAttributes() {Dexterity = 6}),
            new Armor("Enchanted dragon hood", 9, SlotType.Head, ArmorType.Leather,
                new PrimaryAttributes() {Dexterity = 10}),

            new Armor("Rawhide chest", 1, SlotType.Body, ArmorType.Leather, new PrimaryAttributes() {Dexterity = 2}),
            new Armor("Elven sorcerer chest", 5, SlotType.Body, ArmorType.Leather,
                new PrimaryAttributes() {Dexterity = 6}),
            new Armor("Enchanted dragon chest", 9, SlotType.Body, ArmorType.Leather,
                new PrimaryAttributes() {Dexterity = 10}),

            new Armor("Rawhide pants", 1, SlotType.Legs, ArmorType.Leather, new PrimaryAttributes() {Dexterity = 2}),
            new Armor("Elven pants", 5, SlotType.Legs, ArmorType.Leather, new PrimaryAttributes() {Dexterity = 6}),
            new Armor("Enchanted dragon pants", 9, SlotType.Legs, ArmorType.Leather,
                new PrimaryAttributes() {Dexterity = 10}),

            // Mail
            new Armor("Copper hood", 1, SlotType.Head, ArmorType.Mail, new PrimaryAttributes() {Dexterity = 3}),
            new Armor("Iron hood", 5, SlotType.Head, ArmorType.Mail, new PrimaryAttributes() {Dexterity = 7}),
            new Armor("Enchanted steel hood", 9, SlotType.Head, ArmorType.Mail,
                new PrimaryAttributes() {Dexterity = 11}),

            new Armor("Copper chest", 1, SlotType.Body, ArmorType.Mail, new PrimaryAttributes() {Dexterity = 3}),
            new Armor("Iron chest", 5, SlotType.Body, ArmorType.Mail, new PrimaryAttributes() {Dexterity = 7}),
            new Armor("Enchanted steel chest", 9, SlotType.Body, ArmorType.Mail,
                new PrimaryAttributes() {Dexterity = 11}),

            new Armor("Copper pants", 1, SlotType.Legs, ArmorType.Mail, new PrimaryAttributes() {Dexterity = 3}),
            new Armor("Iron pants", 5, SlotType.Legs, ArmorType.Mail, new PrimaryAttributes() {Dexterity = 7}),
            new Armor("Enchanted steel pants", 9, SlotType.Legs, ArmorType.Mail,
                new PrimaryAttributes() {Dexterity = 11}),

            // Plate
            new Armor("Iron helmet", 1, SlotType.Head, ArmorType.Plate, new PrimaryAttributes() {Strength = 4}),
            new Armor("Corundum helmet", 5, SlotType.Head, ArmorType.Plate, new PrimaryAttributes() {Strength = 8}),
            new Armor("Enchanted steel helmet", 9, SlotType.Head, ArmorType.Plate,
                new PrimaryAttributes() {Strength = 12}),

            new Armor("Iron chest", 1, SlotType.Body, ArmorType.Plate, new PrimaryAttributes() {Strength = 4}),
            new Armor("Corundum chest", 5, SlotType.Body, ArmorType.Plate, new PrimaryAttributes() {Strength = 8}),
            new Armor("Enchanted steel chest", 9, SlotType.Body, ArmorType.Plate,
                new PrimaryAttributes() {Strength = 12}),

            new Armor("Iron pants", 1, SlotType.Legs, ArmorType.Plate, new PrimaryAttributes() {Strength = 4}),
            new Armor("Corundum pants", 5, SlotType.Legs, ArmorType.Plate, new PrimaryAttributes() {Strength = 8}),
            new Armor("Enchanted steel pants", 9, SlotType.Legs, ArmorType.Plate,
                new PrimaryAttributes() {Strength = 12}),
        };

        string promt = "Welcome to the ARMOR SHOP!\n"
                       + "(Use the arrow keys to cycle through armors and press ENTER to equip an armor)\n"
                       + $"    |{"Name",25}|{"Type",10}|{"Slot",10}|{"LevelRequired",17}|{"Strength",13}|{"Dexterity",13}|{"Intelligence",13}|";
        string[] options = new string[armors.Length + 1];

        foreach (var (armor, index) in armors.Select((value, index) => (value, index)))
        {
            options[index] =
                $"|{armor.Name,25}|{armor.Type,10}|{armor.Slot,10}|{armor.LevelRequired,17}|{armor.Attributes.Strength,13}|{armor.Attributes.Dexterity,13}|{armor.Attributes.Intelligence,13}|";
        }

        options[armors.Length] = "Leave the shop";
        ConsoleMenu armorShopMenu = new ConsoleMenu(promt, options);
        int selectedOption = armorShopMenu.Run();
        if (selectedOption == armors.Length)
        {
            return;
        }

        Armor selectedArmor = armors[selectedOption];

        try
        {
            PlayerCharacter!.EquipArmor(selectedArmor);
            DisplayInfo($"You equipped {selectedArmor.Name}! Looks good.");
        }
        catch (InvalidArmorException e)
        {
            DisplayInfo(e.Message + $"\nYou can use {string.Join(", ", PlayerCharacter!.AllowedArmorTypes)}.");
        }
        catch (InvalidCharacterLevelException e)
        {
            DisplayInfo(e.Message +
                        $"\nThis armor has level {selectedArmor.LevelRequired} and your level is {PlayerCharacter!.Level}. Do some quests to gain level.");
        }

        DisplayTip();
        DisplayArmorShop();
    }


    /// <summary>
    /// Displays a screen with the player's equipment.
    /// Player can see the stats of equipped items.
    /// </summary>
    private void DisplayEquipment()
    {
        Console.WriteLine();
        Console.WriteLine("ARMOR:");
        Console.WriteLine($"|{"Slot",10}|{"Name",30}|{"Strength",15}|{"Dexterity",15}|{"Intelligence",15}|");
        Console.WriteLine(new string('-', 91));
        foreach (var slot in PlayerCharacter!.Equipment)
        {
            if (slot.Value != null && slot.Key != SlotType.Weapon)
            {
                Armor armor = (Armor)slot.Value;
                Console.WriteLine(
                    $"|{slot.Key,10}|{armor.Name,30}|{armor.Attributes.Strength,15}|{armor.Attributes.Dexterity,15}|{armor.Attributes.Intelligence,15}|");
            }
            else if (slot.Key != SlotType.Weapon)
            {
                Console.WriteLine($"|{slot.Key,10}|{"(empty)",30}|{"(empty)",15}|{"(empty)",15}|{"(empty)",15}|");
            }
        }

        Console.WriteLine();
        Console.WriteLine("WEAPON:");
        Console.WriteLine($"|{"Slot",10}|{"Name",30}|{"Damage",15}|{"AttackSpeed",15}|");
        Console.WriteLine(new string('-', 75));
        Weapon? weapon = (Weapon)PlayerCharacter.Equipment[SlotType.Weapon];
        Console.WriteLine(weapon != null
            ? $"|{SlotType.Weapon,10}|{weapon.Name,30}|{weapon.Damage,15}|{weapon.AttackSpeed,15}|"
            : $"|{SlotType.Weapon,10}|{"(empty)",30}|{"(empty)",15}|{"(empty)",15}|");
    }


    /// <summary>
    /// Runs story menu, where player can select one of the following actions:
    /// <list type="bullet">
    /// <item><description>Do a quest - imitates completing a quest, actually increasing the character's level by 1.</description></item>
    /// <item><description>Visit an armor shop - displays a shop with armors that can be picked up and equipped.</description></item>
    /// <item><description>Visit an weapon shop - displays a shop with weapons that can be picked up and equipped.</description></item>
    /// <item><description>Check your stats - displays player statistics: Type, Name, Level, Attributes.</description></item>
    /// <item><description>Check your equipment - displays player's equipment, what armors and weapon are equipped and their stats.</description></item>
    /// <item><description>Exit game - stop and exit the game with farewell message.</description></item>
    /// </list>
    /// </summary>
    private void BeginStory()
    {
        while (true)
        {
            string promt = "Your journey begins!\n" +
                           "(Use the arrow keys to cycle through options and press ENTER to select an option)";
            string[] options =
            {
                "Do a quest", "Visit an armor shop", "Visit an weapon shop", "Check your stats", "Check your equipment",
                "Exit game"
            };
            ConsoleMenu storyMenu = new ConsoleMenu(promt, options);
            int selectedOption = storyMenu.Run();

            switch (selectedOption)
            {
                case 0:
                    PlayerCharacter!.LevelUp();
                    DisplayInfo("You DONE a QUEST!\nLevel up!");
                    DisplayTip();
                    break;
                case 1:
                    DisplayArmorShop();
                    break;
                case 2:
                    DisplayWeaponShop();
                    break;
                case 3:
                    DisplayInfo($"{PlayerCharacter!.Type.ToString()} - statistics:");
                    PlayerCharacter?.DisplayStats();
                    DisplayTip();
                    break;
                case 4:
                    DisplayInfo($"{PlayerCharacter!.Type.ToString()} - equipment:");
                    DisplayEquipment();
                    DisplayTip();
                    break;
                case 5:
                    if (ConfirmChoice(options[selectedOption]))
                    {
                        ExitGame();
                    }
                    break;
            }
            BeginStory();
            return;
        }

    }
}
    
