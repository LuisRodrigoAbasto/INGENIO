using Abasto.Library.BulkExtensions.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Abasto.Library.BulkExtensions
{
    internal static partial class BulkOperations
    {///column Imput es true=a las columnas de entrada, false=ignorar columnas de entradas
        public static async Task BulkInsertAsync<T>(this DbContext context, IList<T> entities, Action<BulkConfig> options) where T : class
        {
            if (!entities.Take(1).Any()) return;
            BulkConfig bulkConfig = new BulkConfig();
            options?.Invoke(bulkConfig);
            DbConnection Connection = context.Database.Connection;
            var UnderlyingTransaction = context.Database.CurrentTransaction.UnderlyingTransaction;
            SqlConnection sqlConnection = (SqlConnection)Connection;
            SqlTransaction sqlTransaction = (SqlTransaction)UnderlyingTransaction;

            var config = new SqlBulkCopyOptions();
            //config.HasFlag(SqlBulkCopyOptions.KeepIdentity);
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConnection, config, sqlTransaction))
                try
                {
                    bulkCopy.DestinationTableName = bulkConfig.TableName;
                    var dataTable = entities.ToDataTable();
                    await BulkCopyAsync(dataTable, bulkCopy);
                }
                catch (Exception ex)
                {
                    throw new AbastoException(ex.Message, ex);
                }
        }
        ///column Imput es true=a las columnas de entrada, false=ignorar columnas de entradas
        public static async Task BulkUpdateAsync<T>(this DbContext context, IList<T> entities, string table, string key, bool columnInput = true, params string[] column) where T : class
        {
            if (!entities.Take(1).Any()) return;
            DbConnection Connection = context.Database.Connection;
            var UnderlyingTransaction = context.Database.CurrentTransaction.UnderlyingTransaction;
            SqlConnection sqlConnection = (SqlConnection)Connection;
            SqlTransaction sqlTransaction = (SqlTransaction)UnderlyingTransaction;
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
            await context.Database.ExecuteSqlCommandAsync($"create table {TmpTable}({atributo})");
            var config = new SqlBulkCopyOptions();
            //config.HasFlag(SqlBulkCopyOptions.KeepIdentity);
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConnection, config, sqlTransaction))
                try
                {
                    bulkCopy.DestinationTableName = TmpTable;
                    await BulkCopyAsync(dataTable, bulkCopy);
                    var scriptUpdate = string.Empty;
                    foreach (var item in col)
                    {
                        if (!string.IsNullOrEmpty(scriptUpdate)) scriptUpdate += ", ";
                        scriptUpdate += $"tabla.{item}=temp.{item}";
                    }
                    scriptUpdate = scriptUpdate.Trim().Trim(',');
                    await context.Database.ExecuteSqlCommandAsync($"update tabla set {scriptUpdate} from {table} as tabla inner join {TmpTable} as temp on tabla.{key}=temp.{key}");
                    //await Task.Delay(dataTable.Rows.Count + dataTable.Rows.Count);
                    await context.Database.ExecuteSqlCommandAsync($"drop table {TmpTable}");
                }
                catch (Exception ex)
                {
                    throw new AbastoException(ex.Message, ex);
                }
        }
        public static async Task BulkDeleteAsync<T>(this DbContext context, IList<T> entities, string table, string key) where T : class
        {
            if (!entities.Take(1).Any()) return;
            DbConnection Connection = context.Database.Connection;
            var UnderlyingTransaction = context.Database.CurrentTransaction.UnderlyingTransaction;
            SqlConnection sqlConnection = (SqlConnection)Connection;
            SqlTransaction sqlTransaction = (SqlTransaction)UnderlyingTransaction;
            var dataTable = entities.ToDataTable(true, key);
            var atributo = string.Empty;
            DataColumn column = dataTable.Columns[key];
            if (column.DataType == typeof(string)) atributo += $"{column.ColumnName} varchar(max)";
            else if (column.DataType == typeof(long)) atributo += $"{column.ColumnName} bigint";
            else if (column.DataType == typeof(int)) atributo += $"{column.ColumnName} int";
            else if (column.DataType == typeof(DateTime)) atributo += $"{column.ColumnName} datetime";

            string TmpTable = $"#TmpTable{DateTime.Now.ToString("dd_MM_yyyy_HH_mm_fffffff")}";
            await context.Database.ExecuteSqlCommandAsync($"create table {TmpTable}({atributo})");
            var config = new SqlBulkCopyOptions();
            //config.HasFlag(SqlBulkCopyOptions.KeepIdentity);
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConnection, config, sqlTransaction))
                try
                {
                    bulkCopy.DestinationTableName = TmpTable;
                    await BulkCopyAsync(dataTable, bulkCopy);
                    await context.Database.ExecuteSqlCommandAsync($"delete from {table} where {key} in (select temp.{key} from {TmpTable} as temp)");
                    await context.Database.ExecuteSqlCommandAsync($"drop table {TmpTable}");
                }
                catch (Exception ex)
                {
                    throw new AbastoException(ex.Message, ex);
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
    }
}
