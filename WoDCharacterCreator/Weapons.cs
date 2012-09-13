using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WoDCharacterCreator
{
    enum DamageType { Bashing, Leathal, Aggriaged };
    class Weapon
    {
        public string name;
        public int damage;
        public int size;
        public int cost;
    }

    class MeleeWeapon : Weapon
    {
        public string special;
        public DamageType damage_type;

        public void setType(string name)
        {
            if (name == "b")
            {
                this.damage_type = DamageType.Bashing;
            }
            if (name == "l")
            {
                this.damage_type = DamageType.Leathal;
            }
            if (name == "a")
            {
                this.damage_type = DamageType.Aggriaged;
            }
        }
    }

    class RangedWeapon : Weapon
    {
        public int range;
        public int clip_size;
        public int strength;

        public bool automatic = false;

        public int short_range
        {
            get { return range; }
        }
        public int medium_range
        {
            get { return range*2; }
        }
        public int long_range
        {
            get { return range*4; }
        }

        public RangedWeapon(string name)
        {
            this.name = name;
        }
    }
}
