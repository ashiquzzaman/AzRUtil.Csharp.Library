using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace AzRUtil.Csharp.Library.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<ElementWithContext<T>>
          WithContext<T>(this IEnumerable<T> source)
        {
            T previous = default(T);
            T current = source.FirstOrDefault();

            foreach (T next in source.Union(new[] { default(T) }).Skip(1))
            {
                yield return new ElementWithContext<T>(current, previous, next);
                previous = current;
                current = next;
            }
        }

        public static DataTable CreateDataTable<T>(this IEnumerable<T> list)
        {
            Type type = typeof(T);
            var properties = type.GetProperties();


            DataTable dataTable = new DataTable();
            foreach (PropertyInfo info in properties)
            {
                dataTable.Columns.Add(new DataColumn(info.Name,
                    Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            }


            foreach (T entity in list)
            {
                object[] values = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(entity);
                }


                dataTable.Rows.Add(values);
            }


            return dataTable;
        }

        public static object[,] To2DArray<T>(this IEnumerable<T> lines, int number)
        {
            var array = new object[lines.Count(), number];
            var lineCounter = 0;
            lines.ForEach<T>(line =>
            {
                for (var i = 0; i < number; i++)
                {
                    array[lineCounter, i] = line;
                }
                lineCounter++;
            });
            return array;
        }

        public static object[,] To2DArray<T>(this IEnumerable<T> lines, params Func<T, object>[] lambdas)
        {
            var array = new object[lines.Count(), lambdas.Count()];
            var lineCounter = 0;
            lines.ForEach<T>(line =>
            {
                for (var i = 0; i < lambdas.Length; i++)
                {
                    array[lineCounter, i] = lambdas[i](line);
                }
                lineCounter++;
            });
            return array;
        }

        public static void ForEach<T>(this IEnumerable enumeration, Action<T> action)
        {
            foreach (T item in enumeration)
            {
                action(item);
            }
        }
        public static string StringArrayToString(this IEnumerable<string> arrayStrings, string separateWith = ",")
        {
            var str = String.Empty;
            if (arrayStrings != null)
            {
                str = arrayStrings.Aggregate(str, (current, s) => current + (s + separateWith));
                str = str.Remove(str.LastIndexOf(separateWith, StringComparison.Ordinal));
            }
            return str;
        }
        public static string DynamicArrayToString(this IEnumerable<dynamic> arrayStrings, string separateWith = ",")
        {
            if (arrayStrings != null)
            {
                var str = string.Join(separateWith, arrayStrings.ToArray());
                return str;
            }
            return string.Empty;
        }
        public static IEnumerable<T> FindSwitchItem<T>(this IEnumerable<T> items, Predicate<T> matchFilling)
        {
            if (items == null)
                throw new ArgumentNullException("items");
            if (matchFilling == null)
                throw new ArgumentNullException("matchFilling");

            return FindSandwichedItemImpl(items, matchFilling);
        }
        private static IEnumerable<T> FindSandwichedItemImpl<T>(IEnumerable<T> items, Predicate<T> matchFilling)
        {
            using (var iter = items.GetEnumerator())
            {
                T previous = default(T);
                while (iter.MoveNext())
                {
                    if (matchFilling(iter.Current))
                    {
                        yield return previous;
                        yield return iter.Current;
                        if (iter.MoveNext())
                            yield return iter.Current;
                        else
                            yield return default(T);
                        yield break;
                    }
                    previous = iter.Current;
                }
            }
            // If we get here nothing has been found so return three default values
            yield return default(T); // Previous
            yield return default(T); // Current
            yield return default(T); // Next
        }
        public static List<TEntity> SearchAllFields<TEntity>(this IEnumerable<TEntity> list, string searchItem)
        {
            var stringProperties = typeof(TEntity).GetProperties();

            var result = list.Where(item =>
                stringProperties.Any(prop =>
                    (prop.GetValue(item) == null
                    ? ""
                    : prop.GetValue(item).ToString().ToLower()).Contains(searchItem.ToLower())))
                    .ToList();

            return result;
        }

        public static List<TEntity> SearchAllFields<TEntity>(this IEnumerable<TEntity> list, string searchItem, List<string> columns)
        {
            if (string.IsNullOrWhiteSpace(searchItem))
            {
                return list.ToList();
            }
            var stringProperties = !string.IsNullOrWhiteSpace(searchItem) && columns != null && columns.Any()
                ? typeof(TEntity).GetProperties().Where(prop => columns.Contains(prop.Name))
                : typeof(TEntity).GetProperties();

            var result = list.Where(item =>
                stringProperties.Any(prop =>
                    (prop.GetValue(item) == null
                    ? ""
                    : prop.GetValue(item).ToString().ToLower()).Contains(searchItem.ToLower())))
                    .ToList();

            return result;
        }


        public static List<TEntity> SearchAllStringFields<TEntity>(this IEnumerable<TEntity> list, string searchItem)
        {
            var stringProperties =
                typeof(TEntity).GetProperties().Where(prop => prop.PropertyType == searchItem.GetType());

            var result = list.Where(item =>
                stringProperties.Any(prop =>
                    ((prop.GetValue(item, null) == null) ? "" : prop.GetValue(item, null).ToString().ToLower())
                    .Equals(searchItem.ToLower()))).ToList();

            return result;
        }

        public static IEnumerable<T> Distinct<T, TKey>(this IEnumerable<T> list, Func<T, TKey> lookup) where TKey : struct
        {
            return list.Distinct(new StructEqualityComparer<T, TKey>(lookup));
        }
        public static IEnumerable<string> ToCsv<T>(this IEnumerable<T> objectlist, string separator = ",", bool header = true)
        {
            var fields = typeof(T).GetFields();
            var properties = typeof(T).GetProperties();
            if (header)
            {
                yield return string.Join(separator, fields.Select(f => f.Name).Concat(properties.Select(p => p.Name)).ToArray());
            }
            foreach (var o in objectlist)
            {
                yield return string.Join(separator, fields.Select(f => (f.GetValue(o) ?? "").ToString())
                    .Concat(properties.Select(p => (p.GetValue(o, null) ?? "").ToString())).ToArray());
            }
        }
        public class ElementWithContext<T>
        {
            public T Previous { get; private set; }
            public T Next { get; private set; }
            public T Current { get; private set; }

            public ElementWithContext(T current, T previous, T next)
            {
                Current = current;
                Previous = previous;
                Next = next;
            }
        }

        class StructEqualityComparer<T, TKey> : IEqualityComparer<T> where TKey : struct
        {

            Func<T, TKey> lookup;

            public StructEqualityComparer(Func<T, TKey> lookup)
            {
                this.lookup = lookup;
            }

            public bool Equals(T x, T y)
            {
                return lookup(x).Equals(lookup(y));
            }

            public int GetHashCode(T obj)
            {
                return lookup(obj).GetHashCode();
            }
        }
    }

}
