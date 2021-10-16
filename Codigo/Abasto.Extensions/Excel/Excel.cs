using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Abasto.Extensions.Excel
{
    public static partial class Excel
    {
        public static DataTable ReadSpreadSheet<T>(this DataTable dataTable, Action<ExcelConfig> options) where T : class
        {
            DataTable dt = dataTable != null ? dataTable : new DataTable("Excel");
            ExcelConfig excelConfig = new ExcelConfig();
            options?.Invoke(excelConfig);
            if (!string.IsNullOrEmpty(excelConfig.mensaje) && !dt.Columns.Contains(excelConfig.mensaje)) dt.Columns.Add(excelConfig.mensaje, typeof(string));
            if (!string.IsNullOrEmpty(excelConfig.nro) && !dt.Columns.Contains(excelConfig.nro)) dt.Columns.Add(new DataColumn(excelConfig.nro, typeof(int)));
            using (SpreadsheetDocument doc = SpreadsheetDocument.Open(excelConfig.path, false))
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
                            celda = celda.Replace(y.ToString(), "");
                            if (firstRow)
                            {
                                if (c.DataType != null && c.DataType == CellValues.SharedString)
                                {
                                    text = workbookPart.SharedStringTablePart.SharedStringTable.Elements<SharedStringItem>().ElementAt(Convert.ToInt32(c.InnerText)).InnerText.Trim();
                                    string nombre = text.Replace(" ", "");
                                    if (lista.Any(x => x.nombre == nombre))
                                    {
                                        mensaje = $"El Excel tiene La Columna {text} repetida.";
                                        throw new AbastoException(mensaje);
                                    }
                                    else if (!columna.Contains(nombre)) dt.Columns.Add(new DataColumn() { ColumnName = nombre, DataType = typeof(string), Caption = text });
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
                            if (!string.IsNullOrEmpty(excelConfig.nro)) dr[excelConfig.nro] = y;
                            if (!string.IsNullOrEmpty(excelConfig.mensaje) && !string.IsNullOrEmpty(mensaje)) dr[excelConfig.mensaje] = $"{mensaje.Trim()} Fila Excel {y}";
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
                    if (mensaje == ex.Message) throw new AbastoException(ex.Message, ex);
                    else throw new AbastoException("Error al leer en el Documento", ex);
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
