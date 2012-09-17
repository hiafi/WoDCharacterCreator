using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace WoDCharacterCreator
{
    public enum AttrType {Mental, Physical, Social};

    public class Skill
    {
        public string name = "";
        public AttrType type;

        public static AttrType StringToAttrType(string str)
        {
            if (str == "mental")
            {
                return AttrType.Mental;
            }
            if (str == "physical")
            {
                return AttrType.Physical;
            }
            if (str == "social")
            {
                return AttrType.Social;
            }
            return AttrType.Mental;
        }
    }

    public class PlayerSkill : Skill
    {
        public int rank = 0;
        public int mod = 0;
        public List<PlayerAction> actions;
        private int no_rank_penalty
        {
            get
            {
                if (rank > 0)
                {
                    return 0;
                }
                if (this.type == AttrType.Mental)
                {
                    return -3;
                }
                else
                {
                    return -1;
                }
            }
        }

        public PlayerSkill(string name, AttrType type) : base()
        {
            this.name = name;
            this.type = type;
            actions = new List<PlayerAction>();
        }

        public PlayerSkill()
            : base()
        {
            actions = new List<PlayerAction>();
        }

        public int total {
            get { return rank + mod + no_rank_penalty; }
        }
    }

    class SkillTemplate : Skill
    {
        public List<ActionTemplate> actions;
        
        public SkillTemplate(string name, AttrType type)
        {
            this.actions = new List<ActionTemplate>();
            this.name = name;
            this.type = type;
        }

        public void AddAction(string name, string attr_id)
        {
            this.actions.Add(new ActionTemplate(name, attr_id));
        }
    }

    class SkillWithForm : SkillTemplate
    {
        public Label name_form;
        public Label mod_form;
        public Label total_form;
        public NumericUpDown num;

        public const int y_inc = 24;
        public const int x_inc = 80;
        public const int y_small_inc = 18;
        public const int name_width = 200;
        public const int label_width = 80;
        public const int label_height = 16;

        private const int num_width = 32;
        private const int num_height = 16;

        public new List<ActionWithForm> actions;

        public SkillWithForm(string name, AttrType type) : base(name, type)
        {
            this.actions = new List<ActionWithForm>();
            this.name = name;
            this.type = type;
        }

        public int UpdatePosition(bool actions_show, int x, int y)
        {
            name_form.SetBounds(x, y, name_width, label_height);
            num.SetBounds(x + name_form.Width + (80 / 2 - 16) + (x_inc * 1), y, num_width, num_height);
            mod_form.SetBounds(x + name_form.Width + (x_inc * 3), y, label_width, label_height);
            total_form.SetBounds(x + name_form.Width + (x_inc * 4), y, label_width, label_height);
            if (actions_show)
            {
                foreach (ActionWithForm action in this.actions)
                {
                    y += y_small_inc;
                    action.name_form.SetBounds(x, y, name_width, label_height);
                    action.attr_form.SetBounds(x + name_form.Width + (x_inc * 0), y, label_width, label_height);
                    action.base_form.SetBounds(x + name_form.Width + (x_inc * 1), y, label_width, label_height);
                    action.equip_form.SetBounds(x + name_form.Width + (x_inc * 2), y, label_width, label_height);
                    action.mod_form.SetBounds(x + name_form.Width + (x_inc * 3), y, label_width, label_height);
                    action.total_form.SetBounds(x + name_form.Width + (x_inc * 4), y, label_width, label_height);
                }
            }
            foreach (ActionWithForm action in this.actions)
            {
                action.name_form.Visible = actions_show;
                action.attr_form.Visible = actions_show;
                action.base_form.Visible = actions_show;
                action.equip_form.Visible = actions_show;
                action.mod_form.Visible = actions_show;
                action.total_form.Visible = actions_show;
            }
            return y;
        }

        public void UpdateForm(Character character)
        {
            character.skill_list[this.name].rank = (int)num.Value;
            mod_form.Text = character.skill_list[this.name].mod.ToString();
            total_form.Text = character.skill_list[this.name].total.ToString();
            foreach (PlayerAction action in character.skill_list[this.name].actions)
            {
                action.attr = (int)character.GetValue(action.attr_id);
                action.parent_rank = character.skill_list[this.name].total;
                action.equip = 0;
                action.mod = 0;
            }

            for (int i=0; i<this.actions.Count; i++)
            {
                this.actions[i].attr_form.Text = character.skill_list[this.name].actions[i].attr.ToString();
                this.actions[i].base_form.Text = character.skill_list[this.name].actions[i].parent_rank.ToString();
                this.actions[i].equip_form.Text = character.skill_list[this.name].actions[i].equip.ToString();
                this.actions[i].mod_form.Text = character.skill_list[this.name].actions[i].mod.ToString();
                this.actions[i].total_form.Text = character.skill_list[this.name].actions[i].total.ToString();

            }
        }

        public int initForm(int x, int y, Character character, Panel skillsContainer, EventHandler eh)
        {
            int new_x = 0;
            name_form = create_label(String.Format("lbl_{0}_name", this.name), String.Format("({1}) {0}", this.name, this.type.ToString()), x, y, skillsContainer, name_width, ContentAlignment.BottomLeft);
            new_x = name_form.Width;
            num = create_num(String.Format("num_{0}", this.name), x + new_x + (80 / 2 - 16) + (x_inc * 1), y, skillsContainer, eh);
            mod_form = create_label(String.Format("lbl_{0}_mod", this.name),
                    character.skill_list[this.name].mod.ToString(),
                    x + new_x + (x_inc * 3), y, skillsContainer);
            total_form = create_label(String.Format("lbl_{0}_total", this.name),
                    character.skill_list[this.name].total.ToString(),
                    x + new_x + (x_inc * 4), y, skillsContainer);

            foreach (ActionWithForm action in this.actions)
            {
                y += y_small_inc;
                action.name_form = create_label(String.Format("lbl_action_{0}_name", action.name), String.Format("> {0} ({1})", action.name, action.attr_id), x, y, skillsContainer, SkillWithForm.name_width, ContentAlignment.BottomLeft);
                action.attr_form = create_label(String.Format("lbl_action_{0}_attr", action.name), character.GetValue(action.attr_id).ToString(), x + new_x + (x_inc * 0), y, skillsContainer);
                action.base_form = create_label(String.Format("lbl_action_{0}_base", action.name), character.GetValue(action.attr_id).ToString(), x + new_x + (x_inc * 1), y, skillsContainer);
                action.equip_form = create_label(String.Format("lbl_action_{0}_equip", action.name), character.GetValue(action.attr_id).ToString(), x + new_x + (x_inc * 2), y, skillsContainer);
                action.mod_form = create_label(String.Format("lbl_action_{0}_mod", action.name), character.GetValue(action.attr_id).ToString(), x + new_x + (x_inc * 3), y, skillsContainer);
                action.total_form = create_label(String.Format("lbl_action_{0}_total", action.name), character.GetValue(action.attr_id).ToString(), x + new_x + (x_inc * 4), y, skillsContainer);
            }

            UpdateForm(character);

            return y;
        }

        public static NumericUpDown create_num(string name, int x, int y, Panel to_add, EventHandler eh)
        {
            NumericUpDown num = new NumericUpDown();
            num.Name = name;
            num.Value = 0;
            num.Maximum = 5;
            num.SetBounds(x, y, 32, 16);
            num.ValueChanged += eh;
            to_add.Controls.Add(num);
            return num;
        }

        public static Label create_label(string name, string text, int x, int y, Panel to_add, int width=80, ContentAlignment align = ContentAlignment.BottomCenter)
        {
            Label lbl = new Label();
            lbl.Name = name;
            lbl.Text = text;
            lbl.SetBounds(x, y, width, 16);
            lbl.TextAlign = align;
            to_add.Controls.Add(lbl);
            return lbl;
        }

        public void AddAction(string name, string attr_id)
        {
            this.actions.Add(new ActionWithForm(name, attr_id));
        }
    }

    public class ActionTemplate
    {
        public string name = "";
        public string attr_id = "";

        public ActionTemplate(string name, string attr_id)
        {
            this.name = name;
            this.attr_id = attr_id;
        }
    }

    public class PlayerAction : ActionTemplate
    {
        public int parent_rank;
        public int equip;
        public int attr;
        public int mod;

        public PlayerAction(string name, string attr_id)
            : base(name, attr_id)
        { }

        public int total
        {
            get { return parent_rank + attr + mod + equip; }
        }
    }

    class ActionWithForm : ActionTemplate
    {
        public Label name_form;
        public Label attr_form;
        public Label base_form;
        public Label equip_form;
        public Label mod_form;
        public Label total_form;

        public ActionWithForm(string name, string attr_id)
            : base(name, attr_id)
        {
            this.name = name;
            this.attr_id = attr_id;
        }
    }
}
