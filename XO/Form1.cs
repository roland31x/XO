using System.Globalization;
using static System.Formats.Asn1.AsnWriter;

namespace XO
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Height = 800;
            Width = 800;
            pictureBox1.Height = 600;
            pictureBox1.Width = 600;
            pictureBox1.BackColor = Color.Gray;
            pictureBox1.Location = new Point(100, 100);
            button1.Text = "NEW GAME";
            button1.Width = 100;
            button1.Height = 50;
            Mat = new Button[3, 3];
        }
        int round = 1;
        Button[,] Mat;
        int n = 3;
        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Button b = new Button
                    {
                        Size = new Size(pictureBox1.Width / 3, pictureBox1.Height / 3),
                        BackColor = Color.Aquamarine,
                        Location = new Point(j * pictureBox1.Height / 3, i * pictureBox1.Height / 3),
                        Parent = pictureBox1,
                        Font = new Font("Arial", 50),
                    };
                    b.Click += ButtonClick;
                    Mat[i, j] = b;
                }
            }
        }

        private void ButtonClick(object? sender, EventArgs e)
        {
            Button b = sender as Button;
            b.Enabled = false;
            if (round % 2 == 1)
            {
                b.Text = "X";
            }
            else
            {
                b.Text = "O";
            }
            round++;
            if (GameIsWon())
            {
                MessageBox.Show($"Player {b.Text} won!");
                NewGame(false);
                return;
            }
            if (GameIsOver())
            {
                MessageBox.Show("It's a Draw", "Game Over");
                NewGame(false);
            }

        }
        private void NewGame(bool IsEnabled)
        {
            foreach (Button b in Mat)
            {
                b.Enabled = IsEnabled;
                if (IsEnabled)
                {
                    b.Text = "";
                }               
            }            
        }
        private bool GameIsOver()
        {
            if (round == 9)
            {
                return true;
            }

            return false;
        }

        private bool GameIsWon()
        {
            int[,] mat = new int[3, 3];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (Mat[i, j].Text == "X")
                    {
                        mat[i, j] = 1;
                    }
                    else if (Mat[i, j].Text == "O")
                    {
                        mat[i, j] = 10;
                    }
                }
            }
            int diagscore2 = 0;
            int diagscore = 0;
            for (int i = 0; i < n; i++)
            {
                int score = 0;
                int score2 = 0;
                for (int j = 0; j < n; j++)
                {
                    if (i == j)
                    {
                        diagscore += mat[i, j];
                    }
                    if (i + j == n - 1)
                    {
                        diagscore2 += mat[i, j];
                    }
                    score += mat[i, j];
                    score2 += mat[j, i];
                }
                if (score == 3 || score == 30)
                {
                    return true;
                }
                if (score2 == 3 || score2 == 30)
                {
                    return true;
                }
            }
            if (diagscore == 3 || diagscore == 30)
            {
                return true;
            }
            if (diagscore2 == 3 || diagscore2 == 30)
            {
                return true;
            }

            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NewGame(true);
            round = 0;
        }
    }
}