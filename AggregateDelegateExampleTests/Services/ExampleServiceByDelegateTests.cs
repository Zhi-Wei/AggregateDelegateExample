using AggregateDelegateExample.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AggregateDelegateExample.Services.Tests
{
    /// <summary>
    /// 依照委派實作的範例服務的測試類別。
    /// </summary>
    [TestClass()]
    public class ExampleServiceByDelegateTests
    {
        #region -- 前置準備 --

        /// <summary>
        /// 來源資料。
        /// </summary>
        private IReadOnlyCollection<int> _sourceData =
            Enumerable.Range(1, 20).ToList().AsReadOnly();

        /// <summary>
        /// 委派篩選條件。
        /// </summary>
        private static IEnumerable<KeyValuePair<Func<FilterType, bool>, Func<int, bool>>>
            _funcConditions =
            new List<KeyValuePair<Func<FilterType, bool>, Func<int, bool>>>(4)
            {
                new KeyValuePair<Func<FilterType, bool>, Func<int, bool>>
                (
                    filter => (filter & FilterType.Odd) == FilterType.Odd,
                    number => number % 2 != 0
                ),

                new KeyValuePair<Func<FilterType, bool>, Func<int, bool>>
                (
                    filter => (filter & FilterType.Even) == FilterType.Even,
                    number => number % 2 == 0
                ),

                new KeyValuePair<Func<FilterType, bool>, Func<int, bool>>
                (
                    filter => (filter & FilterType.GreaterThanTen) == FilterType.GreaterThanTen,
                    number => number > 10
                ),

                new KeyValuePair<Func<FilterType, bool>, Func<int, bool>>
                (
                    filter => (filter & FilterType.LessThanTen) == FilterType.LessThanTen,
                    number => number < 10
                )
            };

        /// <summary>
        /// 運算式樹狀架構篩選條件。
        /// </summary>
        private static IEnumerable<KeyValuePair<Func<FilterType, bool>, Expression<Func<int, bool>>>>
            _expressionConditions =
            new List<KeyValuePair<Func<FilterType, bool>, Expression<Func<int, bool>>>>(4)
            {
                new KeyValuePair<Func<FilterType, bool>, Expression<Func<int, bool>>>
                (
                    filter => (filter & FilterType.Odd) == FilterType.Odd,
                    number => number % 2 != 0
                ),

                new KeyValuePair<Func<FilterType, bool>, Expression<Func<int, bool>>>
                (
                    filter => (filter & FilterType.Even) == FilterType.Even,
                    number => number % 2 == 0
                ),

                new KeyValuePair<Func<FilterType, bool>, Expression<Func<int, bool>>>
                (
                    filter => (filter & FilterType.GreaterThanTen) == FilterType.GreaterThanTen,
                    number => number > 10
                ),

                new KeyValuePair<Func<FilterType, bool>, Expression<Func<int, bool>>>
                (
                    filter => (filter & FilterType.LessThanTen) == FilterType.LessThanTen,
                    number => number < 10
                )
            };

        /// <summary>
        /// 取得系統測試實體。
        /// </summary>
        /// <returns></returns>
        private ExampleServiceByDelegate GetSystemUnderTestInstance()
        {
            return new ExampleServiceByDelegate();
        }

        #endregion -- 前置準備 --
    }
}