using System;
using System.Collections.Generic;
using System.Drawing;

namespace Plotter
{
    public class Axis
    {
        protected static Pen axPen = new Pen(Color.Black, 1.6f);
        protected const int arrowCapSize = 5; //Начальный размер стрелки
        protected static Font axStepFont = new Font("Courier New", 9f, FontStyle.Regular); //Шрифт для подписи насечек
        protected static Font axLabelFont = new Font("Courier New", 11f, FontStyle.Bold | FontStyle.Italic); //Шрифт для подписи названия оси
        protected const int stepLineHeigth = 5;//Высота насечки на оси
        public const int _DefaultStepPix = 25; //Стандартный шаг оси
        public string AxName; //Название оси
        protected int _stepPix = 25; //Длина шага оси в пикселях
        protected float _stepVal;//Значение шага оси
        private static float axStepFontSize = 9f;//Значение размера шрифта подписей оси
        private static float axLabelFontSize = 11f;//Значение размера шрифта для подписи оси 
        private const float maxFontSize = 14f; //Максимальный размер шрифта для подписей оси
        private const float minFontSize = 7f;//Минимальное значение размера шрифта
        /// <summary>
        /// Минимальное значение оси
        /// </summary>
        public float MinVal;
        /// <summary>
        /// Максимальное значение оси
        /// </summary>
        public float MaxVal;
        public bool doDrawSteps = true;//Индикатор отрисовки насечек на осях координат

        public Axis()
        {
        }

        /// <summary>
        /// Масштабирование размера шрифта
        /// </summary>
        /// <param name="scale">Коэфиициент масштабирования</param>
        /// <param name="f">Масштабируемый шрифт</param>
        /// <param name="currFontSize">Текущий  размер шрифта</param>
        private static void RescaleFont(SizeF scale, ref Font f, ref float currFontSize)
        {
            float ratio = (scale.Height < scale.Height) ? scale.Height : scale.Width;
            currFontSize *= ratio;
            if (currFontSize > maxFontSize)
                f = new Font(f.FontFamily, maxFontSize, f.Style);
            else if (currFontSize < minFontSize)
                f = new Font(f.FontFamily, minFontSize, f.Style);
            else
                f = new Font(f.FontFamily, currFontSize, f.Style);
        }

        /// <summary>
        /// Масштабирование оси
        /// </summary>
        /// <param name="Scale">Коэффициент масштабирования</param>
        public static void Rescale(SizeF Scale)
        {
            RescaleFont(Scale, ref axLabelFont, ref axLabelFontSize);
            RescaleFont(Scale, ref axStepFont, ref axStepFontSize);
        }

        /// <summary>
        /// Назначение цвета для осей координат 
        /// </summary>
        /// <param name="c"></param>
        public static void setColor(Color c)
        {
            axPen.Color = c;
        }

        /// <summary>
        /// Задание и получение значения шала шкалы в единицах
        /// шкалы
        /// </summary>
        public float StepValue
        {
            get
            {
                return _stepVal;
            }
            set
            {
                if (value >= 0)
                    _stepVal = value;
                else
                    _stepVal = 0;

            }
        }

        /// <summary>
        /// Задание и получение длины шага в пикселях
        /// </summary>
        public int StepPix
        {
            get
            {
                return _stepPix;
            }
            set
            {
                if (value > 1)
                    _stepPix = value;
                else
                    _stepPix = 1;
            }
        }

        /// <summary>
        /// Отрисовка оси
        /// </summary>
        /// <param name="g">Объект Graphics</param>
        /// <param name="length">Длина оси в пикселях</param>
        public virtual void Draw(Graphics g, float length)
        {
        }

        protected virtual void DrawSteps(Graphics g, float length)
        {
        }
    }
}
