using RPGHeroesAssignment.RPGCharacters.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGHeroesAssignment.RPGCharacters.Items
{
    public abstract class Item
    {
        public string Name { get; init; } = "Item";
        public int LevelRequired { get; protected init; } = 1;
        public SlotType Slot { get; protected init; }

    }
}
