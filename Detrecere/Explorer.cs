using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detrecere
{
    public class Explorer : Robot
    {
        public List<Point> Path;

        public Explorer(Point Position)
        {
            this.Position = Position;
            Path = new List<Point>();
            color = Color.Blue;
        }

        public override void Work()
        {
            LookAround();
            if (Path.Count != 0)
            {
                Path.RemoveAt(Path.Count - 1);
                if (Path.Count - 1 >= 0)
                {
                    this.Position = Path[Path.Count - 1];
                }
            }
            else
            {
                if (Engine.rnd.Next(10) <= 5)
                {
                    List<Point> PointsForDisc = GetReachebleNearNotVisible();
                    if (PointsForDisc.Count>0)
                    {
                        GetShortestPath(PointsForDisc[Engine.rnd.Next(PointsForDisc.Count)]);
                    }
                    else
                    {
                        bool GotoFound = false;
                        while (!GotoFound)
                        {
                            int xPos = Engine.rnd.Next(Engine.MapSize);
                            int yPos = Engine.rnd.Next(Engine.MapSize);
                            if (Engine.Visible[xPos, yPos] == true && Engine.Map[xPos, yPos].ContaintID == 0)
                            {
                                GotoFound = true;
                                GetShortestPath(new Point(xPos, yPos));
                            }
                        }
                    }
                }
                else
                {
                    bool GotoFound = false;
                    while (!GotoFound)
                    {
                        int xPos = Engine.rnd.Next(Engine.MapSize);
                        int yPos = Engine.rnd.Next(Engine.MapSize);
                        if (Engine.Visible[xPos, yPos] == true && Engine.Map[xPos, yPos].ContaintID == 0)
                        {
                            GotoFound = true;
                            GetShortestPath(new Point(xPos, yPos));
                        }
                    }
                }
            }
        }

        public void LookAround()
        {
            for (int i = Position.X - 2; i <= Position.X + 2; i++)
            {
                for (int j = Position.Y - 2; j <= Position.Y + 2; j++)
                {
                    if (i >= 0 && j >= 0 && i < Engine.MapSize && j < Engine.MapSize)
                    {
                        Engine.Visible[i, j] = true;
                    }
                }
            }
        }

        public List<Point> GetReachebleNearNotVisible()
        {
            CHelper = new CalcHelper(Engine.Map);
            List<Point> TilesToGo = new List<Point>();
            for (int i = 0; i < Engine.MapSize; i++)
            {
                for (int j = 0; j < Engine.MapSize; j++)
                {
                    if (Engine.Visible[i, j] == false)
                    {
                        List<Point> aux = CHelper.GetEmptyAround(new Point(i, j));
                        for (int q = 0; q < aux.Count; q++)
                        {
                            if (!TilesToGo.Contains(aux[q]) && aux[q] != this.Position && CHelper.CalcShortestPath(this.Position, aux[q]) != null)
                            {
                                TilesToGo.Add(aux[q]);
                            }
                        }
                    }
                }
            }
            return TilesToGo;
        }

        public void GetShortestPath(Point TargetLocation)
        {
            Path = new List<Point>();
            CHelper = new CalcHelper(Engine.Map);
            Path = CHelper.CalcShortestPath(this.Position, TargetLocation);
        }
    }
}
