using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Data.Entity;

namespace TSLibrary
{
    public class ClientLogic
    {
        protected internal string Id { get; private set; }
        protected internal NetworkStream _stream { get; private set; }
        private TcpClient _client;
        private ServerLogic _server;
        private LogEventLogic _log;
        private MessageContext db;
        private Message _message;

        public ClientLogic(TcpClient tcpClient, ServerLogic serverLogic)
        {
            db = new MessageContext();
            db.Messages.Load();
            _log = new LogEventLogic("V:\\client.log");
            Id = Guid.NewGuid().ToString();
            _client = tcpClient;
            _server = serverLogic;
            serverLogic.AddConection(this);
        }

        public void Process()
        {
            try
            {
                _stream = _client.GetStream();
                string Imei = GetMessage();
                byte[] msg = new byte[] { 1 };
                _server.SendMessage(msg, this.Id);

                string Message = GetMessage();
                if (Message != null)
                {
                    _message = new Message() { Date = DateTime.Now.ToString("HH:mm:ss"), Imei = Imei, GetMessage = Message };
                    db.Messages.Add(_message);
                    db.SaveChanges();
                }

                msg = new byte[] { 2 };
                _server.SendMessage(msg, this.Id);

                Close();
            }
            catch (Exception ex)
            {
                _log.Write(ex.Message);
            }
            finally
            {
                _server.RemoveConnection(this.Id);
            }
        }

        private string GetMessage()
        {
            byte[] Data = new byte[512];
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            do
            {
                bytes = _stream.Read(Data, 0, Data.Length);
                builder.Append(Encoding.ASCII.GetString(Data, 0, bytes));
            } while (_stream.DataAvailable);

            return builder.ToString();
        }

        protected internal void Close()
        {
            
            if(_stream != null)
            {
                _stream.Close();
            }
            if(_client != null)
            {
                _client.Close();
            }
        }
    }
}
