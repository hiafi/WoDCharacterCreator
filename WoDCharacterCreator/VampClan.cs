using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WoDCharacterCreator
{
    public class VampClan
    {
        public string name;
        public string description;
        public string attr1;
        public string attr2;
        public Discipline disc1;
        public Discipline disc2;
        public Discipline disc3;

        public VampClan(string nm, string atr1, string atr2, Discipline d1, Discipline d2, Discipline d3, string desc)
        {
            name = nm;
            description = desc;
            attr1 = atr1;
            attr2 = atr2;
            disc1 = d1;
            disc2 = d2;
            disc3 = d3;
        }
    }
}
