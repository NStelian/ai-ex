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
    public static partial class Engine
    {
        public static int TopX;
        public static int TopY;
        public static int BottomX;
        public static int BottomY;

        public static int MapSize = 20;
        public static int TileSize = 50;
        public static Tile[,] Map;
        public static bool[,] Visible;

        public static int StringSize = 11;

        public static void InitCanvas()
        {
            Map = new Tile[MapSize, MapSize];
            Visible = new bool[MapSize, MapSize];
            TopX = 0;
            TopY = 0;
            BottomX = Canvas.Width;
            BottomY = Canvas.Height;
            for (int i = 0; i < MapSize; i++)
            {
                for (int j = 0; j < MapSize; j++)
                {
                    Map[i, j] = new Tile(TileSize, new Point(i * TileSize, j * TileSize), new Point(i * TileSize + TileSize, j * TileSize + TileSize));
                }
            }
            DrawMap();
        }

        public static void DrawMap()
        {
            bmp = new Bitmap(Canvas.Width, Canvas.Height);
            grp = Graphics.FromImage(bmp);
            for (int i = 0; i < MapSize; i++)
            {
                for (int j = 0; j < MapSize; j++)
                {
                    if (Map[i, j].Bottom.X > TopX && Map[i, j].Top.X < BottomX && Engine.TopY < BottomY && Engine.BottomY > TopY)
                    {
                        grp.DrawRectangle(new Pen(Color.Black, 2f), Map[i, j].Top.Y - TopY, Map[i, j].Top.X - TopX, Map[i, j].Size, Map[i, j].Size);
                        if (Visible[i, j])
                        {
                            grp.DrawString(Map[i, j].ContaintID.ToString(), new Font("Arial", StringSize), new SolidBrush(Color.Black), Map[i, j].Top.Y - TopY, Map[i, j].Top.X - TopX);
                            if (Map[i, j].ContaintID != 0)
                            {
                                grp.DrawString(Map[i, j].ResourceAmmount.ToString(), new Font("Arial", StringSize), new SolidBrush(Color.Black), Map[i, j].Top.Y - TopY, Map[i, j].Top.X - TopX + StringSize+1);
                                grp.DrawString(Map[i, j].AmmountOnTheGround.ToString(), new Font("Arial", StringSize), new SolidBrush(Color.Black), Map[i, j].Top.Y - TopY, Map[i, j].Top.X - TopX + 2*StringSize+1);
                            }
                            else if(Map[i,j].AmmountOnTheGround!=0)
                            {
                                grp.DrawString(Map[i, j].AmmountOnTheGround.ToString(), new Font("Arial", StringSize), new SolidBrush(Color.Black), Map[i, j].Top.Y - TopY, Map[i, j].Top.X - TopX + 2 * StringSize + 1);
                            }
                        }
                        else if (FogOn == true)
                        {
                            grp.FillRectangle(new SolidBrush(Color.Gray), Map[i, j].Top.Y - TopY, Map[i, j].Top.X - TopX, Map[i, j].Size, Map[i, j].Size);
                        }
                        else
                        {
                            grp.DrawString(Map[i, j].ContaintID.ToString(), new Font("Arial", StringSize), new SolidBrush(Color.Black), Map[i, j].Top.Y - TopY, Map[i, j].Top.X - TopX);
                            if (Map[i, j].ContaintID != 0)
                            {
                                grp.DrawString(Map[i, j].ResourceAmmount.ToString(), new Font("Arial", StringSize), new SolidBrush(Color.Black), Map[i, j].Top.Y - TopY, Map[i, j].Top.X - TopX + StringSize + 1);
                                grp.DrawString(Map[i, j].AmmountOnTheGround.ToString(), new Font("Arial", StringSize), new SolidBrush(Color.Black), Map[i, j].Top.Y - TopY, Map[i, j].Top.X - TopX + 2 * StringSize + 1);
                            }
                            else if (Map[i, j].AmmountOnTheGround != 0)
                            {
                                grp.DrawString(Map[i, j].AmmountOnTheGround.ToString(), new Font("Arial", StringSize), new SolidBrush(Color.Black), Map[i, j].Top.Y - TopY, Map[i, j].Top.X - TopX + 2 * StringSize + 1);
                            }
                        }
                    }
                }
            }
            DrawRobots();
            Canvas.Image = bmp;
        }

        public static void DrawRobots()
        {
            foreach (Base b in Bases)
            {
                foreach (Robot robot in b.Robots)
                {
                    grp.DrawEllipse(new Pen(robot.color, 3f), Map[robot.Position.X, robot.Position.Y].Top.Y - TopY, Map[robot.Position.X, robot.Position.Y].Top.X - TopX, Map[robot.Position.X, robot.Position.Y].Size, Map[robot.Position.X, robot.Position.Y].Size);
                }
            }
        }

        public static void tr()
        {
            CalcHelper c = new CalcHelper(Map);
            List<Point> p = c.CalcShortestPath(new Point(1, 2), new Point(6, 5));
            for (int i = 0; i < p.Count; i++)
            {
                Map[p[i].X, p[i].Y].ContaintID = i + 1;
            }
            DrawMap();
        }
    }
}
