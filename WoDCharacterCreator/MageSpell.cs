using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace WoDCharacterCreator
{

    static class MageSpellList
    {
        public static List<MageSpell>[] spell_list ;
        public static void init()
        {
            spell_list = new List<MageSpell>[Enum.GetValues(typeof(Arcana)).Length];
            for (int i = 0; i < spell_list.Length - 1; i++)
            {
                spell_list[i] = new List<MageSpell>();

                XmlDocument file = new XmlDocument();
                file.Load("Spells.xml");
                foreach (XmlNode node in file.SelectNodes(String.Format("spells/{0}/spell", Enum.GetName(typeof(Arcana), i).ToLower())))
                {
                    XmlElement spell_element = (XmlElement)node;
                    MageSpell spell = new MageSpell();
                    spell.name = spell_element.GetElementsByTagName("name")[0].InnerText;
                    spell.attribute = spell_element.GetElementsByTagName("attribute")[0].InnerText;
                    spell.skill = spell_element.GetElementsByTagName("skill")[0].InnerText;
                    spell.cost = Convert.ToInt32(spell_element.GetElementsByTagName("cost")[0].InnerText);
                    spell.rank = Convert.ToInt32(spell_element.GetElementsByTagName("rank")[0].InnerText);
                    spell.duration = spell_element.GetElementsByTagName("duration")[0].InnerText;
                    spell.vulgar = spell_element.GetElementsByTagName("vulgar").Count > 0;
                    spell.extended = spell_element.GetElementsByTagName("extended").Count > 0;
                    spell.arcana = (Arcana)i;

                    if (spell_element.GetElementsByTagName("description").Count > 0)
                    {
                        spell.desc = spell_element.GetElementsByTagName("description")[0].InnerText;
                    }
                    if (spell_element.GetElementsByTagName("practice").Count > 0)
                    {
                        spell.practice = spell_element.GetElementsByTagName("practice")[0].InnerText;
                    }
                    if (spell_element.GetElementsByTagName("resist").Count > 0)
                    {
                        spell.resist = spell_element.GetElementsByTagName("resist")[0].InnerText;
                    }
                    spell_list[i].Add(spell);
                }
            }
        }
    }

    enum Duration { Transitory, Prolonged, Concentration, Lasting }

    public class MageSpell
    {
        public string name;
        public string practice;
        public int rank;
        public string attribute;
        public string skill;
        public string resist;
        public string desc;
        public Arcana arcana;
        public int cost;
        public string duration;
        public bool extended;
        public bool vulgar;
        
    }
}
