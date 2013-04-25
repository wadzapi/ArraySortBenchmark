using System;
using System.Collections.Generic;
using System.Drawing;

namespace Plotter
{
    public class XAxis:Axis
    {
        public XAxis()
        {
            this.AxName = "X, ед.";
        }

        protected override void DrawSteps(Graphics g, float length)
        {
            if (_stepVal == 0) //Не задан шаг
                return;
            int currPix = 0;//текущий пиксел 
            int counter = 0; //счетчик
            int minNextlabel = 0;//Минимальный пиксель подписи( чтобы не накладывались)
            float currVal = this.MinVal;
            while (currPix < length)
            {
                g.DrawLine(axPen, new Point(currPix, -stepLineHeigth), new Point(currPix, stepLineHeigth));
                if (counter % 2 != 0 && currPix > minNextlabel)
                {
                    string label = String.Format("{0}", Math.Round((double)currVal, 2));//currVal.ToString();
                    SizeF strSize = g.MeasureString(label, axStepFont);
                    minNextlabel = (int)(currPix + strSize.Width ) + 5;
                    g.DrawString(label, axStepFont, axPen.Brush, new PointF(currPix - 0.5f * strSize.Width, stepLineHeigth));
                }
                currPix += _stepPix;
                currVal += _stepVal;
                ++counter;

            }
        }

        public override void Draw(System.Drawing.Graphics g, float length)
        {
            base.Draw(g, length);
            //Рисуем линию оси
            g.DrawLine(axPen, new Point(), new Point((int)length, 0));
            //Рисуем стрелку
            g.FillPolygon(new SolidBrush(axPen.Color), new Point[] { new Point((int)length, arrowCapSize), new Point((int)length, -arrowCapSize), new Point((int)length + arrowCapSize, 0) });
            //Отрисовываем насечки и подписи оси
            if (doDrawSteps)
                DrawSteps(g, length);
            //Подписываем ось
            SizeF labSize = g.MeasureString(AxName, axLabelFont);
            g.DrawString(AxName, axLabelFont, axPen.Brush, new PointF(length - labSize.Width, labSize.Height)); 
        }
    }
}
