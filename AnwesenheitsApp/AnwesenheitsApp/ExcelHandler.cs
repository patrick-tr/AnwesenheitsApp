using Android.Media;
using Syncfusion.XlsIO;
using Syncfusion.XlsIO.Implementation;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AnwesenheitsApp
{
    class ExcelHandler : Page
    {
        private ExcelEngine _excel;
        private IApplication _app;
        private IWorkbook _wb;

        public ExcelHandler(ExcelEngine excel)
        {
            this._excel = excel;
            this._app = this._excel.Excel;
            this._app.DefaultVersion = ExcelVersion.Excel2007;
        }

        public void CreateWorkbook(string[] workSheetNames)
        { 
            this._wb = this._app.Workbooks.Create(workSheetNames);        
        }

        public void CreateWorkbook(string workSheetName)
        {
            this._wb = this._app.Workbooks.Create(new string[] { workSheetName });
        }

        public void FillWorksheetWithData(string worksheetName, object[][] data, bool hasHeading = false)
        {
            int rowCounter = 1;
            int columnCounter = 1;

            if (hasHeading)
                rowCounter++;

            IWorksheet ws = this._wb.Worksheets[worksheetName];

            foreach(var column in data)
            {
                foreach(var cell in column)
                {
                    ws.Range[rowCounter, columnCounter].Text = (string)cell;
                    rowCounter++;
                }
                rowCounter = hasHeading ? 2 : 1;
                columnCounter++;
            }
        }

        public void CreateWorksheetHeadings(string worksheetName, string[] headings)
        {
            IWorksheet ws = this._wb.Worksheets[worksheetName];
            int counter = 1;

            foreach (string heading in headings)
            {
                ws.Range[1, counter].Text = heading;
                
                counter++;
            }
        }

        public async void SaveExcelWoorkbook(string fileName)
        {
            IExtStorage extStorage = DependencyService.Get<IExtStorage>();

            if (!fileName.EndsWith(".xlsx"))
                fileName += ".xlsx";

            if (extStorage.CheckIfFileExists(fileName))
            {
                bool answer = await DisplayAlert("Überschreiben?", "Es existiert bereits" +
                    "eine Datei mit diesem Namen! Möchten Sie diese überschreiben?",
                    "Ja", "Nein");

                if (!answer)
                    return;
            }

            extStorage.SaveExcelWorkbook(this._wb, fileName);
        }
    }
}
