using System;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;

namespace QLSV.Core
{
    class MyExcel
    {
        string path = "";
        _Application excel = new _Excel.Application();
        Workbook wb;
        Worksheet ws;

        public MyExcel(string path, int sheet)
        {
            this.path = path;
            wb = excel.Workbooks.Open(path);
            ws = (Worksheet)wb.Worksheets[sheet];
        }

        public string ReadCell(int row, int collum)
        {
            return ((_Excel.Range)ws.Cells[row, collum]).Value2?.ToString() ?? "";
        }

        public int GetRowCount()
        {
            return ws.UsedRange.Rows.Count;
        }

        public void Close()
        {
            // Release COM objects
            Marshal.ReleaseComObject(ws);
            Marshal.ReleaseComObject(wb);

            // Quit Excel application and release it
            excel.Quit();
            Marshal.ReleaseComObject(excel);

            // Finalize the GC to ensure all COM objects are released
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}