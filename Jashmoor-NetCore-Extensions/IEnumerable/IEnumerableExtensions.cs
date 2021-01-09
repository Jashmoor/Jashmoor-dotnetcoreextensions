namespace Jashmoor_NetCore_Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class IEnumerableExtensions
    {
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
            => source.DistinctBy(keySelector, null);

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector, IEqualityComparer<TKey>? comparer)
        {
            _ = source ?? throw new ArgumentNullException(nameof(source));
            _ = keySelector ?? throw new ArgumentNullException(nameof(keySelector));

            return LoadDistinct();

            IEnumerable<TSource> LoadDistinct()
            {
                HashSet<TKey> knownKeys = new HashSet<TKey>(comparer);
                foreach (var element in source)
                {
                    if (knownKeys.Add(keySelector(element)))
                        yield return element;
                }
            }
        }

        public static T SelectRandom<T>(this IEnumerable<T> items)
        {
            var rand = new Random(Guid.NewGuid().GetHashCode());
            return items.SelectRandom(rand);
        }
        private static T SelectRandom<T>(this IEnumerable<T> items, Random rand)
        {
            if (items is ICollection<T> collection)
                return collection.ElementAt(rand.Next(collection.Count));

            int count = 1;
            T selected = default(T);
            foreach (T element in items)
            {
                if (rand.Next(count++) == 0)
                {
                    selected = element;
                }
            }
            return selected;
        }

    }
}
