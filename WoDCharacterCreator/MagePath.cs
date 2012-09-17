using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum Arcana { Time, Fate, Space, Mind, Matter, Death, Forces, Prime, Life, Spirit }

namespace WoDCharacterCreator
{
    public class MagePath
    {
        public string name;
        public Arcana arcana1;
        public Arcana arcana2;
        public Arcana bad_arcana;
        public string bonus_attribute;

        public MagePath(string name, Arcana a1, Arcana a2, Arcana bad, string atb)
        {
            this.name = name;
            this.arcana1 = a1;
            this.arcana2 = a2;
            this.bad_arcana = bad;
            this.bonus_attribute = atb;
        }
    }
}
