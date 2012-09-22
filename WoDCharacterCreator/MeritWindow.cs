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
    public partial class MeritWindow : Form
    {
        Character character;
        public Merit merit;
        public int rank;
        public MeritWindow()
        {
            InitializeComponent();

            foreach (Merit mer in MeritList.merit_list)
            {
                lb_merits.Items.Add(mer.name);
            }
        }

        public MeritWindow(Character character)
            : this()
        {
            this.character = character;
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (num_ranks.Enabled)
            {
                this.rank = (int)num_ranks.Value;
            }
            else
            {
                this.rank = 1;
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void lb_merits_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Globalization.TextInfo cap = new System.Globalization.CultureInfo("en-US", false).TextInfo;
            merit = MeritList.merit_list[lb_merits.SelectedIndex];
            if (merit.ranks != null)
            {
                num_ranks.Enabled = true;
            }
            else
            {
                num_ranks.Enabled = false;
            }
            panel_desc.Controls.Clear();
            int width = panel_desc.Width - 20;
            int y = 0;
            int tab = 8;

            Label lbl = new Label();
            lbl.Text = merit.name;
            lbl.Font = new System.Drawing.Font(lbl.Font.FontFamily, 15.0f);
            lbl.SetBounds(0, y, width, 32);
            panel_desc.Controls.Add(lbl);
            y = lbl.Height;

            lbl = new Label();
            lbl.Text = String.Format("Cost: {0}", merit.cost);
            lbl.SetBounds(0, y, width, 16);
            panel_desc.Controls.Add(lbl);
            y += lbl.Height;

            if (merit.prereqs != null)
            {
                lbl = new Label();
                lbl.Text = "Requires";
                lbl.SetBounds(0, y, width, 16);
                panel_desc.Controls.Add(lbl);
                y += lbl.Height;

                foreach (Tuple<bool, string, int> prereq in merit.prereqs)
                {

                    lbl = new Label();
                    lbl.Text = String.Format("{0} : {1}", cap.ToTitleCase(prereq.Item2), prereq.Item3);
                    lbl.SetBounds(tab, y, width - tab, 16);
                    if ((prereq.Item1 ? (int)character.GetValue(prereq.Item2) : character.skill_list[prereq.Item2.ToLower()].rank) < prereq.Item3)
                    {
                        lbl.ForeColor = Color.Red;
                    }
                    panel_desc.Controls.Add(lbl);
                    y += lbl.Height;
                }
            }

            lbl = new Label();
            lbl.Text = merit.description;
            lbl.SetBounds(0, y, width, 48);
            panel_desc.Controls.Add(lbl);
            y += lbl.Height;

            if (merit.ranks != null)
            {
                lbl = new Label();
                lbl.Text = "Ranks";
                lbl.SetBounds(0, y, width, 16);
                panel_desc.Controls.Add(lbl);
                y += lbl.Height;

                for (int i = 0; i < merit.ranks.Count; i++)
                {

                    lbl = new Label();
                    lbl.Text = String.Format("Rank {0} - {1}\n{2}", i + 1, merit.ranks[i].name, merit.ranks[i].description);
                    lbl.SetBounds(tab, y, width - tab, 64);
                    if ((i + 1) > (int)num_ranks.Value)
                    {
                        lbl.ForeColor = Color.Red;
                    }
                    panel_desc.Controls.Add(lbl);
                    y += lbl.Height;
                }
            }
        }

        private void resize_window(object sender, EventArgs e)
        {
            int old_width = panel_desc.MaximumSize.Width;
            lb_merits.Height = this.Height - 80;
            btn_add.SetBounds(btn_add.Location.X, lb_merits.Height + lb_merits.Location.Y + 4, btn_add.Width, btn_add.Height);
            lbl_rank.Location = new Point(lbl_rank.Location.X, btn_add.Location.Y);
            num_ranks.Location = new Point(num_ranks.Location.X, lbl_rank.Location.Y);
            lbl_total_dot_cost.Location = new Point(lbl_total_dot_cost.Location.X, lbl_rank.Location.Y);
            panel_desc.MaximumSize = new System.Drawing.Size(this.Width - panel_desc.Location.X - 32, lb_merits.Height - 64);
            panel_desc.Width = this.Width - panel_desc.Location.X - 32;
            panel_desc.Height = lb_merits.Height;
            panel_desc.AutoScroll = true;
            foreach (Control ctr in panel_desc.Controls)
            {
                ctr.Width = (panel_desc.MaximumSize.Width - 32);
            }
            //lb_merits_SelectedIndexChanged(sender, e);
        }
    }
}
