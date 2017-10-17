using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Runtime.InteropServices;

namespace TSLibrary
{
    public class ServerLogic : IDisposable
    {
        protected TcpListener _listener { get; private set; }
        protected TcpClient _client { get; private set; }
        private IPAddress _ipAddress;
        private int _port;
        private List<ClientLogic> _clients = new List<ClientLogic>();
        private LogEventLogic _log;
        private ClientLogic _clientLogic;
        private Thread _thread;
        private string path = "V:\\server.log";

        protected internal void AddConection(ClientLogic clientLogic)
        {
            _clients.Add(clientLogic);
        }

        protected internal void RemoveConnection(string id)
        {
            ClientLogic client = _clients.FirstOrDefault(c => c.Id == id);
            if(client != null)
            {
                _clients.Remove(client);
            }
        }

        public ServerLogic(string address, int port)
        {
            _log = new LogEventLogic(path);
            _ipAddress = IPAddress.Parse(address);
            _port = port;
            _listener = new TcpListener(_ipAddress, _port);
        }

        public void Start()
        {
            try
            {
                _listener.Start();
                _log.Write("server to start");

                while (true)
                {
                    _client = _listener.AcceptTcpClient();
                    _clientLogic = new ClientLogic(_client, this);
                    _thread = new Thread(new ThreadStart(_clientLogic.Process));
                    _thread.Start();
                }
            }
            catch(Exception ex)
            {
                _log.Write(ex.Message);
            }
        }

        public void SendMessage(byte[] Message, string Id)
        {
            //byte[] data = Encoding.UTF8.GetBytes(Message);
            for (int i = 0; i < _clients.Count; i++)
            {
                if (_clients[i].Id == Id)
                {
                    _clients[i]._stream.Write(Message, 0, Message.Length);
                }
            }
        }

        public void Stop()
        {
            _listener.Stop();
            for(int i = 0; i < _clients.Count; i++)
            {
                _clients[i].Close();
            }
            Environment.Exit(0);
            _log.Write("server to stop");
        }

        ~ServerLogic()
        {
            Dispose();
        }

        public void Dispose()
        {
            _listener.Stop();
        }
    }
}
