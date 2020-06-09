using AnwesenheitsApp.DbModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnwesenheitsApp
{
    public class Database
    {
        readonly SQLiteAsyncConnection _db;

        public Database(string dbPath)
        {
            this._db = new SQLiteAsyncConnection(dbPath);
            this._db.CreateTableAsync<LocationData>().Wait();
        }

        public Task<List<LocationData>> GetLocationDataAsync()
        {
            return this._db.Table<LocationData>().ToListAsync();
        }

        public Task<int> SaveLocationDataAsync(LocationData locationData)
        {
            return this._db.InsertAsync(locationData);
        }
    }
}
