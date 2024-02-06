using SkiaCarForms.Network;
using SkiaCarForms.Serialization;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text.Json;
using static SkiaCarForms.Enums;
using static System.Net.Mime.MediaTypeNames;

namespace SkiaCarForms
{
    public partial class FormMain : Form
    {

        private readonly int TRAFFIC_COUNT = 10;

        private bool animationIsActive = false;
        private float scale = 0;
        private Dashboard? dashboard;
        private Road? road;
        private Sensor? sensor;
        private List<Car> cars;
        private List<Car> traffics;
        private Car bestCar;
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
            this.cars = new List<Car>();
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

            InitializeTraffic();

            var brainJson = GetBestCarJSon();


            for (int i = 0; i < 100; i++)
            {
                var car = new Car(centerLaneRoad, this.Height - 150, 30, 50, CarTypeEnum.IAControled, 1, brainJson);
                car.Borders = road.Borders;
                car.Traffics = this.traffics;

                if (car.Brain != null)
                {
                    car.Brain.Levels[car.Brain.Levels.Length - 1].SetLabelOutput(0, upArrow);
                    car.Brain.Levels[car.Brain.Levels.Length - 1].SetLabelOutput(1, leftArrow);
                    car.Brain.Levels[car.Brain.Levels.Length - 1].SetLabelOutput(2, rightArrow);
                    car.Brain.Levels[car.Brain.Levels.Length - 1].SetLabelOutput(3, downArrow);
                }
                cars.Add(car);
            }

            this.cars.ForEach(car => car.Brain.Mutate(0.2f));

            bestCar = cars.First();
            if (bestCar.Type == CarTypeEnum.PlayerControled)
            {
                var controls = new Controls();
                this.KeyPreview = true;
                this.KeyDown += controls.EventHandlerKeyDown;
                this.KeyUp += controls.EventHandlerKeyUp;
                bestCar.Controls = controls;
            }


            dashboard = new Dashboard(10, 10);
            dashboard.MustDraw = ChkDashboard.Checked;
            dashboard.Car = bestCar;

            await InitializeBitmapsAsync();

        }

        private async Task InitializeBitmapsAsync()
        {
            List<Task> tasks = new List<Task>();

            foreach (var car in this.cars)
            {
                tasks.Add(Task.Run(() => car.InitializeBitmap()));
            }

            foreach (var traffic in this.traffics)
            {
                tasks.Add(Task.Run(() => traffic.InitializeBitmap()));
            }

            //Ejecutamos todas las tareas en paralelo
            await Task.WhenAll(tasks);
        }

        private void InitializeTraffic()
        {

            this.traffics = new List<Car>();

            var factorSpeed = 0.5f;

            var height = this.Height * 0.3f;
            this.traffics.Add(new Car(road.GetLaneCenter(2), height, 30, 50, CarTypeEnum.Traffic, factorSpeed));

            height += this.Height;
            this.traffics.Add(new Car(road.GetLaneCenter(1), height, 30, 50, CarTypeEnum.Traffic, factorSpeed));
            this.traffics.Add(new Car(road.GetLaneCenter(3), height, 30, 50, CarTypeEnum.Traffic, factorSpeed));

        }


        private void DisposeTrafficAndCars()
        {
            foreach (var traffic in this.traffics)
            {
                traffic.Speed = 0;
                traffic.Dispose();
            }

            this.traffics.Clear();

            foreach (var car in this.cars)
            {
                car.Speed = 0;
                car.Dispose();
            }

            this.cars.Clear();
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

            tasks = new List<Task>();
            cars.ForEach(c =>
            {
                tasks.Add(Task.Run(() => c.Update()));
            });
            await Task.WhenAll(tasks);

            bestCar.IsBestCar = false;
            bestCar = cars.MinBy(c => c.Y);
            bestCar.IsBestCar = true;
        }

        private void drawCarCanvas(SKCanvas canvas)
        {


            canvas.Clear(SKColors.LightGray);

            canvas.Save();
            canvas.Translate(0, -bestCar.Y + this.Height * 0.7f);

            road?.Draw(canvas);

            this.traffics.ForEach(traffic => traffic.Draw(canvas));

            this.cars.ForEach(car => car.Draw(canvas));

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
            Visualizer.DrawNetwork(canvas, bestCar);
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            userResetAnimation = true;
            animationIsActive = false;
        }

        private async Task ResetAnimationAsync()
        {
            DisposeTrafficAndCars();
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (bestCar == null || bestCar.Brain == null) return;
            NeuronalNetworkSerializer.SaveFile(this.bestCar.Brain);
        }

        private string GetBestCarJSon()
        {
            return NeuronalNetworkSerializer.LoadContentFile();
        }

    }
}
