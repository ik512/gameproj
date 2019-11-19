using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsteroidProj
{
   static class Bullets
    {
      public  static int _count=-1;
      public  static List<Bullet> _bullets = new List<Bullet>(); 
        
        
        
        private static System.Windows.Forms.Timer _timer2 =new System.Windows.Forms.Timer() ;

        static public void Add(Bullet o)
        {
            if (_count >-1)
            {
                bool _break = false;
                for (var i = 0; i <= _count; i++)
                {
                    if (_bullets[i] == null) { _bullets.Insert(i, o);_break=true ;break; } 
                }
                if(_break==false) { ++_count; _bullets.Insert(_count, o);  }
            }
            else
            {
                ++_count; _bullets.Insert(_count, o);
            }
        }

        internal static async void Go()
        {
            _timer2.Interval = 25;
            _timer2.Start();
            _timer2.Tick += Timer_Tick;
               
            while (Game._timer.Enabled)
            {
                  await  Task.Delay(15);
                   Collision();
                // Update();
            }
              
                //Collision();
                //Update();

        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
           //Collision();
            Update();
           // Draw();
           
        }

        public static void Update() 
        {
            foreach (Bullet b in _bullets) { b?.Update(); }
        }
        public static void Draw()
        {
            foreach (Bullet b in _bullets) { b?.Draw(); }
        }
       private static void Collision()
        {
          
                if(_count>-1)
            {
                for (var i=0;i<=_count;i++)
                {
                    if (_bullets[i]?.PosX > Game.Width) _bullets[i] = null;
                    
                    for(int j=0;j<Game._asteroids.Length;j++)
                    {
                        if (_bullets[i] == null) continue;
                        if (Game._asteroids[j] == null) continue;
                        if ( _bullets[i].Collision(Game._asteroids[j]))
                        {
                            _bullets[i] = null; Game._asteroids[j] = null;++Game.Score;
                            System.Media.SystemSounds.Hand.Play();
                            var rnd = new Random();
                            int r = rnd.Next(15, 50);
                           Game._asteroids[j] = new Asteroid(new Point(rnd.Next(700, 750), rnd.Next(10, Game.Height)), new Point(rnd.Next(-10, 20), rnd.Next(-10, 20)), new Size(r, r));
                            //Game._asteroids[j] = new Asteroid(new Point(rnd.Next(200, 600),400), new Point(0, 0), new Size(r, r));

                        }
                    }
                    if (i > _count) break;
                }
            }

        }


    }
}
