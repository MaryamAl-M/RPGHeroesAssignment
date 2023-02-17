using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGHeroesAssignment.RPGCharacters.Exceptions;

public class InvalidCharacterLevelException : Exception
{
    public InvalidCharacterLevelException(string message) : base(message)
    {
    }

    public override string Message => "Invalid character level.";
}