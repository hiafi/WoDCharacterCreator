using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace WoDCharacterCreator
{

    static class MeritList
    {
        public static List<Merit> merit_list;

        public static void init()
        {
            merit_list = new List<Merit>();
            XmlDocument file = new XmlDocument();
            file.Load("Merits.xml");
            foreach (XmlNode node in file.SelectNodes("merits/merit"))
            {
                XmlElement element = (XmlElement)node;
                string desc = "";
                List<Tuple<bool, string, int>> prereqs = null;
                List<Merit> ranks = null;

                string name = element.GetElementsByTagName("name")[0].InnerText;
                int cost = Convert.ToInt32(element.GetElementsByTagName("cost")[0].InnerText);
                if (element.GetElementsByTagName("description").Count > 0) { desc = element.GetElementsByTagName("description")[0].InnerText; }
                //Prereqs
                if (element.GetElementsByTagName("prereq").Count > 0)
                {
                    prereqs = new List<Tuple<bool, string, int>>();
                    foreach (XmlNode prereq in element.GetElementsByTagName("prereq"))
                    {
                        XmlElement prereq_element = (XmlElement)prereq;
                        int prereq_req = Convert.ToInt32(prereq_element.GetElementsByTagName("level")[0].InnerText);
                        if (prereq_element.GetElementsByTagName("attr").Count > 0)
                        {
                            prereqs.Add(new Tuple<bool, string, int>(true, prereq_element.GetElementsByTagName("attr")[0].InnerText, prereq_req));
                        }
                        else
                        {
                            prereqs.Add(new Tuple<bool, string, int>(false, prereq_element.GetElementsByTagName("skill")[0].InnerText, prereq_req));
                        }
                    }
                }
                //Ranks
                if (element.GetElementsByTagName("rank").Count > 0)
                {
                    ranks = new List<Merit>();
                    List<Tuple<int, Merit>> unsorted_ranks = new List<Tuple<int, Merit>>();
                    foreach (XmlNode rank in element.GetElementsByTagName("rank"))
                    {
                        XmlElement rank_element = (XmlElement)rank;
                        int lvl = Convert.ToInt32(rank_element.GetElementsByTagName("level")[0].InnerText);
                        unsorted_ranks.Add(new Tuple<int, Merit>(lvl, new Merit(rank_element.GetElementsByTagName("name")[0].InnerText, rank_element.GetElementsByTagName("description")[0].InnerText)));
                    }

                    unsorted_ranks = unsorted_ranks.OrderBy(o => o.Item1).ToList();

                    foreach (Tuple<int, Merit> rank in unsorted_ranks)
                    {
                        ranks.Add(rank.Item2);
                    }
                }
                if (ranks != null)
                {
                    if (prereqs != null)
                    {
                        merit_list.Add(new Merit(name, desc, cost, prereqs, ranks));
                    }
                    else
                    {
                        merit_list.Add(new Merit(name, desc, cost, ranks));
                    }
                }
                else if (prereqs != null)
                {
                    merit_list.Add(new Merit(name, desc, cost, prereqs));
                }
                else
                {
                    merit_list.Add(new Merit(name, desc, cost));
                }
            }
        }
    }

    public class Merit
    {
        public string name;
        public string description;
        public int cost = 0;
        public List<Tuple<bool, string, int>> prereqs; //Attr? name, cost
        public List<Merit> ranks;

        #region Constructors
        /// <summary>
        /// Use this for making rank nodes
        /// </summary>
        public Merit(string name, string description)
        {
            this.name = name;
            this.description = description;
            this.cost = 0;
            this.prereqs = null;
            this.ranks = null;
        }

        public Merit(string name, string description, int cost)
        {
            this.name = name;
            this.description = description;
            this.cost = cost;
            this.prereqs = null;
            this.ranks = null;
        }

        public Merit(string name, string description, int cost, List<Tuple<bool, string, int>> prereqs)
        {
            this.name = name;
            this.description = description;
            this.cost = cost;
            this.prereqs = prereqs;
            this.ranks = null;
        }

        public Merit(string name, string description, int cost, List<Merit> ranks)
        {
            this.name = name;
            this.description = description;
            this.cost = cost;
            this.prereqs = null;
            this.ranks = ranks;
        }

        public Merit(string name, string description, int cost, List<Tuple<bool, string, int>> prereqs, List<Merit> ranks)
        {
            this.name = name;
            this.description = description;
            this.cost = cost;
            this.prereqs = prereqs;
            this.ranks = ranks;
        }

        #endregion
    }
}
