using System;

//Классы различных алгоритмов сортировки включениями
namespace Sorting.Inserts
{
    /// <summary>
    /// Различные модификации сортировки методом простых включений
    /// </summary>
    public static class SimpleInserts
    {
        #region Классический немодифицированный метод простых включений

        /// <summary>
        /// Классический немодифицированный метод простых включений
        /// </summary>
        /// <param name="Arr">Сортируемый массив</param>
        /// <param name="increase">Порядок сортировки (возрастание/убывание)</param>
        public static void SimpleInsert(ref int[] Arr, bool increase)
        {
            if (increase)
                SimpleInsertsIncr(ref Arr, Arr.Length);
            else
                SimpleInsertsDecr(ref Arr, Arr.Length);
        }

        /// <summary>
        /// Классический немодифицированный метод простых включений,
        /// для сортировки в порядке возрастания
        /// </summary>
        /// <param name="Arr">Сортируемый массив</param>
        /// <param name="size">Размерность массива</param>
        private static void SimpleInsertsIncr(ref int[] Arr, int size)
        {
            int key, i; //Ключ сортировки, его индекс
            int elem, j; //Элемент сравнения, его индекс
            for (i = 1; i < size; ++i) //Начиная со 2го элемента до последнего
            {
                key = Arr[i];//Задание ключа
                j = i - 1;
                while (j >= 0) //Поиск места для включения
                {
                    elem = Arr[j];//Задание элемента сравнения
                    if (key > elem) //предыдущий элемент меньше или равен ключу
                        break;
                    Arr[j + 1] = elem; //сдвигаем предыдущий на место последующего
                    --j;
                }
                Arr[j + 1] = key; //Вставляем ключевой элемент
            }
        }

        /// <summary>
        /// Классический немодифицированный метод простых включений,
        /// для сортировки в порядке убывания
        /// </summary>
        /// <param name="Arr">Сортируемый массив</param>
        /// <param name="size">Размерность массива</param>
        private static void SimpleInsertsDecr(ref int[] Arr, int size)
        {
            int key, i; //Ключ сортировки, его индекс
            int elem, j; //Элемент сравнения, его индекс
            for (i = 1; i < size; ++i) //Начиная со 2го элемента до последнего
            {
                key = Arr[i];
                j = i - 1;
                while (j >= 0) //Поиск места для включения
                {
                    elem = Arr[j];
                    if (key < elem) //предыдущий элемент меньше или равен ключу
                        break;
                    Arr[j + 1] = elem; //сдвигаем предыдущий на место последующего
                    --j;
                }
                Arr[j + 1] = key; //Вставляем ключевой элемент
            }
        }
        #endregion

        #region Метод простых включений с использованием фиктивного элемента(барьера)
        public static void SimpleInsertsGuarded(ref int[] Arr, bool increase)
        {
            if (increase)
                SimpleInsertsIncrGuarded(ref Arr, Arr.Length);
            else
                SimpleInsertsDecrGuarded(ref Arr, Arr.Length);
        } //Метод для вызова сортировки простыми включениями с барьером

        private static void SimpleInsertsDecrGuarded(ref int[] Arr, int size) //Простыми включениями с барьером в возрастающем порядке
        {
            int firstElem = Arr[0];//Запоминаем первый элемент,т.к. на его месте будет расположен фиктивный элемент(барьер)
            int key, i; //Ключ сортировки, его индекс
            int j; //индекс элемента сравнения

            //Проводим включения с фиктивный (первым) элементом
            for (i = 1; i < size; ++i) //Начиная со 2го элемента до последнего
            {
                key = Arr[0] = Arr[i];
                j = i - 1;
                while (key > Arr[j]) //Поиск места для включения
                {
                    Arr[j + 1] = Arr[j]; //сдвигаем предыдущий на место последующего
                    --j;
                }
                Arr[j + 1] = key; //Вставляем ключевой элемент
            }

            //Возращаем изъятый первый элемент на нужное место
            j = 1;
            while (j < size && firstElem < Arr[j - 1]) //Поиск места включения
            {
                Arr[j - 1] = Arr[j]; //Cдвигаем элементы влево, до тех пор, пока не найдем подходящее место
                ++j;
            }
            Arr[j - 1] = firstElem; //вставляем изъятый элемент
        }

        private static void SimpleInsertsIncrGuarded(ref int[] Arr, int size) //Простыми включениями с барьером в убывающем порядке
        {
            int firstElem = Arr[0];//Запоминаем первый элемент,т.к. на его месте будет расположен фиктивный элемент(барьер)
            int key, i; //Ключ сортировки, его индекс
            int j; //индекс элемента сравнения

            //Проводим включения с фиктивный (первым) элементом
            for (i = 1; i < size; ++i) //Начиная со 2го элемента до последнего
            {
                key = Arr[0] = Arr[i];//Задаем значение ключа и барьера
                j = i - 1;//Задаем индекс элемента сравнения
                while (key < Arr[j]) //Поиск места для включения
                {
                    Arr[j + 1] = Arr[j]; //сдвигаем предыдущий на место последующего
                    --j;
                }
                Arr[j + 1] = key; //Вставляем ключевой элемент
            }

            //Возращаем изъятый первый элемент на нужное место
            j = 1;
            while (j < size && firstElem > Arr[j - 1]) //Поиск места включения
            {
                Arr[j - 1] = Arr[j]; //Cдвигаем элементы влево, до тех пор, пока не найдем подходящее место
                ++j;
            }
            Arr[j - 1] = firstElem; //вставляем изъятый элемент
        }
        #endregion
    }
}
