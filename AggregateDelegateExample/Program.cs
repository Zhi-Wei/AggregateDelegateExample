﻿using AggregateDelegateExample.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AggregateDelegateExample
{
    internal class Program
    {
        private static void Main(string[] args)
        {
        }

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
        /// 輸出至畫面。
        /// </summary>
        /// <param name="collection">可列舉的集合。</param>
        private static void PrintOut(IEnumerable collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            foreach (var item in collection)
            {
                Console.WriteLine(item?.ToString());
            }
        }
    }
}