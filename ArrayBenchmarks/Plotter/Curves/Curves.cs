using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Plotter
{
    public class Curves: List<Curve>
    {
        public Curves()
        {
        }

        /// <summary>
        /// Отрисовка всех кривых коллекции
        /// </summary>
        /// <param name="g">Объект Graphics</param>
        /// <param name="scale">Коэффициент масштабирования</param>
        public void Draw(Graphics g)
        {
            if (this.Count == 0)
                return;
            foreach (Curve c in this)
            {
                c.drawCurve(g);
            }
        }

        /// <summary>
        /// Добавление кривых на график из результатов теста
        /// </summary>
        /// <param name="result">Результата теста</param>
        public void AddTestResult(ref Benchmark.SortTestResult result)
        {
            Benchmark.SortMethResult[] res = result.getResults();
            for (int i = 0; i < res.Length; ++i)
            {
                this.Add(new Curve(res[i].Name, res[i].getResults(), Color.FromKnownColor((KnownColor)i + 37)));
            }
        }

        /// <summary>
        /// Трансформация точек в экранные координаты
        /// </summary>
        /// <param name="transform"></param>
        public void Scale(Matrix transform)
        {
            foreach (Curve c in this)
            {
                c.ScalePoints(transform);
            }
        }


        /// <summary>
        /// Нахождение максимального и минимального значений X,Y для кривых графкиа
        /// </summary>
        /// <returns>Возвращает массив вида float[4]{minX, maxX, minY, maxY}</returns>
        public void getMinMax(out float minX, out float maxX, out float minY, out float maxY)
        {
            PointF pt1 = this[0].getPoint(0);
            int lastIndex = this[0].PointsCount - 1;
            PointF pt2 = this[0].getPoint(lastIndex);
            ///Минимальная и макс. абсцисса равна для всех кривых (мин-макс. размерность массива)
            minX = pt1.X;
            maxX = pt2.X;
            ///По у основываемся на прямой зависимости y, с ростом x растет и y
            minY = pt1.Y;
            maxY = pt2.Y;
            for (int i = 1; i < this.Count; ++i)
            {
                lastIndex = this[i].PointsCount;
                --lastIndex;
                pt1 = this[i].getPoint(0);
                pt2 = this[i].getPoint(lastIndex);
                if (minY > pt1.Y)
                    minY = pt1.Y;
                if (maxY < pt2.Y)
                    maxY = pt2.Y;
            }
        }



    }
}
