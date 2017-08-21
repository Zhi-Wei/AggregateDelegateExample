using AggregateDelegateExample.Enums;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AggregateDelegateExample.Services.Tests
{
    /// <summary>
    /// 依照集合實作的範例服務測試類別。
    /// </summary>
    [TestClass()]
    public class ExampleServiceByCollectionTests
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
        private ExampleServiceByCollection GetSystemUnderTestInstance()
        {
            return new ExampleServiceByCollection();
        }

        #endregion -- 前置準備 --

        #region -- GetFilterDelegatePredicate --

        [TestMethod()]
        [TestCategory(nameof(ExampleServiceByCollection))]
        [TestProperty(nameof(ExampleServiceByCollection), "GetFilterDelegatePredicate")]
        public void GetFilterDelegatePredicate_當傳入參數filterType為0時_應無篩選sourceData()
        {
            // Arrange
            var filterType = FilterType.None;
            var expected = this._sourceData;
            var sut = this.GetSystemUnderTestInstance();

            // Act
            var predicate = sut.GetFilterDelegatePredicate(_funcConditions, filterType);
            var actual = this._sourceData.Where(predicate);

            // Assert
            actual.ShouldAllBeEquivalentTo(expected);
        }

        [TestMethod()]
        [TestCategory(nameof(ExampleServiceByCollection))]
        [TestProperty(nameof(ExampleServiceByCollection), "GetFilterDelegatePredicate")]
        public void GetFilterDelegatePredicate_當傳入參數filterType為1時_應篩選sourceData()
        {
            // Arrange
            var filterType = FilterType.Odd;
            var expected = this._sourceData.Where(x => x % 2 != 0);
            var sut = this.GetSystemUnderTestInstance();

            // Act
            var predicate = sut.GetFilterDelegatePredicate(_funcConditions, filterType);
            var actual = this._sourceData.Where(predicate);

            // Assert
            actual.ShouldAllBeEquivalentTo(expected);
        }

        [TestMethod()]
        [TestCategory(nameof(ExampleServiceByCollection))]
        [TestProperty(nameof(ExampleServiceByCollection), "GetFilterDelegatePredicate")]
        public void GetFilterDelegatePredicate_當傳入參數filterType為2時_應篩選sourceData()
        {
            // Arrange
            var filterType = FilterType.Even;
            var expected = this._sourceData.Where(x => x % 2 == 0);
            var sut = this.GetSystemUnderTestInstance();

            // Act
            var predicate = sut.GetFilterDelegatePredicate(_funcConditions, filterType);
            var actual = this._sourceData.Where(predicate);

            // Assert
            actual.ShouldAllBeEquivalentTo(expected);
        }

        [TestMethod()]
        [TestCategory(nameof(ExampleServiceByCollection))]
        [TestProperty(nameof(ExampleServiceByCollection), "GetFilterDelegatePredicate")]
        public void GetFilterDelegatePredicate_當傳入參數filterType為3時_應篩選sourceData()
        {
            // Arrange
            var filterType = FilterType.Odd | FilterType.Even;
            var expected = new int[] { };
            var sut = this.GetSystemUnderTestInstance();

            // Act
            var predicate = sut.GetFilterDelegatePredicate(_funcConditions, filterType);
            var actual = this._sourceData.Where(predicate);

            // Assert
            actual.ShouldAllBeEquivalentTo(expected);
        }

        [TestMethod()]
        [TestCategory(nameof(ExampleServiceByCollection))]
        [TestProperty(nameof(ExampleServiceByCollection), "GetFilterDelegatePredicate")]
        public void GetFilterDelegatePredicate_當傳入參數filterType為4時_應篩選sourceData()
        {
            // Arrange
            var filterType = FilterType.GreaterThanTen;
            var expected = this._sourceData.Where(x => x > 10);
            var sut = this.GetSystemUnderTestInstance();

            // Act
            var predicate = sut.GetFilterDelegatePredicate(_funcConditions, filterType);
            var actual = this._sourceData.Where(predicate);

            // Assert
            actual.ShouldAllBeEquivalentTo(expected);
        }

        [TestMethod()]
        [TestCategory(nameof(ExampleServiceByCollection))]
        [TestProperty(nameof(ExampleServiceByCollection), "GetFilterDelegatePredicate")]
        public void GetFilterDelegatePredicate_當傳入參數filterType為5時_應篩選sourceData()
        {
            // Arrange
            var filterType = FilterType.Odd | FilterType.GreaterThanTen;
            var expected = this._sourceData.Where(x => x % 2 != 0 && x > 10);
            var sut = this.GetSystemUnderTestInstance();

            // Act
            var predicate = sut.GetFilterDelegatePredicate(_funcConditions, filterType);
            var actual = this._sourceData.Where(predicate);

            // Assert
            actual.ShouldAllBeEquivalentTo(expected);
        }

        [TestMethod()]
        [TestCategory(nameof(ExampleServiceByCollection))]
        [TestProperty(nameof(ExampleServiceByCollection), "GetFilterDelegatePredicate")]
        public void GetFilterDelegatePredicate_當傳入參數filterType為6時_應篩選sourceData()
        {
            // Arrange
            var filterType = FilterType.Even | FilterType.GreaterThanTen;
            var expected = this._sourceData.Where(x => x % 2 == 0 && x > 10);
            var sut = this.GetSystemUnderTestInstance();

            // Act
            var predicate = sut.GetFilterDelegatePredicate(_funcConditions, filterType);
            var actual = this._sourceData.Where(predicate);

            // Assert
            actual.ShouldAllBeEquivalentTo(expected);
        }

        [TestMethod()]
        [TestCategory(nameof(ExampleServiceByCollection))]
        [TestProperty(nameof(ExampleServiceByCollection), "GetFilterDelegatePredicate")]
        public void GetFilterDelegatePredicate_當傳入參數filterType為7時_應篩選sourceData()
        {
            // Arrange
            var filterType = FilterType.Odd | FilterType.Even | FilterType.GreaterThanTen;
            var expected = new int[] { };
            var sut = this.GetSystemUnderTestInstance();

            // Act
            var predicate = sut.GetFilterDelegatePredicate(_funcConditions, filterType);
            var actual = this._sourceData.Where(predicate);

            // Assert
            actual.ShouldAllBeEquivalentTo(expected);
        }

        [TestMethod()]
        [TestCategory(nameof(ExampleServiceByCollection))]
        [TestProperty(nameof(ExampleServiceByCollection), "GetFilterDelegatePredicate")]
        public void GetFilterDelegatePredicate_當傳入參數filterType為8時_應篩選sourceData()
        {
            // Arrange
            var filterType = FilterType.LessThanTen;
            var expected = this._sourceData.Where(x => x < 10);
            var sut = this.GetSystemUnderTestInstance();

            // Act
            var predicate = sut.GetFilterDelegatePredicate(_funcConditions, filterType);
            var actual = this._sourceData.Where(predicate);

            // Assert
            actual.ShouldAllBeEquivalentTo(expected);
        }

        [TestMethod()]
        [TestCategory(nameof(ExampleServiceByCollection))]
        [TestProperty(nameof(ExampleServiceByCollection), "GetFilterDelegatePredicate")]
        public void GetFilterDelegatePredicate_當傳入參數filterType為9時_應篩選sourceData()
        {
            // Arrange
            var filterType = FilterType.Odd | FilterType.LessThanTen;
            var expected = this._sourceData.Where(x => x % 2 != 0 && x < 10);
            var sut = this.GetSystemUnderTestInstance();

            // Act
            var predicate = sut.GetFilterDelegatePredicate(_funcConditions, filterType);
            var actual = this._sourceData.Where(predicate);

            // Assert
            actual.ShouldAllBeEquivalentTo(expected);
        }

        [TestMethod()]
        [TestCategory(nameof(ExampleServiceByCollection))]
        [TestProperty(nameof(ExampleServiceByCollection), "GetFilterDelegatePredicate")]
        public void GetFilterDelegatePredicate_當傳入參數filterType為10時_應篩選sourceData()
        {
            // Arrange
            var filterType = FilterType.Even | FilterType.LessThanTen;
            var expected = this._sourceData.Where(x => x % 2 == 0 && x < 10);
            var sut = this.GetSystemUnderTestInstance();

            // Act
            var predicate = sut.GetFilterDelegatePredicate(_funcConditions, filterType);
            var actual = this._sourceData.Where(predicate);

            // Assert
            actual.ShouldAllBeEquivalentTo(expected);
        }

        [TestMethod()]
        [TestCategory(nameof(ExampleServiceByCollection))]
        [TestProperty(nameof(ExampleServiceByCollection), "GetFilterDelegatePredicate")]
        public void GetFilterDelegatePredicate_當傳入參數filterType為11時_應篩選sourceData()
        {
            // Arrange
            var filterType = FilterType.Odd | FilterType.Even | FilterType.LessThanTen;
            var expected = new int[] { };
            var sut = this.GetSystemUnderTestInstance();

            // Act
            var predicate = sut.GetFilterDelegatePredicate(_funcConditions, filterType);
            var actual = this._sourceData.Where(predicate);

            // Assert
            actual.ShouldAllBeEquivalentTo(expected);
        }

        [TestMethod()]
        [TestCategory(nameof(ExampleServiceByCollection))]
        [TestProperty(nameof(ExampleServiceByCollection), "GetFilterDelegatePredicate")]
        public void GetFilterDelegatePredicate_當傳入參數filterType為12時_應篩選sourceData()
        {
            // Arrange
            var filterType = FilterType.GreaterThanTen | FilterType.LessThanTen;
            var expected = new int[] { };
            var sut = this.GetSystemUnderTestInstance();

            // Act
            var predicate = sut.GetFilterDelegatePredicate(_funcConditions, filterType);
            var actual = this._sourceData.Where(predicate);

            // Assert
            actual.ShouldAllBeEquivalentTo(expected);
        }

        [TestMethod()]
        [TestCategory(nameof(ExampleServiceByCollection))]
        [TestProperty(nameof(ExampleServiceByCollection), "GetFilterDelegatePredicate")]
        public void GetFilterDelegatePredicate_當傳入參數filterType為13時_應篩選sourceData()
        {
            // Arrange
            var filterType = FilterType.Odd | FilterType.GreaterThanTen | FilterType.LessThanTen;
            var expected = new int[] { };
            var sut = this.GetSystemUnderTestInstance();

            // Act
            var predicate = sut.GetFilterDelegatePredicate(_funcConditions, filterType);
            var actual = this._sourceData.Where(predicate);

            // Assert
            actual.ShouldAllBeEquivalentTo(expected);
        }

        [TestMethod()]
        [TestCategory(nameof(ExampleServiceByCollection))]
        [TestProperty(nameof(ExampleServiceByCollection), "GetFilterDelegatePredicate")]
        public void GetFilterDelegatePredicate_當傳入參數filterType為14時_應篩選sourceData()
        {
            // Arrange
            var filterType = FilterType.Even | FilterType.GreaterThanTen | FilterType.LessThanTen;
            var expected = new int[] { };
            var sut = this.GetSystemUnderTestInstance();

            // Act
            var predicate = sut.GetFilterDelegatePredicate(_funcConditions, filterType);
            var actual = this._sourceData.Where(predicate);

            // Assert
            actual.ShouldAllBeEquivalentTo(expected);
        }

        [TestMethod()]
        [TestCategory(nameof(ExampleServiceByCollection))]
        [TestProperty(nameof(ExampleServiceByCollection), "GetFilterDelegatePredicate")]
        public void GetFilterDelegatePredicate_當傳入參數filterType為15時_應篩選sourceData()
        {
            // Arrange
            var filterType = FilterType.Odd | FilterType.Even | FilterType.GreaterThanTen | FilterType.LessThanTen;
            var expected = new int[] { };
            var sut = this.GetSystemUnderTestInstance();

            // Act
            var predicate = sut.GetFilterDelegatePredicate(_funcConditions, filterType);
            var actual = this._sourceData.Where(predicate);

            // Assert
            actual.ShouldAllBeEquivalentTo(expected);
        }

        #endregion -- GetFilterDelegatePredicate --

        #region -- GetFilterExpressionPredicate --

        [TestMethod()]
        [TestCategory(nameof(ExampleServiceByCollection))]
        [TestProperty(nameof(ExampleServiceByCollection), "GetFilterExpressionPredicate")]
        public void GetFilterExpressionPredicate_當傳入參數filterType為0時_應無篩選sourceData()
        {
            // Arrange
            var filterType = FilterType.None;
            var expected = this._sourceData;
            var sut = this.GetSystemUnderTestInstance();

            // Act
            var predicate = sut.GetFilterExpressionPredicate(_expressionConditions, filterType);
            var actual = this._sourceData.Where(predicate.Compile());

            // Assert
            actual.ShouldAllBeEquivalentTo(expected);
        }

        [TestMethod()]
        [TestCategory(nameof(ExampleServiceByCollection))]
        [TestProperty(nameof(ExampleServiceByCollection), "GetFilterExpressionPredicate")]
        public void GetFilterExpressionPredicate_當傳入參數filterType為1時_應篩選sourceData()
        {
            // Arrange
            var filterType = FilterType.Odd;
            var expected = this._sourceData.Where(x => x % 2 != 0);
            var sut = this.GetSystemUnderTestInstance();

            // Act
            var predicate = sut.GetFilterExpressionPredicate(_expressionConditions, filterType);
            var actual = this._sourceData.Where(predicate.Compile());

            // Assert
            actual.ShouldAllBeEquivalentTo(expected);
        }

        [TestMethod()]
        [TestCategory(nameof(ExampleServiceByCollection))]
        [TestProperty(nameof(ExampleServiceByCollection), "GetFilterExpressionPredicate")]
        public void GetFilterExpressionPredicate_當傳入參數filterType為2時_應篩選sourceData()
        {
            // Arrange
            var filterType = FilterType.Even;
            var expected = this._sourceData.Where(x => x % 2 == 0);
            var sut = this.GetSystemUnderTestInstance();

            // Act
            var predicate = sut.GetFilterExpressionPredicate(_expressionConditions, filterType);
            var actual = this._sourceData.Where(predicate.Compile());

            // Assert
            actual.ShouldAllBeEquivalentTo(expected);
        }

        [TestMethod()]
        [TestCategory(nameof(ExampleServiceByCollection))]
        [TestProperty(nameof(ExampleServiceByCollection), "GetFilterExpressionPredicate")]
        public void GetFilterExpressionPredicate_當傳入參數filterType為3時_應篩選sourceData()
        {
            // Arrange
            var filterType = FilterType.Odd | FilterType.Even;
            var expected = new int[] { };
            var sut = this.GetSystemUnderTestInstance();

            // Act
            var predicate = sut.GetFilterExpressionPredicate(_expressionConditions, filterType);
            var actual = this._sourceData.Where(predicate.Compile());

            // Assert
            actual.ShouldAllBeEquivalentTo(expected);
        }

        [TestMethod()]
        [TestCategory(nameof(ExampleServiceByCollection))]
        [TestProperty(nameof(ExampleServiceByCollection), "GetFilterExpressionPredicate")]
        public void GetFilterExpressionPredicate_當傳入參數filterType為4時_應篩選sourceData()
        {
            // Arrange
            var filterType = FilterType.GreaterThanTen;
            var expected = this._sourceData.Where(x => x > 10);
            var sut = this.GetSystemUnderTestInstance();

            // Act
            var predicate = sut.GetFilterExpressionPredicate(_expressionConditions, filterType);
            var actual = this._sourceData.Where(predicate.Compile());

            // Assert
            actual.ShouldAllBeEquivalentTo(expected);
        }

        [TestMethod()]
        [TestCategory(nameof(ExampleServiceByCollection))]
        [TestProperty(nameof(ExampleServiceByCollection), "GetFilterExpressionPredicate")]
        public void GetFilterExpressionPredicate_當傳入參數filterType為5時_應篩選sourceData()
        {
            // Arrange
            var filterType = FilterType.Odd | FilterType.GreaterThanTen;
            var expected = this._sourceData.Where(x => x % 2 != 0 && x > 10);
            var sut = this.GetSystemUnderTestInstance();

            // Act
            var predicate = sut.GetFilterExpressionPredicate(_expressionConditions, filterType);
            var actual = this._sourceData.Where(predicate.Compile());

            // Assert
            actual.ShouldAllBeEquivalentTo(expected);
        }

        [TestMethod()]
        [TestCategory(nameof(ExampleServiceByCollection))]
        [TestProperty(nameof(ExampleServiceByCollection), "GetFilterExpressionPredicate")]
        public void GetFilterExpressionPredicate_當傳入參數filterType為6時_應篩選sourceData()
        {
            // Arrange
            var filterType = FilterType.Even | FilterType.GreaterThanTen;
            var expected = this._sourceData.Where(x => x % 2 == 0 && x > 10);
            var sut = this.GetSystemUnderTestInstance();

            // Act
            var predicate = sut.GetFilterExpressionPredicate(_expressionConditions, filterType);
            var actual = this._sourceData.Where(predicate.Compile());

            // Assert
            actual.ShouldAllBeEquivalentTo(expected);
        }

        [TestMethod()]
        [TestCategory(nameof(ExampleServiceByCollection))]
        [TestProperty(nameof(ExampleServiceByCollection), "GetFilterExpressionPredicate")]
        public void GetFilterExpressionPredicate_當傳入參數filterType為7時_應篩選sourceData()
        {
            // Arrange
            var filterType = FilterType.Odd | FilterType.Even | FilterType.GreaterThanTen;
            var expected = new int[] { };
            var sut = this.GetSystemUnderTestInstance();

            // Act
            var predicate = sut.GetFilterExpressionPredicate(_expressionConditions, filterType);
            var actual = this._sourceData.Where(predicate.Compile());

            // Assert
            actual.ShouldAllBeEquivalentTo(expected);
        }

        [TestMethod()]
        [TestCategory(nameof(ExampleServiceByCollection))]
        [TestProperty(nameof(ExampleServiceByCollection), "GetFilterExpressionPredicate")]
        public void GetFilterExpressionPredicate_當傳入參數filterType為8時_應篩選sourceData()
        {
            // Arrange
            var filterType = FilterType.LessThanTen;
            var expected = this._sourceData.Where(x => x < 10);
            var sut = this.GetSystemUnderTestInstance();

            // Act
            var predicate = sut.GetFilterExpressionPredicate(_expressionConditions, filterType);
            var actual = this._sourceData.Where(predicate.Compile());

            // Assert
            actual.ShouldAllBeEquivalentTo(expected);
        }

        [TestMethod()]
        [TestCategory(nameof(ExampleServiceByCollection))]
        [TestProperty(nameof(ExampleServiceByCollection), "GetFilterExpressionPredicate")]
        public void GetFilterExpressionPredicate_當傳入參數filterType為9時_應篩選sourceData()
        {
            // Arrange
            var filterType = FilterType.Odd | FilterType.LessThanTen;
            var expected = this._sourceData.Where(x => x % 2 != 0 && x < 10);
            var sut = this.GetSystemUnderTestInstance();

            // Act
            var predicate = sut.GetFilterExpressionPredicate(_expressionConditions, filterType);
            var actual = this._sourceData.Where(predicate.Compile());

            // Assert
            actual.ShouldAllBeEquivalentTo(expected);
        }

        [TestMethod()]
        [TestCategory(nameof(ExampleServiceByCollection))]
        [TestProperty(nameof(ExampleServiceByCollection), "GetFilterExpressionPredicate")]
        public void GetFilterExpressionPredicate_當傳入參數filterType為10時_應篩選sourceData()
        {
            // Arrange
            var filterType = FilterType.Even | FilterType.LessThanTen;
            var expected = this._sourceData.Where(x => x % 2 == 0 && x < 10);
            var sut = this.GetSystemUnderTestInstance();

            // Act
            var predicate = sut.GetFilterExpressionPredicate(_expressionConditions, filterType);
            var actual = this._sourceData.Where(predicate.Compile());

            // Assert
            actual.ShouldAllBeEquivalentTo(expected);
        }

        [TestMethod()]
        [TestCategory(nameof(ExampleServiceByCollection))]
        [TestProperty(nameof(ExampleServiceByCollection), "GetFilterExpressionPredicate")]
        public void GetFilterExpressionPredicate_當傳入參數filterType為11時_應篩選sourceData()
        {
            // Arrange
            var filterType = FilterType.Odd | FilterType.Even | FilterType.LessThanTen;
            var expected = new int[] { };
            var sut = this.GetSystemUnderTestInstance();

            // Act
            var predicate = sut.GetFilterExpressionPredicate(_expressionConditions, filterType);
            var actual = this._sourceData.Where(predicate.Compile());

            // Assert
            actual.ShouldAllBeEquivalentTo(expected);
        }

        [TestMethod()]
        [TestCategory(nameof(ExampleServiceByCollection))]
        [TestProperty(nameof(ExampleServiceByCollection), "GetFilterExpressionPredicate")]
        public void GetFilterExpressionPredicate_當傳入參數filterType為12時_應篩選sourceData()
        {
            // Arrange
            var filterType = FilterType.GreaterThanTen | FilterType.LessThanTen;
            var expected = new int[] { };
            var sut = this.GetSystemUnderTestInstance();

            // Act
            var predicate = sut.GetFilterExpressionPredicate(_expressionConditions, filterType);
            var actual = this._sourceData.Where(predicate.Compile());

            // Assert
            actual.ShouldAllBeEquivalentTo(expected);
        }

        [TestMethod()]
        [TestCategory(nameof(ExampleServiceByCollection))]
        [TestProperty(nameof(ExampleServiceByCollection), "GetFilterExpressionPredicate")]
        public void GetFilterExpressionPredicate_當傳入參數filterType為13時_應篩選sourceData()
        {
            // Arrange
            var filterType = FilterType.Odd | FilterType.GreaterThanTen | FilterType.LessThanTen;
            var expected = new int[] { };
            var sut = this.GetSystemUnderTestInstance();

            // Act
            var predicate = sut.GetFilterExpressionPredicate(_expressionConditions, filterType);
            var actual = this._sourceData.Where(predicate.Compile());

            // Assert
            actual.ShouldAllBeEquivalentTo(expected);
        }

        [TestMethod()]
        [TestCategory(nameof(ExampleServiceByCollection))]
        [TestProperty(nameof(ExampleServiceByCollection), "GetFilterExpressionPredicate")]
        public void GetFilterExpressionPredicate_當傳入參數filterType為14時_應篩選sourceData()
        {
            // Arrange
            var filterType = FilterType.Even | FilterType.GreaterThanTen | FilterType.LessThanTen;
            var expected = new int[] { };
            var sut = this.GetSystemUnderTestInstance();

            // Act
            var predicate = sut.GetFilterExpressionPredicate(_expressionConditions, filterType);
            var actual = this._sourceData.Where(predicate.Compile());

            // Assert
            actual.ShouldAllBeEquivalentTo(expected);
        }

        [TestMethod()]
        [TestCategory(nameof(ExampleServiceByCollection))]
        [TestProperty(nameof(ExampleServiceByCollection), "GetFilterExpressionPredicate")]
        public void GetFilterExpressionPredicate_當傳入參數filterType為15時_應篩選sourceData()
        {
            // Arrange
            var filterType = FilterType.Odd | FilterType.Even | FilterType.GreaterThanTen | FilterType.LessThanTen;
            var expected = new int[] { };
            var sut = this.GetSystemUnderTestInstance();

            // Act
            var predicate = sut.GetFilterExpressionPredicate(_expressionConditions, filterType);
            var actual = this._sourceData.Where(predicate.Compile());

            // Assert
            actual.ShouldAllBeEquivalentTo(expected);
        }

        #endregion -- GetFilterExpressionPredicate --
    }
}