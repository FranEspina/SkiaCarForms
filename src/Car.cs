using SkiaCarForms.Network;
using SkiaCarForms.Serialization;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static SkiaCarForms.Enums;

namespace SkiaCarForms
{
    internal class Car
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Height { get; set; }
        public float Width { get; set; }
        public float Speed { get; set; }
        public float Acceleration { get; set; } = 0.5f;
        public float MaxSpeed { get; set; } = 10f;
        public float MaxReverseSpeed { get; set; } = 3f;
        public float Friction { get; set; } = 0.2f;

        private float rotationEffect = 0.06f;

        public bool IsBestCar { get; set; }

        public float Angle { get; private set; } = 0f;

        public Controls Controls { get; set; }
        public SKPoint[][]? Borders { get; set; }

        private readonly Sensor? sensor;

        private SKBitmap? carBitmap;

        public SKPoint[] ShapePolygon { get; private set; }

        private bool damaged = false;
        private object value;
        private float v1;
        private int v2;
        private int v3;
        private CarTypeEnum traffic;
        private float factorSpeed;
        public NeuronalNetwork Brain { get; private set; }

        public List<Car> Traffics { get; set; }

        public CarTypeEnum Type { get; private set; }

        public Car(float x, float y, float width, float height, CarTypeEnum type, float maxSpeedFactor = 1, string brainJson = "") { 
            this.X = x;
            this.Y = y;
            this.Height = height;
            this.Width = width;
            this.ShapePolygon = [];
            this.Type = type;
            this.Traffics = [];
            this.Controls = new Controls();
            this.IsBestCar = false;



            switch (this.Type)
            {
                case CarTypeEnum.Traffic:
                    this.Controls.Forward = true;
                    break;

                case CarTypeEnum.PlayerControled:
                    this.sensor = new Sensor(this);
                    this.Brain = InitializeBrain(brainJson);
                    break;

                case CarTypeEnum.IAControled:
                    this.sensor = new Sensor(this);
                    this.Brain = InitializeBrain(brainJson);
                    break;
            }

            MaxSpeed = MaxSpeed * maxSpeedFactor;

           
        }

        private NeuronalNetwork InitializeBrain(string template)
        {
            if (string.IsNullOrEmpty(template))
            {
                return new NeuronalNetwork([this.sensor.RayCount, 6, 4]);
            }
            else
            {
                return NeuronalNetworkSerializer.Load(template);
            }
        }

        public void InitializeBitmap()
        {
            var color = (Type == CarTypeEnum.Traffic) ? Utils.RandomSKColor() : SKColors.DarkBlue;

            this.carBitmap = Utils.GetTintedImage("Car.png", (int)this.Width, (int)this.Height, color);          
        }



        private void createPolygon()
        {
            this.ShapePolygon = new SKPoint[4];
            float x = 0;
            float y = 0;

            var radious = Utils.Hypotenuse(this.Height, this.Width) / 2f;
            var alpha = MathF.Atan2(this.Width, this.Height);

            x = this.X + radious * MathF.Sin(alpha - this.Angle);
            y = this.Y - radious * MathF.Cos(alpha - this.Angle);
            this.ShapePolygon[0] = new SKPoint(x, y);

            x = this.X + radious * MathF.Sin(-alpha - this.Angle);
            y = this.Y - radious * MathF.Cos(-alpha - this.Angle);
            this.ShapePolygon[1] = new SKPoint(x, y);

            x = this.X + radious * MathF.Sin(MathF.PI + alpha - this.Angle);
            y = this.Y - radious * MathF.Cos(MathF.PI + alpha - this.Angle);
            this.ShapePolygon[2] = new SKPoint(x, y);

            x = this.X + radious * MathF.Sin(MathF.PI - alpha - this.Angle);
            y = this.Y - radious * MathF.Cos(MathF.PI - alpha - this.Angle);
            this.ShapePolygon[3] = new SKPoint(x, y);

        }

        public void Update()
        {
            if (!this.damaged)
            {
                move();
                createPolygon();
                this.damaged = assessDamage();

                if (this.sensor != null)
                {
                    sensor.Update();

                    if (this.Brain != null )
                    {
                        var offsets = new float[this.sensor.Readings.Length];
                        for (int i = 0; i < offsets.Length; i++)
                        {
                            var reading = this.sensor.Readings[i];
                            offsets[i] = (reading == null) ? 0 : 1 - reading.Offset;
                        }

                        this.Brain.FeedForward(offsets);
                        var outputs = this.Brain.Outputs;

                        if (this.Type == CarTypeEnum.IAControled)
                        {
                            this.Controls.Forward = outputs[0] > 0;
                            this.Controls.Left = outputs[1] > 0;
                            this.Controls.Right= outputs[2] > 0;
                            this.Controls.Reverse = outputs[3] > 0;
                        }
                    }
                }
            }
        }

        private bool assessDamage()
        {
            if (this.Borders == null) return false;

            foreach (var border in this.Borders)
            {
                if (Utils.IsPolyIntersect(this.ShapePolygon, border))
                    return true;
            }

            if (this.Traffics != null)
            {
                foreach(var traffic in this.Traffics)
                {
                    if (Utils.IsPolyIntersect(this.ShapePolygon, traffic.ShapePolygon))
                        return true;
                }
            }

            return false;
        }

        private void move()
        {
            if (Controls == null) return;

            if (Controls.Forward)
            {
                this.Speed += this.Acceleration;
            }
            if (Controls.Reverse)
            {
                this.Speed -= this.Acceleration;
            }


            if (this.Speed > 0)
            {
                this.Speed -= this.Friction;
            }
            else if (this.Speed < 0)
            {
                this.Speed += this.Friction;
            }

            if (this.Speed > this.MaxSpeed) this.Speed = this.MaxSpeed;
            if (this.Speed < -this.MaxReverseSpeed) this.Speed = -this.MaxReverseSpeed;

            if (Math.Abs(this.Speed) < this.Friction)
            {
                this.Speed = 0;
            }

            if (Speed != 0)
            {
                var flip = (this.Speed > 0) ? 1 : -1;

                if (Controls.Left)
                {
                    this.Angle += this.rotationEffect * flip;
                }
                if (Controls.Right)
                {
                    this.Angle -= this.rotationEffect * flip;
                }

                this.X -= (float)Math.Sin(Angle) * this.Speed;
                this.Y -= (float)Math.Cos(Angle) * this.Speed;
            }
        }

        public void Draw(SKCanvas canvas)
        {
            drawShape(canvas);
            if (this.IsBestCar)
                sensor?.Draw(canvas);
        }

        public void Dispose()
        {
            this.carBitmap?.Dispose();
            this.carBitmap = null;
            this.Controls = null;
            this.Borders = null;
            this.ShapePolygon = [];
        }

        private void drawShape(SKCanvas canvas)
        {
           
            canvas.Save();
            canvas.Translate(this.X, this.Y);
            canvas.RotateRadians(-this.Angle);

            if (this.damaged)
            {
                this.carBitmap = Utils.GetTintedImage("Car.png", (int)this.Width, (int)this.Height, SKColors.DarkGray);
            }
            
            using (var paint = new SKPaint { IsAntialias = true })
            {
                if (this.carBitmap != null)
                {
                    canvas.DrawBitmap(this.carBitmap, -this.Width / 2, -this.Height / 2, paint);  
                }
            }
            
            canvas.Restore(); 
           
        }
    }
}
