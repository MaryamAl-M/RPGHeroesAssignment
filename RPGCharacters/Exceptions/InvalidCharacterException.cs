using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGHeroesAssignment.RPGCharacters.Exceptions;

public class InvalidCharacterException : Exception
{

    public InvalidCharacterException(string message) : base(message)
    {
    }

    public override string Message => "Invalid character type.";
}
