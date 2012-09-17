using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WoDCharacterCreator
{
    class SpellListing
    {
        MageSpell spell;
        public Label lbl_name;
        public Label lbl_dicepool;
        public Label lbl_description;
        int x;
        int y;
        const int width = 128;
        const int height = 16;
        const int desc_height = 48;
        const int desc_width = 600;
        public const int total_height = (height * 2 + desc_height);

        public SpellListing(MageSpell spell, int x, int y)
        {
            this.spell = spell;
            this.x = x;
            this.y = y;
            System.Globalization.TextInfo cap = new System.Globalization.CultureInfo("en-US", false).TextInfo;
            lbl_name = new Label();
            lbl_name.SetBounds(this.x, this.y, width, height);
            lbl_name.Text = spell.name;
            lbl_dicepool = new Label();
            lbl_dicepool.SetBounds(this.x, this.y+16, width, height);
            lbl_dicepool.Text = String.Format("{0}+{1}+{2}", cap.ToTitleCase(spell.attribute), cap.ToTitleCase(spell.skill), Enum.GetName(typeof(Arcana), (int)spell.arcana));
            lbl_description = new Label();
            lbl_description.SetBounds(this.x, this.y+32, desc_width, desc_height);
            lbl_description.Text = spell.desc;
        }

        public void AddToControls(Panel pnl, int y)
        {
            lbl_name.SetBounds(this.x, y, width, height);
            lbl_dicepool.SetBounds(this.x, y+16, width, height);
            lbl_description.SetBounds(this.x, y+32, desc_width, desc_height);
            pnl.Controls.Add(lbl_name);
            pnl.Controls.Add(lbl_dicepool);
            pnl.Controls.Add(lbl_description);

        }
    }
}
