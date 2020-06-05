using Android.Views;
using Java.Security;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AnwesenheitsApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExportPage : ContentPage
    {
        private bool _isCSV;
        private bool _isEXCEL;
        private DateTime _fromDate;
        private DateTime _toDate;
        private Logging.Logging _logger;
        private IExtStorage _extStorage;

        public bool isCSV
        {
            get
            {
                return this._isCSV;
            }
            set
            {
                this._isCSV = value;
                OnPropertyChanged();
            }
        }
        public bool isEXCEL
        {
            get
            {
                return this._isEXCEL;
            }
            set
            {
                this._isEXCEL = value;
                OnPropertyChanged();
            }
        }
        public string LogFileName { get; set; }
        public string BookingFileName { get; set; }
        public DateTime FromDate 
        {
            get
            {
                return this._fromDate;
            }
            set
            {
                this._fromDate = value;
                OnPropertyChanged();
            }    
        }
        public DateTime ToDate 
        {
            get
            {
                return this._toDate;
            }
            set
            {
                this._toDate = value;
                OnPropertyChanged();
            }    
        }

        public ExportPage()
        {
            this.isCSV = false;
            this.isEXCEL = false;
            this.LogFileName = "";
            this._logger = new Logging.Logging();

            this._extStorage = DependencyService.Get<IExtStorage>();

            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            int daysInMonth = DateTime.DaysInMonth(year, month);

            this.FromDate = new DateTime(year, month, 1);
            this.ToDate = new DateTime(year, month, daysInMonth);

            InitializeComponent();

            this.BindingContext = this;
        }

        public async void SaveLogBtnClicked(object sender, EventArgs e)
        {
            
            if (this.LogFileName == null)
                return;
            else if (this.LogFileName.Length <= 0)
                return;

            if(this._extStorage.CheckIfFileExists(this.LogFileName))
            {
                var answer = await DisplayAlert("Datei überschreiben?", "Die Datei existiert bereits. Soll" +
                    "diese überschreiben werden?", "Ja", "Nein");

                if (!answer)
                    return;
            }


            await DisplayAlert("Speichern...",
                this._extStorage.WriteAllText(this.LogFileName, this._logger.GetLogText()) ?
                "Erfolgreich abgeschlossen!" : "Fehlgeschlagen!", "Ok");
                
        }

        public void ExportBookingsBtnClicked(object sender, EventArgs e)
        {
            if(!this._isCSV && !this._isEXCEL)
            {
                DisplayAlert("Warnung!", "Es muss mindestens eine Exportart ausgewählt werden!",
                    "Ok");
                return;
            }

            if (this.isCSV)
                CreateCsvData();
            if (this.isEXCEL)
                CreateExcelFile();
        }

        private string CreateCsvData()
        {





            return "";
        }

        private async void CreateExcelFile()
        {
            if (BookingFileName == null)
            {
                await DisplayAlert("Warnung!", "Der Dateiname darf nicht Leer sein!", "Ok");
                return;
            }

            string fileName = BookingFileName;

            if(this._extStorage.CheckIfFileExists(fileName))
            {
                bool answer = await DisplayAlert("Überschreiben?", "Es ist bereits eine " +
                    "Datei mit diesem Namen vorhanden! Soll diese überschrieben werden?",
                    "Ja", "Nein");

                if (!answer)
                    return;
            }

            using (ExcelEngine excel = new ExcelEngine())
            {
                ExcelHandler exHnd = new ExcelHandler(excel);
                exHnd.CreateWorkbook("Test");
                exHnd.CreateWorksheetHeadings("Test", 
                    new string[] { "Anna", "Michael", "Maria", "Selina" });

                object[][] data =
                {
                    new string[] { "s", "v", "k" },
                    new string[] { "b", "c", "a" },
                    new string[] { "d", "w", "h" },
                    new string[] { "g", "i", "k" }
                };


                exHnd.FillWorksheetWithData("Test", data, true);
                exHnd.SaveExcelWoorkbook("Test.xlsx");
            }
        }
    }
}