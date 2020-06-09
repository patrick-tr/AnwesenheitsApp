using SQLite;
using System;

namespace AnwesenheitsApp.DbModels
{
    public class LocationData
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string AdminArea { get; set; }
        public string Locality { get; set; }
    }
}
