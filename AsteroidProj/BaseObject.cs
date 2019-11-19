using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using AsteroidProj.Interface;

namespace AsteroidProj
{
    abstract class BaseObject:ICollision
    {
        protected Point Pos;
        protected Point Dir;
        protected Size Size;
        public delegate void Message();
        protected BaseObject(Point pos,Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
        }

        public Rectangle Rect =>  new Rectangle (Pos,Size) ;

        public bool Collision(ICollision o) => o.Rect.IntersectsWith(this.Rect);
        

        public abstract void Draw();

        public abstract void Update();
        //{
        //    Pos.X = Pos.X + Dir.X;
        //    Pos.Y = Pos.Y + Dir.Y;
        //    if (Pos.X < 0) Dir.X = -Dir.X;
        //    if (Pos.X > Game.Width) Dir.X = -Dir.X;
        //    if (Pos.Y <0) Dir.Y = -Dir.Y;
        //    if (Pos.Y > Game.Width) Dir.Y = -Dir.Y;
        //}
    }
}
