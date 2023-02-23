using Pelotas.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pelotas
{
    public partial class Comets : Form
    {
        static List<Particle> balls;
        static Bitmap bmp;
        static Graphics g;
        static Random rand = new Random();
        static float deltaTime;
        private float elapsedTime = 0f;
        ColorMatrix cm = new ColorMatrix();
        ImageAttributes ia = new ImageAttributes();
        Image particleImage = Resources.snow;

        public int alpha = 255;

        Emitter emitter = new Emitter(rand, new Size(800, 600), 100, 100);
        public Comets()
        {
            InitializeComponent();
            cm.Matrix33 = alpha / 255f; // set the alpha value
            ia.SetColorMatrix(cm);
        }

        public void Init()
        {
            if (PCT_CANVAS.Width == 0)
                return;

            balls = new List<Particle>();
            bmp = new Bitmap(PCT_CANVAS.Width, PCT_CANVAS.Height);
            g = Graphics.FromImage(bmp);
            deltaTime = 1;
            PCT_CANVAS.Image = bmp;

            for (int b = 0; b < 100; b++)
                balls.Add(new Particle(rand, PCT_CANVAS.Size, b));
        }

        private void Pelotas_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void Pelotas_SizeChanged(object sender, EventArgs e)
        {
            Init();
        }

        private void TIMER_Tick(object sender, EventArgs e)
        {
            g.Clear(Color.Transparent);

            emitter.Update(deltaTime);


            for (int i = balls.Count - 1; i >= 0; i--)
            {
                Particle P;
                P = balls[i];
                balls[i].Update(deltaTime, balls);
            }

            Particle p;
            for (int b = 0; b < balls.Count; b++)//PINTAMOS EN SECUENCIA
            {
                p = balls[b];

                //DRAWN PARTICLES
                // if (alpha < 0) alpha = 0;
                // Create a new Color object with the modified alpha value
                // Color newColor = Color.FromArgb(alpha, p.c.R, p.c.G, p.c.B);
                g.FillEllipse(new SolidBrush(p.c), p.x - p.radio, p.y - p.radio, p.radio * 2, p.radio * 2);

                //IMAGE PARTICLES
                Color color = Color.FromArgb(alpha, 255, 255, 255);

                //g.DrawImage(particleImage, p.x - p.radio, p.y - p.radio, p.radio * 2, p.radio * 2);
                alpha = 255;
                alpha -= 5;
                color = Color.FromArgb(alpha, 255, 255, 255);
                //particleImage.MakeTransparent(color);
                //particleImage.M
            }
            //alpha += alphaStep;


            PCT_CANVAS.Invalidate();
            deltaTime += .1f;
            elapsedTime += deltaTime;

        }


    }
}
