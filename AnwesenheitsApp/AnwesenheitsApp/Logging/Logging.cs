using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AnwesenheitsApp.Logging
{
    public enum LoggingType
    {
        INFO, ERROR, WARNING, DEBUG, FATAL
    }

    class Logging
    {
        private string _filePath = @"error_log.txt";
        private string _logText;

        public Logging()
        {
            this._filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                this._filePath);
        }

        public void WriteLogEntry(LoggingType type, string logEntryMessage)
        {
            if(!CheckIfLogExists())
            {
                CreateLog();
            }

            WriteEntry(type, logEntryMessage);
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

        private void WriteEntry(LoggingType type, string logEntryMessage)
        {
            ReadLog();
            logEntryMessage = "\n" + DateTime.Now.ToString("dd-MM-yyyy H:mm:ss\t[") +
                type.ToString() + "] " + logEntryMessage;
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

        public void ClearLog()
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
