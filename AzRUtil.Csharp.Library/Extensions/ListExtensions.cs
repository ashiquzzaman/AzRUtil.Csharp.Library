using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace AzRUtil.Csharp.Library.Extensions
{
    public static class ListExtensions
    {
        public static DataTable ToListToDataTable<TSource>(this IList<TSource> data)
        {
            var dataTable = new DataTable(typeof(TSource).Name);
            var props = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in props)
            {
                dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ??
                                                 prop.PropertyType);
            }

            foreach (TSource item in data)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }

        public static DataTable ToDataTable<TSource>(this IList<TSource> data, string tableName)
        {
            var dataTable = new DataTable(tableName);
            var props = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in props)
            {
                dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ??
                                                 prop.PropertyType);
            }

            foreach (TSource item in data)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }

        public static DataTable ToDataTable<TSource>(this IList<TSource> data)
        {
            var dataTable = new DataTable(typeof(TSource).Name);
            var props = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in props)
            {
                dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ??
                                                 prop.PropertyType);
            }


            foreach (TSource item in data)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;
        }

    }

}
