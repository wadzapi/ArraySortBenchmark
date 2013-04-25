using System;
using System.Collections.Generic;

namespace Benchmark
{
    #region Класс делегата метода сортировки
    /// <summary>
    /// Класс делегата для методов сортировки
    /// </summary>
    /// <param name="Arr">Сортируемый массив</param>
    /// <param name="Increase">В порядке возрастания/убывания </param>
    public delegate void ArrSortDel(ref int[] Arr, bool Increase);
    #endregion

    /// <summary>
    /// Класс, содержащий информацию о вызываемом (тестируемом) методе
    /// </summary>
    public class SortMethInf
    { 
        protected string _name;//имя метода
        public ArrSortDel deleg;//делегат метода


        public SortMethInf(string Name, ArrSortDel del)
            
        {
            this._name = Name;
            this.deleg = del;
        }

        /// <summary>
        /// Возваращает или задает имя метода
        /// </summary>
        public string MethodName
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
    }
}
