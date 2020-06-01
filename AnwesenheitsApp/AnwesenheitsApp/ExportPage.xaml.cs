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
    public partial class ExportPage : ContentPage
    {
        private bool _isCSV;
        private bool _isEXCEL;

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

        public ExportPage()
        {
            this.isCSV = false;
            this.isEXCEL = false;

            InitializeComponent();

            this.BindingContext = this;
        }

        public void SaveLogBtnClicked(object sender, EventArgs e)
        {
            
        }
    }
}