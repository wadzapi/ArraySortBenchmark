using System;
using System.Collections.Generic;
using Sorting;
using Sorting.Inserts;

namespace Benchmark
{
    #region Классы перечисление флагов, соответствующие типам сортировки
    /// <summary>
    /// Флаги для сортировки простыми включениями
    /// </summary>
    [FlagsAttribute]
    public enum SimpleInsertType
    {
        /// <summary>
        /// не производить тестирование методов простых вставок
        /// </summary>
        None = 0x0,
        /// <summary>
        /// Простыми включениями
        /// </summary>
        SimpleInserts = 0x1,
        /// <summary>
        /// Простыми включениями со сторожевым элементом
        /// </summary>
        SimpleInsertsGuarded = 0x2
    }
    /// <summary>
    /// Флаги для сортировки бинарными включениями
    /// </summary>
    [FlagsAttribute]
    public enum BinaryInserType
    {
        /// <summary>
        /// Не производить тестирование методов бниарных вставок
        /// </summary>
        None = 0x0,
        /// <summary>
        /// Бинарными вставками
        /// </summary>
        BinaryInserts = 0x1,
        /// <summary>
        /// Бинарными вставками, исп. метод Buffer.BlockCopy
        /// </summary>
        BinaryInsBlockCopy = 0x2
    }
    /// <summary>
    /// Флаги для сортировки методом двухпутевых вставок
    /// </summary>
    [FlagsAttribute]
    public enum TwoWaysInsertType
    {
        /// <summary>
        /// Не проводить тестирование методов двухпутевых вставок
        /// </summary>
        None = 0x0,
        /// <summary>
        /// Двухпутевыми вставками
        /// </summary>
        TwoWaysInserts = 0x1,
        /// <summary>
        /// Метод двухпетевых вставок с использование бинарного поиска и Buffer.BlockCopy
        /// </summary>
        TwoWaysInsertBinCopy = 0x2
    }
    /// <summary>
    /// Флаги для сортировки методом Шелла
    /// </summary>
    [FlagsAttribute]
    public enum ShellType
    {
        /// <summary>
        /// Не проводить тестирование метода Шелла
        /// </summary>
        None = 0x0,
        /// <summary>
        ///Обычными вставками, последовательность приращений Шелла
        /// </summary>
        Shell = 0x1,
        /// <summary>
        /// Обычными вставками, последовательность приращений Хиббарда
        /// </summary>
        Hibbard = 0x2,
        /// <summary>
        /// Обычными вставками, последовательность приращений Кнута
        /// </summary>
        Knuth = 0x4,
        /// <summary>
        /// Обычными вставками, последовательность приращений Инсерпи-Седгевика
        /// </summary>
        Incerpi_Sedgewick = 0x8,
        /// <summary>
        /// Обычными вставками, последовательность приращений Седгевика
        /// </summary>
        Sedgewick = 0x10,
        /// <summary>
        /// Обычными вставками, последовательность приращений Токуда
        /// </summary>
        Tokuda = 0x20,
        /// <summary>
        /// Обычными вставками, последовательность приращений Циуры
        /// </summary>
        Ciura = 0x40,
        /// <summary>
        /// Вставками с барьером, последовательность приращений Шелла
        /// </summary>
    }
    #endregion


    /// <summary>
    /// Класс для обработки флагов сортировки
    /// </summary>
    public static class SortFlagsHelper
    {       
        /// <summary>
        /// Обработка флагов для сортировки простыми вставками
        /// </summary>
        /// <param name="SIT">Флаги</param>
        /// <returns>Массив методов</returns>
        public static void ProcessFlags(ref SimpleInsertType SIT, ref Queue<SortMethInf> meths)
        {
            if (SIT > SimpleInsertType.None)
            {
                if ((SIT & SimpleInsertType.SimpleInserts) == SimpleInsertType.SimpleInserts)
                    meths.Enqueue(new SortMethInf("Простыми вставками", new ArrSortDel(SimpleInserts.SimpleInsert)));
                if ((SIT & SimpleInsertType.SimpleInsertsGuarded) == SimpleInsertType.SimpleInsertsGuarded)
                    meths.Enqueue(new SortMethInf("Вставками с барьером", new ArrSortDel(SimpleInserts.SimpleInsertsGuarded)));
            }
        }

        /// <summary>
        /// Обратботка флагов для сортировки бинарными включениями
        /// </summary>
        /// <param name="BIT">Флаги</param>
        /// <returns>Массив методов</returns>
        public static void ProcessFlags(ref BinaryInserType BIT, ref Queue<SortMethInf> meths)
        {
            if (BIT > BinaryInserType.None)
            {
                if ((BIT & BinaryInserType.BinaryInserts) == BinaryInserType.BinaryInserts)
                    meths.Enqueue(new SortMethInf("Бинарными вставками", new ArrSortDel(BinaryInserts.BinaryInsert)));
                if ((BIT & BinaryInserType.BinaryInsBlockCopy) == BinaryInserType.BinaryInsBlockCopy)
                    meths.Enqueue(new SortMethInf("Бинарными вставками + BlockCopy", new ArrSortDel(BinaryInserts.BinaryInsBlockCopy)));
            }
        }

        /// <summary>
        /// Обоаботка флагов для двухпутевых вставок
        /// </summary>
        /// <param name="TWIT">Флаги</param>
        /// <returns>Массив методов</returns>
        public static void ProcessFlags(ref TwoWaysInsertType TWIT, ref Queue<SortMethInf> meths)
        {
            if (TWIT > TwoWaysInsertType.None)
            {
                if ((TWIT & TwoWaysInsertType.TwoWaysInserts) == TwoWaysInsertType.TwoWaysInserts)
                    meths.Enqueue(new SortMethInf("Двухпутевыми вставками", new ArrSortDel(TwoWaysInserts.TwoWaysInsert)));
                if ((TWIT & TwoWaysInsertType.TwoWaysInsertBinCopy) == TwoWaysInsertType.TwoWaysInsertBinCopy)
                    meths.Enqueue(new SortMethInf("Двухпутевыми + двоичный поиск", new ArrSortDel(TwoWaysInserts.TwoWaysInsertBinCopy)));
            }
        }
    }
}
