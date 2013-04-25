using System;
using System.Collections.Generic;

namespace Benchmark
{
    public class SortTestResult
    {
        private string TestName;
        private List<SortMethResult> _results;//Результаты выполнения тестов для методов сортировки

        public SortTestResult(string TestName)
        {
            this.TestName = TestName;
            _results = new List<SortMethResult>();
        }

        public void AddMethResult(SortMethResult result)
        {
            _results.Add(result);
        }


        /// <summary>
        /// Возращает результаты выполнения методов сортировки
        /// </summary>
        /// <returns>Резульататы методов сортировки</returns>
        public SortMethResult[] getResults()
        {
            return _results.ToArray();
        }

        public string Name
        {
            get
            {
                return TestName;
            }
        }
    
    }
}
