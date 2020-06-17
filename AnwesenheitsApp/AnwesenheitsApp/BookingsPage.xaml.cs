using AnwesenheitsApp.DbModels;
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
    public partial class BookingsPage : ContentPage
    {
        private List<LocationData> _bookings;
        public List<LocationData> Bookings
        {
            get
            {
                return this._bookings;
            }
            set
            {
                if (this._bookings == null)
                    this._bookings = new List<LocationData>();

                this._bookings = value;
                OnPropertyChanged();
            }
        }

        public BookingsPage()
        {
            InitializeComponent();
            Test();

            this.BindingContext = this;
        }

        private void Test()
        {
            List<LocationData> data = App.Database.GetLocationDataFromDbAsync().Result;
            this.Bookings = data;
        }
    }
}