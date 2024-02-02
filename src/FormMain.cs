using SkiaSharp;
using System.Diagnostics;
using static SkiaCarForms.Enums;

namespace SkiaCarForms
{
    public partial class FormMain : Form
    {

        private readonly Stopwatch stopwatch = new Stopwatch();
        private bool animationIsActive = false;
        private float scale = 0;
        private bool _isFirstPainting = true;
        private Car car;
        private Dashboard dashboard;
        private Road road;
        private Sensor sensor;
        private List<Car> traffics;

        public FormMain()
        {
            InitializeComponent();

            InitializeObjects();

            animationIsActive = true;

            AnimationLoop();

        }

        private void InitializeObjects()
        {
            InitializeSizeCanvas();

            road = new Road(skglControl.Width / 2, skglControl.Width * 0.9f);
            var centerLaneRoad = road.GetLaneCenter(2);

            InitializeTraffic();

            car = new Car(centerLaneRoad, this.Height - 150, 30, 50, CarTypeEnum.Controled);
            car.Borders = road.Borders;

            var controls = new Controls();
            this.KeyPreview = true;
            this.KeyDown += controls.EventHandlerKeyDown;
            this.KeyUp += controls.EventHandlerKeyUp;
            car.Controls = controls;
            car.Traffics = this.traffics;

            dashboard = new Dashboard(10, 10);
            dashboard.MustDraw = ChkDashboard.Checked;
            dashboard.Car = car;
        }

        private void InitializeTraffic()
        {
            var posLane = road.GetLaneCenter(2);

            this.traffics = new List<Car>();
            this.traffics.Add(
                new Car(posLane, this.Height * 0.3f, 30, 50, CarTypeEnum.Traffic));

        }

        private void InitializeSizeCanvas()
        {
            this.BackColor = Color.DarkGray;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            skglControl.Height = this.Height;
            skglControl.Width = 200;
            skglControl.Top = 0;
            skglControl.Left = (this.Width / 2) - (skglControl.Width / 2);
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
            traffics.ForEach(c => c.Update());
            car.Update();
        }

        private void draw(SKCanvas canvas)
        {


            canvas.Clear(SKColors.LightGray);

            canvas.Save();
            canvas.Translate(0, -car.Y + this.Height * 0.7f);

            road.Draw(canvas);
            traffics.ForEach(c => c.Draw(canvas));
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

        private void BtnReset_Click(object sender, EventArgs e)
        {
            InitializeObjects();
        }

        private void ChkDashboard_CheckedChanged(object sender, EventArgs e)
        {
            dashboard.MustDraw = ChkDashboard.Checked;
        }
    }
}
