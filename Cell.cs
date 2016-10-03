using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Conway_Game_of_Life
{
    public class Cell
    {
        public Button button = new Button();
        public List<Pt> neighbors = new List<Pt>();

        private int indx_row, indx_col;
        private Pt position;
        private State s;

        public State state
        {
            get { return s; }
            set
            {
                s = value;
                if (value == State.Dead)
                    button.BackColor = Color.White;
                if (value == State.Alive)
                    button.BackColor = Color.Black;
            }
        }

        public Cell(int sz, Pt p, State cur_state)
        {
            indx_row = p.x;
            indx_col = p.y;
            position = new Pt(p.y * Form1.sz, p.x * Form1.sz + Form1.sz);
            state = cur_state;

            button.Click += Button_Click;
            button.Location = new Point(position.x, position.y);
            button.Size = new Size(sz, sz);

            findNeighbors();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (state == State.Dead) state = State.Alive;
            else state = State.Dead;
        }

        private void findNeighbors()
        {
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (dx == 0 && dy == 0) continue; // We are not checking a neighbor, we are checking the point itself -> skip it.

                    // allow to wrap around the board.
                    int check_col = Utility.mod(indx_col + dx, Form1.colsn);
                    int check_row = Utility.mod(indx_row + dy, Form1.rowsn);

                    neighbors.Add(new Pt(check_row, check_col));
                }
            }
        }

    }
}
