using System;
using System.Windows.Forms;
using System.Drawing;
using Benchmark;
using Plotter;
using System.Threading;
using System.Diagnostics;

namespace ArrayBenchmarks
{
    public partial class BenchForm : Form
    {
        private ArraySortBenchmark bench; //Тестовый стенд
        private Chart chart; //График
        private Size OldSize; //"Старый" размер формы. Для вычисления коэффициента масштабирования
        private bool isMaximazed = false;//Иникатор состояния окна
        private ArraySortTest test1; //Тест
        private Process currProc; //Текущий процесс

        public BenchForm()
        {
            InitializeComponent();
            chart = new Chart(new Point(), new Size(800, 600));
            chart.oXName = "Размерность массива, ед.";
            chart.oYName = "Время, мсек";
            chart.graphName = "Производительность многопоточной программы";
            this.Controls.Add(chart);
            OldSize = this.ClientSize;
            currProc = Process.GetCurrentProcess();//Получаем текущий процесс
        }

        /// <summary>
        /// Запуск тестирования
        /// </summary>
        private void getBench()
        {
            bench = new ArraySortBenchmark(100, 33333);
            bench.NumIterr = 2; //Число итерраций замера
            test1 = new ArraySortTest("Производительность многопоточной программы"); //Создание экземпляра теста
            test1.CreateLog = true;
            test1.NumSegm = 5; //Число отрезков для замера
            test1.SIT = SimpleInsertType.SimpleInserts | SimpleInsertType.SimpleInsertsGuarded;
            test1.BIT = BinaryInserType.BinaryInsBlockCopy | BinaryInserType.BinaryInserts;
            test1.TWIT = TwoWaysInsertType.TwoWaysInsertBinCopy | TwoWaysInsertType.TwoWaysInserts;
            bench.Test = test1; //Задание теста
            bench.Analize(); //Проведение теста
        }

        //Обработка события изменения размера формы
        protected override void OnResize(EventArgs e)
        {
            if (this == null || chart == null)
                return;
            this.SuspendLayout();
            chart.SuspendLayout();
            //Отлавливаение разворачивания окна
            if (this.WindowState == FormWindowState.Maximized && isMaximazed == false)
            {
                isMaximazed = true;
                OnResizeEnd(null);
            }
            //Сворачивания окна
            else if (this.WindowState == FormWindowState.Normal && isMaximazed == true)
            {
                isMaximazed = false;
                OnResizeEnd(null);
            }
        }

        protected override void OnResizeBegin(EventArgs e)
        {
            this.SuspendLayout();
            chart.SuspendLayout();
            OldSize = this.ClientSize;
        }

        //Изменение размеров формы
        protected override void OnResizeEnd(EventArgs e)
        {
            SizeF scale = new SizeF((float)this.ClientSize.Width / (float)OldSize.Width, (float)this.ClientSize.Height / (float)OldSize.Height);
            chart.Rescale(scale);
            OldSize = this.ClientSize;
            chart.ResumeLayout();
            this.ResumeLayout();
        }

        #region СТАРТ / СТОП
        //Обработка нажатия на кнопку старта
        private void START_Click_1(object sender, EventArgs e)
        {
            START.Enabled = false; //Блокируем кнопку старта
            currProc.PriorityClass = ProcessPriorityClass.RealTime; //Самый высокий (доступный) приоритет
            Thread testThread = new Thread(new ThreadStart(getBench));
            testThread.Start();
            testThread.Join();
            chart.Clear();
            chart.AddCurves(bench.getResults());
            chart.SaveBmp();
            currProc.PriorityClass = ProcessPriorityClass.Normal;
            this.Text = "Выполнено";
            START.Enabled = true;
            this.Invalidate();
            chart.Invalidate();

        }
        #endregion

        private void BenchForm_Load(object sender, EventArgs e)
        {

        }
    }
}
