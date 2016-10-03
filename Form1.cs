using System;
using System.Drawing;
using System.Windows.Forms;

namespace Conway_Game_of_Life
{
    public enum State { Dead, Alive };

    public struct Pt
    {
        public int x, y;
        public Pt(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public Pt(Pt p)
        {
            x = p.x;
            y = p.y;
        }
    }

    public partial class Form1 : Form
    {
        public static int sz = 25, rowsn = 28, colsn = 40;
        Cell[,] cells = new Cell[rowsn,colsn];
        State[,] temp = new State[rowsn, colsn];

        public Form1()
        {
            InitializeComponent();

            CreateGrid();
            Save_Load_Buttons();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Size = new Size(1000, 700);
        }

        private void CreateGrid()
        {
            for (int i = 0; i < rowsn; i++)
            {
                for (int k = 0; k < colsn; k++)
                {
                    cells[i, k] = new Cell(sz, new Pt(i, k), State.Dead);
                    Controls.Add(cells[i, k].button);
                }
            }
        }

        private void Start_Click(object sender, EventArgs e)
        {
            timer1.Interval = 250;

            Button button = sender as Button;
            if (button.Text == "Start")
            {
                timer1.Start();
                button.Text = "Stop";
            }
            else if (button.Text == "Stop")
            {
                timer1.Stop();
                button.Text = "Start";

                // clear the board
                for (int i = 0; i < rowsn; i++)
                    for (int k = 0; k < colsn; k++)
                        cells[i, k].state = temp[i, k] = State.Dead;
            }
        }

        private void updateSpeed(object sender, EventArgs e)
        {
            NumericUpDown ticker = sender as NumericUpDown;
            timer1.Interval = (int)ticker.Value;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < rowsn; i++)
            {
                for (int k = 0; k < colsn; k++)
                {
                    //update the cell.
                    int live_neighbor = 0;
                    foreach (Pt n in cells[i, k].neighbors)
                    {
                        if (cells[n.x, n.y].state == State.Alive)  live_neighbor++;
                    }

                    // apply rules
                    if (cells[i, k].state == State.Alive)
                    {
                        if (live_neighbor < 2 || live_neighbor > 3)
                            temp[i, k] = State.Dead;
                        else
                            temp[i, k] = State.Alive;
                    }
                    else if (cells[i, k].state == State.Dead && (live_neighbor == 3))
                        temp[i, k] = State.Alive;
                }
            }

            for (int i = 0; i < rowsn; i++)
                for (int k = 0; k < colsn; k++)
                    if (cells[i, k].state != temp[i, k])
                        cells[i, k].state = temp[i, k];
        }
    }
}
