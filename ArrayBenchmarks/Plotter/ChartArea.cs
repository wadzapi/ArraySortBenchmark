using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Plotter
{
    public class ChartArea
    {
        private RectangleF chartArea; //Область отрисовки кривых графика
        private SizeF Scale; //Коэффициенты масштабирования по X, Y;
        private Matrix Transform; //Матрица трансформации в экранные координаты
        public XAxis oX; //Ось X
        public YAxis oY; //Ось y
        private Color col1 = Color.CadetBlue; //Цвет 1 градиентной заливки
        private Color col2 = Color.BlanchedAlmond;//Цвет 2 градиентной заливки
        public Curves _curves = new Curves();//Кривые графиков

        public ChartArea(int x, int y, int Width, int Heigth)
        {
            this.chartArea = new Rectangle(new Point(x, y), new Size(Width, Heigth));
            this.oX = new XAxis();
            this.oY = new YAxis();
            this.Transform = new Matrix();
        }

        public void Draw(Graphics g)
        {
            //Градиентная заливка поля графиков
            g.FillRectangle(new LinearGradientBrush(chartArea, col1, col2, LinearGradientMode.BackwardDiagonal),chartArea);
            g.TranslateTransform(chartArea.Left, chartArea.Bottom);
            //Рисуем оси координат
            oX.Draw(g, chartArea.Width);
            oY.Draw(g, chartArea.Height);
            g.ResetTransform();
            //Устанавливаем обрезку
            g.SetClip(chartArea, CombineMode.Intersect);
            //Отрисовываем оси
            _curves.Draw(g);
        }

        /// <summary>
        /// Масштабирование при изменение размеров формы-контейнера
        /// </summary>
        /// <param name="ScaleFactor">Коэффициет масштабирования</param>
        public void Rescale(SizeF ScaleFactor)
        {
            this.chartArea.Location = new PointF(this.chartArea.X * ScaleFactor.Width, this.chartArea.Y * ScaleFactor.Height);
            this.chartArea.Width *= ScaleFactor.Width;
            this.chartArea.Height *= ScaleFactor.Height;
            Axis.Rescale(ScaleFactor);
            CalcGridStep();
        }

        /// <summary>
        /// Задает цвета градиентной заливки
        /// </summary>
        /// <param name="Color1"></param>
        /// <param name="Color2"></param>
        public void setColors(Color Color1, Color Color2)
        {
            this.col1 = Color1;
            this.col2 = Color2;
        }

        /// <summary>
        /// Ищет значение шага вида a*10^n
        /// </summary>
        /// <param name="StepVal"></param>
        /// <returns></returns>
        private float getStepSize(float StepVal)
        {
            // Ищем значениешага вида  a*10^n 
            double mag = Math.Floor(Math.Log10(StepVal));
            double magPow = Math.Pow((double)10.0, mag);
            // Значение наиболее большого разряда шага
            double magMsd = ((int)(StepVal / magPow + 0.5));

            if (magMsd > 5.0)
                magMsd = 10.0;
            else if (magMsd > 2.0)
                magMsd = 5.0;
            else if (magMsd > 1.0)
                magMsd = 2.0;

            return (float)(magMsd * magPow);
        }

        /// <summary>
        /// Выполняет расчет шага и значений сетки по осям,
        /// перерасчет коэффициентов масштабирования
        /// </summary>
        public void CalcGridStep()
        {
            if (_curves.Count == 0)
                return;
            //Находим экстремумы
            _curves.getMinMax(out oX.MinVal, out oX.MaxVal, out oY.MinVal, out oY.MaxVal);
            //Проверка на нулевой интервал
            if (oX.MaxVal - oX.MinVal == 0 || oY.MaxVal - oY.MinVal == 0)
                return;
            Scale.Width = chartArea.Width / (oX.MaxVal - oX.MinVal);
            Scale.Height = chartArea.Height / (oY.MaxVal - oY.MinVal);
            //начальное значение шага
            oX.StepValue = Axis._DefaultStepPix / Scale.Width;
            oY.StepValue = Axis._DefaultStepPix / Scale.Height;
            // Ищем значение шага вида  n*10^n 
            oX.StepValue = getStepSize(oX.StepValue);
            oY.StepValue = getStepSize(oY.StepValue);
            //Исправляем границы интревалов с учетом шага
            oX.MinVal = (float)Math.Truncate((double)oX.MinVal / (double)oX.StepValue) * oX.StepValue;
            oX.MaxVal = (float)Math.Floor((double)oX.MaxVal / (double)oX.StepValue + 0.5d) * oX.StepValue;
            oY.MinVal = (float)Math.Truncate((double)oY.MinVal / (double)oY.StepValue) * oY.StepValue;
            oY.MaxVal = (float)Math.Floor((double)oY.MaxVal / (double)oY.StepValue) * oY.StepValue;
            //Исправляем Scale c учетом нового шага и нового интревала
            Scale.Width = chartArea.Width / (oX.MaxVal - oX.MinVal);
            Scale.Height = chartArea.Height / (oY.MaxVal - oY.MinVal);
            //Новое значение шага в пикселях
            oX.StepPix = (int)Math.Floor((double)oX.StepValue * (double)Scale.Width);
            oY.StepPix = (int)Math.Floor((double)oY.StepValue * (double)Scale.Height);
            //Исправляем масштаб, с учетом размера нового шага
            Scale.Width = oX.StepPix / oX.StepValue;
            Scale.Height = oY.StepPix / oY.StepValue;
            //Созданим матрицу трансформации в экранные координаты для кривых графика
            Transform.Reset();
            Transform.Translate(chartArea.Left, chartArea.Bottom);
            Transform.Scale(Scale.Width, -Scale.Height);
            _curves.Scale(Transform);
        }

        /// <summary>
        /// Возращает позицию области отрисовки
        /// </summary>
        public RectangleF Area
        {
            get
            {
                return chartArea;
            }
        }
    }
}
