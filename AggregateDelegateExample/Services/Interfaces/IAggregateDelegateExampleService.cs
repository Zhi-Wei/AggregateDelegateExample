using AggregateDelegateExample.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AggregateDelegateExample.Services.Interfaces
{
    /// <summary>
    /// 定義彙總委派範例服務的方法。
    /// </summary>
    public interface IAggregateDelegateExampleService
    {
        /// <summary>
        /// 取得委派的篩選判斷條件。
        /// </summary>
        /// <param name="conditions">條件集合。</param>
        /// <param name="filterType">篩選類型。</param>
        /// <returns>委派的篩選判斷條件。</returns>
        Func<int, bool> GetFilterDelegatePredicate(
            IEnumerable<KeyValuePair<Func<FilterType, bool>, Func<int, bool>>> conditions,
            FilterType filterType);

        /// <summary>
        /// 取得運算式樹狀架構的篩選判斷條件。
        /// </summary>
        /// <param name="conditions">條件集合。</param>
        /// <param name="filterType">篩選類型。</param>
        /// <returns>運算式樹狀架構的篩選判斷條件。</returns>
        Expression<Func<int, bool>> GetFilterExpressionPredicate(
            IEnumerable<KeyValuePair<Func<FilterType, bool>, Expression<Func<int, bool>>>> conditions,
            FilterType filterType);
    }
}