using System;
using System.Collections.Generic;
using System.Drawing;

namespace Benchmark
{
    /// <summary>
    /// Класс, содержащий результаты выполнения теста производительности для
    /// метода сортировки
    /// </summary>
    public class SortMethResult
    {
        private string _name; //Имя метода
        private List<PointF> Results; //Список результатов метода

        /// <summary>
        /// Результаты тестирования метода сортировки
        /// </summary>
        /// <param name="name">Имя метода</param>
        public SortMethResult(string name)
        {
            this._name = name;
            Results = new List<PointF>();
        }

        /// <summary>
        /// Возвращает имя метода
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
        }

        /// <summary>
        /// Возвращает массив результатов
        /// </summary>
        /// <returns></returns>
        public PointF[] getResults()
        {
            return Results.ToArray();
        }

        /// <summary>
        /// Добавление очередного результат в список
        /// </summary>
        /// <param name="size">Размерность</param>
        /// <param name="time">Время</param>
        public void AddResult(int size, float time)
        {
            Results.Add(new PointF(size, time));
        }
    }
}
