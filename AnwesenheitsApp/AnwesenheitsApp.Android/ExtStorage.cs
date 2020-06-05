using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Android.App;
using AnwesenheitsApp.Droid;
using Java.IO;
using Syncfusion.XlsIO;

[assembly: Xamarin.Forms.Dependency(typeof(ExtStorage))]
namespace AnwesenheitsApp.Droid
{
    class ExtStorage : IExtStorage
    {
        private Logging.Logging _logger;

        public ExtStorage()
        {
            this._logger = new Logging.Logging();
        }

        private string GetPath()
        {
            //Todo Check if directory exists
            return Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDocuments).AbsolutePath;
        }

        public string ReadAllText(string fileName)
        {
            try
            {
                string path = System.IO.Path.Combine(GetPath(), fileName);
                return System.IO.File.ReadAllText(path);
            }
            catch(Exception ex)
            {
                this._logger.WriteLogEntry(Logging.LoggingType.ERROR, 
                    ex.Message + " ReadAllText() in class ExtStorage Android");
            }

            return null;
        }

        public bool WriteAllText(string fileName, string text)
        {
            try
            {
                string path = System.IO.Path.Combine(GetPath(), fileName);
                System.IO.File.WriteAllText(path,text);
            }
            catch(Exception ex)
            {
                this._logger.WriteLogEntry(Logging.LoggingType.ERROR,
                    ex.Message + " WriteAllText() in class ExtStorage Android");
                return false;
            }

            return true;
        }

        public bool CheckIfFileExists(string fileName)
        {
            if (System.IO.File.Exists(System.IO.Path.Combine(GetPath(), fileName)))
                return true;

            return false;
        }

        public bool SaveExcelWorkbook(IWorkbook wb, string fileName)
        {
            try
            {
                var fs = new FileStream(Path.Combine(GetPath(), fileName), FileMode.OpenOrCreate);
                wb.SaveAs(fs);
            }
            catch(Exception ex)
            {
                this._logger.WriteLogEntry(Logging.LoggingType.ERROR, 
                    ex.Message + " SaveExcelWorkbook() in class ExtStorage Android");
                return false;
            }

            return true;
        }

    }
}