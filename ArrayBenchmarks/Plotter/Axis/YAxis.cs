using System;
using System.Collections.Generic;
using System.Drawing;

namespace Plotter
{
    public class YAxis:Axis
    {
        public YAxis()
        {
            this.AxName = "Y, ед.";
        }

        /// <summary>
        /// Отрисовка насечек и подписей оси
        /// </summary>
        /// <param name="g"></param>
        /// <param name="length"></param>
        protected override void DrawSteps(Graphics g, float length)
        {
            if (_stepVal == 0) //Если не задан шаг
                return;
            int currPix = 0; //Текущий пиксель
            int counter = 0; //счетчик
            int minNextlabel =0;//Минимальный пиксель подписи( чтобы не накладывались)
            float currVal = this.MinVal;
            while (currPix > -length)
            {
                g.DrawLine(axPen, new Point(-stepLineHeigth, currPix), new Point(stepLineHeigth, currPix));
                if (counter % 2 != 0 && currPix < minNextlabel)
                {
                    string label = String.Format("{0}", Math.Round((double)currVal, 2));//currVal.ToString();
                    SizeF strSize = g.MeasureString(label, axStepFont);
                    minNextlabel = (int)(currPix - strSize.Height) - 5;
                    g.DrawString(label, axStepFont, axPen.Brush, new PointF(-strSize.Width - 1.5f * stepLineHeigth, currPix - 0.5f * strSize.Height));
                }
                currPix -= _stepPix;
                currVal += _stepVal;
                ++counter;
            }
        }

        public override void Draw(System.Drawing.Graphics g, float length)
        {
            //рисуем линию оси
            g.DrawLine(axPen, new System.Drawing.Point(), new System.Drawing.Point(0, (int)-length));
            //Рисуем стрелку
            g.FillPolygon(new SolidBrush(axPen.Color), new Point[] { new Point(-arrowCapSize, -(int)length), new Point(arrowCapSize, -(int)length), new Point(0, -(int)length - arrowCapSize) });
            //Рисуем насечки и пождписи оси
            if (doDrawSteps)
                DrawSteps(g, length);
            //Подписываем ось
            SizeF labSize = g.MeasureString(AxName, axLabelFont);
            g.DrawString(AxName, axLabelFont, axPen.Brush, new PointF(g.VisibleClipBounds.Left/*-labSize.Width - 2 * stepLineHeigth*/, -length - labSize.Height));
        }
    }
}
