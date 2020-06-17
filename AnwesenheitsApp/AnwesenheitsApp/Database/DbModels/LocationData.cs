using SQLite;
using System;

namespace AnwesenheitsApp.DbModels
{
    public class LocationData
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public DateTime CreationDate { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string AdminArea { get; set; }
        public string Locality { get; set; }

        public override string ToString()
        {
            return this.Locality + " " + this.CreationDate.ToString("dd.MM.yyyy h:mm:ss");
        }
    }
}
