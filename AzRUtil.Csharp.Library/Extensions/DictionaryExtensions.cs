using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace AzRUtil.Csharp.Library.Extensions
{
    public static class DictionaryExtensions
    {

        public static object GetObject(this Dictionary<string, object> dict, Type type)
        {
            var obj = Activator.CreateInstance(type);

            foreach (var item in dict)
            {
                var propertyInfo = obj.GetType().GetProperty(item.Key);
                if (propertyInfo == null) continue;
                var underlyingType = Nullable.GetUnderlyingType(propertyInfo.PropertyType);
                if (underlyingType == null)
                {
                    propertyInfo.SetValue(obj, Convert.ChangeType(item.Value, propertyInfo.PropertyType), null);
                }

                propertyInfo.SetValue(obj,
                   item.Value != null ?
                   string.IsNullOrEmpty(item.Value.ToString())
                       ? null
                       : Convert.ChangeType(item.Value, underlyingType ?? propertyInfo.PropertyType) : null, null);
            }
            return obj;
        }
        public static T GetObject<T>(this Dictionary<string, object> dict)
        {
            return (T)GetObject(dict, typeof(T));
        }
        public static List<T> DictionaryToList<T>(this Dictionary<string, object> dictionary)
        {
            if (dictionary.Count == 0) return null;
            List<object> result = dictionary.GroupBy(item => item.Key.Substring(0, item.Key.IndexOf(".", StringComparison.Ordinal)))
                  .Select(group => group.Aggregate(Activator.CreateInstance(typeof(T)), (obj, item) =>
                  {
                      var propertyInfo = obj.GetType().GetProperty(item.Key.Substring(item.Key.IndexOf(".", StringComparison.Ordinal) + 1));
                      if (propertyInfo == null) return obj;
                      var underlyingType = Nullable.GetUnderlyingType(propertyInfo.PropertyType);
                      if (underlyingType == null)
                      {
                          propertyInfo.SetValue(obj, Convert.ChangeType(item.Value, propertyInfo.PropertyType), null);
                      }
                      propertyInfo.SetValue(obj,
                          string.IsNullOrEmpty(item.Value.ToString())
                              ? null
                              : Convert.ChangeType(item.Value, underlyingType ?? propertyInfo.PropertyType), null);

                      return obj;

                  })).ToList();
            return result.OfType<T>().ToList();
        }

        public static EnumerableRowCollection<Dictionary<string, object>> ToDictionary(this DataTable dt)
        {
            return dt.AsEnumerable()
                .Select(dr => dt.Columns.Cast<DataColumn>().ToDictionary(dc => dc.ColumnName, dc => dr[dc]));

            //return dt.AsEnumerable().ToDictionary<DataRow, dynamic, dynamic>(row => row.Field<string>(0),
            //                            row => row.Field<object>(1));
        }
    }
}
