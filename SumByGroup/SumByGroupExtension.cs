using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumByGroup
{
    public static class SumByGroupExtension
    {
        /// <summary>
        /// IEnumerable擴充方法, 以指定的每組數量將集合內的元素分組, 並將同組內的指定運算式(return int)加總後以集合回傳
        /// e.g. Items.SumByGroup(4, p => p.Revenue)
        /// </summary>
        /// <typeparam name="T">IEnumerable泛型型別</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="groupCount">每組數量, 需大於0</param>
        /// <param name="func">指定運算式</param>
        /// <returns>運算結果的集合</returns>
        public static IEnumerable<int> SumByGroup<T>(this IEnumerable<T> collection, int groupCount, Func<T, int> func)
        {
            if (groupCount <= 0)
            {
                throw new ArgumentOutOfRangeException("GroupCount should be bigger than zero.");
            }
            
            int totalCount = collection.Count();

            //20170110 註解原有寫法改用linq
            int index = 0;
            while (index < totalCount)
            {
                yield return collection.Skip(index).Take(groupCount).Sum(func);
                index += groupCount;
            }
            
            //int sum = 0;
            //for (int i = 0; i < totalCount; i++)
            //{                
            //    T item = collection.ElementAt(i);

            //    sum += func(item);                

            //    //每計算到每組數量上限或集合中的最後一個元素就回傳當下結果, 並歸零重新計算下一組
            //    if ((i + 1) % groupCount == 0 || i == totalCount - 1)
            //    {
            //        yield return sum;
            //        sum = 0;
            //    }
            //}            
        }
    }
}
