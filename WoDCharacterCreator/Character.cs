﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WoDCharacterCreator
{
    class Character
    {
        string name = "";

        //attributes
        public int intelligence = 1;
        public int wits = 1;
        public int resolve = 1;
        public int strength = 1;
        public int dexterity = 1;
        public int stamina = 1;
        public int presence = 1;
        public int manipulation = 1;
        public int composure = 1;

        public int init_mod = 0;
        public int speed_mod = 0;
        public int speed_bonus = 5;
        
        public int size = 5;

        public int health
        {
            get { return stamina + size; }
        }

        public int defense
        {
            get { return Math.Min(dexterity, wits); }
        }

        public int initative
        {
            get { return dexterity + composure + init_mod; }
        }

        public int speed
        {
            get { return strength + dexterity + speed_bonus + speed_mod; }
        }

        public int willpower
        {
            get { return resolve + composure; }
        }

        public Dictionary<string, PlayerSkill> skill_list;

        public int attribute_mental_dots = 0;
        public int attribute_physical_dots = 0;
        public int attribute_social_dots = 0;

        public int attribute_spent_mental_dots = 0;
        public int attribute_spent_physical_dots = 0;
        public int attribute_spent_social_dots = 0;

        public int skill_mental_dots = 0;
        public int skill_physical_dots = 0;
        public int skill_social_dots = 0;

        public int skill_spent_mental_dots = 0;
        public int skill_spent_physical_dots = 0;
        public int skill_spent_social_dots = 0;

        public Character() 
        {
            skill_list = new Dictionary<string, PlayerSkill>();
        }

        public object GetValue(string name)
        {
            return this.GetType().GetField(name).GetValue(this);
        }
    }
}