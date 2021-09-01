using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Abasto.Libreria
{
    public static class ProcesarExcel
    {
        #region connstrings
        private static string conXls = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=Yes;'";
        //private static string conXlsx = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0 Xml;HDR=Yes;IMEX=1'";
        private static string conXlsx = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0 Xml;HDR=Yes;IMEX=1'";
        private static string cmdSelect = "SELECT * FROM [{0}$]";
        private static string cmdInsert = @"INSERT INTO [{0}$] ({1}) VALUES ({2});";
        #endregion

        #region private
        public static DataTable ReadCsv(string file, string columnsSeparator = ";")
        {
            string fileName = Path.GetFileName(file);
            DataTable dt = new DataTable();
            int counter = 0;

            using (StreamReader sr = new StreamReader(file))
            {
                while (sr.Peek() >= 0)
                {
                    string row = sr.ReadLine();
                    if (counter == 0)
                    {
                        string columns = row;
                        string[] listColumns = columns.Split(columnsSeparator[0]);

                        foreach (string column in listColumns)
                        {
                            dt.Columns.Add(new DataColumn(column.Trim(), typeof(string)));
                        }
                    }
                    else
                    {

                        if (!string.IsNullOrEmpty(row))
                        {
                            dt.Rows.Add();
                            int i = 0;
                            try
                            {
                                foreach (string cell in row.Split(columnsSeparator[0]))
                                {
                                    dt.Rows[dt.Rows.Count - 1][i] = cell;
                                    i++;
                                }
                            }
                            catch { }
                        }
                    }
                    counter++;
                }
            }
            return dt;
        }

        public static DataTable ReadSpreadsheet(string file, DataTable data = null, string nro = null, string validar = null)
        {
            DataTable dt = data != null ? data : new DataTable("Excel");
            using (SpreadsheetDocument doc = SpreadsheetDocument.Open(file, false))
            {
                string mensaje = string.Empty;
                try
                {
                    if (!string.IsNullOrEmpty(validar) && !dt.Columns.Contains(validar)) dt.Columns.Add(validar, typeof(string));
                    if (!string.IsNullOrEmpty(nro) && !dt.Columns.Contains(nro)) dt.Columns.Add(nro, typeof(long));
                    var columna = dt.Columns;
                    var lista = new List<ExcelTabla>();
                    bool firstRow = true;
                    WorkbookPart workbookPart = doc.WorkbookPart;
                    WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();
                    SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();
                    int cantidad = 1;
                    foreach (Row row in sheetData.Elements<Row>())
                    {
                        int i = 0, y = 0;
                        bool convirtio = false;
                        DataRow dr = dt.NewRow();
                        mensaje = string.Empty;
                        foreach (Cell c in row.Elements<Cell>())
                        {
                            string text = string.Empty, celda = c.CellReference.Value;
                            for (int v = 1; !convirtio && v < celda.Length; v++)
                            {
                                convirtio = int.TryParse(celda.Substring(v), out y);
                            }
                            celda = General.ReplaceAll(celda, y.ToString(), "");
                            if (firstRow)
                            {
                                if (c.DataType != null && c.DataType == CellValues.SharedString)
                                {
                                    text = workbookPart.SharedStringTablePart.SharedStringTable.Elements<SharedStringItem>().ElementAt(Convert.ToInt32(c.InnerText)).InnerText;
                                    string nombre = General.ReplaceAll(text, " ", "");
                                    if (lista.Any(x => x.nombre == nombre))
                                    {
                                        mensaje = $"El Excel tiene La Columna {text} repetida.";
                                        throw new Exception(mensaje);
                                    }
                                    else if (!columna.Contains(nombre)) dt.Columns.Add(new DataColumn() { ColumnName = nombre, DataType = Type.GetType("System.String"), Caption = text });
                                    lista.Add(new ExcelTabla()
                                    {
                                        id = cantidad,
                                        celda = celda,
                                        nombre = nombre,
                                    });
                                    cantidad++;
                                }
                            }
                            else
                            {
                                if (c.DataType != null && c.DataType == CellValues.SharedString) text = workbookPart.SharedStringTablePart.SharedStringTable.Elements<SharedStringItem>().ElementAt(Convert.ToInt32(c.InnerText)).InnerText;
                                else if (c.CellValue != null) text = c.CellValue.Text;
                                if (!string.IsNullOrEmpty(text))
                                {
                                    var obj = lista.Where(x => x.celda == celda).Select(x => new { x.id, x.nombre }).FirstOrDefault();
                                    if (obj == null) break;
                                    try
                                    {
                                        if (columna[obj.nombre].DataType == typeof(DateTime)) dr[obj.nombre] = DateTime.FromOADate(Double.Parse(text));
                                        else dr[obj.nombre] = text;
                                    }
                                    catch
                                    {
                                        mensaje += $"{columna[obj.nombre].Caption} [{text}] No Valido | ";
                                    }
                                    i = obj.id;
                                    i++;
                                }
                                if (i>=cantidad) break;
                            }
                        }
                        if (i > 0)
                        {
                            if (!string.IsNullOrEmpty(nro)) dr[nro] = y;
                            if (!string.IsNullOrEmpty(validar) && !string.IsNullOrEmpty(mensaje)) dr[validar] = $"{mensaje.Trim()} Fila Excel {y}";
                            dt.Rows.Add(dr);
                        }
                        else
                        {
                            firstRow = false;
                            columna = dt.Columns;
                        }
                    }
                }
                catch (Exception ex)
                {
                    doc.Close();
                    doc.Dispose();
                    if (mensaje == ex.Message) throw new Exception(ex.Message, ex);
                    else throw new Exception("Error al leer en el Documento", ex);
                }
                doc.Close();
                doc.Dispose();
            }
            return dt;
        }
        private static DataTable ReadExcelInternal(string path, string conType, string sheet)
        {
            DataTable dt = new DataTable("Excel");
            string con = string.Format(conType, path);
            string cmd = string.Format(cmdSelect, sheet);
            using (OleDbConnection connection = new OleDbConnection(con))
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand(cmd, connection);
                using (OleDbDataReader dr = command.ExecuteReader())
                {
                    dt.Load(dr);
                }
                connection.Close();
                connection.Dispose();
            }
            return dt;
        }
        private static void WriteExcelInternal(string path, string conType, string sheet, string columns, string values)
        {
            string con = string.Format(conType, path);
            string cmd = string.Format(cmdInsert, sheet, columns, values);
            using (OleDbConnection connection = new OleDbConnection(con))
            {
                try
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand(cmd, connection);
                    command.ExecuteNonQuery();
                }
                catch
                {
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }
        #endregion

        #region public

        public static DataTable ReadXls(string path, string sheet)
        {
            return ReadExcelInternal(path, conXls, sheet);
        }
        public static DataTable ReadXlsx(string path, string sheet)
        {
            return ReadExcelInternal(path, conXlsx, sheet);
        }
        public static DataTable ReadExcel(string path, string sheet)
        {
            if (path.ToUpper().Contains(".XLSX"))
            {
                return ReadXlsx(path, sheet);
            }
            else if (path.ToUpper().Contains(".XLS"))
            {
                return ReadXls(path, sheet);
            }
            throw new NotSupportedException("Formato no soportado");
        }

        public static void WriteXls(string path, string sheet, string columns, string values)
        {
            WriteExcelInternal(path, conXls, sheet, columns, values);
        }
        public static void WriteXlsx(string path, string sheet, string columns, string values)
        {
            WriteExcelInternal(path, conXlsx, sheet, columns, values);
        }
        public static void WriteExcel(string path, string sheet, string columns, string values)
        {
            if (path.ToUpper().Contains(".XLSX"))
            {
                WriteXlsx(path, sheet, columns, values);
                return;
            }
            else if (path.ToUpper().Contains(".XLS"))
            {
                WriteXls(path, sheet, columns, values);
                return;
            }
            throw new NotSupportedException("Formato no soportado");
        }

        #endregion

        public static async Task BulkCopy(string conexion, string destTable, Dictionary<string, string> columMap, DataTable objDataTable)
        {
            using (var sqlConexion = new SqlConnection(conexion))
            {
                await sqlConexion.OpenAsync();
                using (var db = sqlConexion.BeginTransaction())
                {
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConexion, SqlBulkCopyOptions.KeepIdentity, db))
                    {
                        try
                        {
                            bulkCopy.DestinationTableName = destTable;
                            foreach (var item in columMap)
                            {
                                bulkCopy.ColumnMappings.Add(item.Key, item.Value);
                            }
                            await bulkCopy.WriteToServerAsync(objDataTable);
                            db.Commit();
                        }
                        catch (Exception ex)
                        {
                            db.Rollback();
                            throw new Exception("Error al Guardar", ex);
                        }
                    }
                }
            }
        }
        private class ExcelTabla
        {
            public int id { get; set; }
            public string celda { get; set; }
            public string nombre { get; set; }
        }
    }
}