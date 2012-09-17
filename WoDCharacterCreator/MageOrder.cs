using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WoDCharacterCreator
{
    public class MageOrder
    {
        public string name;
        public string skill1;
        public string skill2;
        public string skill3;

        public MageOrder(string nm, string sk1, string sk2, string sk3)
        {
            this.name = nm;
            this.skill1 = sk1;
            this.skill2 = sk2;
            this.skill3 = sk3;
        }
    }
}
