using System;

namespace Sorting.Inserts
{
    /// <summary>
    /// Различные модификации метода сортировки бинарными включениями
    /// </summary>
    public static class BinaryInserts
    {
        #region  Немодифицировнный метод бинарных включений

        /// <summary>
        /// Немодифицированный метод сортировки бинарными включениями
        /// </summary>
        /// <param name="Arr">Сортируемый массив</param>
        /// <param name="Incr">Порядок сортировки (возрастание/убыванеие)</param>
        public static void BinaryInsert(ref int[] Arr, bool Incr)
        {
            if (Incr)
                BinaryInsertsIncr(ref Arr, Arr.Length);
            else
                BinaryInsertsDecr(ref Arr, Arr.Length);
        }

        /// <summary>
        /// Немодифицированная сортировка бинарными включениями в порядке возрастания
        /// </summary>
        /// <param name="Arr">Сортируемый массив</param>
        /// <param name="size">Размерность массива</param>
        private static void BinaryInsertsIncr(ref int[] Arr, int size)
        {
            int key, i; //Ключ и его индекс
            int l, r, m; //Индексы левой и правой границы и среднего для двоичного поиска
            for (i = 1; i < size; ++i)
            {
                key = Arr[i];//Задание ключа
                l = 0; //Начальная левая граница поиска
                r = i - 1; //Начальная правая граница поиска
                //Двоичный поиск места включения
                while (l <= r)
                {
                    m = (l + r) >> 1;
                    if (key < Arr[m])
                        r = --m;
                    else
                        l = ++m;
                }
                //Сдвигаем элементы вправо, освобождая место для включаемого элемента
                for (int k = i - 1; k >= l; --k)
                {
                    Arr[k + 1] = Arr[k];
                }
                Arr[l] = key; //Вставляем ключевой элемент
            }
        }

        /// <summary>
        /// Немодифицированная сортировка бинарными включениями в порядке убывания
        /// </summary>
        /// <param name="Arr">Сортируемый массив</param>
        /// <param name="size">Размерность массива</param>
        private static void BinaryInsertsDecr(ref int[] Arr, int size)
        {
            int key, i; //Ключ и его индекс
            int l, r, m; //Индексы левой и правой границы и среднего для двоичного поиска
            for (i = 1; i < size; ++i)
            {
                key = Arr[i];//Задание ключа
                l = 0; //Начальная левая граница поиска
                r = i - 1; //Начальная правая граница поиска
                //Двоичный поиск места включения
                while (l <= r)
                {
                    m = (l + r) >> 1;
                    if (key >= Arr[m])
                        r = --m;
                    else
                        l = ++m;
                }
                //Сдвигаем элементы вправо, освобождая место для включаемого элемента
                for (int k = i - 1; k >= l; --k)
                {
                    Arr[k + 1] = Arr[k];
                }
                Arr[l] = key; //Вставляем ключевой элемент
            }
        }
        #endregion

        #region Метод бинарных вставок с использованием Buffer.BlockCopy

        /// <summary>
        /// Метод сортировки бинарными включениниями, с использованием вместо цикла сдвига
        /// копирование методом Buffer.BlockCopy
        /// </summary>
        /// <param name="Arr">Сортируемый массив</param>
        /// <param name="Incr">Порядок сортировки (возрастание/убывание)</param>
        public static void BinaryInsBlockCopy(ref int[] Arr, bool Incr)
        {
            if (Incr)
                BinaryInsBlockCopyIncr(ref Arr, Arr.Length);
            else
                BinaryInsBlockCopyDecr(ref Arr, Arr.Length);
        }

        /// <summary>
        /// Сортировка бинарными включениями с исп. метода BlockCopy в порядке возрастания
        /// </summary>
        /// <param name="Arr">Сортируемый массив</param>
        /// <param name="Incr">Порядок сортировки (возрастание/убывание)</param>
        private static void BinaryInsBlockCopyIncr(ref int[] Arr, int size)
        {
            int key, i; //Ключ и его индекс
            int l, r, m; //Индексы левой и правой границы и среднего для двоичного поиска
            for (i = 1; i < size; ++i)
            {
                key = Arr[i];//Задание ключа
                l = 0; //Начальная левая граница поиска
                r = i - 1; //Начальная правая граница поиска
                //Двоичный поиск места включения
                while (l <= r)
                {
                    m = (l + r) >> 1;
                    if (key < Arr[m])
                        r = --m;
                    else
                        l = ++m;
                }
                //Сдвигаем элементы вправо, освобождая место для включаемого элемента
                if (i - l > 0)
                {
                    Buffer.BlockCopy(Arr, l * 4, Arr, (l + 1) * 4, (i - l) * 4);
                    //Array.Copy(Arr, l, Arr, l + 1, l - l);
                }
                Arr[l] = key;  //Вставляем ключевой элемент
            }
        }

        /// <summary>
        /// Сортировка бинарными включениями с исп. метода BlockCopy в порядке убывания
        /// </summary>
        /// <param name="Arr">Сортируемый массив</param>
        /// <param name="Incr">Порядок сортировки (возрастание/убывание)</param>
        private static void BinaryInsBlockCopyDecr(ref int[] Arr, int size)
        {
            int key, i; //Ключ и его индекс
            int l, r, m; //Индексы левой и правой границы и среднего для двоичного поиска
            for (i = 1; i < size; ++i)
            {
                key = Arr[i];//Задание ключа
                l = 0; //Начальная левая граница поиска
                r = i - 1; //Начальная правая граница поиска
                //Двоичный поиск места включения
                while (l <= r)
                {
                    m = (l + r) >> 1;
                    if (key >= Arr[m])
                        r = --m;
                    else
                        l = ++m;
                }
                //Сдвигаем элементы вправо, освобождая место для включаемого элемента
                if (i - l > 0)
                {
                    Buffer.BlockCopy(Arr, l * 4, Arr, (l + 1) * 4, (i - l) * 4);
                    //Array.Copy(Arr, l, Arr, l + 1, l - l);
                }
                Arr[l] = key;  //Вставляем ключевой элемент
            }
        }

        #endregion

    }
}
