namespace WoDCharacterCreator
{
    partial class MeritWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_add = new System.Windows.Forms.Button();
            this.lb_merits = new System.Windows.Forms.ListBox();
            this.num_ranks = new System.Windows.Forms.NumericUpDown();
            this.lbl_rank = new System.Windows.Forms.Label();
            this.lbl_total_dot_cost = new System.Windows.Forms.Label();
            this.panel_desc = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.num_ranks)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_add
            // 
            this.btn_add.Location = new System.Drawing.Point(12, 251);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(75, 23);
            this.btn_add.TabIndex = 0;
            this.btn_add.Text = "Add";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // lb_merits
            // 
            this.lb_merits.FormattingEnabled = true;
            this.lb_merits.Location = new System.Drawing.Point(12, 7);
            this.lb_merits.Name = "lb_merits";
            this.lb_merits.Size = new System.Drawing.Size(158, 238);
            this.lb_merits.TabIndex = 1;
            this.lb_merits.SelectedIndexChanged += new System.EventHandler(this.lb_merits_SelectedIndexChanged);
            // 
            // num_ranks
            // 
            this.num_ranks.Enabled = false;
            this.num_ranks.Location = new System.Drawing.Point(231, 225);
            this.num_ranks.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.num_ranks.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_ranks.Name = "num_ranks";
            this.num_ranks.Size = new System.Drawing.Size(46, 20);
            this.num_ranks.TabIndex = 2;
            this.num_ranks.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_ranks.ValueChanged += new System.EventHandler(this.lb_merits_SelectedIndexChanged);
            // 
            // lbl_rank
            // 
            this.lbl_rank.AutoSize = true;
            this.lbl_rank.Location = new System.Drawing.Point(190, 227);
            this.lbl_rank.Name = "lbl_rank";
            this.lbl_rank.Size = new System.Drawing.Size(33, 13);
            this.lbl_rank.TabIndex = 3;
            this.lbl_rank.Text = "Rank";
            // 
            // lbl_total_dot_cost
            // 
            this.lbl_total_dot_cost.AutoSize = true;
            this.lbl_total_dot_cost.Location = new System.Drawing.Point(283, 227);
            this.lbl_total_dot_cost.Name = "lbl_total_dot_cost";
            this.lbl_total_dot_cost.Size = new System.Drawing.Size(75, 13);
            this.lbl_total_dot_cost.TabIndex = 4;
            this.lbl_total_dot_cost.Text = "Total Dot Cost";
            // 
            // panel_desc
            // 
            this.panel_desc.AutoScroll = true;
            this.panel_desc.AutoSize = true;
            this.panel_desc.Location = new System.Drawing.Point(176, 7);
            this.panel_desc.MaximumSize = new System.Drawing.Size(298, 212);
            this.panel_desc.Name = "panel_desc";
            this.panel_desc.Size = new System.Drawing.Size(298, 212);
            this.panel_desc.TabIndex = 5;
            // 
            // MeritWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 285);
            this.Controls.Add(this.panel_desc);
            this.Controls.Add(this.lbl_total_dot_cost);
            this.Controls.Add(this.lbl_rank);
            this.Controls.Add(this.num_ranks);
            this.Controls.Add(this.lb_merits);
            this.Controls.Add(this.btn_add);
            this.Name = "MeritWindow";
            this.Text = "MeritWindow";
            this.Resize += new System.EventHandler(this.resize_window);
            ((System.ComponentModel.ISupportInitialize)(this.num_ranks)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.ListBox lb_merits;
        private System.Windows.Forms.NumericUpDown num_ranks;
        private System.Windows.Forms.Label lbl_rank;
        private System.Windows.Forms.Label lbl_total_dot_cost;
        private System.Windows.Forms.Panel panel_desc;
    }
}