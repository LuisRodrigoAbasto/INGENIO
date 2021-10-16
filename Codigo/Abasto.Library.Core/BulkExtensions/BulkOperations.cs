using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Abasto.Library.Core.BulkExtensions
{
    internal static partial class BulkOperations
    {///column Imput es true=a las columnas de entrada, false=ignorar columnas de entradas
        public static async Task BulkInsertAsync<T>(this DbContext context, IList<T> entities, string table, bool columnImput = true, params string[] column) where T : class
        {
            if (!entities.Take(1).Any()) return;
            DbConnection connection = context.Database.GetDbConnection();
            var transaction = context.Database.CurrentTransaction;
            var config = new SqlBulkCopyOptions();
            using var sqlBulkCopy = GetSqlBulkCopy((SqlConnection)connection, transaction, config);
            //config.HasFlag(SqlBulkCopyOptions.KeepIdentity);           
                try
                {
                sqlBulkCopy.DestinationTableName = table;
                    var dataTable = entities.ToDataTable(columnImput, column);
                    await BulkCopyAsync(dataTable, sqlBulkCopy);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
        }
        ///column Imput es true=a las columnas de entrada, false=ignorar columnas de entradas
        public static async Task BulkUpdateAsync<T>(this DbContext context, IList<T> entities, string table, string key, bool columnInput = true, params string[] column) where T : class
        {
            if (!entities.Take(1).Any()) return;
 
            if (column.Count() > 0)
            {
                if (columnInput)
                {
                    if (!column.Any(x => x == key))
                    {
                        var para = column.ToList();
                        para.Add(key);
                        column = para.ToArray();
                    }
                }
                else if (!columnInput)
                {
                    if (column.Any(x => x == key)) column = column.Where(x => x != key).ToArray();
                }
            }
            var dataTable = entities.ToDataTable(columnInput, column);
            var atributo = string.Empty;
            var col = new List<string>();
            foreach (DataColumn item in dataTable.Columns)
            {
                if (key != item.ColumnName) col.Add(item.ColumnName);
                if (!string.IsNullOrEmpty(atributo)) atributo += ",";
                if (item.DataType == typeof(string)) atributo += $"{item.ColumnName} varchar(max)";
                else if (item.DataType == typeof(long)) atributo += $"{item.ColumnName} bigint";
                else if (item.DataType == typeof(int)) atributo += $"{item.ColumnName} int";
                else if (item.DataType == typeof(decimal)) atributo += $"{item.ColumnName} decimal(20,10)";
                else if (item.DataType == typeof(DateTime)) atributo += $"{item.ColumnName} datetime";
            }
            atributo = atributo.Trim(',');
            string TmpTable = $"#TmpTable_{DateTime.Now.ToString("dd_MM_yyyy_HH_mm_fffffff")}";
            await context.Database.ExecuteSqlInterpolatedAsync($"create table {TmpTable}({atributo})");
            DbConnection connection = context.Database.GetDbConnection();
            var transaction = context.Database.CurrentTransaction;
            var config = new SqlBulkCopyOptions();
            using var sqlBulkCopy = GetSqlBulkCopy((SqlConnection)connection, transaction, config);
            try
                {
                    sqlBulkCopy.DestinationTableName = TmpTable;
                    await BulkCopyAsync(dataTable, sqlBulkCopy);
                    var scriptUpdate = string.Empty;
                    foreach (var item in col)
                    {
                        if (!string.IsNullOrEmpty(scriptUpdate)) scriptUpdate += ", ";
                        scriptUpdate += $"tabla.{item}=temp.{item}";
                    }
                    scriptUpdate = scriptUpdate.Trim().Trim(',');
                    await context.Database.ExecuteSqlInterpolatedAsync($"update tabla set {scriptUpdate} from {table} as tabla inner join {TmpTable} as temp on tabla.{key}=temp.{key}");
                    //await Task.Delay(dataTable.Rows.Count + dataTable.Rows.Count);
                    await context.Database.ExecuteSqlInterpolatedAsync($"drop table {TmpTable}");
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
        }
        public static async Task BulkDeleteAsync<T>(this DbContext context, IList<T> entities, string table, string key) where T : class
        {
            if (!entities.Take(1).Any()) return;           
            var dataTable = entities.ToDataTable(true, key);
            var atributo = string.Empty;
            DataColumn column = dataTable.Columns[key];
            if (column.DataType == typeof(string)) atributo += $"{column.ColumnName} varchar(max)";
            else if (column.DataType == typeof(long)) atributo += $"{column.ColumnName} bigint";
            else if (column.DataType == typeof(int)) atributo += $"{column.ColumnName} int";
            else if (column.DataType == typeof(DateTime)) atributo += $"{column.ColumnName} datetime";

            string TmpTable = $"#TmpTable{DateTime.Now.ToString("dd_MM_yyyy_HH_mm_fffffff")}";
            await context.Database.ExecuteSqlInterpolatedAsync($"create table {TmpTable}({atributo})");
            DbConnection connection = context.Database.GetDbConnection();
            var transaction = context.Database.CurrentTransaction;
            var config = new SqlBulkCopyOptions();
            using var sqlBulkCopy = GetSqlBulkCopy((SqlConnection)connection, transaction, config);
            try
                {
                    sqlBulkCopy.DestinationTableName = TmpTable;
                    await BulkCopyAsync(dataTable, sqlBulkCopy);
                    await context.Database.ExecuteSqlInterpolatedAsync($"delete from {table} where {key} in (select temp.{key} from {TmpTable} as temp)");
                    await context.Database.ExecuteSqlInterpolatedAsync($"drop table {TmpTable}");
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
        }
        private static async Task BulkCopyAsync(DataTable dataTable, SqlBulkCopy bulkCopy)
        {
            foreach (DataColumn item in dataTable.Columns) bulkCopy.ColumnMappings.Add(item.ColumnName, item.ColumnName);
            await bulkCopy.WriteToServerAsync(dataTable);
        }
        private static DataTable ToDataTable<T>(this IList<T> data, bool columnImput = true, params string[] column)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            var intCol = column.Count();
            foreach (PropertyDescriptor prop in properties)
            {
                if (intCol > 0)
                {
                    if (!column.Any(x => x == prop.Name) && columnImput) continue;
                    else if (column.Any(x => x == prop.Name) && !columnImput) continue;
                }
                var propertyType = prop.PropertyType;
                var underlyingType = Nullable.GetUnderlyingType(propertyType);
                if (underlyingType != null) propertyType = underlyingType;
                if (propertyType.IsClass)
                {
                    if (typeof(string) != propertyType) continue;
                }
                else if (propertyType.IsConstructedGenericType) continue;
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
        private static SqlBulkCopy GetSqlBulkCopy(SqlConnection sqlConnection, IDbContextTransaction transaction, SqlBulkCopyOptions config)
        {
            var sqlTransaction = transaction == null ? null : (SqlTransaction)((DbTransaction)transaction);
            var sqlBulkCopy = new SqlBulkCopy(sqlConnection, config, sqlTransaction);
            return sqlBulkCopy;
        }
    }
}
