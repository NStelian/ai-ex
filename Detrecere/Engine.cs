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

    public enum TileTipes
    {
        Empty, Iron = 10001, Base
    };

    public static partial class Engine
    {
        public static bool FogOn = true;

        public static PictureBox Canvas;
        public static Graphics grp;
        public static Bitmap bmp;

        public static Random rnd = new Random();

        public static List<Base> Bases = new List<Base>();

        public static int Rows;
        public static int Columns;

        public static void Init(PictureBox canvas)
        {
            Bases.Add(new Base(new Point(0, 0)));
            Canvas = canvas;
            InitCanvas();
            SpawnMats();
            Map[0, 0].ContaintID = (int)TileTipes.Base;
        }

        public static void GameSeq()
        {
            foreach (Base bas in Bases)
            {
                bas.Work();

                foreach (Robot robot in bas.Robots)
                {
                    robot.Work();
                }
            }
        }

        public static void SpawnMats()
        {
            int MatsToSpawn = MapSize / 5;
            //int MatsToSpawn = 50;

            for (int q=0;q<MatsToSpawn;q++)
            { 
                int xPos = Engine.rnd.Next(1, MapSize);
                int yPos = Engine.rnd.Next(1, MapSize);

                Map[xPos, yPos].ContaintID = (int)TileTipes.Iron;
                Map[xPos, yPos].ResourceAmmount = 200;

                for (int i = xPos - 1; i <= xPos + 1; i++)
                {
                    for (int j = yPos - 1; j <= yPos + 1; j++)
                    {
                        if (i >= 0 && j >= 0 && i < MapSize && j < MapSize && (i!=xPos || j!=yPos) && Map[i,j].ContaintID==0)
                        {
                            int xDist = Math.Abs(xPos - i);
                            int yDist = Math.Abs(yPos - j);

                            Map[i, j].ContaintID = (int)TileTipes.Iron;
                            Map[i, j].ResourceAmmount = Engine.rnd.Next(200 - Engine.rnd.Next(20 + 30 * xDist) - Engine.rnd.Next(20 + 30 * yDist));
                        }
                    }
                }
            }
        }
    }
}
