using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidProj
{
    class Asteroid : BaseObject
    {
        public int Power { get; set; }
        public Asteroid (Point pos, Point dir, Size size ) : base(pos, dir, size)
        {
            this.Power = 1;
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.FillEllipse(Brushes.White, Pos.X, Pos.Y, Size.Width, Size.Height);

        }
        public override void Update()
        {
            //Pos.X = 100;
            Pos.X = Pos.X + Dir.X;
            Pos.Y = Pos.Y + Dir.Y;
            if (Pos.X < 0) Dir.X = -Dir.X;
            if (Pos.X > Game.Width) Dir.X = -Dir.X;
            if (Pos.Y < 0) Dir.Y = -Dir.Y;
           if (Pos.Y > Game.Width) Dir.Y = -Dir.Y;
        }
    }
}
