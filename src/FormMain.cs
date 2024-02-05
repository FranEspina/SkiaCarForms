using SkiaSharp;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using static SkiaCarForms.Enums;

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
        private float phaseDashLine = 0f;

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

        private void drawNetworkCanvas(SKCanvas canvas)
        {
            canvas.Clear(SKColors.Black);


            var level = car.Brain.Levels[0];
            var sizeCanvas = canvas.DeviceClipBounds;

            var levelHeight = 40f;
            var margin = 20f;
            var radious = 10f;
            float spacing = sizeCanvas.Height - 2 * margin - car.Brain.Levels.Length * levelHeight;


            using (var paint = new SKPaint {
                IsAntialias = true, 
                Color = SKColors.White, 
                StrokeWidth = 1,
                Style = SKPaintStyle.Stroke
                
            })
            {

                var levelSize = sizeCanvas.Width - 2 * margin - 2 * radious;
                var left = margin + radious;
                var right = margin + radious + levelSize;

                float inputY = sizeCanvas.Height - margin - (levelHeight / 2);
                for (int i=0; i < level.Inputs.Length; i++)
                {
                    float inputX = Utils.Lerp(left, right, (float) i / (float) (level.Inputs.Length - 1));
                    canvas.DrawCircle(new SKPoint(inputX, inputY), radious, paint);
                }

                float outputY = margin + (levelHeight / 2);
                for (int i = 0; i < level.Outputs.Length; i++)
                {
                    float outputX = Utils.Lerp(left, right, (float)i / (float)(level.Outputs.Length - 1));
                    canvas.DrawCircle(new SKPoint(outputX, outputY), radious, paint);
                }

                phaseDashLine += 1;
                paint.PathEffect = SKPathEffect.CreateDash(new float[] { 5, 2 }, phaseDashLine);
                for (int i=0; i < level.Inputs.Length; i++)
                {
                    float inputX = Utils.Lerp(left, right, (float) i / (float)(level.Inputs.Length - 1));
                    for (int j = 0; j < level.Outputs.Length; j++)
                    {
                        float outputX = Utils.Lerp(left, right, (float) j / (float)(level.Outputs.Length - 1));
                        canvas.DrawLine(new SKPoint(inputX, inputY), new SKPoint(outputX, outputY), paint);
                    }
                }








            }



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
            drawNetworkCanvas(canvas);
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
