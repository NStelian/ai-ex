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
    public class Tile
    {
        public int Size;
        public Point Top;
        public Point Bottom;

        public int ContaintID;
        public int ResourceAmmount;

        public int AmmountOnTheGround;

        public Tile(int Size, Point Top, Point Bottom,int ContaintID = 0)
        {
            this.Size = Size;
            this.Top = Top;
            this.Bottom = Bottom;
            this.ContaintID = ContaintID; 
            AmmountOnTheGround = 0;
            if(ContaintID == 0)
            {
                ResourceAmmount = 0;
            }
        }
    }
}
