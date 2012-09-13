using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace WoDCharacterCreator
{
    public partial class charactor_creator : Form
    {
        Character character;
        List<SkillWithForm> skill_list;
        List<VirtueVice> virtue_list;
        List<VirtueVice> vice_list;

        List<RangedWeapon> ranged_weapon_list;
        List<MeleeWeapon> melee_weapon_list;
        List<Armor> armor_list;

        public charactor_creator()
        {
            InitializeComponent();
            character = new Character();
            init_skills();
            init_vice_virtues();
            init_combat();
            tab_main.TabPages.Remove(mageView);
            tab_main.TabPages.Remove(vampireView);
        }

        #region File Functions
        public void new_character()
        {
            
        }
        #endregion

        #region Form1 Update Form Fields
        
        private void updateSkills()
        {
            character.skill_spent_mental_dots = 0;
            character.skill_spent_physical_dots = 0;
            character.skill_spent_social_dots = 0;
            foreach (SkillWithForm skill in skill_list)
            {
                skill.UpdateForm(character);
                
                if (skill.type == AttrType.Mental)
                {
                    character.skill_spent_mental_dots += (int)skill.num.Value + (((int)skill.num.Value >= 5) ? 1 : 0);
                }
                if (skill.type == AttrType.Physical)
                {
                    character.skill_spent_physical_dots += (int)skill.num.Value + (((int)skill.num.Value >= 5) ? 1 : 0);
                }
                if (skill.type == AttrType.Social)
                {
                    character.skill_spent_social_dots += (int)skill.num.Value + (((int)skill.num.Value >= 5) ? 1 : 0);
                }
            }

            lbl_skill_mental.Text = String.Format("{0} / {1}", character.skill_mental_dots - character.skill_spent_mental_dots, character.skill_mental_dots);
            lbl_skill_physical.Text = String.Format("{0} / {1}", character.skill_physical_dots - character.skill_spent_physical_dots, character.skill_physical_dots);
            lbl_skill_social.Text = String.Format("{0} / {1}", character.skill_social_dots - character.skill_spent_social_dots, character.skill_social_dots);

            if (character.skill_mental_dots - character.skill_spent_mental_dots < 0)
            {
                lbl_skill_mental.ForeColor = Color.Red;
            }
            else { lbl_skill_mental.ForeColor = Color.Black; }

            if (character.skill_physical_dots - character.skill_spent_physical_dots < 0)
            {
                lbl_skill_physical.ForeColor = Color.Red;
            }
            else { lbl_skill_physical.ForeColor = Color.Black; }

            if (character.skill_social_dots - character.skill_spent_social_dots < 0)
            {
                lbl_skill_social.ForeColor = Color.Red;
            }
            else { lbl_skill_social.ForeColor = Color.Black; }

        }

        private void updateCombat()
        {
            lbl_combat_brawl.Text = String.Format("Brawl: {0}", character.strength + character.skill_list["Brawl"].total);
            lbl_combat_weaponry.Text = String.Format("Weaponry: {0}", character.strength + character.skill_list["Weaponry"].total);
            lbl_combat_firearms.Text = String.Format("Firearms: {0}", character.dexterity + character.skill_list["Firearms"].total);
            lbl_combat_thrown.Text = String.Format("Thrown: {0}", character.dexterity + character.skill_list["Athletics"].total);

            if (cb_combat_armor.SelectedIndex >= 0)
            {
                lbl_combat_armor_melee.Text = String.Format("Melee Dice Pool: {0}", armor_list[cb_combat_armor.SelectedIndex].melee_rating + character.defense);
                lbl_combat_armor_firearm.Text = String.Format("Firearms Dice Pool: {0}", armor_list[cb_combat_armor.SelectedIndex].armor_rating_firearm);
            }

            if (cb_combat_melee.SelectedIndex >= 0)
            {
                lbl_combat_melee_damage.Text = String.Format("Damage: {0}", melee_weapon_list[cb_combat_melee.SelectedIndex].damage);
                lbl_combat_melee_total.Text = String.Format("Total Dice Pool: {0}", melee_weapon_list[cb_combat_melee.SelectedIndex].damage + character.strength + character.skill_list["Weaponry"].total);
            }

            if (combo_combat_ranged.SelectedIndex >= 0)
            {
                lbl_combat_range_damage.Text = String.Format("Damage: {0}", ranged_weapon_list[combo_combat_ranged.SelectedIndex].damage);
                lbl_combat_range_range.Text = String.Format("Range (+0/-2/-4): {0}/{1}/{2}", ranged_weapon_list[combo_combat_ranged.SelectedIndex].range, ranged_weapon_list[combo_combat_ranged.SelectedIndex].range * 2, ranged_weapon_list[combo_combat_ranged.SelectedIndex].range*4);
                //lbl_combat_range_damage.Text = String.Format("Damage: {0}", ranged_weapon_list[combo_combat_ranged.SelectedIndex].damage);
                lbl_combat_range_total.Text = String.Format("Total Dice Pool: {0}", character.dexterity + character.skill_list["Firearms"].total + ranged_weapon_list[combo_combat_ranged.SelectedIndex].damage);
            }
        }

        private void updateAdvantages()
        {
            lbl_stat_defense.Text = String.Format("Defense: {0}", character.defense);
            lbl_stat_health.Text = String.Format("Health: {0}", character.health);
            lbl_stat_init.Text = String.Format("Iniative: {0}", character.initative);
            lbl_stat_speed.Text = String.Format("Speed: {0}", character.speed);
            lbl_stat_willpower.Text = String.Format("Willpower: {0}", character.willpower);
        }

        private void updateAttributes()
        {
            character.intelligence = (int)num_attr_int.Value;
            character.wits = (int)num_attr_wits.Value;
            character.resolve = (int)num_attr_resolve.Value;
            character.strength = (int)num_attr_strength.Value;
            character.dexterity = (int)num_attr_dexterity.Value;
            character.stamina = (int)num_attr_stamina.Value;
            character.presence = (int)num_attr_presence.Value;
            character.manipulation = (int)num_attr_manipulation.Value;
            character.composure = (int)num_attr_composure.Value;
        }

        #endregion

        #region Attributes

        public void calc_attribute_dots()
        {
            character.attribute_spent_mental_dots = 0;
            character.attribute_spent_mental_dots += (int)num_attr_int.Value - 1 + ((num_attr_int.Value >= 5) ? 1 : 0);
            character.attribute_spent_mental_dots += (int)num_attr_wits.Value - 1 + ((num_attr_wits.Value >= 5) ? 1 : 0);
            character.attribute_spent_mental_dots += (int)num_attr_resolve.Value - 1 + ((num_attr_resolve.Value >= 5) ? 1 : 0);

            character.attribute_spent_physical_dots = 0;
            character.attribute_spent_physical_dots += (int)num_attr_strength.Value - 1 + ((num_attr_strength.Value >= 5) ? 1 : 0);
            character.attribute_spent_physical_dots += (int)num_attr_dexterity.Value - 1 + ((num_attr_dexterity.Value >= 5) ? 1 : 0);
            character.attribute_spent_physical_dots += (int)num_attr_stamina.Value - 1 + ((num_attr_stamina.Value >= 5) ? 1 : 0);

            character.attribute_spent_social_dots = 0;
            character.attribute_spent_social_dots += (int)num_attr_presence.Value - 1 + ((num_attr_presence.Value >= 5) ? 1 : 0);
            character.attribute_spent_social_dots += (int)num_attr_manipulation.Value - 1 + ((num_attr_manipulation.Value >= 5) ? 1 : 0);
            character.attribute_spent_social_dots += (int)num_attr_composure.Value - 1 + ((num_attr_composure.Value >= 5) ? 1 : 0);

            change_attribute_labels();
        }

        private void change_attribute_labels()
        {
            lbl_mental_dots.Text = String.Format("{0}/{1} Dots Remaining", character.attribute_mental_dots - character.attribute_spent_mental_dots, character.attribute_mental_dots);
            lbl_physical_dots.Text = String.Format("{0}/{1} Dots Remaining", character.attribute_physical_dots - character.attribute_spent_physical_dots, character.attribute_physical_dots);
            lbl_social_dots.Text = String.Format("{0}/{1} Dots Remaining", character.attribute_social_dots - character.attribute_spent_social_dots, character.attribute_social_dots);

            if (character.attribute_mental_dots < character.attribute_spent_mental_dots) { 
                lbl_mental_dots.ForeColor = Color.Red;
            }
            else { 
                lbl_mental_dots.ForeColor = Color.Black; 
            }

            if (character.attribute_physical_dots < character.attribute_spent_physical_dots) { 
                lbl_physical_dots.ForeColor = Color.Red; 
            }
            else { 
                lbl_physical_dots.ForeColor = Color.Black; 
            }

            if (character.attribute_social_dots < character.attribute_spent_social_dots) { 
                lbl_social_dots.ForeColor = Color.Red; 
            }
            else { 
                lbl_social_dots.ForeColor = Color.Black; 
            }
        }

        #endregion

        #region Attribute Radio Boxes

        private void check_attribute_radiobuttons(object sender, RadioButton mental, RadioButton physical, RadioButton social)
        {
            RadioButton me = sender as RadioButton;
            me.Checked = true;
            if (mental.Checked && sender != mental)
            {
                mental.Checked = false;
                character.attribute_mental_dots = 0;
            }
            if (physical.Checked && sender != physical)
            {
                physical.Checked = false;
                character.attribute_physical_dots = 0;
            }
            if (social.Checked && sender != social)
            {
                social.Checked = false;
                character.attribute_social_dots = 0;
            }
            
        }

        private void allocate_attribute_dots(object sender, int amount)
        {
            RadioButton me = sender as RadioButton;
            if (me.Tag == "mental")
            {
                character.attribute_mental_dots = amount;
            }
            if (me.Tag == "physical")
            {
                character.attribute_physical_dots = amount;
            }
            if (me.Tag == "social")
            {
                character.attribute_social_dots = amount;
            }
            change_attribute_labels();
            
        }

        private void primary_CheckedChanged(object sender, EventArgs e)
        {
            
            check_attribute_radiobuttons(sender, rb_mental_prim, rb_physical_prim, rb_social_prim);
            allocate_attribute_dots(sender, 5);
            
        }

        private void secondary_CheckedChanged(object sender, EventArgs e)
        {
            check_attribute_radiobuttons(sender, rb_mental_sec, rb_physical_sec, rb_social_sec);
            allocate_attribute_dots(sender, 4);
        }

        private void tertiary_CheckedChanged(object sender, EventArgs e)
        {
            check_attribute_radiobuttons(sender, rb_mental_ter, rb_physical_ter, rb_social_ter);
            allocate_attribute_dots(sender, 3);
        }
        #endregion     
   
        #region Form1 Events

        private void changeRace(object sender, EventArgs e)
        {
            tab_main.TabPages.Remove(mageView);
            tab_main.TabPages.Remove(vampireView);
            if (rb_show_mage.Checked)
            {
                tab_main.TabPages.Add(mageView);
            }
            if (rb_show_vampire.Checked)
            {
                tab_main.TabPages.Add(vampireView);
            }
        }

        private void cb_show_actions_CheckedChanged(object sender, EventArgs e)
        {
            int x = 8;
            int y = 0;
            foreach (SkillWithForm skill in skill_list)
            {
                y = skill.UpdatePosition(cb_show_actions.Checked, x, y);
                y += SkillWithForm.y_inc;
            }
        }

        private void combo_combat_ranged_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateCombat();
        }

        private void skill_allocate_points(ComboBox selected)
        {
            if (selected.SelectedIndex == cb_skill_mental.SelectedIndex && selected != cb_skill_mental)
            {
                cb_skill_mental.SelectedItem = null;
            }
            if (selected.SelectedIndex == cb_skill_physical.SelectedIndex && selected != cb_skill_physical)
            {
                cb_skill_physical.SelectedItem = null;
            }
            if (selected.SelectedIndex == cb_skill_social.SelectedIndex && selected != cb_skill_social)
            {
                cb_skill_social.SelectedItem = null;
            }
            character.skill_mental_dots = (cb_skill_mental.SelectedIndex == 0 ? 11 : (cb_skill_mental.SelectedIndex == 1 ? 7 : 4));
            character.skill_physical_dots = (cb_skill_physical.SelectedIndex == 0 ? 11 : (cb_skill_physical.SelectedIndex == 1 ? 7 : 4));
            character.skill_social_dots = (cb_skill_social.SelectedIndex == 0 ? 11 : (cb_skill_social.SelectedIndex == 1 ? 7 : 4));
        }

        private void combo_skill_change_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox me = sender as ComboBox;
            int value = (me.SelectedIndex == 0 ? 11 : (me.SelectedIndex == 1 ? 7 : 4));
            skill_allocate_points(me);
            updateSkills();
        }

        public void updateAttributes(object sender, EventArgs e)
        {
            calc_attribute_dots();
            updateAttributes();
            updateAdvantages();
            updateSkills();
            updateCombat();
        }

        public void updateSkills(object sender, EventArgs e)
        {
            updateSkills();
            updateCombat();
        }
        #endregion

        #region init lists
        private void init_vice_virtues()
        {
            virtue_list = new List<VirtueVice>();
            vice_list = new List<VirtueVice>();
            XmlDocument file = new XmlDocument();
            file.Load("VirtueVice.xml");
            foreach (XmlNode node in file.SelectNodes("items/virtue"))
            {
                XmlElement element = (XmlElement)node;
                virtue_list.Add(new VirtueVice(element.GetElementsByTagName("name")[0].InnerText, 
                    element.GetElementsByTagName("effect")[0].InnerText,
                    element.GetElementsByTagName("description")[0].InnerText));
                combo_virtue.Items.Add(element.GetElementsByTagName("name")[0].InnerText);
            }
            foreach (XmlNode node in file.SelectNodes("items/vice"))
            {
                XmlElement element = (XmlElement)node;
                vice_list.Add(new VirtueVice(element.GetElementsByTagName("name")[0].InnerText, 
                    element.GetElementsByTagName("effect")[0].InnerText,
                    element.GetElementsByTagName("description")[0].InnerText));
                combo_vice.Items.Add(element.GetElementsByTagName("name")[0].InnerText);
            }


        }

        private void init_combat()
        {
            ranged_weapon_list = new List<RangedWeapon>();
            melee_weapon_list = new List<MeleeWeapon>();
            armor_list = new List<Armor>();
            XmlDocument file = new XmlDocument();
            file.Load("RangedWeapons.xml");
            foreach (XmlNode node in file.SelectNodes("weapons/firearm"))
            {
                XmlElement element = (XmlElement)node;
                RangedWeapon weapon = new RangedWeapon(element.GetElementsByTagName("name")[0].InnerText);
                weapon.damage = Convert.ToInt32(element.GetElementsByTagName("damage")[0].InnerText);
                weapon.range = Convert.ToInt32(element.GetElementsByTagName("range")[0].InnerText);
                weapon.clip_size = Convert.ToInt32(element.GetElementsByTagName("clip")[0].InnerText);
                weapon.strength = Convert.ToInt32(element.GetElementsByTagName("strength")[0].InnerText);
                weapon.size = Convert.ToInt32(element.GetElementsByTagName("size")[0].InnerText);
                weapon.cost = Convert.ToInt32(element.GetElementsByTagName("cost")[0].InnerText);
                weapon.automatic = (element.GetElementsByTagName("autofire").Count > 0);
                ranged_weapon_list.Add(weapon);
                combo_combat_ranged.Items.Add(element.GetElementsByTagName("name")[0].InnerText);
            }

            file = new XmlDocument();
            file.Load("MeleeWeapons.xml");
            foreach (XmlNode node in file.SelectNodes("weapons/weapon"))
            {
                XmlElement element = (XmlElement)node;
                MeleeWeapon weapon = new MeleeWeapon();
                weapon.name = element.GetElementsByTagName("name")[0].InnerText;
                weapon.damage = Convert.ToInt32(element.GetElementsByTagName("damage")[0].InnerText);
                weapon.setType(element.GetElementsByTagName("type")[0].InnerText);
                weapon.size = Convert.ToInt32(element.GetElementsByTagName("size")[0].InnerText);
                weapon.cost = Convert.ToInt32(element.GetElementsByTagName("cost")[0].InnerText);
                melee_weapon_list.Add(weapon);
                cb_combat_melee.Items.Add(weapon.name);
            }

            file = new XmlDocument();
            file.Load("Armor.xml");
            foreach (XmlNode node in file.SelectNodes("items/armor"))
            {
                XmlElement element = (XmlElement)node;
                Armor armor = new Armor();
                armor.name = element.GetElementsByTagName("name")[0].InnerText;
                armor.armor_rating_general = Convert.ToInt32(element.GetElementsByTagName("general")[0].InnerText);
                armor.armor_rating_firearm = Convert.ToInt32(element.GetElementsByTagName("firearm")[0].InnerText);
                armor.strength = Convert.ToInt32(element.GetElementsByTagName("strength")[0].InnerText);
                armor.defense_penalty = Convert.ToInt32(element.GetElementsByTagName("defense")[0].InnerText);
                armor.speed_penalty = Convert.ToInt32(element.GetElementsByTagName("speed")[0].InnerText);
                armor.cost = Convert.ToInt32(element.GetElementsByTagName("cost")[0].InnerText);
                armor_list.Add(armor);
                cb_combat_armor.Items.Add(armor.name);
            }

            updateCombat();
        }

        private void init_skills()
        {
            skill_list = new List<SkillWithForm>();

            int x = 8;
            int y = 0;
            int y_inc = 32;
            int x_inc = 80;
            List<string> skill_names = new List<string>();

            int label_y = 64;

            //Create the headers
            SkillWithForm.create_label("lbl_skills_name", "Name", x, label_y, skillsView, SkillWithForm.name_width);
            SkillWithForm.create_label("lbl_skills_attribute", "Attribute", x + SkillWithForm.name_width + (x_inc * 0), label_y, skillsView);
            SkillWithForm.create_label("lbl_skills_base", "Base", x + SkillWithForm.name_width + (x_inc * 1), label_y, skillsView);
            SkillWithForm.create_label("lbl_skills_equipment", "Equipment", x + SkillWithForm.name_width + (x_inc * 2), label_y, skillsView);
            SkillWithForm.create_label("lbl_skills_mods", "Modifier", x + SkillWithForm.name_width + (x_inc * 3), label_y, skillsView);
            SkillWithForm.create_label("lbl_skills_total", "Total", x + SkillWithForm.name_width + (x_inc * 4), label_y, skillsView);

            XmlDocument file = new XmlDocument();
            file.Load("Skills.xml");
            foreach (XmlNode node in file.SelectNodes("skills/skill"))
            {
                XmlElement element = (XmlElement) node;
                string skill_name = element.GetElementsByTagName("name")[0].InnerText;
                character.skill_list.Add(skill_name, new PlayerSkill(skill_name, 
                    Skill.StringToAttrType(element.GetElementsByTagName("type")[0].InnerText)));
                
                SkillWithForm skill = new SkillWithForm(element.GetElementsByTagName("name")[0].InnerText,
                                                        Skill.StringToAttrType(element.GetElementsByTagName("type")[0].InnerText));
                foreach (XmlNode action in element.GetElementsByTagName("action"))
                {
                    XmlElement action_element = (XmlElement)action;
                    skill.AddAction(action_element.GetElementsByTagName("name")[0].InnerText,
                                    action_element.GetElementsByTagName("attribute")[0].InnerText);
                    character.skill_list[skill_name].actions.Add(new PlayerAction(
                                    action_element.GetElementsByTagName("name")[0].InnerText,
                                    action_element.GetElementsByTagName("attribute")[0].InnerText));
                }
                skill_list.Add(skill);
                skill_names.Add(skill.name);
            }

            skill_names.Sort();

            foreach (string name in skill_names)
            {
                cb_skill_spec1.Items.Add(name);
                cb_skill_spec2.Items.Add(name);
                cb_skill_spec3.Items.Add(name);
            }
            
            foreach (SkillWithForm skill in skill_list)
            {
                y = skill.initForm(x, y, character, skillsContainer, new EventHandler(updateSkills));
                y += y_inc;
            }

            x = 8;
            y = 0;
            foreach (SkillWithForm skill in skill_list)
            {
                y = skill.UpdatePosition(cb_show_actions.Checked, x, y);
                y += SkillWithForm.y_inc;
            }

        }
        #endregion

        private void combo_virtue_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl_virtue_effect.Text = virtue_list[combo_virtue.SelectedIndex].effect;
        }

        private void combo_vice_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl_vice_effect.Text = vice_list[combo_vice.SelectedIndex].effect;
        }

        

        
    }
}
