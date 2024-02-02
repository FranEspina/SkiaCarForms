using SkiaSharp;
using System.Diagnostics;

namespace SkiaCarForms
{
    public partial class Form1 : Form
    {

        private readonly Stopwatch stopwatch = new Stopwatch();
        private bool animationIsActive = false;
        private float scale = 0;
        private bool _isFirstPainting = true;
        private Car car;
        private Dashboard dashboard;
        private Road road;
        private Sensor sensor;

        public Form1()
        {
            InitializeComponent();

            this.BackColor = Color.DarkGray;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            skglControl.Height = this.Height;
            skglControl.Width = 200;
            skglControl.Top = 0;
            skglControl.Left = (this.Width / 2) - (skglControl.Width / 2);

            var controls = new Controls();
            this.KeyPreview = true;
            this.KeyDown += controls.EventHandlerKeyDown;
            this.KeyUp += controls.EventHandlerKeyUp;
            
            road = new Road(skglControl.Width / 2, skglControl.Width * 0.9f);
            var centerLaneRoad = road.GetLaneCenter(2);

            car = new Car(centerLaneRoad, this.Height - 150 , 30, 50);
            car.Controls = controls;
            car.Borders = road.Borders;           
            
            dashboard = new Dashboard(10, 10);
            dashboard.Car = car;

            animationIsActive = true;
            AnimationLoop();

        }

        async Task AnimationLoop()
        {
            stopwatch.Start();

            while (animationIsActive)
            {
                skglControl.Invalidate();
                await Task.Delay(TimeSpan.FromSeconds(1.5 / 60));
            }

            stopwatch.Stop();
        }

        private void BtnParar_Click(object sender, EventArgs e)
        {
            animationIsActive = false;
        }

        private void update()
        {
            car.Update();
        }

        private void draw(SKCanvas canvas)
        {


            canvas.Clear(SKColors.LightGray);

            canvas.Save();
            canvas.Translate(0, -car.Y + this.Height * 0.7f);

            road.Draw(canvas);
            car.Draw(canvas);

            canvas.Restore();

            dashboard.Draw(canvas);
        }

        private void skglControl_PaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintGLSurfaceEventArgs e)
        {
            var surface = e.Surface;
            var canvas = surface.Canvas;
            update();
            draw(canvas);
        }
    }
}
