﻿using System;
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

        List<MagePath> path_list;
        List<MageOrder> mage_order_list;

        List<VampClan> clan_list;
        List<VampCovenant> covenant_list;

        List<SpellListing> spell_forms;

        List<MeritListing> merit_list;

        public charactor_creator()
        {
            InitializeComponent();
            character = new Character();
            init_skills();
            init_vice_virtues();
            init_combat();
            init_merits();
            init_mage();
            init_vamp();
            
            tab_main.TabPages.Remove(mageView);
            tab_main.TabPages.Remove(vampireView);
            tab_main.TabPages.Remove(equipmentView);
            MeritList.init();
            MageSpellList.init();
            VampPowerList.init();
            character.is_mage = false;
            character.is_vamp = false;

            updateAttributes();
            updateAdvantages();
            updateSkills();
            updateCombat();
            updateMage();
            updateVampire();
        }

        #region File Functions
        public void new_character()
        {
            
        }
        #endregion

        #region Update Values

        private void updateAttributeValues()
        {
            character.name = input_Name.Text;
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

        private void updateSkillValues()
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
        }

        private void updateMeritsValues()
        {
            character.merits_spent = 0;
            foreach (Tuple<int, Merit> merit in character.merit_list)
            {
                character.merits_spent += merit.Item2.cost * merit.Item1;
            }
        }

        private void updateMageValues()
        {
            updateMeritsValues();
            character.merits_spent += ((int)num_mage_gnosis.Value - 1) * 3;
        }

        private void updateVampireValues()
        {
            updateMeritsValues();
            character.merits_spent += ((int)num_vamp_blood_potency.Value - 1) * 3;
        }

        #endregion

        #region Form1 Update Forms

        private void updateSkills()
        {

            updateSkillValues();
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
            lbl_combat_brawl.Text = String.Format("Brawl: {0}", character.strength + character.skill_list["brawl"].total);
            lbl_combat_weaponry.Text = String.Format("Weaponry: {0}", character.strength + character.skill_list["weaponry"].total);
            lbl_combat_firearms.Text = String.Format("Firearms: {0}", character.dexterity + character.skill_list["firearms"].total);
            lbl_combat_thrown.Text = String.Format("Thrown: {0}", character.dexterity + character.skill_list["athletics"].total);

            if (cb_combat_armor.SelectedIndex >= 0)
            {
                lbl_combat_armor_melee.Text = String.Format("Melee Dice Pool: {0}", armor_list[cb_combat_armor.SelectedIndex].melee_rating + 
                    character.defense);
                lbl_combat_armor_firearm.Text = String.Format("Firearms Dice Pool: {0}", armor_list[cb_combat_armor.SelectedIndex].armor_rating_firearm);
            }

            if (cb_combat_melee.SelectedIndex >= 0)
            {
                lbl_combat_melee_damage.Text = String.Format("Damage: {0}", melee_weapon_list[cb_combat_melee.SelectedIndex].damage);
                lbl_combat_melee_total.Text = String.Format("Total Dice Pool: {0}", 
                    melee_weapon_list[cb_combat_melee.SelectedIndex].damage + character.strength + 
                    ((melee_weapon_list[cb_combat_melee.SelectedIndex].brawl) ? (character.skill_list["brawl"].total) : (character.skill_list["weaponry"].total)));
            }

            if (combo_combat_ranged.SelectedIndex >= 0)
            {
                lbl_combat_range_damage.Text = String.Format("Damage: {0}", ranged_weapon_list[combo_combat_ranged.SelectedIndex].damage);
                lbl_combat_range_range.Text = String.Format("Range (+0/-2/-4): {0}/{1}/{2}", ranged_weapon_list[combo_combat_ranged.SelectedIndex].range, ranged_weapon_list[combo_combat_ranged.SelectedIndex].range * 2, ranged_weapon_list[combo_combat_ranged.SelectedIndex].range*4);
                //lbl_combat_range_damage.Text = String.Format("Damage: {0}", ranged_weapon_list[combo_combat_ranged.SelectedIndex].damage);
                lbl_combat_range_total.Text = String.Format("Total Dice Pool: {0}", character.dexterity + character.skill_list["firearms"].total + ranged_weapon_list[combo_combat_ranged.SelectedIndex].damage);
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
            updateAttributeValues();
        }

        private void updateMerits()
        {
            updateMeritsValues();
            if (character.is_mage)
            {
                updateMageValues();
            }
            if (character.is_vamp)
            {
                updateVampireValues();
            }
            lbl_merits.Text = String.Format("Merits: {0}/{1}", character.merits_dots - character.merits_spent, character.merits_dots);
            int y = 0;
            foreach (MeritListing merit in merit_list)
            {
                merit.Draw(panel_merit_merits, y);
                y += merit.height + 8;
            }
            if (character.is_mage)
            {
                lbl_mage_merits.Text = String.Format("Merits Left: {0}", character.merits_dots - character.merits_spent);
            }
            if (character.is_mage)
            {
                //lbl_mage_merits.Text = String.Format("Merits Left: {0}", character.merits_dots - character.merits_spent);
            }
        }

        private void updateMage()
        {
            updateMageValues();
            updateMerits();
            int index = 0;
            foreach (Control control in group_mage_arcanas.Controls)
            {
                if (control.GetType() == typeof(Label))
                {
                    control.Text = String.Format("{0}: {1}", Enum.GetName(typeof(Arcana), index), character.mage.arcana[index].ToString());
                    index++;
                }
            }
            panel_mage_spells.Controls.Clear();
            int y = 0;
            foreach (SpellListing spell in spell_forms)
            {
                spell.AddToControls(panel_mage_spells, y);
                y += SpellListing.total_height;
            }
            
        }

        private void updateVampire()
        {
            updateVampireValues();
            updateMerits();
            if (cb_vamp_clan.SelectedIndex >= 0)
            {
                lbl_vamp_clan.Text = String.Format("Clan Disciplines:\n{0}\n{1}\n{2}\n\nAttributes\n{3}\n{4}", clan_list[cb_vamp_clan.SelectedIndex].disc1,
                    clan_list[cb_vamp_clan.SelectedIndex].disc2, clan_list[cb_vamp_clan.SelectedIndex].disc3, clan_list[cb_vamp_clan.SelectedIndex].attr1, clan_list[cb_vamp_clan.SelectedIndex].attr2);
            }
            
        }

        private void updateVampirePowers()
        {
            int y = 0;
            panel_vamp_powers.Controls.Clear();
            for (int i = 0; i < character.vampire.discipline.Length; i++)
            {
                for (int j = 0; j < character.vampire.discipline[i]; j++)
                {
                    Label lbl = new Label();
                    lbl.Text = VampPowerList.power_list[i][j].name;
                    lbl.SetBounds(0, y, panel_vamp_powers.Width - 32, 20);
                    y += 20;
                    panel_vamp_powers.Controls.Add(lbl);
                    lbl = new Label();
                    lbl.Text = String.Format("{0} + {1} + {2}", VampPowerList.power_list[i][j].attr, VampPowerList.power_list[i][j].skill, 
                        Enum.GetName(typeof(Discipline), i));
                    lbl.SetBounds(0, y, panel_vamp_powers.Width - 32, 20);
                    y += 20;
                    panel_vamp_powers.Controls.Add(lbl);
                    lbl = new Label();
                    lbl.Text = VampPowerList.power_list[i][j].description;
                    lbl.SetBounds(0, y, panel_vamp_powers.Width - 32, 64);
                    y += 64;
                    panel_vamp_powers.Controls.Add(lbl);
                }
            }
        }

        private void updateVampireCreation()
        {
            for (int i = 0; i < character.vampire.discipline.Length; i++)
            {
                character.vampire.discipline[i] = 0;
            }
            character.vampire.discipline[cb_vamp_disc1.SelectedIndex] += 1;
            character.vampire.discipline[cb_vamp_disc2.SelectedIndex] += 1;
            character.vampire.discipline[cb_vamp_disc3.SelectedIndex] += 1;
            updateVampirePowers();
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
            character.is_mage = false;
            character.is_vamp = false;
            if (rb_show_mage.Checked)
            {
                tab_main.TabPages.Add(mageView);
                character.is_mage = true;
                updateMage();
            }
            if (rb_show_vampire.Checked)
            {
                tab_main.TabPages.Add(vampireView);
                character.is_vamp = true;
                updateVampire();
            }
        }

        private void combo_virtue_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl_virtue_effect.Text = virtue_list[combo_virtue.SelectedIndex].effect;
            character.virtue = virtue_list[combo_virtue.SelectedIndex];
        }

        private void combo_vice_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl_vice_effect.Text = vice_list[combo_vice.SelectedIndex].effect;
            character.vice = vice_list[combo_vice.SelectedIndex];
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

        #region Mage
        private void cb_mage_path_SelectedIndexChanged(object sender, EventArgs e)
        {
            character.mage.path = path_list[cb_mage_path.SelectedIndex];
            cb_mage_arcana11.Enabled = true;
            cb_mage_arcana12.Enabled = true;
            cb_mage_arcana21.Enabled = true;
            cb_mage_arcana22.Enabled = true;

            lbl_mage_favored_arcana.Text = String.Format("Favored Arcana: {0}, {1}",
                Enum.GetName(typeof(Arcana), character.mage.path.arcana1),
                Enum.GetName(typeof(Arcana), character.mage.path.arcana2));

            lbl_mage_bad_arcana.Text = String.Format("Inferior Arcana: {0}",
                Enum.GetName(typeof(Arcana), character.mage.path.bad_arcana));

            lbl_mage_favored_atb.Text = String.Format("Favored Attribute: {0}",
                character.mage.path.bonus_attribute);

        }

        private void cb_mage_order_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Globalization.TextInfo cap = new System.Globalization.CultureInfo("en-US", false).TextInfo;
            MageOrder order = mage_order_list[cb_mage_order.SelectedIndex];
            character.mage.order = order;
            lbl_mage_order_spec.Text = String.Format("Rote Specialties\n{0}\n{1}\n{2}\n", 
                cap.ToTitleCase(order.skill1), cap.ToTitleCase(order.skill2), cap.ToTitleCase(order.skill3));
        }

        private bool check_arcana()
        {
            int count = 0;
            int target_index1 = (int)character.mage.path.arcana1;
            int target_index2 = (int)character.mage.path.arcana2;
                
            if (cb_mage_arcana21.SelectedIndex == target_index1 ||
                    cb_mage_arcana21.SelectedIndex == target_index2)
            {
                count++;
            }
            if (cb_mage_arcana22.SelectedIndex == target_index1 ||
                cb_mage_arcana22.SelectedIndex == target_index2)
            {
                count++;
            }
            if (cb_mage_arcana11.SelectedIndex == target_index1 ||
                cb_mage_arcana11.SelectedIndex == target_index2)
            {
                count++;
            }
            if (cb_mage_arcana21.SelectedIndex >= 0 &&
                    cb_mage_arcana22.SelectedIndex >= 0 &&
                    cb_mage_arcana11.SelectedIndex >= 0 &&
                    count < 2)
            {
                return true;
            }
            return false;
        }

        private void cb_mage_arcana_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (character.mage.path != null)
            {
                if (check_arcana())
                {
                    MessageBox.Show("2 of the top three boxes need to be from your primary arcana");
                }
            }
            for (int i = 0; i < character.mage.arcana.Length; i++)
            {
                character.mage.arcana[i] = 0;
            }
            List<ComboBox> arc_list = new List<ComboBox>();
            arc_list.Add(cb_mage_arcana11);
            arc_list.Add(cb_mage_arcana12);
            arc_list.Add(cb_mage_arcana21);
            arc_list.Add(cb_mage_arcana22);

            foreach (ComboBox combo in arc_list)
            {
                if (combo.SelectedIndex >= 0)
                {
                    character.mage.arcana[combo.SelectedIndex] += Convert.ToInt32(combo.Tag);
                }
            }
            updateMage();
        }

        #endregion

        #region Vampire
        private void cb_vamp_clan_SelectedIndexChanged(object sender, EventArgs e)
        {
            character.vampire.clan = clan_list[cb_vamp_clan.SelectedIndex];
            updateVampire();
        }

        private void cb_vamp_covenant_SelectedIndexChanged(object sender, EventArgs e)
        {
            character.vampire.covenant = covenant_list[cb_vamp_covenant.SelectedIndex];
            updateVampire();
        }

        private void cb_vamp_disc_SelectedIndexChanged(object sender, EventArgs e)
        {
            int count = 0;
            int disc1 = (int)character.vampire.clan.disc1;
            int disc2 = (int)character.vampire.clan.disc2;
            int disc3 = (int)character.vampire.clan.disc3;
            if (cb_vamp_disc1.SelectedIndex == disc1 || cb_vamp_disc1.SelectedIndex == disc2 || cb_vamp_disc1.SelectedIndex == disc3)
            {
                count++;
            }
            if (cb_vamp_disc2.SelectedIndex == disc1 || cb_vamp_disc2.SelectedIndex == disc2 || cb_vamp_disc2.SelectedIndex == disc3)
            {
                count++;
            }
            if (cb_vamp_disc3.SelectedIndex == disc1 || cb_vamp_disc3.SelectedIndex == disc2 || cb_vamp_disc3.SelectedIndex == disc3)
            {
                count++;
            }
            if (cb_vamp_disc1.SelectedIndex >= 0 && cb_vamp_disc2.SelectedIndex >= 0 && cb_vamp_disc3.SelectedIndex >= 0)
            {
                if (count >= 2)
                {
                    updateVampireCreation();
                }
                else
                {
                    MessageBox.Show("2 of the 3 disciplines must be part of your clan");
                }
            }
        }
        #endregion

        #endregion

        #region update events
        public void updateAttributes(object sender, EventArgs e)
        {
            calc_attribute_dots();
            updateAttributes();
            updateAdvantages();
            updateSkills();
            updateCombat();
            updateMage();
        }

        public void updateSkills(object sender, EventArgs e)
        {
            updateSkills();
            updateCombat();
            if (character.is_mage)
            {
                updateMage();
            }
        }

        private void updateMage(object sender, EventArgs e)
        {
            updateMage();
        }

        private void updateVamp(object sender, EventArgs e)
        {
            updateVampire();
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
                weapon.brawl = (element.GetElementsByTagName("brawl").Count > 0);
                if (element.GetElementsByTagName("special").Count > 0) { weapon.special = element.GetElementsByTagName("special")[0].InnerText; };
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
                character.skill_list.Add(skill_name.ToLower(), new PlayerSkill(skill_name, 
                    Skill.StringToAttrType(element.GetElementsByTagName("type")[0].InnerText)));
                
                SkillWithForm skill = new SkillWithForm(element.GetElementsByTagName("name")[0].InnerText,
                                                        Skill.StringToAttrType(element.GetElementsByTagName("type")[0].InnerText));
                foreach (XmlNode action in element.GetElementsByTagName("action"))
                {
                    XmlElement action_element = (XmlElement)action;
                    skill.AddAction(action_element.GetElementsByTagName("name")[0].InnerText,
                                    action_element.GetElementsByTagName("attribute")[0].InnerText);
                    character.skill_list[skill_name.ToLower()].actions.Add(new PlayerAction(
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

        private void init_mage()
        {
            path_list = new List<MagePath>();
            path_list.Add(new MagePath("Acanthus", Arcana.Time, Arcana.Fate, Arcana.Forces, "composure"));
            path_list.Add(new MagePath("Mastigos", Arcana.Space, Arcana.Mind, Arcana.Matter, "resolve"));
            path_list.Add(new MagePath("Moros", Arcana.Matter, Arcana.Death, Arcana.Spirit, "composure"));
            path_list.Add(new MagePath("Obrimos", Arcana.Forces, Arcana.Prime, Arcana.Death, "resolve"));
            path_list.Add(new MagePath("Thyrsus", Arcana.Life, Arcana.Spirit, Arcana.Mind, "composure"));

            mage_order_list = new List<MageOrder>();
            mage_order_list.Add(new MageOrder("The Adamantine Arrow", "athletics", "intimidation", "medicine"));
            mage_order_list.Add(new MageOrder("The Free Council", "crafts", "persuasion", "science"));
            mage_order_list.Add(new MageOrder("The Guardians", "investigation", "stealth", "subterfuge"));
            mage_order_list.Add(new MageOrder("The Mysterium", "investigation", "occult", "survival"));
            mage_order_list.Add(new MageOrder("The Silver Ladder", "expression", "persuasion", "subterfuge"));


            spell_forms = new List<SpellListing>();

            foreach (MagePath path in path_list)
            {
                cb_mage_path.Items.Add(path.name);
            }

            foreach (MageOrder order in mage_order_list)
            {
                cb_mage_order.Items.Add(order.name);
            }

            Label lbl;
            int wid = 56;
            int hei = 16;
            int y = 20;
            int lbl_x = 4;
            
            foreach (string arcana in Enum.GetNames(typeof(Arcana)))
            {
                lbl = new Label();
                lbl.Text = arcana;
                lbl.SetBounds(lbl_x, y, wid, hei);

                y += (group_mage_arcanas.Height - 10) / 10;
                group_mage_arcanas.Controls.Add(lbl);

                cb_mage_arcana21.Items.Add(arcana);
                cb_mage_arcana22.Items.Add(arcana);
                cb_mage_arcana11.Items.Add(arcana);
                cb_mage_arcana12.Items.Add(arcana);
                
            }
            
        }

        private void init_merits()
        {
            merit_list = new List<MeritListing>();
        }

        private void init_vamp()
        {
            clan_list = new List<VampClan>();
            clan_list.Add(new VampClan("Daeva", "dexterity", "manipulation", Discipline.Celerity, Discipline.Majesty, Discipline.Vigor, 
                ""));
            clan_list.Add(new VampClan("Gangrel", "composure", "stamina", Discipline.Animalism, Discipline.Protean, Discipline.Resilience, 
                ""));
            clan_list.Add(new VampClan("Mekhet", "intelligence", "wits", Discipline.Auspex, Discipline.Celerity, Discipline.Obfuscate, 
                ""));
            clan_list.Add(new VampClan("Nosferatu", "composure", "strength", Discipline.Nightmare, Discipline.Obfuscate, Discipline.Vigor, 
                ""));
            clan_list.Add(new VampClan("Ventrue", "presence", "resolve", Discipline.Animalism, Discipline.Dominate, Discipline.Resilience, 
                ""));


            covenant_list = new List<VampCovenant>();
            covenant_list.Add(new VampCovenant("The Carthians", 
                "Members may purchase the Allies, Contacts, haven and Herd Merits at half the normal exp cost (round up) (Does not apply to character creation)", 
                ""));
            covenant_list.Add(new VampCovenant("The Circle of the Crone",
                "Members may learn the discipline of Cruac.",
                ""));
            covenant_list.Add(new VampCovenant("The Invictus",
                "Members may purchase the Herd, Mentor, Resources and Retainer merits at half the normal exp cost (round up) (Does not apply to character creation)",
                ""));
            covenant_list.Add(new VampCovenant("The Lancea Sanctum",
                "Members may learn the discipline of Theban Sorcery",
                ""));
            covenant_list.Add(new VampCovenant("The Carthians",
                "Members may learn the Coils of the Dragon",
                ""));

            foreach (VampClan clan in clan_list)
            {
                cb_vamp_clan.Items.Add(clan.name);
            }
            foreach (VampCovenant cov in covenant_list)
            {
                cb_vamp_covenant.Items.Add(cov.name);
            }
            foreach (string disc in Enum.GetNames(typeof(Discipline)))
            {
                cb_vamp_disc1.Items.Add(disc.Replace("_", " "));
                cb_vamp_disc2.Items.Add(disc.Replace("_", " "));
                cb_vamp_disc3.Items.Add(disc.Replace("_", " "));
            }
            
        }
        #endregion

        private void btn_mage_add_spell_Click(object sender, EventArgs e)
        {
            SpellForm form = new SpellForm(character);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                character.mage.rotes.Add(form.spell);
                spell_forms.Add(new SpellListing(form.spell, 0, 0));
                updateMage();
            }
            
            form.Dispose();
        }

        private void btn_merit_add_Click(object sender, EventArgs e)
        {
            MeritWindow form = new MeritWindow(character);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                character.merit_list.Add(new Tuple<int, Merit>(form.rank, form.merit));
                merit_list.Add(new MeritListing(form.merit, form.rank));
                updateMerits();
            }
            form.Dispose();
        }

        private void createTextFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog form = new SaveFileDialog();
            form.Filter = "Text Files (*.txt) |*.txt";
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                character.CreateText(form.FileName);
            }
        }        
    }
}
