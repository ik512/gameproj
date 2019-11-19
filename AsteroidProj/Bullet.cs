using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidProj
{
    class Bullet : BaseObject
    {
        public int PosX { get { return this.Pos.X; } }
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size) 
        {
        
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawRectangle(Pens.Yellow, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
        public override void Update()
        {
           this.Pos.X = Pos.X + 5;
            
        }
    }
}
