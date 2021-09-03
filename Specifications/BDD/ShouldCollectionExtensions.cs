using System.Collections;

namespace Aksio.BDD
{
    /// <summary>
    /// Holds extension methods for fluent "Should*" assertions related to collections.
    /// </summary>
    public static class ShouldCollectionExtensions
    {
        /// <summary>
        /// Assert that a collection only contains the expected elements.
        /// </summary>
        /// <param name="collection">Collection to assert.</param>
        /// <param name="expected">Expected values.</param>
        /// <typeparam name="T">Type of element.</typeparam>
        public static void ShouldContainOnly<T>(this IEnumerable<T> collection, IEnumerable<T> expected)
        {
            Assert.Empty(collection.Select(_ => !expected.Contains(_)));
        }

        /// <summary>
        /// Assert that a collection only contains the expected elements - based on params.
        /// </summary>
        /// <param name="collection">Collection to assert.</param>
        /// <param name="expected">Expected values.</param>
        /// <typeparam name="T">Type of element.</typeparam>
        public static void ShouldContainOnly<T>(this IEnumerable<T> collection, params T[] expected)
        {
            collection.ShouldContainOnly(expected as IEnumerable<T>);
        }

        /// <summary>
        /// Assert that a collection contains all the expected elements.
        /// </summary>
        /// <param name="collection">Collection to assert.</param>
        /// <param name="expected">Expected elements.</param>
        /// <typeparam name="T">Type of element.</typeparam>
        public static void ShouldContain<T>(this IEnumerable<T> collection, IEnumerable<T> expected)
        {
            foreach (var item in expected)
            {
                Assert.Contains(item, collection);
            }
        }

        /// <summary>
        /// Assert that a collection contains all the expected elements - based on params.
        /// </summary>
        /// <param name="collection">Collection to assert.</param>
        /// <param name="expected">Expected elements as params.</param>
        /// <typeparam name="T">Type of element.</typeparam>
        public static void ShouldContain<T>(this IEnumerable<T> collection, params T[] expected)
        {
            collection.ShouldContain(expected as IEnumerable<T>);
        }

        /// <summary>
        /// Assert that a dictionary contains a specific key.
        /// </summary>
        /// <param name="actual">Dictionary to assert.</param>
        /// <param name="expected">Expected key.</param>
        /// <typeparam name="TKey">Type of key.</typeparam>
        /// <typeparam name="TValue">Type of value.</typeparam>
        public static void ShouldContain<TKey, TValue>(this IDictionary<TKey, TValue> actual, TKey expected)
        {
            Assert.Contains(expected, actual);
        }

        /// <summary>
        /// Assert that a collection contains specific element(s) based on a predicate filter.
        /// </summary>
        /// <param name="collection">Collection to assert.</param>
        /// <param name="filter">Filter to apply.</param>
        /// <typeparam name="T">Type of element.</typeparam>
        public static void ShouldContain<T>(this IEnumerable<T> collection, Predicate<T> filter)
        {
            Assert.Contains(collection, filter);
        }

        /// <summary>
        /// Assert that a collection contains a specific element.
        /// </summary>
        /// <param name="collection">Collection to assert.</param>
        /// <param name="expected">Expected element.</param>
        /// <typeparam name="T">Type of element</typeparam>
        public static void ShouldContain<T>(this IEnumerable<T> collection, T expected)
        {
            Assert.Contains(expected, collection);
        }

        /// <summary>
        /// Assert that a dictionary does not contain a specific key.
        /// </summary>
        /// <param name="actual">Dictionary to assert.</param>
        /// <param name="expected">Not expected key.</param>
        /// <typeparam name="TKey">Type of key.</typeparam>
        /// <typeparam name="TValue">Type of value.</typeparam>
        public static void ShouldNotContain<TKey, TValue>(this IDictionary<TKey, TValue> actual, TKey expected)
        {
            Assert.DoesNotContain(expected, actual);
        }

        /// <summary>
        /// Assert that a collection does not contain specific element(s) based on a predicate filter.
        /// </summary>
        /// <param name="collection">Collection to assert.</param>
        /// <param name="filter">Filter to apply.</param>
        /// <typeparam name="T">Type of element.</typeparam>
        public static void ShouldNotContain<T>(this IEnumerable<T> collection, Predicate<T> filter)
        {
            Assert.DoesNotContain(collection, filter);
        }

        /// <summary>
        /// Assert that a collection does not contain a specific element.
        /// </summary>
        /// <param name="collection">Collection to assert.</param>
        /// <param name="expected">Expected element.</param>
        /// <typeparam name="T">Type of element</typeparam>
        public static void ShouldNotContain<T>(this IEnumerable<T> collection, T expected)
        {
            Assert.DoesNotContain(expected, collection);
        }

        /// <summary>
        /// Assert that a collection is empty.
        /// </summary>
        /// <param name="collection">Collection to assert.</param>
        public static void ShouldBeEmpty(this IEnumerable collection)
        {
            Assert.Empty(collection);
        }

        /// <summary>
        /// Assert that a collection is not empty.
        /// </summary>
        /// <param name="collection">Collection to assert.</param>
        public static void ShouldNotBeEmpty(this IEnumerable collection)
        {
            Assert.NotEmpty(collection);
        }

        /// <summary>
        /// Assert that a collection has a single item.
        /// </summary>
        /// <param name="collection">Collection to assert.</param>
        public static void ShouldContainSingleItem(this IEnumerable collection)
        {
            Assert.Single(collection);
        }
    }
}
