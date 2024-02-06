using SkiaCarForms.Network;
using SkiaCarForms.Serialization;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System.Diagnostics;
using System.Numerics;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text.Json;
using static SkiaCarForms.Enums;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SkiaCarForms
{
    public partial class FormMain : Form
    {


        private bool animationIsActive = false;
        private float scale = 0;
        private Dashboard? dashboard;
        private Road? road;
        private Sensor? sensor;
        private List<Car> cars;
        private SimulationModeEnum simulationMode;
        private List<Car> traffics;
        private Car bestCar;
        private bool userResetAnimation = false;
        private float mutateAmount = 0.2f;

        private readonly string upArrow = "\u2191";    // Flecha hacia arriba
        private readonly string leftArrow = "\u2190";  // Flecha hacia la izquierda
        private readonly string rightArrow = "\u2192"; // Flecha hacia la derecha
        private readonly string downArrow = "\u2193";  // Flecha hacia abajo



        private int countRays = 5;
        private int countNSecondLayer = 6;

        public FormMain()
        {
            InitializeComponent();

            this.BackColor = Color.DarkGray;
            this.skglControl.Top = 0;
            this.skglControl.Height = this.Height;

            this.traffics = new List<Car>();
            this.cars = new List<Car>();

            this.simulationMode = SimulationModeEnum.IADriveMode;

            updateMutateTextField();
            updateCountRaysTextField();
            updateNSecondLayerTextField();
        }

        private async void FormMain_Load(object sender, EventArgs e)
        {
            await InitializeObjectsAsync();

            animationIsActive = true;
            await AnimationLoop();
        }


        private async Task InitializeObjectsAsync()
        {
            switch (this.simulationMode)
            {
                case SimulationModeEnum.IADriveMode:
                    InitializeObjectsIAMode();
                    break;

                case SimulationModeEnum.PlayerDriveMode:
                    InitializeObjectsPlayerDriveMode();
                    break;
            }

            await InitializeBitmapsAsync();
        }


        private void InitializeObjectsIAMode()
        {

            road = new Road(skglControl.Width / 2, skglControl.Width * 0.9f);
            var centerLaneRoad = road.GetLaneCenter(2);

            InitializeTrafficForIAMode();

            var brainJson = GetBestCarJSon();
            var brainSaved = NeuronalNetworkSerializer.Load(brainJson);
            if (brainSaved != null)
            {
                this.countRays = brainSaved.Levels[0].Inputs.Length;
                this.countNSecondLayer = brainSaved.Levels[1].Inputs.Length;
                updateCountRaysTextField();
                updateNSecondLayerTextField();
            }


            for (int i = 0; i < 100; i++)
            {
                var car = new Car(centerLaneRoad, this.Height - 150, 30, 50, CarTypeEnum.IAControled, 0.8f, brainJson, this.countRays, this.countNSecondLayer);
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

            if (this.mutateAmount != 0)
            {
                for (int i = 1; i < this.cars.Count; i++)
                {
                    this.cars[i].Brain.Mutate(this.mutateAmount);
                }
            }

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
            dashboard.Traffics = this.traffics;
            dashboard.Cars = this.cars;


        }

        private void InitializeObjectsPlayerDriveMode()
        {

            road = new Road(skglControl.Width / 2, skglControl.Width * 0.9f);
            var centerLaneRoad = road.GetLaneCenter(2);

            InitializeRandomTrafficForDriveMode(2);

            var car = new Car(centerLaneRoad, this.Height - 150, 30, 50, CarTypeEnum.PlayerControled, 0.8f, "", this.countRays);
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

            var controls = new Controls();
            this.KeyPreview = true;
            this.KeyDown += controls.EventHandlerKeyDown;
            this.KeyUp += controls.EventHandlerKeyUp;
            car.Controls = controls;


            dashboard = new Dashboard(10, 10);
            dashboard.MustDraw = ChkDashboard.Checked;
            dashboard.Car = car;



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

            await Task.WhenAll(tasks);
        }


        private void InitializeRandomTrafficForDriveMode(int trafficCount)
        {

            this.traffics = new List<Car>();


            for (int j = 0; j < trafficCount; j++)
            {

                var heightTop = -(float)j * ((float)this.Height) + ((float)this.Height / 2f);
                var heightBottom = heightTop + (this.Height);

                for (int i = 1; i < 5; i++)
                {
                    var height = Utils.Lerp(heightTop, heightBottom, RandomNumberGenerator.GetInt32(1, 10) / 10f);
                    var number = RandomNumberGenerator.GetInt32(0, 5);
                    var posLane = road.GetLaneCenter(number);
                    var factorSpeed = Utils.Lerp(0.1f, 0.9f, RandomNumberGenerator.GetInt32(1, 10) / 10f);

                    this.traffics.Add(
                    new Car(posLane, -height, 30, 50, CarTypeEnum.Traffic, factorSpeed));

                    if (this.traffics.Count == trafficCount - 1)
                    {
                        //Añadimos un coche último coche parado por si necesitamos probar colisiones 
                        this.traffics.Add(new Car(road.GetLaneCenter(2), ((float)this.Height / 4f), 30, 50, CarTypeEnum.Traffic, 0));
                        return;
                    }

                }
            }
        }

        private void InitializeTrafficForIAMode()
        {

            this.traffics = new List<Car>();

            var factorSpeed = 0.5f;

            var height = this.Height * 0.3f;
            this.traffics.Add(new Car(road.GetLaneCenter(2), height, 30, 50, CarTypeEnum.Traffic, factorSpeed));

            height = -(this.Height / 2);
            this.traffics.Add(new Car(road.GetLaneCenter(1), height, 30, 50, CarTypeEnum.Traffic, factorSpeed));
            this.traffics.Add(new Car(road.GetLaneCenter(3), height, 30, 50, CarTypeEnum.Traffic, factorSpeed));

            height -= (this.Height / 2);
            this.traffics.Add(new Car(road.GetLaneCenter(4), height, 30, 50, CarTypeEnum.Traffic, factorSpeed));

            height -= (this.Height / 2);
            this.traffics.Add(new Car(road.GetLaneCenter(2), height, 30, 50, CarTypeEnum.Traffic, factorSpeed));

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
            dashboard.Car = bestCar;
        }

        private void drawCarCanvas(SKCanvas canvas)
        {


            canvas.Clear(SKColors.LightGray);

            canvas.Save();
            canvas.Translate(0, -bestCar.Y + this.Height * 0.7f);

            road?.Draw(canvas);

            float topVisible = bestCar.Y - ((float)this.Height);
            float botomVisible = bestCar.Y + ((float)this.Height);

            this.traffics.ForEach(traffic =>
            {
                if (topVisible < traffic.Y && traffic.Y < botomVisible)
                {
                    traffic.Draw(canvas);
                }
            });

            this.cars.ForEach(car =>
            {
                if (topVisible < car.Y && car.Y < botomVisible)
                {
                    car.Draw(canvas);
                }
            });

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
            this.simulationMode = SimulationModeEnum.IADriveMode;
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

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Exportar mejor coche";

            saveFileDialog.Filter = "Archivos de texto (*.json)|*.json|Todos los archivos (*.*)|*.*";

            DialogResult result = saveFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                var jsonString = NeuronalNetworkSerializer.GetContent(this.bestCar.Brain);
                File.WriteAllText(filePath, jsonString);
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            var result = openFileDialog.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                var json = File.ReadAllText(filePath);
                var brain = NeuronalNetworkSerializer.Load(json);
                NeuronalNetworkSerializer.SaveFile(this.bestCar.Brain);

                userResetAnimation = true;
                animationIsActive = false;
            }
        }

        private void btnPlusMutate_Click(object sender, EventArgs e)
        {
            if (this.mutateAmount >= 0.1f)
                this.mutateAmount += 0.1f;
            else
                this.mutateAmount += 0.01f;

            if (this.mutateAmount > 1)
                this.mutateAmount = 1;

            updateMutateTextField();

        }

        private void btnMinusMutate_Click(object sender, EventArgs e)
        {

            if (this.mutateAmount <= 0.1f)
                this.mutateAmount -= 0.01f;
            else
                this.mutateAmount -= 0.1f;

            if (this.mutateAmount < 0)
                this.mutateAmount = 0;

            updateMutateTextField();

        }
        private void updateMutateTextField()
        {
            this.txtMutate.Text = this.mutateAmount.ToString("F2");
        }

        private void updateCountRaysTextField()
        {
            this.txtCountRays.Text = this.countRays.ToString();
        }

        private void updateNSecondLayerTextField()
        {
            this.txtSecondLevel.Text = this.countNSecondLayer.ToString();

        }

        private void btnRandomReset_Click(object sender, EventArgs e)
        {
            this.simulationMode = SimulationModeEnum.IADriveMode;
            userResetAnimation = true;
            animationIsActive = false;
            NeuronalNetworkSerializer.DeleteFile();
        }

        private void btnPlusCountRays_Click(object sender, EventArgs e)
        {
            this.countRays += 1;
            updateCountRaysTextField();
        }

        private void btnMinusCountRays_Click(object sender, EventArgs e)
        {
            this.countRays -= 1;
            if (this.countRays < 1)
                this.countRays = 1;

            updateCountRaysTextField();
        }

        private void btnDriveMode_Click(object sender, EventArgs e)
        {
            this.simulationMode = SimulationModeEnum.PlayerDriveMode;
            userResetAnimation = true;
            animationIsActive = false;
        }


        private void btnPlusSecondLevel_Click(object sender, EventArgs e)
        {
            this.countNSecondLayer += 1;
            if (this.countNSecondLayer > 20)
                this.countRays = 20;

            updateNSecondLayerTextField();
        }

        private void btnMinusSecondLevel_Click(object sender, EventArgs e)
        {
            this.countNSecondLayer -= 1;
            if (this.countNSecondLayer < 1)
                this.countNSecondLayer = 1;

            updateNSecondLayerTextField();
        }
    }
}
