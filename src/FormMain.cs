using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using static SkiaCarForms.Enums;
using static System.Net.Mime.MediaTypeNames;

namespace SkiaCarForms
{
    public partial class FormMain : Form
    {

        private readonly int TRAFFIC_COUNT = 10;

        private bool animationIsActive = false;
        private float scale = 0;
        private Car car;
        private Dashboard? dashboard;
        private Road? road;
        private Sensor? sensor;
        private List<Car> traffics;
        private bool userResetAnimation = false;

        private readonly string upArrow = "\u2191";    // Flecha hacia arriba
        private readonly string leftArrow = "\u2190";  // Flecha hacia la izquierda
        private readonly string rightArrow = "\u2192"; // Flecha hacia la derecha
        private readonly string downArrow = "\u2193";  // Flecha hacia abajo

        public FormMain()
        {
            InitializeComponent();

            this.BackColor = Color.DarkGray;
            this.skglControl.Top = 0;
            this.skglControl.Height = this.Height;

            this.traffics = new List<Car>();
        }

        private async void FormMain_Load(object sender, EventArgs e)
        {
            await InitializeObjectsAsync();

            animationIsActive = true;
            await AnimationLoop();
        }

        private async Task InitializeObjectsAsync()
        {

            road = new Road(skglControl.Width / 2, skglControl.Width * 0.9f);
            var centerLaneRoad = road.GetLaneCenter(2);

            InitializeOneTraffic();

            car = new Car(centerLaneRoad, this.Height - 150, 30, 50, CarTypeEnum.IAControled);
            car.Borders = road.Borders;

            if (car.Type != CarTypeEnum.IAControled)
            {
                var controls = new Controls();
                this.KeyPreview = true;
                this.KeyDown += controls.EventHandlerKeyDown;
                this.KeyUp += controls.EventHandlerKeyUp;
                car.Controls = controls;
            }

            if (car.Brain != null)
            {
                car.Brain.Levels[car.Brain.Levels.Length - 1].SetLabelOutput(0, upArrow);
                car.Brain.Levels[car.Brain.Levels.Length - 1].SetLabelOutput(1, leftArrow);
                car.Brain.Levels[car.Brain.Levels.Length - 1].SetLabelOutput(2, rightArrow);
                car.Brain.Levels[car.Brain.Levels.Length - 1].SetLabelOutput(3, downArrow);
            }

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

        private List<Car> InitializeRandomTraffic()
        {

            this.traffics = new List<Car>();

            for (int i = 1; i < this.TRAFFIC_COUNT; i++)
            {
                var height = Utils.Lerp(0, 1f * this.Height, RandomNumberGenerator.GetInt32(1, 10) / 10f);
                var number = RandomNumberGenerator.GetInt32(0, 5);
                var posLane = road.GetLaneCenter(number);
                var factorSpeed = Utils.Lerp(0.1f, 0.9f, RandomNumberGenerator.GetInt32(1, 10) / 10f);

                this.traffics.Add(
                new Car(posLane, -height, 30, 50, CarTypeEnum.Traffic, factorSpeed));

            }

            return this.traffics;

        }

        private void InitializeOneTraffic()
        {

            this.traffics = new List<Car>();

            var height = this.Height;
            var posLane = road.GetLaneCenter(2);
            var factorSpeed = 0.5f;

            this.traffics.Add(new Car(posLane, this.Height *0.3f, 30, 50, CarTypeEnum.Traffic, factorSpeed));

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

        async Task AnimationLoop()
        {

            while (animationIsActive)
            {
                skglControl.Invalidate();
                skglNetworkControl.Invalidate();
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
            traffics.ForEach(c =>
            {
                tasks.Add(Task.Run(() => c.Update()));
            });
            await Task.WhenAll(tasks);

            car?.Update();
        }

        private void drawCarCanvas(SKCanvas canvas)
        {


            canvas.Clear(SKColors.LightGray);

            canvas.Save();
            canvas.Translate(0, -car.Y + this.Height * 0.7f);

            road?.Draw(canvas);

            this.traffics.ForEach(traffic => traffic.Draw(canvas));

            car.Draw(canvas);

            canvas.Restore();

            dashboard?.Draw(canvas);
        }



        private async void skglControl_PaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintGLSurfaceEventArgs e)
        {
            var surface = e.Surface;
            var canvas = surface.Canvas;

            await updateAsync();
            drawCarCanvas(canvas);
        }
        private void skglNetworkControl_PaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintGLSurfaceEventArgs e)
        {
            var surface = e.Surface;
            var canvas = surface.Canvas;
            Visualizer.DrawNetwork(canvas, car);
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
