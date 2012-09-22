using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WoDCharacterCreator
{
    public class VampCovenant
    {
        public string name;
        public string advantage;
        public string description;

        public VampCovenant(string nm, string adv, string desc)
        {
            name = nm;
            advantage = adv;
            description = desc;
        }
    }
}
