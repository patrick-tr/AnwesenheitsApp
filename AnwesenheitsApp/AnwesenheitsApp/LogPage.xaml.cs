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

            var logger = new Logging.Logging();

            this.LogText = logger.GetLogText();

            this.BindingContext = this;
        }
    }
}