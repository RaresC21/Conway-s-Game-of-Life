using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Conway_Game_of_Life
{

    public partial class Form1
    {

        private Button save = new Button();
        private Button load = new Button();
        private NumericUpDown enter_num = new NumericUpDown();

        private List<Pt>[] dict_positions = new List<Pt>[11];

        public void Save_Load_Buttons()
        {
            // Save Button
            save.Location = new Point(100, 0);
            save.Text = "Save";
            save.Size = new Size(100, 20);
            save.BackColor = Color.Gray;
            save.Click += showTextBox_Save;
            
            // Load Button
            load.Location = new Point(200, 0);
            load.Text = "Load";
            load.Size = new Size(100, 20);
            load.BackColor = Color.Gray;
            load.Click += showTextBox_Load;

            // Enter Name of Position Textbox.
            enter_num.Location = new Point(500, 0);
            enter_num.Size = new Size(100, 20);
            enter_num.BackColor = Color.White;
            enter_num.ForeColor = Color.Red;
            enter_num.Enabled = true;
            enter_num.Hide();

            // Show on Screen
            Controls.Add(save);
            Controls.Add(load);
            Controls.Add(enter_num);
        }

        private void showTextBox_Save (object sender, EventArgs e)
        {
            enter_num.Show();
            enter_num.Enabled = true;
            enter_num.KeyPress -= load_on_enter;
            enter_num.KeyPress += save_on_enter;

            load.Enabled = false;
        }

        private void showTextBox_Load (object sender, EventArgs e)
        {
            enter_num.Show();
            enter_num.Enabled = true;
            enter_num.KeyPress -= save_on_enter;
            enter_num.KeyPress += load_on_enter;

            save.Enabled = false;
        }

        private void save_on_enter(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)13) return;

            int value = (int)enter_num.Value;

            if ((value >= 1 && value <= 10) == false)
            {
                enter_num.Value = 0;
                enter_num.Enabled = false;
                enter_num.Hide();
                return;
            }

            List<Pt> cur = new List<Pt>();
            for (int i = 0; i < rowsn; i++) for (int k = 0; k < colsn; k++)
                    if (cells[i, k].state == State.Alive)
                        cur.Add(new Pt(i, k));

            dict_positions[value] = cur;

            enter_num.Text = "";
            enter_num.Enabled = false;
            enter_num.Hide();

            load.Enabled = true;
        }

        private void load_on_enter(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)13) return;

            int value = (int)enter_num.Value;
            if ((value >= 1 && value <= 10) == false)
            {
                enter_num.Value = 0;
                enter_num.Enabled = false;
                enter_num.Hide();

                save.Enabled = true;
                return;
            }

            for (int i = 0; i < rowsn; i++)
                for (int k = 0; k < colsn; k++)
                    cells[i, k].state = temp[i, k] = State.Dead;

            foreach (var c in dict_positions[value])
            {
                var i = c.x;
                var k = c.y;

                cells[i, k].state = State.Alive;
            }

            enter_num.Value = 0;
            enter_num.Enabled = false;
            enter_num.Hide();

            save.Enabled = true;
        }
    }
}
