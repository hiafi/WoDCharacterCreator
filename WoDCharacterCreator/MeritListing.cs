using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WoDCharacterCreator
{
    class MeritListing
    {
        public Merit merit;
        Label name;
        Label rank;
        Label desc;
        Label rank_desc;

        public int height
        {
            get {
                int total = 0;
                if (this.name != null)
                {
                    total += this.name.Height;
                }
                if (this.desc != null)
                {
                    total += this.desc.Height;
                }
                if (this.rank_desc != null)
                {
                    total += this.rank_desc.Height;
                }
                return total;
            }
        }

        public MeritListing(Merit merit, int rank)
        {
            this.merit = merit;

            int cur_pos = 0;

            name = new Label();
            name.Text = merit.name;
            name.Location = new System.Drawing.Point(0, cur_pos);
            name.AutoSize = true;


            if (merit.ranks != null)
            {
                this.rank = new Label();
                this.rank.Text = String.Format("Rank: {0}", rank);
                this.rank.Location = new System.Drawing.Point(name.Width + 8, cur_pos);
                this.rank.AutoSize = true;
            }
            cur_pos += name.Height;

            desc = new Label();
            desc.Text = merit.description;
            desc.Location = new System.Drawing.Point(0, cur_pos);
            desc.AutoSize = true;
            cur_pos += name.Height;

            if (merit.ranks != null)
            {
                rank_desc = new Label();
                string rank_text = "\n";
                for (int i = 0; i < rank; i++)
                {
                    rank_text += String.Format("\n{0}\n{1}\n", merit.ranks[i].name, merit.ranks[i].description);
                }
                rank_desc.Text = rank_text;
                rank_desc.Location = new System.Drawing.Point(0, cur_pos);
                rank_desc.AutoSize = true;
                rank_desc.MaximumSize = new System.Drawing.Size(600, 800);
            }

        }

        public void Draw(Panel pnl, int y)
        {
            int cur_pos = y;
            name.Location = new System.Drawing.Point(0, cur_pos);
            pnl.Controls.Add(name);
            if (rank != null)
            {
                this.rank.Location = new System.Drawing.Point(name.Width + 8, cur_pos);
                pnl.Controls.Add(rank);
            }
            cur_pos += name.Height;
            desc.Location = new System.Drawing.Point(0, cur_pos);
            pnl.Controls.Add(desc);
            if (rank_desc != null)
            {
                rank_desc.Location = new System.Drawing.Point(0, cur_pos);
                pnl.Controls.Add(rank_desc);
            }
        }
    }
}
