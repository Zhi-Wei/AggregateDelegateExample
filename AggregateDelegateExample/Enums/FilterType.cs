using System;

namespace AggregateDelegateExample.Enums
{
    /// <summary>
    /// 過濾器類型。
    /// </summary>
    [Flags]
    public enum FilterType
    {
        /// <summary>
        /// 無。
        /// </summary>
        None = 0,

        /// <summary>
        /// 奇數。
        /// </summary>
        Odd = 1,

        /// <summary>
        /// 偶數。
        /// </summary>
        Even = 2,

        /// <summary>
        /// 大於十。
        /// </summary>
        GreaterThanTen = 4,

        /// <summary>
        /// 小於十。
        /// </summary>
        LessThanTen = 8,

        /// <summary>
        /// 全部。
        /// </summary>
        All = Odd | Even | GreaterThanTen | LessThanTen
    }
}