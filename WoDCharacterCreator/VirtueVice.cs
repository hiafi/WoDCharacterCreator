using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WoDCharacterCreator
{
    class VirtueVice
    {
        public string name = "";
        public string effect = "";
        public string desc = "";

        public VirtueVice(string name, string effect, string description)
        {
            this.name = name;
            this.effect = effect;
            this.desc = description;
        }
    }
}
