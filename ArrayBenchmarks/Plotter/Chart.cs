using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Plotter
{
    public class Chart: System.Windows.Forms.Control
    {
        public string graphName = "График"; //Название графика
        private ChartArea chartArea; //Область отрисовки кривых графика
        private static Font NameFont = new Font("Courier", 20.0f, FontStyle.Bold | FontStyle.Italic); //Шрифт для назавния графика
        private Legend legend; //Легенда графика
        private const int defWidth = 800; //Стнадартная ширина
        private const int defHeight = 600;//Стандартныая высота

        /// <summary>
        /// Масштабирование размера шрифта
        /// </summary>
        /// <param name="scale">Коэффициент масштабирования</param>
        /// <param name="f">Масштабируемый шрифт</param>
        protected static void RescaleFont(SizeF scale, ref Font f)
        {
            float ratio = (scale.Height < scale.Height) ? scale.Height : scale.Width;
            f = new Font(f.FontFamily, f.Size * ratio, f.Style);
        }

        public Chart(Point location, Size size)
        {
            this.Location = location;
            this.Size = this.ClientSize = new Size(defWidth, defHeight);
            this.BackColor = Color.Cornsilk;
            this.chartArea = new ChartArea((int)(0.08f * defWidth), (int)(0.1f * defHeight), (int)(0.89f * defWidth), (int)(0.62f * defHeight));
            Rescale(new SizeF((float)size.Width/(float)defWidth , (float)size.Height/(float)defHeight));
            legend = new Legend(new RectangleF(new PointF(chartArea.Area.Left, 1.08f * chartArea.Area.Bottom), new SizeF(chartArea.Area.Width, this.Height - 1.01f * chartArea.Area.Bottom)));
        }

        /// <summary>
        /// Зание имени оси X
        /// </summary>
        public string oXName
        {
            set
            {
                chartArea.oX.AxName = value;
            }
        }

        /// <summary>
        /// Задание имени оси Y
        /// </summary>
        public string oYName
        {
            set
            {
                chartArea.oY.AxName = value;
            }
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            Draw(e.Graphics);
        }

        private void Draw(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.HighQuality; //Режим сглаживания
            g.InterpolationMode = InterpolationMode.HighQualityBicubic; //Качество интерполяции
            g.CompositingQuality = CompositingQuality.HighQuality; //Качество отображения составных изображений
            //Рисуем название графика
            SizeF nameSize = g.MeasureString(graphName, NameFont);
            g.DrawString(graphName, NameFont, Brushes.Black, new PointF(this.ClientSize.Width / 2f - 0.5f * nameSize.Width, 0.015f * this.ClientSize.Height));
            //Отрисовываем область графиков
            chartArea.Draw(g);
            //Рисуем Легенду графика
            legend.Draw(g);
        }

        /// <summary>
        /// Добавление кривых на график
        /// из резутата проведенного теста
        /// </summary>
        /// <param name="result">Результат проведенного теста</param>
        public void AddCurves(Benchmark.SortTestResult result)
        {
            if (result == null)
                return;
            chartArea._curves.AddTestResult(ref result);
            chartArea.CalcGridStep();
            legend.addCurvesInfo(ref chartArea._curves);
            legend.calcCellSize(Graphics.FromHwnd(this.Handle));
        }

        public void SaveBmp()
        {
            Bitmap img = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            Graphics g = Graphics.FromImage(img);
            g.FillRectangle(Brushes.Cornsilk, this.ClientRectangle);
            Draw(g);
            g.Dispose();
            img.Save(System.IO.Directory.GetCurrentDirectory() + "\\Logs\\"+ graphName + ".jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
            img.Dispose();
        }

        //Очистка от кривых и легенды
        public void Clear()
        {
            chartArea._curves.Clear();
            legend.Clear();
        }

        /// <summary>
        /// Действия на события масштабирования формы - контейнера
        /// </summary>
        /// <param name="ScaleFactor"></param>
        public void Rescale(SizeF ScaleFactor)
        {
            if (ScaleFactor.Width == 1 && ScaleFactor.Height == 1)
                return;
            SuspendLayout();
            this.Scale(ScaleFactor);
            chartArea.Rescale(ScaleFactor);
            RescaleFont(ScaleFactor, ref NameFont);
            if (legend != null)
                legend.Rescale(ScaleFactor, Graphics.FromHwnd(this.Handle));
            ResumeLayout();
            this.Invalidate();
        }
    }
}
