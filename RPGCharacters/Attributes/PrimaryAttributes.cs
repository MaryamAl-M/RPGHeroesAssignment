using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGHeroesAssignment.RPGCharacters.Attributes;

public class PrimaryAttributes
{
        public int Dexterity { get; set; }
        public int Intelligence { get; set; }
        public int Strength { get; set; }

        public PrimaryAttributes(int strength = 0, int dexterity = 0, int intelligence = 0)
        {
            Strength = strength;
            Dexterity = dexterity;
            Intelligence = intelligence;
        }

        /// <summary>
        /// Addition two primary attributes with + operator.
        /// </summary>
        /// <param name="left">Left operand of addition.</param>
        /// <param name="right">Right operand of addition.</param>
        /// <returns>New summarized primary attributes.</returns>
        public static PrimaryAttributes operator +(PrimaryAttributes left, PrimaryAttributes right)
        {
            return new PrimaryAttributes(
                left.Strength + right.Strength,
                left.Dexterity + right.Dexterity,
                left.Intelligence + right.Intelligence
            );
        }

    /// <summary>
    /// Multiplying primary attributes with integer as right operand.
    /// </summary>
    /// <param name="left">Left operand of multiplication.</param>
    /// <param name="right">Right operand of multiplication.</param>
    /// <returns>New multiplied primary attributes.</returns>
    public static PrimaryAttributes operator *(PrimaryAttributes left, int right)
    {
        return new PrimaryAttributes(
            left.Strength * right,
            left.Dexterity * right,
            left.Intelligence * right
        );

    }
}       
