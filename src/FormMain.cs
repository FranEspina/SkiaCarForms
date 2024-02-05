using SkiaSharp;
using System.Diagnostics;
using System.Security.Cryptography;
using static SkiaCarForms.Enums;

namespace SkiaCarForms
{
    public partial class FormMain : Form
    {

        private bool animationIsActive = false;
        private float scale = 0;
        private Car car;
        private Dashboard? dashboard;
        private Road? road;
        private Sensor? sensor;
        private List<Car> traffics;
        private bool userResetAnimation = false;

        public FormMain()
        {
            InitializeComponent();

            this.traffics = new List<Car>();
        }

        private async void FormMain_Load(object sender, EventArgs e)
        {
            try
            {
                await InitializeObjectsAsync();

                animationIsActive = true;

                await AnimationLoop();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private async Task InitializeObjectsAsync()
        {
            InitializeSizeCanvas();

            road = new Road(skglControl.Width / 2, skglControl.Width * 0.9f);
            var centerLaneRoad = road.GetLaneCenter(2);

            InitializeTraffic(20);

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

            await InitializeBitmapsAsync();

        }

        private async Task InitializeBitmapsAsync()
        {
            List<Task> tasks = new List<Task>();
            tasks.Add(Task.Run(() => car?.InitializeBitmap()));
            foreach (var traffic in this.traffics)
            {
                tasks.Add(Task.Run(() => traffic.InitializeBitmap()));
            }

            //Ejecutamos todas las tareas en paralelo
            await Task.WhenAll(tasks);
        }

        private List<Car> InitializeTraffic(int count)
        {

            this.traffics = new List<Car>();

            for (int i = 1; i < count; i++)
            {
                var height = Utils.Lerp(0, 2f * this.Height, RandomNumberGenerator.GetInt32(1, 10) / 10f);
                var number = RandomNumberGenerator.GetInt32(0, 5);
                var posLane = road.GetLaneCenter(number);
                var factorSpeed = Utils.Lerp(0.1f, 0.9f, RandomNumberGenerator.GetInt32(1, 10) / 10f);

                this.traffics.Add(
                new Car(posLane, -height, 30, 50, CarTypeEnum.Traffic, factorSpeed));

            }

            return this.traffics;

        }

        private void DisposeTraffic()
        {
            foreach (var traffic in this.traffics)
            {
                traffic.Speed = 0;
                traffic.Dispose();
            }

            this.traffics.RemoveAll(p => p.Speed == 0);
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

            while (animationIsActive)
            {
                skglControl.Invalidate();
                await Task.Delay(TimeSpan.FromSeconds(1.5 / 60));
            }

            if (userResetAnimation)
            {
                await ResetAnimationAsync();
            }
        }

        private void BtnParar_Click(object sender, EventArgs e)
        {
            animationIsActive = false;
        }

        private async Task updateAsync()
        { 
            var tasks = new List<Task>();
            traffics.ForEach(c => {
                tasks.Add(Task.Run(() => c.Update()));
            });
            await Task.WhenAll(tasks);

            car?.Update();
        }

        private void draw(SKCanvas canvas)
        {


            canvas.Clear(SKColors.LightGray);

            canvas.Save();
            canvas.Translate(0, -car.Y + this.Height * 0.7f);

            road?.Draw(canvas);

            foreach (var traffic in this.traffics)
            {
                traffic.Draw(canvas);
            }

            car.Draw(canvas);

            canvas.Restore();

            dashboard?.Draw(canvas);
        }

        //No usar. SkiaSharp no es seguro para hilos. Y no funciona ni compartiendo objeto lock 
        private async Task drawAsync(SKCanvas canvas)
        {


            canvas.Clear(SKColors.LightGray);

            canvas.Save();
            canvas.Translate(0, -car.Y + this.Height * 0.7f);

            road?.Draw(canvas);

            await drawTrafficAsync(canvas);

            car.Draw(canvas);

            canvas.Restore();

            dashboard?.Draw(canvas);
        }

        //No usar. SkiaSharp no es seguro para hilos. Y no funciona ni compartiendo objeto lock 
        private async Task drawTrafficAsync(SKCanvas canvas)
        {
            List<Task> tasks = new List<Task>();
            foreach (var traffic in this.traffics)
            {
                tasks.Add(Task.Run(() => traffic.Draw(canvas)));
            }
            await Task.WhenAll(tasks);
        }

        private async void skglControl_PaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintGLSurfaceEventArgs e)
        {
            var surface = e.Surface;
            var canvas = surface.Canvas;

            try
            {
                await updateAsync();
                await drawAsync(canvas);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            userResetAnimation = true;
            animationIsActive = false;
        }

        private async Task ResetAnimationAsync()
        {
            DisposeTraffic();
            await InitializeObjectsAsync();
            
            animationIsActive = true;
            userResetAnimation = false;
            await AnimationLoop();
        }

        private void ChkDashboard_CheckedChanged(object sender, EventArgs e)
        {
            if (dashboard != null)
                dashboard.MustDraw = ChkDashboard.Checked;
        }

    }
}
