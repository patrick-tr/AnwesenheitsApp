
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnwesenheitsApp
{
    public interface IExtStorage
    {
        bool WriteAllText(string fileName, string text);
        string ReadAllText(string fileName);
        bool CheckIfFileExists(string fileName);
        bool SaveExcelWorkbook(IWorkbook wb, string fileName);
    }
}
