using AggregateDelegateExample.Enums;
using AggregateDelegateExample.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AggregateDelegateExample.Services
{
    /// <summary>
    /// 依照委派實作的範例服務。
    /// </summary>
    /// <seealso cref="AggregateDelegateExample.Services.Interfaces.IAggregateDelegateExampleService" />
    public class ExampleServiceByDelegate : IAggregateDelegateExampleService
    {
        /// <summary>
        /// 取得委派的篩選判斷條件。
        /// </summary>
        /// <param name="conditions">條件集合。</param>
        /// <param name="filterType">篩選類型。</param>
        /// <returns>
        /// 委派的篩選判斷條件。
        /// </returns>
        public Func<int, bool> GetFilterDelegatePredicate(
            IEnumerable<KeyValuePair<Func<FilterType, bool>, Func<int, bool>>> conditions,
            FilterType filterType)
        {
            Func<int, bool> func = null;

            foreach (var condition in conditions)
            {
                if (condition.Key(filterType))
                {
                    func += condition.Value;
                }
            }

            if (func == null)
            {
                return number => true;
            }

            var predicate = func.GetInvocationList().Cast<Func<int, bool>>()
                .Aggregate((current, next) =>
                    number => current(number) && next(number));

            return predicate;
        }

        /// <summary>
        /// 取得運算式樹狀架構的篩選判斷條件。
        /// </summary>
        /// <param name="conditions">條件集合。</param>
        /// <param name="filterType">篩選類型。</param>
        /// <returns>
        /// 運算式樹狀架構的篩選判斷條件。
        /// </returns>
        public Expression<Func<int, bool>> GetFilterExpressionPredicate(
            IEnumerable<KeyValuePair<Func<FilterType, bool>, Expression<Func<int, bool>>>> conditions,
            FilterType filterType)
        {
            Func<int, bool> func = null;

            foreach (var condition in conditions)
            {
                if (condition.Key(filterType))
                {
                    func += condition.Value.Compile();
                }
            }

            if (func == null)
            {
                return number => true;
            }

            var predicate = func.GetInvocationList().Cast<Func<int, bool>>()
                .Aggregate((current, next) =>
                    number => current(number) && next(number));

            Expression<Func<int, bool>> result = number => predicate(number);
            return result;
        }
    }
}