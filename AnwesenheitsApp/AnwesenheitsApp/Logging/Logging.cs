using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AnwesenheitsApp.Logging
{
    class Logging
    {
        private string _filePath = @"error_log.txt";
        private string _logText;

        public Logging()
        {
            this._filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                this._filePath);
        }

        public void WriteLogEntry(string logEntryMessage)
        {
            if(!CheckIfLogExists())
            {
                CreateLog();
            }

            WriteEntry(logEntryMessage);
        }

        public string GetLogText()
        {
            if(CheckIfLogExists())
            {
                ReadLog();
                return this._logText;
            }

            return "No Log available!";
        }

        private void WriteEntry(string logEntryMessage)
        {
            ReadLog();
            logEntryMessage = "\n" + DateTime.Now.ToString("dd-MM-yyyy H:mm:ss\t") + logEntryMessage;
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
            File.WriteAllText(this._filePath, "");
        }

        private void ClearLog()
        {
            File.WriteAllText(this._filePath, "");
        }

        private bool CheckIfLogExists()
        {
            if (File.Exists(this._filePath))
                return true;

            return false;
        }
    }
}
