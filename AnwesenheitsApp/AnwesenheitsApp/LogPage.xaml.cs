using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AnwesenheitsApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogPage : ContentPage
    {
        private string _logText;
        private Logging.Logging _logger; 

        public string LogText
        {
            get
            {
                return this._logText;
            }

            private set
            {
                this._logText = value;
                OnPropertyChanged();
            }
        }

        public LogPage()
        {
            InitializeComponent();

            this._logger = new Logging.Logging();  

            this.LogText = this._logger.GetLogText();

            this.BindingContext = this;
        }

        public async void ClearLogBtn_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Warnung!", "Möchtest du das Log wirklich Löschen?",
                "Ja", "Nein");

            if (answer)
            {
                this._logger.ClearLog();
                this.LogText = this._logger.GetLogText();
            }
        }
    }
}