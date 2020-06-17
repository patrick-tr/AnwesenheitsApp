using Android.Graphics;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AnwesenheitsApp
{
    public partial class App : Application
    {
        static Database database;
        static bool isServiceRunning;

        public static Database Database
        {
            get
            {
                if(database == null)
                {
                    database = new Database(System.IO.Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "AnwesenheitsAppDb.db3"));
                }
                return database;
            }
        }

        public static bool IsServiceRunning { get; set; }

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
            var service = DependencyService.Get<IPositionService>();
            App.IsServiceRunning = service.ServiceState;
        }
    }
}
