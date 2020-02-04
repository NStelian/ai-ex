using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Detrecere
{
    public class CalcHelper
    {
        public int[,] map;

        public CalcHelper(Tile[,] map)
        {

            this.map = new int[map.GetLength(0), map.GetLength(1)];

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    this.map[i, j] = map[i, j].ContaintID;
                }
            }
        }



        public List<Point> CalcShortestPath(Point Sp, Point Ep)
        {
            if(Ep==Sp)
            {
                return new List<Point>() { Ep };
            }
            List<Point> points = new List<Point>();
            points.Add(Sp);
            int index = 1;
            map[Sp.X, Sp.Y] = index;
            while (points.Count!=0)
            {
                Point aux = points[0];
                index = map[aux.X, aux.Y];
                index++;
                points.RemoveAt(0);

                if (aux.X - 1 >= 0 && map[aux.X - 1, aux.Y] == 0 && Engine.Visible[aux.X-1,aux.Y])
                {
                    points.Add(new Point(aux.X - 1, aux.Y));
                    map[aux.X - 1, aux.Y] = index;
                    if (new Point(aux.X - 1, aux.Y) == Ep)
                    {
                        break;
                    }
                }
                if (aux.Y - 1 >= 0 && map[aux.X, aux.Y - 1] == 0 && Engine.Visible[aux.X, aux.Y-1])
                {
                    points.Add(new Point(aux.X, aux.Y - 1));
                    map[aux.X, aux.Y - 1] = index;
                    if (new Point(aux.X, aux.Y - 1) == Ep)
                    {
                        break;
                    }
                }
                if (aux.X + 1 < map.GetLength(0) && map[aux.X + 1, aux.Y] == 0 && Engine.Visible[aux.X+1, aux.Y])
                {
                    points.Add(new Point(aux.X + 1, aux.Y));
                    map[aux.X + 1, aux.Y] = index;
                    if (new Point(aux.X + 1, aux.Y) == Ep)
                    {
                        break;
                    }
                }
                if (aux.Y + 1 < map.GetLength(1) && map[aux.X, aux.Y + 1] == 0 && Engine.Visible[aux.X, aux.Y+1])
                {
                    points.Add(new Point(aux.X, aux.Y + 1));
                    map[aux.X, aux.Y + 1] = index;
                    if (new Point(aux.X, aux.Y + 1) == Ep)
                    {
                        break;
                    }
                }
            }
            if(map[Ep.X,Ep.Y]==0)
            {
                return new List<Point>();
            }
            return BackTrackPath(Sp,Ep);
        }



        public List<Point> BackTrackPath(Point Sp, Point Ep)
        {

            List<Point> Route = new List<Point>();
            Point cp = Ep;
            Route.Add(Ep);
            while(cp!=Sp)
            {
                List<Point> RoutesToChoose = new List<Point>();
                Point aux = cp;
                int cv = map[aux.X, aux.Y];

                if(aux.X-1>=0 && map[aux.X-1,aux.Y]==cv-1)
                {
                    RoutesToChoose.Add(new Point(aux.X - 1, aux.Y));
                }
                if (aux.Y - 1 >= 0 && map[aux.X,aux.Y-1]==cv-1)
                {
                    RoutesToChoose.Add(new Point(aux.X, aux.Y - 1));
                }
                if(aux.X+1<map.GetLength(0)&& map[aux.X+1,aux.Y]==cv-1)
                {
                    RoutesToChoose.Add(new Point(aux.X + 1, aux.Y));
                }
                if(aux.Y+1<map.GetLength(0)&& map[aux.X,aux.Y+1]==cv-1)
                {
                    RoutesToChoose.Add(new Point(aux.X, aux.Y + 1));
                }

                cp = RoutesToChoose[Engine.rnd.Next(RoutesToChoose.Count)];
                Route.Add(cp);
            }
            return Route;
        }

        public List<Point> GetEmptyAround(Point Pos)
        {
            List<Point> p = new List<Point>();

            if (Pos.X - 1 >= 0 && map[Pos.X - 1, Pos.Y] == 0 && Engine.Visible[Pos.X-1,Pos.Y])
            {
                p.Add(new Point(Pos.X - 1, Pos.Y));
            }

            if (Pos.Y - 1 >= 0 && map[Pos.X, Pos.Y - 1] == 0 && Engine.Visible[Pos.X, Pos.Y-1])
            {
                p.Add(new Point(Pos.X, Pos.Y - 1));
            }

            if (Pos.X + 1 < map.GetLength(0) && map[Pos.X + 1, Pos.Y] == 0 && Engine.Visible[Pos.X+1, Pos.Y])
            {
                p.Add(new Point(Pos.X + 1, Pos.Y));
            }

            if (Pos.Y + 1 < map.GetLength(1) && map[Pos.X, Pos.Y + 1] == 0 && Engine.Visible[Pos.X, Pos.Y+1])
            {
                    p.Add(new Point(Pos.X, Pos.Y + 1));
            }
            return p;
        }

    }
}
