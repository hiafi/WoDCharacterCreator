using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WoDCharacterCreator
{
    class Armor
    {
        public string name;
        public int armor_rating_general;
        public int armor_rating_firearm;
        public int strength;
        public int defense_penalty;
        public int speed_penalty;
        public int cost;

        public int melee_rating
        {
            get { return this.armor_rating_general; }
        }
    }
}
