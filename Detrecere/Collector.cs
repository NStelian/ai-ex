using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detrecere
{
    public class Collector:Robot
    {
        List<Point> Path;
        public Point Target;
        public Point BaseLoc;

        public Point CollectPoint;

        public bool IsIdle;

        public int CollectAmmount = 5;
        public int MaxHoldAmmount = 20;
        public int InInventory = 0;

        public Collector(Point Position,Point Target,Point BaseLoc)
        {
            this.Position = Position;
            this.BaseLoc = BaseLoc;

            CollectPoint = GetBaseCollectPoint();

            GetShortestPath(Target);
            IsIdle = false;

            color = Color.Purple;
        }


        public override void Work()
        {
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
                if(Position == CollectPoint)
                {
                    Engine.Map[BaseLoc.X, BaseLoc.Y].ResourceAmmount += InInventory;
                    InInventory = 0;
                    if(Engine.Map[Target.X,Target.Y].AmmountOnTheGround>0)
                    {
                        GetShortestPath(Target);
                    }
                    else
                    {
                        IsIdle = true;
                    }
                }
                else
                {
                    if(Engine.Map[Position.X,Position.Y].AmmountOnTheGround>=CollectAmmount)
                    {
                        if(InInventory+CollectAmmount<=MaxHoldAmmount)
                        {
                            Engine.Map[Position.X, Position.Y].AmmountOnTheGround-=CollectAmmount;
                            InInventory += CollectAmmount;
                        }
                        else
                        {
                            Engine.Map[Position.X, Position.Y].AmmountOnTheGround -= MaxHoldAmmount - InInventory;
                            InInventory = MaxHoldAmmount;
                            GetShortestPath(CollectPoint);
                        }
                    }
                    else
                    {
                        if(InInventory+Engine.Map[Position.X,Position.Y].AmmountOnTheGround<=MaxHoldAmmount)
                        {
                            InInventory += Engine.Map[Position.X, Position.Y].AmmountOnTheGround;
                            Engine.Map[Position.X, Position.Y].AmmountOnTheGround = 0;
                            GetShortestPath(CollectPoint);
                            IsIdle = true;
                        }
                        else
                        {
                            Engine.Map[Position.X, Position.Y].AmmountOnTheGround -= MaxHoldAmmount - InInventory;
                            InInventory = MaxHoldAmmount;
                            GetShortestPath(CollectPoint);
                        }
                    }
                }
            }
        }

        public Point GetBaseCollectPoint()
        {
            CHelper = new CalcHelper(Engine.Map);
            List<Point> ar = new List<Point>();
            ar = CHelper.GetEmptyAround(BaseLoc);
            return ar[Engine.rnd.Next(ar.Count)];
        }

        public void GetShortestPath(Point TargetLocation)
        {
            Path = new List<Point>();
            CHelper = new CalcHelper(Engine.Map);
            Path = CHelper.CalcShortestPath(this.Position, TargetLocation);
        }


    }
}
