using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.Entity;

namespace TSLibrary
{
    public class LogEventLogic
    {
        private EventLog _log;
        private string _path;
        //MessageContext db;
        //Message _mess;
        //private StreamWriter _writer;

        public LogEventLogic(string path)
        {
            try
            {
                _path = path;
                //db = new MessageContext();
                //db.Messages.Load();
            }
            catch(Exception ex)
            {
                Logger(ex.TargetSite.ToString(), ex.Message);
            }
        }

        public void WriteBase(string text)
        {
            //_mess = new Message() { GetMessage = text };
            //db.Messages.Add(_mess);
            //db.SaveChanges();
        }

        private void Logger(string Head, string Text)
        {
            using(StreamWriter _write = new StreamWriter("V:\\ExtLog.log"))
            {
                _write.WriteLine(string.Format("{0} : get message - {1} / {2}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"), Head, Text));
            }
        }

        public void Write(string events)
        {
            using (StreamWriter _writer = new StreamWriter(_path))
            {
                _writer.WriteLine(string.Format("{0} : get message - {1}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"), events));
            }
        }
    }
}
