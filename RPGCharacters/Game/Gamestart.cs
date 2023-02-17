using RPGHeroesAssignment.RPGCharacters.Attributes;
using RPGHeroesAssignment.RPGCharacters.Characters;
using RPGHeroesAssignment.RPGCharacters.Exceptions;
using RPGHeroesAssignment.RPGCharacters.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGHeroesAssignment.RPGCharacters.Game
{
    internal class Gamestart
    {
        private Character? PlayerCharacter { get; set; }
        public void StartNewGame()
        {
            RunMainMenu();
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

    }
}
