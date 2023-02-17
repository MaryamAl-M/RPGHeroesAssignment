using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGHeroesAssignment.RPGCharacters.Game;

public class ConsoleMenu
{

    private int _selectedIndex;

    private readonly string[] _options;
    private readonly string _title;

    public ConsoleMenu(string title, string[] options)
    {
        _title = title;
        _options = options;
        _selectedIndex = 0;
    }

    /// <summary>
    /// Displays available options for selection, highlights the active option in white. Also deactivates console cursor.
    /// </summary>
    private void DisplayOptions()
    {
        Console.WriteLine(_title);
        for (int i = 0; i < _options.Length; i++)
        {
            string currentOption = _options[i];
            string prefix;
            if (i == _selectedIndex)
            {
                prefix = "*";
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                prefix = " ";
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
            }

            Console.WriteLine($"{prefix} [ {currentOption} ]");
        }
        Console.WriteLine();
        Console.ResetColor();
        Console.CursorVisible = false;
    }

    public int Run()
    {
        ConsoleKey keyPressed;

        do
        {
            Console.Clear();
            DisplayOptions();

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            keyPressed = keyInfo.Key;

            // Update selected index based on error
            if (keyPressed == ConsoleKey.UpArrow)
            {
                _selectedIndex--;
                if (_selectedIndex == -1)
                {
                    _selectedIndex = _options.Length - 1;
                }
            }
            else if (keyPressed == ConsoleKey.DownArrow)
            {
                _selectedIndex++;
                if (_selectedIndex == _options.Length)
                {
                    _selectedIndex = 0;
                }
            }
        } while (keyPressed != ConsoleKey.Enter);

        return _selectedIndex;
    }
}
   

