using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AnwesenheitsApp.Logging
{
    class Logging
    {
        private string _filePath = @"Log/error_log.txt";
        private string _logText;

        public Logging()
        {

        }

        public void WriteLogEntry(string logEntryMessage)
        {
            if(!CheckIfLogExists())
            {
                CreateLog();
            }

            WriteEntry(logEntryMessage);
        }

        private void WriteEntry(string logEntryMessage)
        {
            logEntryMessage = "\n" + DateTime.Now.ToString("dd-MM-YYYY H:mm:ss") + logEntryMessage;
            this._logText += logEntryMessage;

            File.WriteAllText(this._filePath, this._logText);
        }

        private void ReadLog()
        {
            if(CheckIfLogExists())
            {
                _logText = File.ReadAllText(this._filePath);
            }
        }

        private void CreateLog()
        {

        }

        private bool CheckIfLogExists()
        {
            if (File.Exists(this._filePath))
                return true;

            return false;
        }
    }
}
