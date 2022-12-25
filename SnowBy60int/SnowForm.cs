using SnowBy60int.Properties;
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
using static SnowBy60int.SnowForm;

namespace SnowBy60int
{
    public partial class SnowForm : Form
    {
        public struct Snowflake
        {
            public float X { get; set; }
            public float Y { get; set; }
            public float Speed { get; set; }
            public float Size { get; set; }
            public float Time { get; set; }

            public Snowflake(float x, float y, float speed, float size, float time)
            {
                X = x;
                Y = y;
                Speed = speed;
                Size = size;
                Time = time;
            }
        }
        Graphics Canvas;
        Random rnd;
        long FrameCount = 0;
        private Snowflake[] Snowflakes;
        readonly int SnowflakeCount = 100;

        readonly static Image flake1 = Resources.Snowflake32_1;

        public SnowForm()
        {
            InitializeComponent();
            Start();
        }
        private void MakeFirstSnowflake()
        {
            for (int i = 0; i < SnowflakeCount; i++)
            {
                float addSpeed = 3 + (float)rnd.NextDouble() + (float)rnd.NextDouble() + (float)rnd.NextDouble();
                Snowflakes[i] = new Snowflake(rnd.Next(-100, 1920), rnd.Next(0, 1920), addSpeed, rnd.Next(4, 16), 1);
            }
        }
        private void Start()
        {
            rnd = new Random();

            Canvas = MainCanvas.CreateGraphics();

            Bitmap myBitmap = new Bitmap(Width, Height);

            MainCanvas.Image = myBitmap;

            Canvas = Graphics.FromImage(MainCanvas.Image);
            Snowflakes = new Snowflake[SnowflakeCount];
            MakeFirstSnowflake();
        }
        private void TimerUpdate_Tick(object sender, EventArgs e)
        {
            MainCanvas.Invalidate();
            Interlocked.Increment(ref FrameCount);
        }

        private void MainCanvas_Paint_1(object sender, PaintEventArgs e)
        {
            Width = 1920;
            Height = 1080;
            Canvas.Clear(Color.Transparent);
            for (int i = 0; i < SnowflakeCount; i++)
            {
                Canvas.DrawImage(flake1, Snowflakes[i].X, Snowflakes[i].Y, Snowflakes[i].Size, Snowflakes[i].Size);
                Snowflakes[i].Time += 0.2f;

                if (Snowflakes[i].Y > 1920)
                {
                    Snowflakes[i].Y = -15;
                    Snowflakes[i].Time = 0;
                }
                if (Snowflakes[i].X > 580 & Snowflakes[i].X < -5)
                {
                    Snowflakes[i].X = rnd.Next(0, 1920);
                }
                else
                {
                    Snowflakes[i].Y += Snowflakes[i].Speed + rnd.Next(-1, 1);
                }
            }
        }
    }
}
