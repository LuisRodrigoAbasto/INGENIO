using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace Abasto.Library.General
{
    public static partial class Extension
    {
        public static string ReplaceAll(this string value, string quitar, string nuevo)
        {
            if (!string.IsNullOrEmpty(value) && quitar != nuevo)
            {
                string result = value;
                try
                {
                    value = value.Trim();
                    if (!nuevo.Contains(quitar))
                    {
                        while (value.Contains(quitar))
                        {
                            value = value.Replace(quitar, nuevo);
                        }
                    }
                    else value.Replace(quitar, nuevo);
                    return value.Trim();
                }
                catch
                {
                    return result;
                }
            }
            else
            {
                return value;
            }
        }

        //Pasara solo los datos, colocara null a las clase de inyeccion
        public static T MapToObject<T>(this T value) where T : class
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            foreach (PropertyDescriptor prop in properties)
            {
                var propertyType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                var val = prop.GetValue(value);
                if (val == null) continue;
                if ((typeof(string) != propertyType) && propertyType.IsClass)
                {
                    prop.SetValue(value, null);
                }
                else if (propertyType.IsConstructedGenericType)
                {
                    prop.SetValue(value, null);
                }
            }
            return value;
        }
        public static DateTime TimeZone(this DateTime value, string timeZone)
        {
            value = TimeZoneInfo.ConvertTime(value, TimeZoneInfo.FindSystemTimeZoneById(Zone(timeZone)));
            return value;
        }
        private static string Zone(string timeZone)
        {
            if (timeZone.Length <= 2)
            {
                switch (timeZone.ToUpper())
                {
                    case "BO": timeZone = "SA Western Standard Time"; break;
                    case "MX": timeZone = "Central Standard Time (Mexico)"; break;
                    default: timeZone = "UTC"; break;
                }
            }
            return timeZone;
        }
        public static DataTable ToDataTable<T>(this IList<T> data) where T : class
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
            {
                var propertyType = prop.PropertyType;
                var underlyingType = Nullable.GetUnderlyingType(propertyType);
                if (underlyingType != null) propertyType = underlyingType;
                if (propertyType != typeof(string) && propertyType.IsClass)
                {
                    continue;
                }
                table.Columns.Add(prop.Name, propertyType);
            }
            var columna = table.Columns;
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (DataColumn prop in columna)
                {
                    var valor = properties[prop.ColumnName].GetValue(item);
                    if (valor != null) row[prop.ColumnName] = valor;
                }
                table.Rows.Add(row);
            }
            return table;
        }
    }
}
