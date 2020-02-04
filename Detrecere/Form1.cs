using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Detrecere
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public bool MDown = false;
        public Point Mdp;

        private void Form1_Load(object sender, EventArgs e)
        {
            Engine.Init(pictureBox1);
            int t = 0;
            for(int i=0;i<Engine.MapSize;i++)
            {
                for(int j=0;j<Engine.MapSize;j++)
                {
                    t += Engine.Map[i, j].ResourceAmmount;
                }
            }
            label2.Text = timer1.Interval.ToString()+" ms";
        }

        private void MoveUp_Click(object sender, EventArgs e)
        {
            if (!(Engine.TopX <= 0))
            {
                Engine.TopX -= Engine.TileSize;
                Engine.BottomX -= Engine.TileSize;
            }
            Engine.DrawMap();
        }

        private void MoveRight_Click(object sender, EventArgs e)
        {
            if (!(Engine.BottomY >= Engine.TileSize * Engine.MapSize))
            {
                Engine.TopY += Engine.TileSize;
                Engine.BottomY += Engine.TileSize;
            }
            Engine.DrawMap();
        }

        private void MoveDown_Click(object sender, EventArgs e)
        {
            if (!(Engine.BottomX >= Engine.TileSize * Engine.MapSize))
            {
                Engine.TopX += Engine.TileSize;
                Engine.BottomX += Engine.TileSize;
            }
            Engine.DrawMap();

        }

        private void MoveLeft_Click(object sender, EventArgs e)
        {
            if (!(Engine.TopY <= 0))
            {
                Engine.TopY -= Engine.TileSize;
                Engine.BottomY -= Engine.TileSize;
            }
            Engine.DrawMap();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            MDown = true;
            Mdp = new Point(Cursor.Position.X, Cursor.Position.Y);
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            MDown = false;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if(MDown)
            {
                Point mpos = new Point(Cursor.Position.X, Cursor.Position.Y);
                int xC = mpos.X - Mdp.X;
                int yC = mpos.Y - Mdp.Y;
                if (Engine.TopY - xC >= 0)
                {
                    Engine.TopY += -xC;
                    Engine.BottomY += -xC;
                }
                if (Engine.TopX - yC >= 0)
                {
                    Engine.TopX += -yC;
                    Engine.BottomX += -yC;
                }
                Engine.DrawMap();
                Mdp = mpos;
               
            }
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            MDown = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Engine.GameSeq();
            Engine.DrawMap();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Engine.FogOn = !Engine.FogOn;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Interval = int.Parse(richTextBox1.Text);
            label2.Text = timer1.Interval.ToString()+" ms";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled;
        }
    }
}
