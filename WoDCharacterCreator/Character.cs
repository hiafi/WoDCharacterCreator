using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace WoDCharacterCreator
{
    public class Character
    {
        public string name = "";

        public MageTemplate mage;
        public VampireTemplate vampire;


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

        public VirtueVice vice = null;
        public VirtueVice virtue = null;

        List<Tuple<Skill, string>> specialty;

        public bool is_mage = false;
        public bool is_vamp = false;
        
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
        public List<Tuple<int, Merit>> merit_list;

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

        public int merits_dots = 7;
        public int merits_spent = 0;

        public Character() 
        {
            skill_list = new Dictionary<string, PlayerSkill>();
            merit_list = new List<Tuple<int, Merit>>();
            specialty = new List<Tuple<Skill, string>>();
            mage = new MageTemplate();
            vampire = new VampireTemplate();
        }

        public object GetValue(string name)
        {
            return this.GetType().GetField(name).GetValue(this);
        }

        /// <summary>
        /// Create a basic text dump of the character
        /// </summary>
        /// <param name="filename">Name of the file</param>
        public void CreateText(string filename)
        {
            StreamWriter file = new StreamWriter(filename);
            file.WriteLine(this.name);
            file.WriteLine(String.Format("Intelligence: {0}", this.intelligence));
            file.WriteLine(String.Format("Wits: {0}", this.wits));
            file.WriteLine(String.Format("Resolve: {0}", this.resolve));
            file.WriteLine(String.Format("Strength: {0}", this.strength));
            file.WriteLine(String.Format("Dexterity: {0}", this.dexterity));
            file.WriteLine(String.Format("Stamina: {0}", this.stamina));
            file.WriteLine(String.Format("Presence: {0}", this.presence));
            file.WriteLine(String.Format("Manipulation: {0}", this.manipulation));
            file.WriteLine(String.Format("Composure: {0}", this.composure));
            file.WriteLine("");
            file.WriteLine(String.Format("Size: {0}", this.size));
            file.WriteLine(String.Format("Health: {0}", this.health));
            file.WriteLine(String.Format("Defense: {0}", this.defense));
            file.WriteLine(String.Format("Initative: {0}", this.initative));
            file.WriteLine(String.Format("Speed: {0}", this.speed));
            file.WriteLine(String.Format("Willpower: {0}", this.willpower));
            file.WriteLine("");
            file.WriteLine("Skills");

            foreach (KeyValuePair<string, PlayerSkill> skill in skill_list)
            {
                file.WriteLine(String.Format("{0}: {1}", skill.Value.name, skill.Value.rank));
            }
            foreach (Tuple<Skill, string> skill in specialty)
            {
                file.WriteLine(String.Format("Specialty {0}: {1}", skill.Item1.name, skill.Item2));
            }

            file.WriteLine("");
            file.WriteLine("Merits");

            foreach (Tuple<int, Merit> merit in merit_list)
            {
                if (merit.Item2.ranks == null)
                {
                    file.WriteLine(String.Format("{0}", merit.Item2.name));
                }
                else
                {
                    file.WriteLine(String.Format("{0} (Rank: {1})", merit.Item2.name, merit.Item1));
                }
            }

            file.WriteLine("");
            file.WriteLine(String.Format("Virtue: {0}", this.virtue.name));
            file.WriteLine(String.Format("Vice: {0}", this.vice.name));

            if (is_mage)
            {
                mage.CreateText(file);
            }

            if (is_vamp)
            {
                vampire.CreateText(file);
            }

            file.Close();
        }

        /// <summary>
        /// Create a PDF of the character
        /// </summary>
        /// <param name="filename">Name of the file</param>
        public void CreatePDF(string filename)
        {

        }
    }

    public class MageTemplate
    {
        public MagePath path;
        public MageOrder order = null;
        public int[] arcana;

        public List<MageSpell> rotes;

        public MageTemplate()
        {
            rotes = new List<MageSpell>();
            arcana = new int[Enum.GetValues(typeof(Arcana)).Length];
            for (int i = 0; i < arcana.Length; i++)
            {
                arcana[i] = 0;
            }
        }

        public void CreateText(StreamWriter file)
        {
            file.WriteLine("");
            file.WriteLine(String.Format("Mage Path: {0}", path.name));
            file.WriteLine(String.Format("Mage Order: {0}", order.name));
            file.WriteLine("");
            for (int i=0; i < arcana.Length; i++)
            {
                file.WriteLine(String.Format("{0}: {1}", Enum.GetName(typeof(Arcana), i), arcana[i]));
            }
            file.WriteLine("");
            file.WriteLine("Rotes");
            foreach (MageSpell spell in rotes)
            {
                file.WriteLine(String.Format("{0} ({1})", spell.name, Enum.GetName(typeof(Arcana), spell.arcana)));
            }
        }

        public void CreatePDF(string filename)
        {

        }
    }

    public class VampireTemplate
    {
        public VampClan clan;
        public VampCovenant covenant;
        public int[] discipline;

        public VampireTemplate()
        {
            discipline = new int[Enum.GetValues(typeof(Discipline)).Length];
            for (int i = 0; i < discipline.Length; i++)
            {
                discipline[i] = 0;
            }
        }

        public void CreateText(StreamWriter file)
        {

        }

        public void CreatePDF(string filename)
        {

        }
    }
}
