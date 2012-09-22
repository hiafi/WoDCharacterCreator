using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace WoDCharacterCreator
{
    public enum Discipline { Animalism, Auspex, Celerity, Coils_of_the_Dragon, Cruac, Dominate, Majesty, Nightmare, Obfuscate, Protean, Resilience, Theban_Sorcery, Vigor };

    static class VampPowerList
    {
        public static List<VampPower>[] power_list;

        public static void init()
        {
            power_list = new List<VampPower>[Enum.GetValues(typeof(Discipline)).Length];
            for (int i = 0; i < power_list.Length - 1; i++)
            {
                power_list[i] = new List<VampPower>();
                List<Tuple<int, VampPower>> unsorted_list = new List<Tuple<int, VampPower>>();
                XmlDocument file = new XmlDocument();
                file.Load("VampirePower.xml");
                foreach (XmlNode power_element in file.SelectNodes(String.Format("powers/{0}/power", Enum.GetName(typeof(Discipline), i).ToLower())))
                {
                    XmlElement node = (XmlElement)power_element;
                    VampPower power = new VampPower();
                    power.name = node.GetElementsByTagName("name")[0].InnerText;
                    if (node.GetElementsByTagName("description").Count > 0) { power.description = node.GetElementsByTagName("description")[0].InnerText; }
                    power.cost = Convert.ToInt32(node.GetElementsByTagName("cost")[0].InnerText);
                    power.attr = node.GetElementsByTagName("attribute")[0].InnerText;
                    power.skill = node.GetElementsByTagName("skill")[0].InnerText;
                    if (node.GetElementsByTagName("resist").Count > 0) { power.resist = node.GetElementsByTagName("attribute")[0].InnerText; }
                    if (node.GetElementsByTagName("extended").Count > 0) { power.extended = true; }
                    unsorted_list.Add(new Tuple<int, VampPower>(Convert.ToInt32(node.GetElementsByTagName("cost")[0].InnerText), power));
                }

                foreach (Tuple<int, VampPower> pwr in unsorted_list.OrderBy(o => o.Item1).ToList())
                {
                    power_list[i].Add(pwr.Item2);
                }
            }
        }
    }

    public class VampPower
    {
        public string name;
        public string description = "";
        public int cost;
        public string attr;
        public string skill;
        public string resist = "";
        public bool extended = false;

    }
}
