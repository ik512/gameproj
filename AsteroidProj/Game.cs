using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace AsteroidProj
{
    class Game
    {
        private static readonly Image BackGrnd = new Bitmap(Path.GetFullPath(@"Resourses\Space.jpg"));
        private static Image Bimage=new Bitmap (BackGrnd,800,600);
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static BaseObject[] _objs;
        public static Asteroid[] _asteroids;
       // public static Bullet _bullet;
        public static  Task _task = new Task(() =>Bullets.Go());
        private static Ship _ship = new Ship(new Point(10, 400), new Point(5, 5), new Size(10, 10));
        public static readonly Timer _timer = new Timer();
        public static Random Rnd = new Random();
        public static int Score = 0;

        public static void Load()
        {
            _objs = new BaseObject[30];
            //_bullet = new Bullet(new Point(0, 200), new Point(5, 0), new Size(10, 10));
            _asteroids = new Asteroid[30];
            var rnd = new Random();
            for (var i = 0; i < _objs.Length; i++)
            {
               // int r = rnd.Next(5, 50);
                _objs[i] = new Star(new Point(800, rnd.Next(0, Game.Height)), new Point(rnd.Next(5,50), 1), new Size(2, 2));
            }
            for (var i = 0; i < _asteroids.Length; i++)
            {
                int r = rnd.Next(15, 50);
                _asteroids[i] = new Asteroid(new Point(rnd.Next(700, 750), rnd.Next(10, Game.Height)), new Point(rnd.Next(-10, 20), rnd.Next(-10, 20)), new Size(r, r));
            }


        }

        public Game()
        {
        }
        public static void Init(Form form)
        {
            Graphics g;
            _context = BufferedGraphicsManager.Current; //доступ к главному буферу
            g = form.CreateGraphics();
            Width = form.ClientSize.Width; //считываем размеры формы
            Height = form.ClientSize.Height;
            //Allocate связывает графику и буфер
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
            Load();
            Ship.MessageDie += Finish;
            _timer.Interval = 50;
            _timer.Start();
            _timer.Tick += Timer_Tick;
            _task.RunSynchronously();
            form.KeyDown += Form_KeyDown; 
        }
        public static void Draw()
        {
            Buffer.Graphics.DrawImage(Bimage,new Point(0,0));            
            foreach (BaseObject obj in _objs)
                obj.Draw();
            foreach (Asteroid a in _asteroids)
            {
                a?.Draw();
            }
          // _bullet?.Draw();
            Bullets.Draw();
            _ship?.Draw();
            if (_ship != null)
                Buffer.Graphics.DrawString("Energy:" + _ship.Energy,
                SystemFonts.DefaultFont, Brushes.White, 0, 0) ;
            Buffer.Graphics.DrawString($"Score:{ Score}",
               SystemFonts.DefaultFont, Brushes.GreenYellow, 100, 0);
            Buffer.Graphics.DrawString($"Bullets now:{ Bullets._count}",
              SystemFonts.DefaultFont, Brushes.GreenYellow, 200, 0);
            Buffer.Render();
            
        }

        public static void Update()
        {
            var rnd = new Random();
           // Bullets.Update();
            foreach (BaseObject obj in _objs) obj.Update();
            //_bullet?.Update();
            for (var i = 0; i < _asteroids.Length; i++)
            {
                if (_asteroids[i] == null) continue;
                _asteroids[i].Update();
                //if (_bullet != null && _bullet.Collision(_asteroids[i]))
                //{
                //    System.Media.SystemSounds.Hand.Play();
                //    _asteroids[i] = null;
                //    _bullet = null;
                //    int r = rnd.Next(5, 50);
                //    //_asteroids[i] = new Asteroid(new Point(rnd.Next(200, 750), rnd.Next(10, Game.Height)), new Point(rnd.Next(-10, 20), rnd.Next(-10, 20)), new Size(r, r));
                //    continue;
                //}
                if (!_ship.Collision(_asteroids[i])) continue;
                // var rnd = new Random();
                _ship?.EnergyLow(rnd.Next(1, 10));
                System.Media.SystemSounds.Asterisk.Play();
                if (_ship.Energy <= 0) _ship?.Die();
            }
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
           
        }
        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            
                 if (e.KeyCode == Keys.ControlKey) Bullets.Add(new Bullet(new
                 Point(_ship.Rect.X + 10, _ship.Rect.Y + 4), new Point(4, 0), new Size(2, 1)));
                //if (e.KeyCode == Keys.ControlKey) _bullet = new Bullet(new
                //Point(_ship.Rect.X + 10, _ship.Rect.Y + 4), new Point(4, 0), new Size(4, 1));
                if (e.KeyCode == Keys.Up) _ship.Up();
                if (e.KeyCode == Keys.Down) _ship.Down();

                //if (e.KeyCode == Keys.Up) _ship.Up();
                // if (e.KeyCode == Keys.Down) _ship.Down();
            
        }
        
        public static void Finish()
        {
            _timer.Stop();
            Buffer.Graphics.DrawString("The End", new Font(FontFamily.GenericSansSerif,
            60, FontStyle.Underline), Brushes.White, 200, 100);
            Buffer.Render();
        }


    }
}
