
using RPGHeroesAssignment.RPGCharacters.Game;
using System.Threading;

namespace RPGHeroesAssignment;

public static class Program
{
    public static void Main(string[] args)
    {
        Gamestart game = new Gamestart();
        game.StartNewGame();
    }
}