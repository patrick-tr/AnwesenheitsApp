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
        private string _folderPath;
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

        public ExportPage()
        {
            this.isCSV = false;
            this.isEXCEL = false;
            this.LogFileName = "";
            this._logger = new Logging.Logging();

            this._folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            this._extStorage = DependencyService.Get<IExtStorage>();

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
            string fileName = "test.xlsx";
            if(this._extStorage.CheckIfFileExists(fileName))
            {
                bool answer = await DisplayAlert("Überschreiben?", "Es ist bereits eine " +
                    "DAtei mit diesem Namen vorhanden! Soll diese überschrieben werden?",
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