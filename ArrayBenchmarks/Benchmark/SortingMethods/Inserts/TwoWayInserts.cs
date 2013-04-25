using System;

namespace Sorting.Inserts
{
    /// <summary>
    /// Метод сортировки двухпутевыми вставками
    /// </summary>
    public static class TwoWaysInserts
    {
        #region Немодифицированный метод двухпутевых вставок
        /// <summary>
        /// Метод сортировки двухпутевыми вставками
        /// </summary>
        /// <param name="Arr">Сортируемый массив</param>
        /// <param name="Incr">Порядок сортировки(возрастание/убывание)</param>
        public static void TwoWaysInsert(ref int[] Arr, bool Incr)
        {
            if (Incr)
                TwoWaysInsertIncr(ref Arr, Arr.Length);
            else
                TwoWaysInsertDecr(ref Arr, Arr.Length);
        }

        /// <summary>
        /// Метод двухпутевых вставок в порядке возрастания
        /// </summary>
        /// <param name="Arr">Сортируемый массив</param>
        /// <param name="size">Размерность массива</param>
        private static void TwoWaysInsertIncr(ref int[] Arr, int size)
        {
            int[] tempArr = new int[2 * size + 1];//Временный массив, для размещения элементов, размером 2N+1
            int left, right; //Левая и правая границы выходного массива
            int key; //Ключ сортровки
            int InsPos; //Место (индекс) включения
            left = right = size; //Начальные границы выходного массива
            tempArr[size] = Arr[0]; //Задаем центральный элемент выходного массива
            for (int i = 1; i < size; ++i)
            {
                key = Arr[i];//Задаем ключ
                if (key >= Arr[0]) //Превышает центральный элемент
                {
                    ++right;//Увеличиваем правую границу
                    //Поиск места включения
                    InsPos = right;
                    while (key < tempArr[InsPos - 1]) //сдвигаем все что больше ключа вправо
                    {
                        tempArr[InsPos] = tempArr[InsPos - 1];
                        --InsPos;
                    }
                    tempArr[InsPos] = key; //Вставляем ключ
                }
                else
                {
                    --left; //Сдвигаем (уменьшаем) левую границу
                    //Поиск места включения
                    InsPos = left;
                    while (key > tempArr[InsPos + 1]) //Сдвигаем все, что меньше ключа влево
                    {
                        tempArr[InsPos] = tempArr[InsPos + 1];
                        ++InsPos;
                    }
                    tempArr[InsPos] = key; //Вставляем ключ
                }
            }
            //Копируем отсортированные элементы из tempArray во входной Arr
            for (int i = 0; i < size; ++i)
            {
                Arr[i] = tempArr[left + i];
            }

        }

        /// <summary>
        /// Метод двухпутевых вставок в порядке убывания
        /// </summary>
        /// <param name="Arr">Сортируемый массив</param>
        /// <param name="size">Размерность массива</param>
        private static void TwoWaysInsertDecr(ref int[] Arr, int size)
        {
            int[] tempArr = new int[2 * size + 1];//Временный массив, для размещения элементов, размером 2N+1
            int left, right; //Левая и правая границы выходного массива
            int key; //Ключ сортровки
            int InsPos; //Место (индекс) включения
            left = right = size; //Начальные границы выходного массива
            tempArr[size] = Arr[0]; //Задаем центральный элемент выходного массива
            for (int i = 1; i < size; ++i)
            {
                key = Arr[i];//Задаем ключ
                if (key <= Arr[0]) //Меньше центрального элемента
                {
                    ++right;//Увеличиваем правую границу
                    //Поиск места включения
                    InsPos = right;
                    while (key > tempArr[InsPos - 1]) //сдвигаем все что меньше ключа вправо
                    {
                        tempArr[InsPos] = tempArr[InsPos - 1];
                        --InsPos;
                    }
                    tempArr[InsPos] = key; //Вставляем ключ
                }
                else
                {
                    --left; //Сдвигаем (уменьшаем) левую границу
                    //Поиск места включения
                    InsPos = left;
                    while (key < tempArr[InsPos + 1]) //Сдвигаем все, что больше ключа влево
                    {
                        tempArr[InsPos] = tempArr[InsPos + 1];
                        ++InsPos;
                    }
                    tempArr[InsPos] = key; //Вставляем ключ
                }
            }
            //Копируем отсортированные элементы из tempArray во входной Arr
            for (int i = 0; i < size; ++i)
            {
                Arr[i] = tempArr[left + i];
            }
        }
        #endregion

        #region Метод двухпетевых вставок с использование бинарного поиска и BlockCopy

        /// <summary>
        /// Метод двухпетевых вставок с использование бинарного поиска места включения 
        /// и Buffer.BlockCopy для пересылки блока элементов
        /// </summary>
        /// <param name="Arr">Сортируемый массив</param>
        /// <param name="size">Размерность массива</param>
        public static void TwoWaysInsertBinCopy(ref int[] Arr, bool Incr)
        {
            if (Incr)
                TwoWaysInsertBinCopyIncr(ref Arr, Arr.Length);
            else
                TwoWaysInsertBinCopyDecr(ref Arr, Arr.Length);
        }

        /// <summary>
        /// Метод двухпетевых вставок с использование бинарного поиска и BlockCopy
        /// в порядке возрастания
        /// </summary>
        /// <param name="Arr">Сортируемый массив</param>
        /// <param name="size">Размерность массива</param>
        private static void TwoWaysInsertBinCopyIncr(ref int[] Arr, int size)
        {
            int Bl, Bm, Br;//Правая левая и средняя гранци бинарного поиска
            int[] tempArr = new int[2 * size + 1];//Временный массив, для размещения элементов, размером 2N+1
            int left, right; //Левая и правая границы выходного массива
            int key; //Ключ сортровки
            left = right = size; //Начальные границы выходного массива
            tempArr[size] = Arr[0]; //Задаем центральный элемент выходного массива
            for (int i = 1; i < size; ++i)
            {
                key = Arr[i];//Задаем ключ
                if (key >= Arr[0]) //Превышает центральный элемент
                {
                    ++right;//Увеличиваем правую границу
                    //Поиск места включения методом бинарного поиска
                    Bl = size + 1; //Нач. Левая граница бин. поиска
                    Br = right - 1; //Нач. Правая граница бинарного поиска 
                    while (Bl <= Br)
                    {
                        Bm = (Bl + Br) >> 1;//Индекс среднего элемента
                        if (key < tempArr[Bm]) //Ключ меньше среднего
                            Br = --Bm; //Уменьшаем правую границу
                        else
                            Bl = ++Bm; //иначе увеличиваем левую
                    }
                    //Сдвигаем вправо все, что больше ключа
                    if (right - Bl > 0)
                    {
                        Buffer.BlockCopy(tempArr, (Bl) * 4, tempArr, (Bl + 1) * 4, (right - Bl) * 4);
                    }
                    tempArr[Bl] = key; //Вставляем ключ
                }
                else
                {
                    --left; //Сдвигаем (уменьшаем) левую границу
                    //Поиск места включения методом бинарного поиска
                    Bl = left + 1; //Нач. Левая граница бин. поиска
                    Br = size - 1; //Нач. Правая граница бинарного поиска 
                    while (Bl <= Br)
                    {
                        Bm = (Bl + Br) >> 1;//Индекс среднего элемента
                        if (key < tempArr[Bm]) //Ключ меньше среднего
                            Br = --Bm; //Уменьшаем правую границу
                        else
                            Bl = ++Bm; //иначе увеличиваем левую
                    }
                    //Сдвигаем влево все, что меньше ключа
                    if (Br - left > 0)
                    {
                        Buffer.BlockCopy(tempArr, (left + 1) * 4, tempArr, left * 4, (Br - left) * 4);
                    }
                    tempArr[Br] = key; //Вставляем ключ
                }
            }
            //Копируем отсортированные элементы из tempArray во входной Arr
            Array.Copy(tempArr, left, Arr, 0, Arr.Length);
        }

        /// <summary>
        /// Метод двухпетевых вставок с использование бинарного поиска и BlockCopy
        /// в порядке убывания
        /// </summary>
        /// <param name="Arr">Сортируемый массив</param>
        /// <param name="size">Размерность массива</param>
        private static void TwoWaysInsertBinCopyDecr(ref int[] Arr, int size)
        {
            int Bl, Bm, Br;//Правая левая и средняя гранци бинарного поиска
            int[] tempArr = new int[2 * size + 1];//Временный массив, для размещения элементов, размером 2N+1
            int left, right; //Левая и правая границы выходного массива
            int key; //Ключ сортровки
            left = right = size; //Начальные границы выходного массива
            tempArr[size] = Arr[0]; //Задаем центральный элемент выходного массива
            for (int i = 1; i < size; ++i)
            {
                key = Arr[i];//Задаем ключ
                if (key <= Arr[0]) //Превышает центральный элемент
                {
                    ++right;//Увеличиваем правую границу
                    //Поиск места включения методом бинарного поиска
                    Bl = size + 1; //Нач. Левая граница бин. поиска
                    Br = right - 1; //Нач. Правая граница бинарного поиска 
                    while (Bl <= Br)
                    {
                        Bm = (Bl + Br) >> 1;//Индекс среднего элемента
                        if (key >= tempArr[Bm]) //Ключ больше среднего
                            Br = --Bm; //Уменьшаем правую границу
                        else
                            Bl = ++Bm; //иначе увеличиваем левую
                    }
                    //Сдвигаем вправо все, что меньше ключа
                    if (right - Bl > 0)
                    {
                        Buffer.BlockCopy(tempArr, (Bl) * 4, tempArr, (Bl + 1) * 4, (right - Bl) * 4);
                    }
                    tempArr[Bl] = key; //Вставляем ключ
                }
                else
                {
                    --left; //Сдвигаем (уменьшаем) левую границу
                    //Поиск места включения методом бинарного поиска
                    Bl = left + 1; //Нач. Левая граница бин. поиска
                    Br = size - 1; //Нач. Правая граница бинарного поиска 
                    while (Bl <= Br)
                    {
                        Bm = (Bl + Br) >> 1;//Индекс среднего элемента
                        if (key >= tempArr[Bm]) //Ключ меньше среднего
                            Br = --Bm; //Уменьшаем правую границу
                        else
                            Bl = ++Bm; //иначе увеличиваем левую
                    }
                    //Сдвигаем влево все, что больше ключа
                    if (Br - left > 0)
                    {
                        Buffer.BlockCopy(tempArr, (left + 1) * 4, tempArr, left * 4, (Br - left) * 4);
                    }
                    tempArr[Br] = key; //Вставляем ключ
                }
            }
            //Копируем отсортированные элементы из tempArray во входной Arr
            Array.Copy(tempArr, left, Arr, 0, Arr.Length);
        }

        #endregion
    }
}
