using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Abasto.Lib;

namespace Abasto.Libreria.General
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
        public static DataTable ReadSpreadSheet<T>(this DataTable dataTable, string path, string nro = null, string validar = null) where T : DataTable
        {
            DataTable dt = dataTable != null ? dataTable : new DataTable("Excel");
            if (!string.IsNullOrEmpty(validar) && !dt.Columns.Contains(validar)) dt.Columns.Add(validar, typeof(string));
            if (!string.IsNullOrEmpty(nro) && !dt.Columns.Contains(nro)) dt.Columns.Add(new DataColumn(nro, typeof(long)));
            using (SpreadsheetDocument doc = SpreadsheetDocument.Open(path, false))
            {
                string mensaje = string.Empty;
                try
                {
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
                            celda = celda.ReplaceAll(y.ToString(), "");
                            if (firstRow)
                            {
                                if (c.DataType != null && c.DataType == CellValues.SharedString)
                                {
                                    text = workbookPart.SharedStringTablePart.SharedStringTable.Elements<SharedStringItem>().ElementAt(Convert.ToInt32(c.InnerText)).InnerText.Trim();
                                    string nombre = text.ReplaceAll(" ", "");
                                    if (lista.Any(x => x.nombre == nombre))
                                    {
                                        mensaje = $"El Excel tiene La Columna {text} repetida.";
                                        throw new BusinessException(mensaje);
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
                                if (c.DataType != null && c.DataType == CellValues.SharedString) text = workbookPart.SharedStringTablePart.SharedStringTable.Elements<SharedStringItem>().ElementAt(Convert.ToInt32(c.InnerText)).InnerText.Trim();
                                else if (c.CellValue != null) text = c.CellValue.Text.Trim();
                                if (!string.IsNullOrEmpty(text))
                                {
                                    var obj = lista.Where(x => x.celda == celda).Select(x => new { x.id, x.nombre }).FirstOrDefault();
                                    if (obj != null)
                                    {
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
                                    }
                                    i++;
                                }
                                else continue;
                                if (cantidad < i) break;
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
                    if (mensaje == ex.Message) throw new BusinessException(ex.Message, ex);
                    else throw new BusinessException("Error al leer en el Documento", ex);
                }
                doc.Close();
                doc.Dispose();
            }
            return dt;
        }
        private class ExcelTabla
        {
            public int id { get; set; }
            public string celda { get; set; }
            public string nombre { get; set; }
        }
    }
}
