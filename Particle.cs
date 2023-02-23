using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Pelotas
{
    public class Particle
    {
        int index;
        Size space;
        public Color c;
        static Random rand = new Random();


        // Variables de posición
        public float x;
        public float y;

        // Variables de velocidad
        private float vx;
        private float vy;

        // Variable de vida y tamaño
        public float radio;
        public float lifespan;


        // Particle generator
        public Particle(Random rand,Size size, int index)
        {
            this.radio = rand.Next(1, 10);
            this.x      = rand.Next(1,1249);
            this.x++;
            this.y      = rand.Next(1,766);
            this.y++;

            c           = Color.FromArgb(rand.Next(200, 255), rand.Next(250, 255), rand.Next(250, 255), rand.Next(250, 255));
            
            // Velocidades iniciales
            this.vx = rand.Next(1, 3);
            this.vy = rand.Next(1, 100);

            this.lifespan = rand.Next(20, 430);

            this.index = index;
            space = size;
        }

        // Método para actualizar la posición de la pelota en función de su velocidad
        public void Update(float deltaTime, List<Particle> balls)
        {
            Random RAND = new Random();
            //int alpha;
            if (this.lifespan <= 0)
            {
                balls.Remove(this);
                if (balls.Count <= 80)
                {
                    balls.Add(new Particle(rand, space, balls.Count));
                }

            }

            if ((x - radio) <= 0 || (x + radio) >= space.Width)
            {
                if (x - radio <= 0)
                    x = radio + 3;
                else
                    x = space.Width - radio-3;
                    
                vx *= -.50f;
                vy *= .75f;
            }
            /*
            if ((y - radio) <= 0 || (y + radio) >= space.Height)
            {
                if (y - radio <=  0)
                    y = radio + 3;
                else
                    y = space.Height - radio-3;

                vx *=  .75f;
                vy *= -.55f;
                
            }*/

            this.x += this.vx + .55f; //* deltaTime;
            this.y += this.vy + 4; //* deltaTime;

            int alpha = (int)Math.Min(255, 255 * this.lifespan / 160);
            if (alpha <= 0) alpha = 0;
            this.c = Color.FromArgb(alpha, c.R, c.G, c.B);
            // Si el tiempo de vida llega a cero, remover la pelota de la lista

            this.lifespan -= 40; 
        }

        // Método para manejar colisiones entre pelotas
        /*public void Collision(Particle otraPelota)
        {
            float distancia = (float)Math.Sqrt(Math.Pow((otraPelota.x - this.x), 2) + Math.Pow((otraPelota.y - this.y), 2));

            if (distancia < (this.radio + otraPelota.radio))//ESTO SIGNIFICA COLISIÓN...
            {
                // Calculamos las velocidades finales de cada pelota en función de su masa y velocidad inicial
                float masaTotal = this.radio + otraPelota.radio;
                float masaRelativa = this.radio / masaTotal;

                float v1fx = this.vx - masaRelativa * (this.vx - otraPelota.vx) / 2;
                float v1fy = this.vy - masaRelativa * (this.vy - otraPelota.vy) / 12;

                float v2fx = otraPelota.vx - masaRelativa * (otraPelota.vx - this.vx) / 2;
                float v2fy = otraPelota.vy - masaRelativa * (otraPelota.vy - this.vy) / 12;

                // Actualizamos las velocidades de las pelotas
                this.vx = v1fx;     // -----AQUI CAMBIAMOS EL ANGULO---------
                this.vy = v1fy;     // -----AQUI CAMBIAMOS EL ANGULO--------------

                otraPelota.vx = v2fx;//-----AQUI CAMBIAMOS EL ANGULO----------------------
                otraPelota.vy = v2fy;//-----AQUI CAMBIAMOS EL ANGULO----------------------

                // Movemos las pelotas para evitar que se superpongan
                float distanciaOverlap = (this.radio + otraPelota.radio) - distancia;
                float dx = (this.x - otraPelota.x) / distancia;
                float dy = (this.y - otraPelota.y) / distancia;

                this.x += dx * distanciaOverlap / 2f;
                this.y += dy * distanciaOverlap / 2f;

                otraPelota.x -= dx * distanciaOverlap / 2f;
                otraPelota.y -= dy * distanciaOverlap / 2f;
            }
        }
        */

        public bool IsExpired(float elapsedTime)
        {
            return (elapsedTime >= this.lifespan);
        }

    }

}
