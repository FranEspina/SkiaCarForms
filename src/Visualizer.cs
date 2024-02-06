using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiaCarForms
{
    internal class Visualizer
    {
        private static float phaseDashLine = 0f;


        private static SKColor getRGBA(float value)
        {
            int alpha = (int)MathF.Round((MathF.Abs(value) * 255f), 0); // Pesos cercanos a 0 poco visibles  
            if (alpha > 255) alpha = 255;

            int R = (value < 0) ? 0 : 255; // Pesos positivos amarillos (rojo + verde)
            int G = (value < 0) ? 0 : 255;
            int B = (value < 0) ? 255 : 0; // Pesos negativos azul

            return Utils.SKColorFromARgb(R, G, B, alpha);
        }

        public static void DrawNetwork(SKCanvas canvas, Car car)
        {
            canvas.Clear(SKColors.Black);

            var sizeCanvas = canvas.DeviceClipBounds;
            var levelHeight = 40f;
            var margin = 20f;
            var radious = 15f;
            var network = car.Brain;

            var horizontalSize = sizeCanvas.Width - 2 * margin - 2 * radious;

            var left = margin + radious;
            var right = margin + radious + horizontalSize;
            float top = margin + (levelHeight / 2);
            float bottom = sizeCanvas.Height - margin - (levelHeight / 2);
            float lerpStep = 0;

            for (int k = 0; k < network.Levels.Length; k++)
            {

                phaseDashLine -= 0.5f;

                var level = car.Brain.Levels[k];

                float inputY = Utils.Lerp(bottom, top, (float) k / (float) network.Levels.Length);

                using (var paint = new SKPaint
                {
                    IsAntialias = true,
                    Color = SKColors.White,
                    Style = SKPaintStyle.Fill,
                    FakeBoldText = true,
                    TextSize = 15,
                })
                {

                    float outputY = Utils.Lerp(bottom, top, (float)(k + 1) / (float)(network.Levels.Length));

                    //************************************************************
                    //* Dibujamos los pesos entre la entrada y salida del nivel  
                    //************************************************************ 
                    paint.Style = SKPaintStyle.Stroke;
                    paint.StrokeWidth = 1;
                    paint.PathEffect = SKPathEffect.CreateDash(new float[] { 5, 2 }, phaseDashLine);
                    for (int i = 0; i < level.Inputs.Length; i++)
                    {
                        lerpStep = (level.Inputs.Length == 1) ? 0.5f : (float)i / (float)(level.Inputs.Length - 1);

                        float inputX = Utils.Lerp(left, right, lerpStep);
                        for (int j = 0; j < level.Outputs.Length; j++)
                        {
                            paint.Color = getRGBA(level.Weights[i][j]);

                            lerpStep = (level.Outputs.Length == 1) ? 0.5f : (float)j / (float)(level.Outputs.Length - 1);
                            float outputX = Utils.Lerp(left, right, lerpStep);
                            canvas.DrawLine(new SKPoint(inputX, inputY), new SKPoint(outputX, outputY), paint);
                        }
                    }
                    paint.PathEffect = null;
                    paint.StrokeWidth = 2;


                    //**************************************
                    //* Dibujamos la entrada del nivel  
                    //************************************** 
                    paint.Style = SKPaintStyle.Fill;
                    for (int i = 0; i < level.Inputs.Length; i++)
                    {
                        lerpStep = (level.Inputs.Length == 1) ? 0.5f : (float)i / (float)(level.Inputs.Length - 1);
                        float inputX = Utils.Lerp(left, right, lerpStep);

                        paint.Color = SKColors.Black;
                        paint.Style = SKPaintStyle.Fill;
                        canvas.DrawCircle(new SKPoint(inputX, inputY), radious, paint);

                        paint.Color = getRGBA(level.Inputs[i]);
                        paint.Style = SKPaintStyle.Fill;
                        canvas.DrawCircle(new SKPoint(inputX, inputY), radious * 0.6f, paint);

                        if (k > 0)
                        {
                            var levelPrevious = car.Brain.Levels[k - 1];
                            paint.Style = SKPaintStyle.Stroke;
                            paint.PathEffect = SKPathEffect.CreateDash(new float[] { 3, 3 }, phaseDashLine);
                            paint.Color = getRGBA(levelPrevious.Biases[i]);
                            canvas.DrawCircle(new SKPoint(inputX, inputY), radious * 0.8f, paint);
                            paint.PathEffect = null;
                        }


                    } 
                    

                    paint.Style = SKPaintStyle.Stroke;
                    paint.Color = SKColors.Black;
                    for (int i = 0; i < level.Inputs.Length; i++)
                    {
                        lerpStep = (level.Inputs.Length == 1) ? 0.5f : (float)i / (float)(level.Inputs.Length - 1);
                        float inputX = Utils.Lerp(left, right, lerpStep);
                        canvas.DrawCircle(new SKPoint(inputX, inputY), radious, paint);
                    }


                    //**************************************
                    //* Dibujamos la salida del nivel  
                    //************************************** 
                    for (int i = 0; i < level.Outputs.Length; i++)
                    {
                        lerpStep = (level.Outputs.Length == 1) ? 0.5f : (float)i / (float)(level.Outputs.Length - 1);

                        float outputX = Utils.Lerp(left, right, lerpStep);
                        var position = new SKPoint(outputX, outputY);
                        paint.Style = SKPaintStyle.Fill;

                        paint.Color = SKColors.Black;
                        canvas.DrawCircle(position, radious, paint);

                        paint.Color = (level.Outputs[i] == 1) ? SKColors.Yellow : SKColors.Black;
                        canvas.DrawCircle(position, radious * 0.6f, paint);

                        paint.PathEffect = SKPathEffect.CreateDash(new float[] { 3, 3 }, phaseDashLine);
                        paint.Style = SKPaintStyle.Stroke;
                        paint.Color = getRGBA(level.Biases[i]);
                        canvas.DrawCircle(position, radious * 0.8f, paint);
                        paint.PathEffect = null;


                        if (!string.IsNullOrEmpty(level.LabelOutputs[i]))
                        {
                            paint.Color = (level.Outputs[i] == 1) ? SKColors.Black : SKColors.White;
                            float textWidth = paint.MeasureText(level.LabelOutputs[i]);
                            var textPosition = new SKPoint(position.X - textWidth / 2, position.Y + 5);
                            canvas.DrawText(level.LabelOutputs[i], textPosition, paint);
                            paint.Color = SKColors.White;
                        }
                    }

                    
                   

                }
            }

        }


    }
}
