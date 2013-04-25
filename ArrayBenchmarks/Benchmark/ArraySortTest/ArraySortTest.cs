using System;
using System.Collections.Generic;


namespace Benchmark
{

    /// <summary>
    /// Задает парметры тестовых испытаний для серии тестов по оценке
    /// производительности различных методов сортировки
    /// </summary>
    public class ArraySortTest
    {
        /// <summary>
        /// Число замеров с различными размерностями для одного метода
        /// (Число точек замера)
        /// </summary>
        private int numSegm = 10;
        /// <summary>
        /// Очередь из делегатов методов сортировки
        /// </summary>
        private Queue<SortMethInf> sortingMethods;
        /// <summary>
        /// Название (имя) теста
        /// </summary>
        private string _testName;
        /// <summary>
        /// Индикатор для создания лога
        /// </summary>
        public bool CreateLog = false;

        #region Флаги
        /// <summary>
        /// Флаги сортировки простыми вставками
        /// </summary>
        public SimpleInsertType SIT;
        /// <summary>
        /// Флаги сортировки бинарными вставками
        /// </summary>
        public BinaryInserType BIT;
        /// <summary>
        /// Флаги сортировки двухпутевыми вставками
        /// </summary>
        public TwoWaysInsertType TWIT;

        /// <summary>
        /// Обработка флагов
        /// </summary>
        public void processFlags()
        {
            SortFlagsHelper.ProcessFlags(ref SIT, ref  sortingMethods);
            SortFlagsHelper.ProcessFlags(ref BIT, ref  sortingMethods);
            SortFlagsHelper.ProcessFlags(ref TWIT, ref  sortingMethods);
        }
        #endregion

        public ArraySortTest(string name)
        {
            this._testName = name;
            Init();
        }
        public ArraySortTest()
        {
            Init();
        }

        /// <summary>
        /// Начальная инициализация
        /// </summary>
        private void Init()
        {
            sortingMethods = new Queue<SortMethInf>();
        }

        /// <summary>
        /// Задает или получает число сегментов, на которые нужно разделить функцию
        /// </summary>
        public int NumSegm
        {
            get
            {
                return numSegm;
            }
            set
            {
                if (value > 0)
                    numSegm = value;
            }
        }

        /// <summary>
        /// Число методов теста
        /// </summary>
        public int Count
        {
            get
            {
                return sortingMethods.Count;
            }
        }

        /// <summary>
        /// Получение следующего делегата метода сортировки из очереди
        /// </summary>
        /// <returns>Метод очереди делегатов</returns>
        public SortMethInf getNextMeth()
        {
            SortMethInf meth;
            try
            {
                meth = sortingMethods.Dequeue();
            }
            catch (InvalidOperationException)
            {
                meth = null;
            }
            return meth;
        }

        /// <summary>
        /// Задает или возвращает имя метода
        /// </summary>
        public string TestName
        {
            get
            {
                return _testName;
            }
            set
            {
                _testName = value;
            }
        }
    }
}
