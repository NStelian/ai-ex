using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detrecere
{
    public class Base
    {
        public Point Position;

        public List<Robot> Robots;


        public Base(Point Position)
        {
            Robots = new List<Robot>();
            this.Position = Position;
            Robots.Add(new Explorer(this.Position));
        }

        public void Work()
        {
            for (int i = 0; i < Engine.MapSize; i++)
            {
                for (int j = 0; j < Engine.MapSize; j++)
                {
                    if (Engine.Visible[i, j] && Engine.Map[i, j].ContaintID == (int)TileTipes.Iron)
                    {
                        PutExploiterToWork(new Point(i,j));
                    }

                    if(Engine.Visible[i,j] && Engine.Map[i,j].AmmountOnTheGround>0 && Engine.Map[i,j].ContaintID==0)
                    {
                        PutCollecterToWork(new Point(i,j));
                    }
                }
            }
        }


        private void PutCollecterToWork(Point loc)
        {
            if(Robots.Count<=10)
            {
                Robots.Add(new Collector(this.Position, loc, this.Position));
            }
            else
            {
                foreach(Robot col in Robots)
                {
                    if(col is Collector)
                    {
                        Collector CCol = (col as Collector);
                        if(CCol.IsIdle)
                        {
                            CCol.GetShortestPath(loc);
                            CCol.IsIdle = false;
                        }
                    }
                }
            }
        }


        private void PutExploiterToWork(Point loc)
        {
            if (Robots.Count <= 3)
            {
                Robots.Add(new Exploiter(this.Position, loc,this.Position));
            }
            else
            {
                foreach (Robot exp in Robots)
                {
                    if (exp is Exploiter)
                    {
                        Exploiter CEx = (exp as Exploiter);
                        if (CEx.IsIdle)
                        {
                            CEx.GetShortestPath(loc);
                            CEx.IsIdle = false;
                        }
                    }
                }
            }
        }

    }

}
