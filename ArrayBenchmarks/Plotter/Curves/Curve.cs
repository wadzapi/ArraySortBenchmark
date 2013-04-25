using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Plotter
{
    /// <summary>
    /// Класс, инкапсулирующий параметры кривой на графике
    /// </summary>
    public class Curve
    {
        private PointF[] Points;//Список исходных точек
        private PointF[] scrPoints;//Точки в экранных координатах
        private string _curveName; //Имя кривой
        private Pen cPen;
        
        /// <summary>
        /// Экземпляр класса кривой на графике
        /// </summary>
        /// <param name="Name">Имя кривой</param>
        /// <param name="points">Массив точек кривой</param>
        /// <param name="col">Цвет кривой</param>
        public Curve(string Name, PointF[] points, Color col)
        {
            this.Points = points;
            this.scrPoints = new PointF[points.Length];
            this.cPen = new Pen(col, 1.5f);
            cPen.EndCap = System.Drawing.Drawing2D.LineCap.NoAnchor;
            this._curveName = Name;
        }

        /// <summary>
        /// Трансформация точек кривой в экранные координаты
        /// </summary>
        /// <param name="transform"></param>
        public void ScalePoints(Matrix transform)
        {
            Array.Copy(Points, scrPoints, Points.Length);
            transform.TransformPoints(scrPoints);
        }

        /// <summary>
        /// Возращает точку кривой по индексу
        /// </summary>
        /// <param name="index">Индекс в массиве точек</param>
        /// <returns>Точка кривой</returns>
        public PointF getPoint(int index)
        {
            return Points[index];
        }


        /// <summary>
        /// Возвращает число точек на кривой
        /// </summary>
        public int PointsCount
        {
            get
            {
                return Points.Length;
            }
        }

        /// <summary>
        /// Метод отрисовки кривой
        /// </summary>
        /// <param name="g">Объект Graphics</param>
        /// <param name="Scale">Коэффициент масштабирования</param>
        public void drawCurve(Graphics g)
        {
            g.DrawCurve(cPen, scrPoints);
        }

        /// <summary>
        /// Возвращает имя кривой
        /// </summary>
        public string Name
        {
            get
            {
                return _curveName;
            }
        }

        /// <summary>
        /// Здадает или получает цвет кривой на графике
        /// </summary>
        public Color lineColor
        {
            get
            {
                return cPen.Color;
            }
            set
            {
                cPen.Color = value;
            }
        }

    }
}
