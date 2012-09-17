namespace WoDCharacterCreator
{
    partial class SpellForm
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
            this.cb_spell_order = new System.Windows.Forms.ComboBox();
            this.list_spells = new System.Windows.Forms.ListBox();
            this.lbl_spell_name = new System.Windows.Forms.Label();
            this.lbl_spell_action = new System.Windows.Forms.Label();
            this.lbl_spell_duration = new System.Windows.Forms.Label();
            this.lbl_spell_covert = new System.Windows.Forms.Label();
            this.lbl_spell_cost = new System.Windows.Forms.Label();
            this.lbl_spell_dp = new System.Windows.Forms.Label();
            this.lbl_spell_description = new System.Windows.Forms.Label();
            this.lbl_spell_add = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cb_spell_order
            // 
            this.cb_spell_order.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_spell_order.FormattingEnabled = true;
            this.cb_spell_order.Location = new System.Drawing.Point(12, 12);
            this.cb_spell_order.Name = "cb_spell_order";
            this.cb_spell_order.Size = new System.Drawing.Size(157, 21);
            this.cb_spell_order.TabIndex = 0;
            this.cb_spell_order.SelectedIndexChanged += new System.EventHandler(this.cb_spell_order_SelectedIndexChanged);
            // 
            // list_spells
            // 
            this.list_spells.FormattingEnabled = true;
            this.list_spells.Location = new System.Drawing.Point(12, 39);
            this.list_spells.Name = "list_spells";
            this.list_spells.Size = new System.Drawing.Size(157, 264);
            this.list_spells.TabIndex = 1;
            this.list_spells.SelectedIndexChanged += new System.EventHandler(this.list_spells_SelectedIndexChanged);
            // 
            // lbl_spell_name
            // 
            this.lbl_spell_name.AutoSize = true;
            this.lbl_spell_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_spell_name.Location = new System.Drawing.Point(174, 13);
            this.lbl_spell_name.Name = "lbl_spell_name";
            this.lbl_spell_name.Size = new System.Drawing.Size(90, 20);
            this.lbl_spell_name.TabIndex = 2;
            this.lbl_spell_name.Text = "Spell Name";
            // 
            // lbl_spell_action
            // 
            this.lbl_spell_action.AutoSize = true;
            this.lbl_spell_action.Location = new System.Drawing.Point(175, 55);
            this.lbl_spell_action.Name = "lbl_spell_action";
            this.lbl_spell_action.Size = new System.Drawing.Size(37, 13);
            this.lbl_spell_action.TabIndex = 3;
            this.lbl_spell_action.Text = "Action";
            // 
            // lbl_spell_duration
            // 
            this.lbl_spell_duration.AutoSize = true;
            this.lbl_spell_duration.Location = new System.Drawing.Point(175, 68);
            this.lbl_spell_duration.Name = "lbl_spell_duration";
            this.lbl_spell_duration.Size = new System.Drawing.Size(47, 13);
            this.lbl_spell_duration.TabIndex = 4;
            this.lbl_spell_duration.Text = "Duration";
            // 
            // lbl_spell_covert
            // 
            this.lbl_spell_covert.AutoSize = true;
            this.lbl_spell_covert.Location = new System.Drawing.Point(175, 42);
            this.lbl_spell_covert.Name = "lbl_spell_covert";
            this.lbl_spell_covert.Size = new System.Drawing.Size(38, 13);
            this.lbl_spell_covert.TabIndex = 5;
            this.lbl_spell_covert.Text = "Covert";
            // 
            // lbl_spell_cost
            // 
            this.lbl_spell_cost.AutoSize = true;
            this.lbl_spell_cost.Location = new System.Drawing.Point(175, 81);
            this.lbl_spell_cost.Name = "lbl_spell_cost";
            this.lbl_spell_cost.Size = new System.Drawing.Size(28, 13);
            this.lbl_spell_cost.TabIndex = 6;
            this.lbl_spell_cost.Text = "Cost";
            // 
            // lbl_spell_dp
            // 
            this.lbl_spell_dp.AutoSize = true;
            this.lbl_spell_dp.Location = new System.Drawing.Point(175, 94);
            this.lbl_spell_dp.Name = "lbl_spell_dp";
            this.lbl_spell_dp.Size = new System.Drawing.Size(53, 13);
            this.lbl_spell_dp.TabIndex = 7;
            this.lbl_spell_dp.Text = "Dice Pool";
            // 
            // lbl_spell_description
            // 
            this.lbl_spell_description.AutoSize = true;
            this.lbl_spell_description.Location = new System.Drawing.Point(175, 149);
            this.lbl_spell_description.MaximumSize = new System.Drawing.Size(150, 0);
            this.lbl_spell_description.Name = "lbl_spell_description";
            this.lbl_spell_description.Size = new System.Drawing.Size(60, 13);
            this.lbl_spell_description.TabIndex = 8;
            this.lbl_spell_description.Text = "Description";
            // 
            // lbl_spell_add
            // 
            this.lbl_spell_add.Location = new System.Drawing.Point(12, 309);
            this.lbl_spell_add.Name = "lbl_spell_add";
            this.lbl_spell_add.Size = new System.Drawing.Size(157, 23);
            this.lbl_spell_add.TabIndex = 9;
            this.lbl_spell_add.Text = "Add";
            this.lbl_spell_add.UseVisualStyleBackColor = true;
            this.lbl_spell_add.Click += new System.EventHandler(this.lbl_spell_add_Click);
            // 
            // SpellForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 341);
            this.Controls.Add(this.lbl_spell_add);
            this.Controls.Add(this.lbl_spell_description);
            this.Controls.Add(this.lbl_spell_dp);
            this.Controls.Add(this.lbl_spell_cost);
            this.Controls.Add(this.lbl_spell_covert);
            this.Controls.Add(this.lbl_spell_duration);
            this.Controls.Add(this.lbl_spell_action);
            this.Controls.Add(this.lbl_spell_name);
            this.Controls.Add(this.list_spells);
            this.Controls.Add(this.cb_spell_order);
            this.Name = "SpellForm";
            this.Text = "SpellForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cb_spell_order;
        private System.Windows.Forms.ListBox list_spells;
        private System.Windows.Forms.Label lbl_spell_name;
        private System.Windows.Forms.Label lbl_spell_action;
        private System.Windows.Forms.Label lbl_spell_duration;
        private System.Windows.Forms.Label lbl_spell_covert;
        private System.Windows.Forms.Label lbl_spell_cost;
        private System.Windows.Forms.Label lbl_spell_dp;
        private System.Windows.Forms.Label lbl_spell_description;
        private System.Windows.Forms.Button lbl_spell_add;
    }
}