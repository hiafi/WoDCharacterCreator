using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WoDCharacterCreator
{
    public partial class SpellForm : Form
    {
        Character character;
        public MageSpell spell;
        public SpellForm()
        {
            InitializeComponent();
            foreach (string arcana in Enum.GetNames(typeof(Arcana)))
            {
                cb_spell_order.Items.Add(arcana);
                cb_spell_order.SelectedIndex = 0;
            }
        }

        public SpellForm(Character character)
            : this()
        {
            this.character = character;
        }

        private void list_spells_SelectedIndexChanged(object sender, EventArgs e)
        {
            spell = MageSpellList.spell_list[cb_spell_order.SelectedIndex][list_spells.SelectedIndex];
            System.Globalization.TextInfo cap = new System.Globalization.CultureInfo("en-US", false).TextInfo;
            lbl_spell_name.Text = spell.name;
            lbl_spell_duration.Text = String.Format("Duration: {0}", spell.duration);
            lbl_spell_cost.Text = String.Format("Cost: {0}", spell.cost.ToString());
            lbl_spell_covert.Text = (spell.vulgar == true) ? "Vulgar" : "Covert";
            lbl_spell_action.Text = (spell.extended == true) ? "Extended" : "Instant";
            lbl_spell_dp.Text = String.Format("Dicepool:\n{0} + {1} + {2}\nTotal: {3}", 
                cap.ToTitleCase(spell.attribute), cap.ToTitleCase(spell.skill), Enum.GetName(typeof(Arcana), cb_spell_order.SelectedIndex),
                (int)character.GetValue(spell.attribute) + character.skill_list[spell.skill.ToLower()].rank + character.mage.arcana[cb_spell_order.SelectedIndex]);
            lbl_spell_description.Text = spell.desc;
        }

        private void cb_spell_order_SelectedIndexChanged(object sender, EventArgs e)
        {
            list_spells.Items.Clear();
            foreach (MageSpell spell in MageSpellList.spell_list[cb_spell_order.SelectedIndex])
            {
                list_spells.Items.Add(String.Format("{0} - {1}", spell.rank, spell.name));
            }
            if (list_spells.Items.Count > 0)
            {
                list_spells.SelectedIndex = 0;
            }
        }

        private void lbl_spell_add_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
