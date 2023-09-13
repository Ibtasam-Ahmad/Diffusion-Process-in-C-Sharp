using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diffusion_Process
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            double D = 1, dx = 1, dt = 0.5;
            double sigma;
            double[,] rho = new double[100, 100];
            int x = -50;
            for (int i = 0; i < 100; i++)
            {
                for (int t = 0; t < 100; t++)
                {
                    sigma = Math.Sqrt(2 * D * (t + 1));
                    rho[i, t] = (1 / sigma) * (Math.Exp(-(x * x) / (2 * sigma * sigma)));
                
                }
                x = x + 1;
            }


            for (int i = 1; i < 99; i++)
            {
                for (int t = 0; t < 99; t++)
                {
                    rho[i, t + 1] = rho[i, t] + D * (rho[i + 1, t] - 2 * rho[i, t] + rho[i - 1, t]) * (1 / (dx * dx))*dt;
                
                }
           
            }
            Graphics gg = CreateGraphics();
            SolidBrush sb = new SolidBrush(Color.Green);
            for (int i = 0; i < 100; i++)
            {
                for (int t = 0; t < 100; t++)
                {
                    gg.FillEllipse(sb, i*2 + 200, -(float)rho[i, t]*200 + 200, 5, 5);
                   // Thread.Sleep(1);
                }
            
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            double xc = cube.ClientSize.Width / 2;
            double yc = cube.ClientSize.Height / 2;
            //for smaller box
            double xmin = xc - 10, xmax = xc + 10, ymin = yc - 10, ymax = yc + 10;
            //for bigger box
            double xleft = xc - xc, xright = xc + xc, yup = yc - yc, ydown = yc + yc;
            Random rnd = new Random();
            Graphics gg = cube.CreateGraphics();
            SolidBrush sb1 = new SolidBrush(Color.White);
            SolidBrush sb2 = new SolidBrush(Color.Black);
            int mole = 100;
            Point[] particle=new Point[100];
            double px,py,prob;
            //plotting the drop of cream
            for(int i=0;i<mole;i++)
            {
                prob = rnd.NextDouble();
                px = xmin + (xmax - xmin) * prob;
                prob = rnd.NextDouble();
                py = ymin + (ymax - ymin) * prob;
                particle[i].X =(int) px;
                particle[i].Y = (int)py;
                gg.FillEllipse(sb1, particle[i].X, particle[i].Y, 4, 4);
            
            }
            Thread.Sleep(100);

            //start diffusion
            while(true)
            {
            for(int i=0;i<mole;i++)
            {
            gg.FillEllipse(sb2,particle[i].X,particle[i].Y,4,4);

                prob=rnd.NextDouble();
                if(prob<0.5)
                {
                particle[i].X=particle[i].X+2;

                }
                else
                {
                particle[i].X=particle[i].X-2;
                }
                 prob=rnd.NextDouble();

                if(prob<0.5)
                {
                particle[i].Y=particle[i].Y+2;

                }
                else
                {
                particle[i].Y=particle[i].Y-2;
                }

                //applying constraints
                if(particle[i].X<xleft)
                    particle[i].X=(int)xleft;
                if(particle[i].X>xright)
                    particle[i].X=(int)xright;
                if(particle[i].Y<yup)
                    particle[i].Y=(int)yup;
                if(particle[i].Y>ydown)
                    particle[i].Y=(int)ydown;
                 
                //plot
                gg.FillEllipse(sb1,particle[i].X,particle[i].Y,4,4);

            
            }
            
            }



        }
    }
}
