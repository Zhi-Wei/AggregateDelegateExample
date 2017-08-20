using AggregateDelegateExample.Enums;
using AggregateDelegateExample.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AggregateDelegateExample
{
    /// <summary>
    /// 程式類別。
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// 主程式。
        /// </summary>
        /// <param name="args">參數集合。</param>
        private static void Main(string[] args)
        {
            var filterType = FilterType.Odd | FilterType.GreaterThanTen;

            var exampleServiceByCollection = new ExampleServiceByCollection();
            var funcPredicateByCollection =
                exampleServiceByCollection.GetFilterDelegatePredicate(_funcConditions, filterType);
            var expressionPredicateByCollection =
                exampleServiceByCollection.GetFilterExpressionPredicate(_expressionConditions, filterType);

            var exampleServiceByDelegate = new ExampleServiceByDelegate();
            var funcPredicateByDelegate =
                exampleServiceByDelegate.GetFilterDelegatePredicate(_funcConditions, filterType);
            var expressionPredicateByDelegate =
                exampleServiceByDelegate.GetFilterExpressionPredicate(_expressionConditions, filterType);

            var source = Enumerable.Range(1, 20);

            Console.WriteLine("Func Filter Predicate By Collection:");
            var funcResultByCollection = source.Where(funcPredicateByCollection);
            PrintOut(funcResultByCollection);

            Console.WriteLine("Func Filter Predicate By Delegate:");
            var funcResultByDelegate = source.Where(funcPredicateByDelegate);
            PrintOut(funcResultByDelegate);

            Console.WriteLine("====================");

            Console.WriteLine("Expression Filter Predicate By Collection:");
            var expressionResultByCollection = source.Where(expressionPredicateByCollection.Compile());
            PrintOut(expressionResultByCollection);

            Console.WriteLine("Expression Filter Predicate By Delegate:");
            var expressionResultByDelegate = source.Where(expressionPredicateByDelegate.Compile());
            PrintOut(expressionResultByDelegate);
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