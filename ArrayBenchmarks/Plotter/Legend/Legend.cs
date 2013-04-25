using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Plotter
{
    /// <summary>
    /// Класс, представляющий легенду графика
    /// </summary>
    public class Legend: List<LegendItem>
    {
        private RectangleF legArea; //Прямоугольник, содержаций легенду (область отрисовки)
        private static Pen borderPen = new Pen(Brushes.Blue, 1.0f);//для границ легенды
        private int numCols;// Число столбцов легенды
        private int numRows;//Число строк ленеды
        private const float colWidth = 180.0f; //Минимальная ширина колонки
        private SizeF cellSize;

        /// <summary>
        /// Легенда графика
        /// </summary>
        /// <param name="area">Обасть построения легенды</param>
        public Legend(RectangleF area)
        {
            this.legArea = area;
        }

        /// <summary>
        /// Добавление информации о кривых на легенду
        /// </summary>
        /// <param name="curves">Кривые графика</param>
        public void addCurvesInfo(ref Curves curves)
        {
            foreach (Curve c in curves)
            {
                this.Add(new LegendItem(c.lineColor, c.Name));
            }
        }

        /// <summary>
        /// Перемасштабирование области графиков
        /// </summary>
        /// <param name="scaleFactor">Коэффициент масштабирования</param>
        /// <param name="g">Объект graphics</param>
        public void Rescale(SizeF scaleFactor, Graphics g)
        {
            //Новые размеры области легенды
            legArea.X *= scaleFactor.Width;
            legArea.Y *= scaleFactor.Height;
            legArea.Width *= scaleFactor.Width;
            legArea.Height *= scaleFactor.Height;
            if (this.Count == 0)
                return;
            //Положение записей легенды, размер шрифта
            calcCellSize(g);
        }

        /// <summary>
        /// Вычисление расположения записей легеды, размера шрифта
        /// </summary>
        /// <param name="g"></param>
        public void calcCellSize(Graphics g)
        {
            LegendItem.setMinFont();
            do
            {
                numRows = (int)Math.Floor((double)legArea.Height / (double)(2f * this[0].getSize(g).Height));
                if (numRows != 0)
                    numCols = (int)Math.Ceiling((double)this.Count / (double)numRows);
                else
                    numCols = 0;
            } while (numCols < (int)(legArea.Width / colWidth) && (LegendItem.setFontDelta(1.2f)) == true);
            LegendItem.setFontDelta(1 / 1.2f);
            cellSize.Height = 2f * this[0].getSize(g).Height;
            cellSize.Width = legArea.Width / (float)numCols;
        }

        /// <summary>
        /// Отрисовка легенды
        /// </summary>
        /// <param name="g"></param>
        public void Draw(Graphics g)
        {
            PointF currPoint = new PointF(); //Точка отрисовки записи легенды
            currPoint.X = 1.5f + legArea.Left;
            for (int col =0; col < numCols; ++col)
            {
                currPoint.Y = 0.25f * cellSize.Height + legArea.Top;
                g.SetClip(new RectangleF(currPoint.X, currPoint.Y, cellSize.Width, legArea.Height), CombineMode.Union);
                for (int row = 0; row < numRows; ++row)
                {
                    int index = col * numRows + row;
                    if (index >= this.Count)
                        break;
                    this[index].Draw(g, currPoint);
                    currPoint.Y += cellSize.Height;
                }
                currPoint.X += cellSize.Width;
            }
        }


    }
}
