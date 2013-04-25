using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Benchmark
{
    /// <summary>
    /// Абстрактный родительский класс для проведения сравнительных испытаний эффективности 
    /// различных методов, связанных с массивами целых чисел int32
    /// </summary>
    public abstract class ArrayBenchmark
    {
        protected static System.Diagnostics.Stopwatch timer;//Таймер для замера времени испытаний
        protected static Random rnd = new Random();
        protected int numIter = 2; //Число итерраций замеров
        protected float AVGTime; //Среднее время выполнения метода
        protected int[] SourceArray;//Исходный тестовый массив
        protected TestLog Log; //Текстовый лог
        protected delegate void benchDel();//Класс делегата

        protected ArrayBenchmark(int SourceLenght)
        {
            timer = new System.Diagnostics.Stopwatch();
            SourceArray = new int[SourceLenght];
            newRandomSource(SourceLenght);
        }

        /// <summary>
        /// Задает или возвращает число итерраций замеров
        /// </summary>
        public abstract int NumIterr
        {
            get;
            set;
        }

        public abstract void Analize();//Метод запуска тестовых испытаний

        protected abstract void newRandomSource(int sourceLength); //Создание тестового массива новой размерности и заполнение случайными числами

    }
}
