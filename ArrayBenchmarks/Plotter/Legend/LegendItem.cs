using System;
using System.Drawing;

namespace Plotter
{
    /// <summary>
    /// Класс, представляющий единичную запись в легенды графика
    /// </summary>
    public class LegendItem
    {
        private string _name; //Текст записи (имя кривой)
        public Color _color;//Цвет кривой на графике
        public static Font legFont; //Шрифт легенды
        private const float minFontSize = 5f; //Минимальное значение размера шрифта
        private const float maxFontSize = 14f;//Максимальное значене размера шрифта
        private static Color fontColor = Color.Black;//Цвет шрифта
        private static float currFontSize;//Текущее значение размера шрифта

        /// <summary>
        /// Установка минимального размера шрифта
        /// </summary>
        public static void setMinFont()
        {
            legFont = new Font("Arial", minFontSize, FontStyle.Regular);
        }

        //Измение размера шрифта для подбоар его размера
        public static bool setFontDelta(float scale)
        {
            currFontSize = legFont.Size * scale;
            if (currFontSize < minFontSize)
            {
                legFont = new Font("Arial", minFontSize, FontStyle.Regular);
                return false;
            }
            else if (currFontSize > maxFontSize)
            {
                legFont = new Font("Arial", maxFontSize, FontStyle.Regular);
                return false;
            }
            else
            {
                legFont = new Font("Arial", currFontSize, FontStyle.Regular);
                return true;
            }
        }

        /// <summary>
        /// Запись легенды
        /// </summary>
        /// <param name="col">Цвет кривой</param>
        /// <param name="Name">Имя кривой</param>
        public LegendItem( Color col, string Name)
        {
            this._name = Name;
            this._color = col;
        }

        /// <summary>
        /// Получение или задание имени кривой в легенде
        /// </summary>
        public string Name
        {
            get
            {
                if (_name!=null)
                    return _name;
                else
                    return "Без имени";
            }
            set
            {
                if (value != "")
                    _name = value;
            }
        }

        /// <summary>
        /// Получение размера записи легенды в пикселях
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        public SizeF getSize(Graphics g)
        {
            return g.MeasureString(this._name, legFont);
        }

        /// <summary>
        /// Отрисовка записи легенды
        /// </summary>
        /// <param name="g"></param>
        /// <param name="currPoint"></param>
        public void Draw(Graphics g, PointF currPoint)
        {
            g.FillRectangle(new SolidBrush(this._color), new RectangleF(currPoint, new SizeF(legFont.Size, legFont.Size)));
            g.DrawString(_name, legFont, new SolidBrush(fontColor), new PointF(currPoint.X + 1.5f * legFont.Size, currPoint.Y));
        }
    
    }
}
