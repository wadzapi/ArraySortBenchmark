using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;

namespace Benchmark
{
    /// <summary>
    /// Класс для проведения сравнительных испытаний эффективности
    /// алогритмов сортировки массивов
    /// </summary>
    public class ArraySortBenchmark:ArrayBenchmark
    {
        /// <summary>
        /// Минимальная размерность сортируемого массива
        /// </summary>
        private int MinSize;
        /// <summary>
        /// Максимальная размерность сортируемого массива
        /// </summary>
        private int _maxSize;
        /// <summary>
        /// Очередь тестов, заданных пользователем
        /// </summary>
        private ArraySortTest test;
        /// <summary>
        /// Список полученных результатов для теста
        /// </summary>
        private SortTestResult BenchResults;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="minSize">Минимальная размерность сортируемого массива</param>
        /// <param name="maxSize">Максимальная размерность сортируемого массива</param>
        /// <param name="methType">Тестируемые методы</param>
        public ArraySortBenchmark(int minSize, int maxSize )
            : base(maxSize)
        {
            this._maxSize = maxSize;
            this.MinSize = minSize;
        }

        /// <summary>
        /// Создание нового тестируемого массива указанной длины 
        /// и заполнение его случайными числами
        /// </summary>
        /// <param name="sourceLength">Число элементов тестрируемого массива</param>
        protected override void newRandomSource(int sourceLength) 
        {
            try
            {
                SourceArray = new int[sourceLength];
                for (int i = 0; i < sourceLength; ++i)
                {
                    SourceArray[i] = rnd.Next();
                }
            }
            catch (OutOfMemoryException)
            {
                throw new OutOfMemoryException("Задана слишком больщая размерность. Не хватает памяти");
            }
        }
        /// <summary>
        /// Задание или получение длины тестируемого массива
        /// </summary>
        public int MaxSize
        {
            get
            {
                return _maxSize;
            }
            set
            {
                if (value > _maxSize)
                    newRandomSource(value);
                _maxSize = value;
            }
        }

        /// <summary>
        /// Запуск тестовых испытаний
        /// </summary>
        public override void Analize()
        {
            //Обработка флагов теста
            test.processFlags();
            if (test.Count == 0)//Если в тест не добавлены методы
                throw new Exception("Не заданы тестируемые методы сортировки");
            //Создание экземпляра результата и добавление в список результатов
            BenchResults = new SortTestResult(test.TestName);
            //Выполнение теста
            goTest();
        }

        /// <summary>
        /// Проведение теста на производительность
        /// </summary>
        /// <param name="test">Проводимый тест</param>
        /// <param name="TestNum">Номер в списке результатов</param>
        private void goTest()
        {
            SortMethResult SingleThread = new SortMethResult("Однопоточность");
            SortMethResult MultiThread = new SortMethResult("Многопоточность");
            //Массив делегатов создаем и заполняем
            int count = test.Count;
            ArrSortDel[] dels = new ArrSortDel[count];
            for (int i = 0; i < count; ++i)
            {
                dels[i] = test.getNextMeth().deleg;
            }
            int delta = (MaxSize - MinSize) / count; //Вычисляем шаг размерности массива
            int currSize = MinSize;
            //ОДНОТОТОЧНОСТЬ
            while (currSize < MaxSize)
            {
                AVGTime = 0f;
                for (int i = 0; i < numIter + 1; ++i)
                {
                    timer.Reset(); //Сбрасываем таймер
                    timer.Start();//начало замера
                    for (int k = 0; k < count; ++k)
                    {
                        int[] testArray = new int[currSize]; //Массив тестируемой размерности
                        Array.Copy(SourceArray, testArray, currSize);//заполняем из TestArray
                        dels[k](ref testArray, true);//сортировка
                    }
                    timer.Stop();//конец замера
                    if (i != 0)
                    {
                        //SingleThread.AddResult(currSize, timer.ElapsedMilliseconds);
                        AVGTime += timer.ElapsedMilliseconds / (float)numIter;
                    }
                }
                SingleThread.AddResult(currSize, AVGTime);
                currSize += delta;
            }
            BenchResults.AddMethResult(SingleThread);
            //МНОГОПОТОЧНОСТЬ
            IAsyncResult[] results = new IAsyncResult[count];
            //Массив дескрипторов ожидания
            WaitHandle[] WaitHandles = new WaitHandle[count];
            currSize = MinSize;
            while (currSize < MaxSize)
            {
                AVGTime = 0f;
                for (int i = 0; i < numIter + 1; ++i)
                {
                    timer.Reset(); //Сбрасываем таймер
                    timer.Start();//начало замера
                    for (int k = 0; k < count; ++k)
                    {
                        int[] testArray = new int[currSize]; //Массив тестируемой размерности
                        Array.Copy(SourceArray, testArray, currSize);//заполняем из TestArray
                        results[k] = dels[k].BeginInvoke(ref testArray, true, null, null);//сортировка
                        WaitHandles[k] = results[k].AsyncWaitHandle;
                    }
                    WaitHandle.WaitAll(WaitHandles);
                    if (i != 0)
                    {
                        //MultiThread.AddResult(currSize, timer.ElapsedMilliseconds);
                        AVGTime += timer.ElapsedMilliseconds / (float)numIter;
                    }
                }
                MultiThread.AddResult(currSize, AVGTime);

                currSize += delta;
            }
            BenchResults.AddMethResult(MultiThread);
            //Если нужно пишем лог
            if (test.CreateLog)
                CreateLog(BenchResults);
        }

        private void CreateLog(SortTestResult result)
        {
            Log = new TestLog(Directory.GetCurrentDirectory() + @"\Logs\" + result.Name);
            SortMethResult[] MethResults = result.getResults();
            for (int meth = 0; meth < MethResults.Length; ++meth)
            {
                Log.WriteLine(MethResults[meth].Name);
                Log.Separate();
                System.Drawing.PointF[] ST = MethResults[meth].getResults();
                for (int i = 0; i < ST.Length; ++i)
                {
                    Log.WriteLine(ST[i].X.ToString() + " элементов", ST[i].Y.ToString() + " мсек");
                }
                Log.Separate();
            }
        }

        /// <summary>
        /// Добавление теста в список тестов
        /// </summary>
        /// <param name="test">Добавляемый тест</param>
        public ArraySortTest Test
        {
            set
            {
                test = value;
            }
        }

        /// <summary>
        /// Задает и получает число итерраций замеров
        /// </summary>
        public override int NumIterr
        {
            get
            {
                return numIter;
            }
            set
            {
                if (value > 0)
                    numIter = value;
                else
                    numIter = 1;
            }
        }

        /// <summary>
        /// Возвращает результаты замеров для протестированных методов
        /// </summary>
        /// <returns></returns>
        public SortTestResult getResults()
        {
            return BenchResults;
        }
    }
}

