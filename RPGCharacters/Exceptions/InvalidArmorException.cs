using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGHeroesAssignment.RPGCharacters.Exceptions;

public class InvalidArmorException : Exception
{
    public InvalidArmorException(string message) : base(message)
    {
    }

    public override string Message => "Invalid armor type. This character can't use it.";
}